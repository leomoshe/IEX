using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Diagnostics;
using IEX.Utilities;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using System.Threading;
using IEX.Utilities.Threading;
using System.Configuration;


namespace IEX.Server.Monitor.Host
{
    class Program
    {


        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            IEX.Utilities.Dialogs.MessageDlg.Error(e.ExceptionObject as Exception);
            Tracer.WriteUnhandledError(e.ExceptionObject as Exception);
        }

        static void Main(string[] args)
        {
            Tracer.StartSession("IEX Monitoring logs", "IEX Monitoring");

            using (IexGlobalMutex singleMonitorMutex = new IexGlobalMutex(MutexNames.SingleMonitorMutexName))
            {
                ValidateSingleMonitor(singleMonitorMutex);
                //IexGlobalMutex.SingleProcessValidationHelper(singleMonitorMutex, true);
                
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                //set the console window title
                

                run_console();

                //  ---------  UZI   uncomment when implementing the monitor as a service    ----------

                //if (args.Count() == 0)
                //{
                //    //Service Mode
                //    System.ServiceProcess.ServiceBase[] ServicesToRun;
                //    ServicesToRun = new System.ServiceProcess.ServiceBase[] 
                //    { 
                //        new MonitorWindowsService() 
                //    };
                //    System.ServiceProcess.ServiceBase.Run(ServicesToRun);
                //}
                //else
                //{
                //    switch (args[0])
                //    {
                //        case "/i":
                //            install_windows_service();
                //            break;
                //        case "/u":
                //            uninstall_windows_service();
                //            break;
                //        default:
                //            run_console();
                //            break;
                //    }
                //}

            }
        }

        // for minimize the window
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public const int SW_SHOWMINIMIZED = 2;

        // TODO: move in a global assembly

        private static AutoResetEvent initCallbackComplete = new AutoResetEvent(false);

        private static void run_console()
        {
            StartMonitor();

            // listen to cmd console input ...
            while (true)
            {
                string line = Console.ReadLine().ToUpper();
                if (line.Equals("EXIT"))
                {
                    Console.WriteLine(DateTime.Now.ToString() + " Shutting down...");
                    break;
                }
                else
                {
                    string[] data = line.Split(' ');
                    int iex_number;
                    if (data.Length > 1 && int.TryParse(data[1], out iex_number))
                    {
                        if (data[0].StartsWith("STARTIEX"))
                            _manager.StartIEX(iex_number);
                        else if (data[0].StartsWith("RESTARTIEX"))
                            _manager.RestartIEX(iex_number);
                        else if (data[0].StartsWith("STOPIEX"))
                            _manager.StopIEX(iex_number);
                    }
                }
            }

            _manager.StopHosts();            
        }
        private static WebSocketClientManager _wsClientManager;
        private static void StartMonitor()
        {
            // grab machine mutex as soon as possible
            using (IexGlobalMutex iexInitMutex = new IexGlobalMutex(MutexNames.MonitorInitMutexName))
            {
                try
                {                    
                    iexInitMutex.GrabMutex();
                    
                    // for debugging iexInitMutex only
                    /*
                    Console.WriteLine("Grabbed Monitor Init Mutex, now sleeping for 10 seconds");
                    Thread.Sleep(20 * 1000);
                    Console.WriteLine("... woke up");
                    */

                    HouseKeepingInit();

                    _manager = new Manager();

                    bool webSocketEnabled = bool.Parse(ConfigurationManager.AppSettings["WebSocketEnabled"]);
                    if (webSocketEnabled)
                        _wsClientManager = new WebSocketClientManager();
                    
                    GetServicesHandler get_services = new GetServicesHandler(IEX.Server.ServiceManagement.MetaData.Tools.GetServices);

                    Console.WriteLine(DateTime.Now.ToString() + " Loading metadata.");
                    IAsyncResult asyncResult = get_services.BeginInvoke(new AsyncCallback(OnFinish), null);

                    _manager.StartHosts();
                    if (!Directory.Exists(Path.Combine(IEX.Utilities.IEXConfiguration.GetIexInstallationFolder(), "OCR")))
                    {
                        MessageBox.Show("After 'StartHosts' Your OCR folder is missing, call Leo / Uzi NOW! - thanks");
                    }

                    WaitForInitToFinish(asyncResult);

                    Console.WriteLine(DateTime.Now.ToString() + " Started hosting Monitoring Service.");
                }
                catch (Exception ex)
                {
                    Tracer.Write(Tracer.TraceLevel.ERROR, ex.ToString());
                    throw;
                }
            }
        }

        private static void WaitForInitToFinish(IAsyncResult result)
        {
            Console.WriteLine("Waiting for initialization to finish ...");
            result.AsyncWaitHandle.WaitOne();
            // ensures callback completes before continuing
            initCallbackComplete.WaitOne();
            Console.WriteLine("Initialization finished.");
        }

        /// <summary>
        /// Initializes Tracer, Cmd Console, verifies Monitor is not already running, starts FTP server
        /// </summary>
        private static void HouseKeepingInit()
        {
            if (!Directory.Exists(Path.Combine(IEX.Utilities.IEXConfiguration.GetIexInstallationFolder(), "OCR")))
            {
                MessageBox.Show("Before starting Monitor Your OCR folder is missing, call Leo / Uzi NOW! - thanks");
            }

            Console.Title = "IEX Monitor" + " - " + Assembly.GetExecutingAssembly().GetName().Version.ToString();

            // minimize the window:
            IntPtr winHandle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            ShowWindow(winHandle, SW_SHOWMINIMIZED);

            ValidateSingleMonitor(null);
            startFTP();
        }

        private static void startFTP()
        {
            Tracer.Write(Tracer.TraceLevel.INFO, "entered");
            try
            {
                if (Process.GetProcessesByName("war-ftpd").Count() == 0)
                {
                    string ftpPath = Path.Combine(IEX.Utilities.IEXConfiguration.GetIexInstallationFolder(), "FTP_Server");
                    ProcessStartInfo si = new ProcessStartInfo() { FileName = Path.Combine(ftpPath, "war-ftpd.exe"), WorkingDirectory = ftpPath };
                    Process.Start(si);
                }
            }
            catch (Exception exc)
            {
                Tracer.Write(Tracer.TraceLevel.ERROR, "error starting FTP server", exc);
            }
        }

        private static Manager _manager;
        private delegate IEX.Server.ServiceManagement.MetaData.ServiceCollection GetServicesHandler();

        private static void OnFinish(IAsyncResult ar)
        {
            try
            {
                _manager.FillProviders();
            }
            finally
            {
                initCallbackComplete.Set();
            }
        }

        // Path to installutil.exe        
        private static String InstallUtillPath()
        {
            return System.IO.Path.Combine(System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory(), "installutil.exe");
        }

        private static void uninstall_windows_service()
        {
            if (System.IO.File.Exists(InstallUtillPath()))
            {
                System.Diagnostics.Process.Start(InstallUtillPath(), String.Format("/u \"{0}\"", System.Reflection.Assembly.GetExecutingAssembly().Location));
                System.Diagnostics.Trace.WriteLine("Service uninstalled.");
            }
            else
                throw new System.IO.FileNotFoundException("InstallUtil.exe not found");
        }

        private static void install_windows_service()
        {
            if (System.IO.File.Exists(InstallUtillPath()))
            {
                System.Diagnostics.Process.Start(InstallUtillPath(), String.Format("\"{0}\"", System.Reflection.Assembly.GetExecutingAssembly().Location));
                System.Diagnostics.Trace.WriteLine("Service installed.");
            }
            else
                throw new System.IO.FileNotFoundException("InstallUtil.exe not found");
        }

        private static void ValidateSingleMonitor(IexGlobalMutex singleMonitorMutex)
        {
            //if another Monitor is active shutdown
            // TODO: this won't work in the scenario of two different remote terminal sessions, they won't see each other's processes!
            // TOOD: use a global named mutex instead.

            if (singleMonitorMutex != null)
            {
                if (!singleMonitorMutex.IsMutexOwner)
                {
                    Tracer.Write(Tracer.TraceLevel.WARN, "Monitor already running, shutting down.");
                    Environment.Exit(-1);

                }
            }
            else
            {
                foreach (Process p in Process.GetProcesses())
                {
                    if (p.ProcessName.Contains("Monitor.Host") && !p.ProcessName.Contains("vshost") && p.Id != Process.GetCurrentProcess().Id)
                    {
                        Tracer.Write(Tracer.TraceLevel.INFO, "watchdog already active with process id: " + p.Id + ", shutting down");
                        Environment.Exit(-1);
                    }
                }

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Diagnostics;
using System.Management;
using System.IO;
using IEX.Utilities;
using System.Threading;
using System.Windows.Forms;
using IEX.Utilities.Collections;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Web;

namespace IEX.Server.Monitor
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WatchdogService" in both code and config file together.
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true, ConcurrencyMode = ConcurrencyMode.Multiple)]
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MonitoringService : IMonitoringService, IPinger, IDisposable
    {
        private const int MAX_SERVER_LOAD_TIME = 120;

        private object syncObject = new object();

        public int IexId { get; set; }

        private readonly System.Threading.Timer _update_server_full_info_timer;
        private int _performance_interval;
        private int _ping_server_status_count = -1;
        private readonly int _performance_interval_long = 1000 * 10;
        private readonly int _performance_interval_short = 1000 * 4;
        private readonly IEX.Server.Monitor.WebClients.ServiceServer _service_server;
        public MonitoringService()
        {
            _update_server_full_info_timer = new System.Threading.Timer(UpdateServerFullInfo, null, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

            string data;
            data = System.Configuration.ConfigurationManager.AppSettings["PerformanceIntervalLong"];
            if (!string.IsNullOrEmpty(data))
                _performance_interval_long = (int)TimeSpan.Parse(data).TotalMilliseconds;

            data = System.Configuration.ConfigurationManager.AppSettings["PerformanceIntervalShort"];
            if (!string.IsNullOrEmpty(data))
                _performance_interval_short = (int)TimeSpan.Parse(data).TotalMilliseconds;

            _performance_interval = _performance_interval_long;

            _service_server = new WebClients.ServiceServer();
            try
            {
                _cpu_counter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                _mem_counter = new PerformanceCounter("Memory", "Available MBytes");
            }
            catch (Exception exc)
            {
                Tracer.Write(Tracer.TraceLevel.ERROR, "Monitor counters creation", exc);
            }
        }

        private bool _started = false;
        public void Start()
        {
            if (_started)
                return;
            _started = true;
            _update_server_full_info_timer.Change(0, _performance_interval);
        }

        private readonly ServerFullInfoCollection _servers_full_info = new ServerFullInfoCollection();
        private object _lock = new object();

        private void UpdateServerFullInfo(object obj)
        {
            lock (_lock)
            {
                _update_server_full_info_timer.Change(Timeout.Infinite, Timeout.Infinite);

                List<WaitHandle> waitHandles = new List<WaitHandle>();

                foreach (var data in _servers_full_info)
                {
                    AutoResetEvent autoResetEvent = new AutoResetEvent(false);
                    waitHandles.Add(autoResetEvent);
                    System.Threading.Thread thread = new System.Threading.Thread(UpdateServerFullInfoThread) { IsBackground = true };
                    thread.Start(new object[] { data.Key, autoResetEvent });
                }

                WaitHandle.WaitAll(waitHandles.ToArray());

                if (_ping_server_status_count < 0)
                {
                    if (_performance_interval != _performance_interval_long)
                    {
                        _performance_interval = _performance_interval_long;
                        //_update_server_full_info_timer.Change(_performance_interval, _performance_interval);
                    }
                }
                else
                    _ping_server_status_count--;

                _update_server_full_info_timer.Change(_performance_interval, _performance_interval);
            }
        }

        private void UpdateServerFullInfoThread(object prm)
        {
            object[] prms = prm as object[];
            int server_id = (int)prms[0];
            UpdateServerFullInfo(server_id);
            ((AutoResetEvent)prms[1]).Set();
        }

        //private void UpdateServerFullInfoThread(object state)
        //{
        //    int server_id = (int)state;
        //    UpdateServerFullInfo(server_id);
        //}

        private readonly PerformanceCounter _cpu_counter;
        private readonly PerformanceCounter _mem_counter;
        //private Dictionary<string, string> ping()
        //{
        //    Dictionary<string, string> result = new Dictionary<string, string>();
        //    if (_cpu_counter != null)
        //    {
        //        try
        //        {
        //            result.Add("% Processor Time", this._cpu_counter.NextValue().ToString("#00.00", CultureInfo.InvariantCulture));
        //        }
        //        catch
        //        {
        //            result.Add("% Processor Time", IEX.Utilities.Consts.Base.ERROR_PERFORMANCE_COUNTER);
        //        }
        //    }
        //    else
        //        result.Add("% Processor Time", IEX.Utilities.Consts.Base.ERROR_PERFORMANCE_COUNTER);
        //    if (_mem_counter != null)
        //    {
        //        Int64 physical_available = IEX.Utilities.PerformanceInfo.GetPhysicalAvailableMemoryInMiB();
        //        Int64 total = IEX.Utilities.PerformanceInfo.GetTotalMemoryInMiB();
        //        decimal percent_free = ((decimal)physical_available / (decimal)total) * 100;
        //        decimal percent_occupied = 100 - percent_free;
        //        result.Add("Available MBytes", percent_occupied.ToString("#00.00", CultureInfo.InvariantCulture));
        //    }
        //    else
        //        result.Add("Available MBytes", IEX.Utilities.Consts.Base.ERROR_PERFORMANCE_COUNTER);
        //    return result;
        //}

        private ICollection<ManagementServer.Model.Monitoring.PerformanceCounterInstance> ping()
        {
            List<ManagementServer.Model.Monitoring.PerformanceCounterInstance> result = new List<ManagementServer.Model.Monitoring.PerformanceCounterInstance>();

            if (_cpu_counter != null)
            {
                try
                {

                    IEX.ManagementServer.Model.Monitoring.PerformanceCounter cpuPerformanceCounter = new ManagementServer.Model.Monitoring.PerformanceCounter
                    {
                        Name = "ComputerCPU",
                        Threshold = 80,

                    };

                    IEX.ManagementServer.Model.Monitoring.PerformanceCounterInstance cpuCounterInstance = new ManagementServer.Model.Monitoring.PerformanceCounterInstance
                    {
                        PerformanceCounter = cpuPerformanceCounter,
                        Value = this._cpu_counter.NextValue()//.ToString("#00.00", CultureInfo.InvariantCulture);

                    };
                    result.Add(cpuCounterInstance);

                }
                catch
                {
                    //TODO ILAY: ask uzi what to do in case of exception

                    //result.Add("% Processor Time", IEX.Utilities.Consts.Base.ERROR_PERFORMANCE_COUNTER);
                }
            }
            else
            {
            }

            //result.Add("% Processor Time", IEX.Utilities.Consts.Base.ERROR_PERFORMANCE_COUNTER);
            if (_mem_counter != null)
            {
                Int64 physical_available = IEX.Utilities.PerformanceInfo.GetPhysicalAvailableMemoryInMiB();
                Int64 total = IEX.Utilities.PerformanceInfo.GetTotalMemoryInMiB();
                decimal percent_free = ((decimal)physical_available / (decimal)total) * 100;
                decimal percent_occupied = 100 - percent_free;

                IEX.ManagementServer.Model.Monitoring.PerformanceCounter memoryPerformanceCounter = new ManagementServer.Model.Monitoring.PerformanceCounter
                {
                    Name = "Available MBytes",
                    Threshold = 80,

                };

                IEX.ManagementServer.Model.Monitoring.PerformanceCounterInstance memoryCounterInstance = new ManagementServer.Model.Monitoring.PerformanceCounterInstance
                {
                    PerformanceCounter = memoryPerformanceCounter,
                    Value = (float)percent_occupied

                };
                result.Add(memoryCounterInstance);
                // result.Add("Available MBytes", percent_occupied.ToString("#00.00", CultureInfo.InvariantCulture));
            }
            else
            {
                //  result.Add("Available MBytes", IEX.Utilities.Consts.Base.ERROR_PERFORMANCE_COUNTER);
            }

            return result;

        }

        ICollection<ManagementServer.Model.Monitoring.PerformanceCounterInstance> IPinger.Ping()
        {
            return ping();
        }

        //start the iex in silent mode. 
        //returns after services loading was completed or upon timeout
        bool IMonitoringService.StartIEX(int iexNumber, bool shutdownUponErrors, out ServerInfo server_info)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { iexNumber, shutdownUponErrors, null });

            Tracer.Write(Tracer.TraceLevel.INFO, "aquired sync lock");

            Console.WriteLine(DateTime.Now.ToString() + " Starting processing: StartIEX for IEX " + iexNumber.ToString());

            //validate iexNumber
            ValidateIexNumber(iexNumber);

            server_info = new ServerInfo();

            //make sure the requested server is not already running
            Process process = null;
            try
            {
                process = IEX.Utilities.Tools.ProcessLocator.GetIexProcess(iexNumber);
            }
            catch
            {
                return false;
            }
            if (process != null)
            {
                Tracer.Write(Tracer.TraceLevel.WARN, "iex " + iexNumber + " is already running");
            }
            else
            {

                bool finishedLoading = false;

                //define the new IEX to launch
                Process p = new Process();

                //iex process path
                string installedBasePath = IEX.Utilities.IEXConfiguration.GetIexInstallationFolder();

                //launch parameters
                ProcessStartInfo si = new ProcessStartInfo(Path.Combine(installedBasePath, "IEX.ServerX.Host.exe"), iexNumber.ToString());

                //set the working directory
                si.WorkingDirectory = installedBasePath;
                p.StartInfo = si;

                //start the iex process in silent mode
                p.Start();

                Tracer.Write(Tracer.TraceLevel.INFO, "after launching. process id: " + p.Id);

                //the max time to wait for the server to launch
                DateTime dueTime = DateTime.Now + TimeSpan.FromSeconds(MAX_SERVER_LOAD_TIME);

                //loop till the state is finished loading, either successfully or with errors
                while (DateTime.Now < dueTime)
                {
                    Thread.Sleep(2000);

                    try
                    {
                        UpdateServerFullInfo(iexNumber);
                        server_info = ((IMonitoringService)this).GetServerInfo(iexNumber);

                        if (server_info.Status == ServerState.Idle || server_info.Status == ServerState.Connected)
                        {
                            finishedLoading = true;
                            break;
                        }
                        else
                        {
                            Tracer.Write(Tracer.TraceLevel.INFO, "load state is: " + server_info.Status.ToString());
                        }
                    }
                    catch (Exception exc)
                    {
                        Tracer.Write(Tracer.TraceLevel.WARN, "error while getting state", exc);
                    }
                }

                //server didn't load till timeout, throw error
                if (!finishedLoading)
                {
                    string errorMessage = "IEX " + iexNumber + " timed out while loading";
                    Tracer.Write(Tracer.TraceLevel.ERROR, errorMessage);

                    Console.WriteLine(DateTime.Now.ToString() + " Finished processing: StartIEX for IEX " + iexNumber.ToString() + ". Error: Timed out while loading");

                    throw new FaultException(errorMessage);
                }
                else
                {
                    //temporary for 4.8 - let the VB start listening for TCP
                    //in 5.0 we'll get the real server status and return the the right time
                    Thread.Sleep(5000);
                }
            }

            var instance_statuses = from service in server_info.Services
                                    let implementations = service.Implementations
                                    from implementation in implementations
                                    let instances = implementation.Instances
                                    from instance in instances
                                    where instance.Status == ServiceManagement.ServiceState.Faulted
                                    select instance.Status;

            //update the out parameter of the services state
            UpdateServerFullInfo(iexNumber);
            server_info = ((IMonitoringService)this).GetServerInfo(iexNumber);

            //server finished loading but one or more of the services are faulted
            if (instance_statuses.Any())
            {
                Tracer.Write(Tracer.TraceLevel.INFO, "state contains a faulted service");
                if (shutdownUponErrors)
                {
                    Tracer.Write(Tracer.TraceLevel.INFO, "shutting down server");
                    ((IMonitoringService)this).StopIEX(iexNumber);
                }
                Console.WriteLine(DateTime.Now.ToString() + " Finished processing: StartIEX for IEX " + iexNumber.ToString() + ". Server loaded with errors");

                return false;
            }

            Console.WriteLine(DateTime.Now.ToString() + " Finished processing: StartIEX for IEX " + iexNumber.ToString() + ". Server loaded successfully");

            return true;
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        void IMonitoringService.StopIEX(int iexNumber)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { iexNumber });

            Tracer.Write(Tracer.TraceLevel.INFO, "aquired sync lock");

            Console.WriteLine(DateTime.Now.ToString() + " Starting processing: StopIEX for IEX " + iexNumber.ToString());

            ValidateIexNumber(iexNumber);

            //find the requested iex process
            Process iexProcess = null;
            try
            {
                iexProcess = IEX.Utilities.Tools.ProcessLocator.GetIexProcess(iexNumber);
            }
            catch
            {
            }

            //found the process, stop it
            if (iexProcess != null)
            {
                try
                {
                    stopProcess(iexProcess);
                    Console.WriteLine(DateTime.Now.ToString() + " Finished processing: StopIEX for IEX " + iexNumber.ToString() + ". Server stopped successfully");
                }
                catch (Exception exc)
                {
                    Tracer.Write(Tracer.TraceLevel.ERROR, "error while trying to call shutdown on service manager", exc);
                }
            }
            else
            {
                Console.WriteLine(DateTime.Now.ToString() + " Finished processing: StopIEX for IEX " + iexNumber.ToString() + ". Server was not running");

                Tracer.Write(Tracer.TraceLevel.INFO, "requested iex was not running, performed no operation");
            }
        }

        bool IMonitoringService.RestartIEX(int iexNumber, bool shutdownUponErrors, out ServerInfo server_info)
        {
            Tracer.Write(Tracer.TraceLevel.RemoteReport, "Restart requested for IEX number " + iexNumber);
            Tracer.Write(Tracer.TraceLevel.INFO, "aquired sync lock");
            Console.WriteLine(DateTime.Now.ToString() + " Starting processing: RestartIEX for IEX " + iexNumber.ToString());

            ValidateIexNumber(iexNumber);

            server_info = null;

            ((IMonitoringService)this).StopIEX(iexNumber);

            //start a freash instance of the requested server
            return ((IMonitoringService)this).StartIEX(iexNumber, shutdownUponErrors, out server_info);
        }

        private void UpdateServerFullInfo(int iexNumber)
        {
            Process iexProcess = null;
            try
            {
                iexProcess = IEX.Utilities.Tools.ProcessLocator.GetIexProcess(iexNumber);
            }
            catch
            {
                _servers_full_info.ServerFullInfo(iexNumber, new ServerInfo(ServerState.Unknown), null);
                return;
            }

            if (iexProcess != null)
            {
                bool has_changes = false;
                ICollection<IEX.ManagementServer.Model.Monitoring.PerformanceCounterInstance> performance = _service_server.PingServer(iexNumber, out has_changes, _servers_full_info.HashCode(iexNumber));
                _servers_full_info.Performance(iexNumber, performance);
                if (performance == null)
                {
                    ServerInfo si = new ServerInfo(ServerState.Loading);
                    si.HashCode = -3; //in order to force 'hasChanges' and show in client the loading state
                    _servers_full_info.ServerInfo(iexNumber, si);
                    return;
                }
                else if (has_changes)
                {
                    ServerInfo server_info = new ServerInfo();
                    ServerServiceReference.ServerInfo data = _service_server.GetServerInfo(iexNumber);
                    if (data != null)
                        server_info = new ServerInfo(data);
                    _servers_full_info.ServerInfo(iexNumber, server_info);
                }
            }
            else
            {
                _servers_full_info.ServerFullInfo(iexNumber, new ServerInfo(ServerState.NotRunning), null);
                return;
            }

#if IEX_DEBUG
            string status = "null";
            if (_servers_full_info.ServerInfo(iexNumber) != null)
                status = _servers_full_info.ServerInfo(iexNumber).Status.ToString();
            Console.WriteLine(string.Format("Server {0}: {1}", iexNumber, status));
#endif
        }

        private void SetUpdateServersFullInfoTimeToShort()
        {
            if (_performance_interval != _performance_interval_short)
            {
                _performance_interval = _performance_interval_short;
                _update_server_full_info_timer.Change(_performance_interval, _performance_interval);
            }
            _ping_server_status_count = 5;
        }

        MonitorInfo IMonitoringService.GetMonitorInfo(Dictionary<int, int> servers_hash_code)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { servers_hash_code });
            SetUpdateServersFullInfoTimeToShort();
            MonitorInfo result = new MonitorInfo { State = new ManagementServer.Model.Monitoring.ComputerState() };
            result.State.PerformanceCounters = ping();
            List<ServerData> servers_data = new List<ServerData>();
            foreach (KeyValuePair<int, int> server_hash_code in servers_hash_code)
            {
                ServerData server_data = new ServerData();
                if (server_data.State == null)
                {
                    server_data.State = new ManagementServer.Model.Monitoring.IexServerState { IexServer = new ManagementServer.Model.Resources.IexServer() };
                }
                //server_data.Id = server_hash_code.Key;
                server_data.State.IexServer.Instance = server_hash_code.Key;
                server_data.has_changes = _servers_full_info.HashCode(server_hash_code.Key) != server_hash_code.Value;
                server_data.State.PerformanceCounters = _servers_full_info.Performance(server_hash_code.Key);
                servers_data.Add(server_data);
            }
            result.Servers = servers_data.ToArray();
            Tracer.Write(Tracer.TraceLevel.DEBUG, "exiting. result is: " + result);
            return result;
        }



        ServerInfo IMonitoringService.GetServerInfo(int iexNumber)
        {
            if (WebOperationContext.Current != null)
            {
                //for all cors requests  
                WebOperationContext.Current.OutgoingResponse.Headers
                    .Add("Access-Control-Allow-Origin", "*");
                //identify preflight request and add extra headers  
                if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
                {
                    WebOperationContext.Current.OutgoingResponse.Headers
                        .Add("Access-Control-Allow-Methods", "POST, OPTIONS, GET");
                    WebOperationContext.Current.OutgoingResponse.Headers
                        .Add("Access-Control-Allow-Headers",
                             "Content-Type, Accept, Authorization, x-requested-with");
                    return null;
                }
            }

            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { iexNumber });
            SetUpdateServersFullInfoTimeToShort();
            ServerInfo result = _servers_full_info.ServerInfo(iexNumber);
            Tracer.Write(Tracer.TraceLevel.DEBUG, "exiting. result is: " + result);
            return result;
        }

        void IMonitoringService.RunProcess(Dictionary<string, string> data)
        {
            if (data == null || !data.ContainsKey("FileName"))
            {
                if (!ConfigurationManager.AppSettings.AllKeys.Contains("AutomaticInstallerPath"))
                {
                    Tracer.Write(Tracer.TraceLevel.ERROR, "There is not AutomaticInstallerPath key in IEX.Server.Monitor.Host.config");
                    return;
                }

                if (data == null)
                    data = new Dictionary<string, string>();
                data.Add("FileName", ConfigurationManager.AppSettings["AutomaticInstallerPath"] + "IEX.AutomaticInstaller.Console.exe");
            }

            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { data.ToString<string, string>() });
            System.Diagnostics.ProcessStartInfo start_info = new System.Diagnostics.ProcessStartInfo();
            string file_name;
            if (data.TryGetValue("FileName", out file_name) == false)
                return;
            start_info.FileName = file_name;
            string arguments;
            if (data.TryGetValue("Arguments", out arguments) == true)
                start_info.Arguments = arguments;
            string working_directory;
            if (data.TryGetValue("WorkingDirectory", out working_directory) == true)
                start_info.WorkingDirectory = working_directory;
            Process process = System.Diagnostics.Process.Start(start_info);
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. Process running: " + (process != null).ToString());
        }

        void IMonitoringService.GetOptions()
        {
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Methods", "POST, GET, OPTIONS");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization, x-requested-with");
        }

        private void stopProcess(Process process)
        {
            try
            {
                IEX.Utilities.Tools.ProcessLocator.StopProcess(process);
            }
            catch (Exception exc)
            {
                throw new FaultException(exc.Message);
            }
        }

        private void ValidateIexNumber(int iexNumber)
        {
            if (iexNumber < 0)
            {
                string errorMessage = "IexNumber must be a psitive integer. The provided number: " + iexNumber + " is not valid";
                Tracer.Write(Tracer.TraceLevel.ERROR, errorMessage);
                throw new FaultException(errorMessage);
            }
        }

        public void Dispose()
        {
            try
            {
                if (_cpu_counter != null)
                    _cpu_counter.Dispose();
                if (_mem_counter != null)
                    _mem_counter.Dispose();
            }
            finally
            {
                PerformanceCounter.CloseSharedResources();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace IEX.Lab.App
{
    using StructureMap;
    using StructureMap.Attributes;
    using IEX.Utilities;
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            IEX.Utilities.Tracer.StartSession("IEX Lab logs", "Lab App");
            ObjectFactory.Initialize(x =>
                {
                    x.UseDefaultStructureMapConfigFile = false;
                    x.AddRegistry(new IEX.Lab.Client.StructureMapRegistry());

                    x.For<IEX.Lab.App.Views.IShell>().Singleton().Add<IEX.Lab.App.Views.frmLab>();
                    //x.For<IEX.Lab.App.Views.IShell>().Singleton().Add<IEX.Lab.App.Views.frmMain>();
                });
#if IEX_DEBUG
            string what_do_I_have = ObjectFactory.WhatDoIHave();
            System.Diagnostics.Debug.WriteLine(what_do_I_have);
#endif
            ObjectFactory.AssertConfigurationIsValid();

            var shell = ObjectFactory.GetInstance<IEX.Lab.App.Views.IShell>();

            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(AppDomain_UnhandledException);

            try
            {
                if (args.Length == 1)
                    shell.RepositoryString = args[0];
                Application.Run((Form)shell);
                //Application.Run(new IEX.Lab.App.Views.frmMain());
            }
            catch (Exception exc)
            {
                IEX.Utilities.Dialogs.MessageDlg.Error(exc);
                Tracer.Write(Tracer.TraceLevel.ERROR, "Run", exc);
            }
            Tracer.Write(Tracer.TraceLevel.INFO, "Close");
        }

        public static void HandleException(string message)
        {
            IEX.Utilities.Dialogs.MessageDlg.Error(message, String.Format("{0} : Exception", Application.ProductName));
            Tracer.Write(Tracer.TraceLevel.ERROR, "Run", message);
        }

        //both of these need to be handled so the default Windows exception dialog isn't shown instead
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception.Message); return;
            Application.Exit();
        }

        private static void AppDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject.ToString());
            Application.Exit();
        }

    }
}
    
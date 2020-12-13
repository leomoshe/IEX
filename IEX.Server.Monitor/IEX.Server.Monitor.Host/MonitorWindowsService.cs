using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Server.Monitor.Host
{
    using System.ServiceProcess;
    using System.Diagnostics;
    public class MonitorWindowsService: ServiceBase
    {
        public const string SERVICE_NAME = "IEX.Server.Monitor";
        private Manager _manager = null;
        public MonitorWindowsService()
        {
            Tools.WriteLog("MonitorWindowsService", Utilities.Tracer.TraceLevel.API_ENTER); 
            ServiceName = SERVICE_NAME;
            Tools.WriteLog("MonitorWindowsService - Exit", Utilities.Tracer.TraceLevel.API_EXIT); 
        }

        protected override void OnStart(string[] args)
        {
            Tools.WriteLog("OnStart", Utilities.Tracer.TraceLevel.API_ENTER); 
            _manager = new Manager();
            _manager.StartHosts();
            _manager.FillProviders(5000);
            //base.OnStart(args);
            Tools.WriteLog("OnStart - Exit", Utilities.Tracer.TraceLevel.API_EXIT);
        }

        protected override void OnStop()
        {
            Tools.WriteLog("OnStop", Utilities.Tracer.TraceLevel.API_ENTER); 
            _manager.StopHosts();
            //_manager.Dispose();
            //base.OnStop();
            Tools.WriteLog("OnStop - Exit", Utilities.Tracer.TraceLevel.API_EXIT);
        }

        protected virtual int Execute()
        {
            // for right now we'll just log a message in the
            // Application message log to let us know that
            // our service is working
            Tools.WriteLog(ServiceName + "::Execute()");

            return 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Server.Monitor.Host
{
    using System.Diagnostics;
    using IEX.Utilities;
    public class Tools
    {
        private const string LOG = MonitorWindowsService.SERVICE_NAME + "_Log";
        static public void WriteLog(string message)
        {
            WriteLog(message, Tracer.TraceLevel.INFO);
        }
        
        static private bool _tracer_initialized = false;
        static public void WriteLog(string message, Tracer.TraceLevel tracer_level)
        {         
            if (!Environment.UserInteractive)
            {

                EventLog event_log = init_event_log();
                EventLogEntryType type = EventLogEntryType.Information;
                switch (tracer_level)
                {
                    case Tracer.TraceLevel.ERROR:
                        type = EventLogEntryType.Error;
                        break;
                    case Tracer.TraceLevel.WARN:
                        type = EventLogEntryType.Warning;
                        break;
                }
                event_log.WriteEntry(message, type);
                //EventLog.WriteEntry(_source, message, type);
            }
            Tracer.Write(tracer_level, message);
        }

        static private EventLog _event_log = null;
        static private EventLog init_event_log()
        {
            if (_event_log == null)
            {
                _event_log = new EventLog();
                if (!EventLog.SourceExists(MonitorWindowsService.SERVICE_NAME))
                    EventLog.CreateEventSource(MonitorWindowsService.SERVICE_NAME, LOG);
                _event_log.Source = MonitorWindowsService.SERVICE_NAME;
                //_event_log.Log = LOG;
                //_event_log.EnableRaisingEvents = true;
            }
            return _event_log;
        }
    }
}

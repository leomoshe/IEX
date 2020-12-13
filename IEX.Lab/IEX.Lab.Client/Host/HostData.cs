using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.Client
{
    using IEX.Utilities;
    using IEX.Utilities.Collections;
    using System.ComponentModel;
    using System.Threading;
    using System.Net.NetworkInformation;
    using System.Diagnostics;
    using System.Net.Sockets;
    using System.Reflection;
    using IEX.Lab.Client.Extensions;
    using System.Globalization;
    using IEX.Server.Monitor.Client.MonitoringServiceReference;
    public class HostData : INotifyPropertyChanged, IDisposable
    {
        IEX.Server.Monitor.Client.ServiceMonitoring _service_monitoring;
        IEX.Server.Monitor.Client.ServiceConfiguration _service_configuration;

        System.Timers.Timer _ping_timer;
        int performance_interval;
        private Dictionary<int, ServerFullInfo> _servers_server_full_info = new Dictionary<int,ServerFullInfo>();

        public HostData(string host, IEnumerable<int> servers_id)
        {
            Host = host;
            ServersId = servers_id;
            _service_monitoring = new IEX.Server.Monitor.Client.ServiceMonitoring(host);
            _service_configuration = new IEX.Server.Monitor.Client.ServiceConfiguration(host);

            performance_interval = 5000;
            string data = System.Configuration.ConfigurationManager.AppSettings["HostPerformanceInterval"];
            if (!string.IsNullOrEmpty(data))
                performance_interval = (int)TimeSpan.Parse(data).TotalMilliseconds;
            _ping_timer = new System.Timers.Timer(performance_interval);
            _ping_timer.Elapsed += new System.Timers.ElapsedEventHandler(PingElapsed);
            _ping_timer.Start();

            //foreach (int i in _service_configuration.ConfiguratedIdsServers())
            foreach (int i in ServersId)
            {
                _servers_server_full_info.Add(i, new ServerFullInfo(_service_monitoring.GetServerInfo(i), new Dictionary<string, string>()));
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (object sender, DoWorkEventArgs e) =>
                {
                    List<object> arguments = e.Argument as List<object>;
                    int id = (int)arguments[1];
                    IEX.Server.Monitor.Client.MonitoringServiceReference.ServerInfo server_info;
                    switch ((Action)arguments[0])
                    {
                        case Action.Start:
                            e.Result = _service_monitoring.StartIEX(id, out server_info);
                            break;
                        case Action.Stop:
                            _service_monitoring.StopIEX(id);
                            break;
                        case Action.Restart:
                            e.Result = _service_monitoring.RestartIEX(id, out server_info);
                            break;
                    }
                };
                worker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
                {
                };
                _workers.Add(i, worker);
            }
            Tracer.Write(Tracer.TraceLevel.INFO, string.Format("HostData for '{0}' was created.", host));
        }      

        private void PingElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_ping_timer != null)
                PingMonitor();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Host { get; private set; }
        public IEnumerable<int> ServersId { get; private set; }
        public IEX.Server.Monitor.Client.MonitoringServiceReference.MonitorState Status { get; private set; }
        public string CPU { get; private set; }
        public string Memory { get; private set; }
        public string MonitorVersion { get; set; }
        public IEX.Server.Monitor.Client.ConfigurationServiceReference.ServiceCollection SubsystemsMetadata { get; private set; }
        public IEX.Server.Monitor.Client.MonitoringServiceReference.ServerFullInfo GetServerFullInfo(int server_id)
        {
            if (!_servers_server_full_info.ContainsKey(server_id))
                //throw new Exception(string.Format("Server '{0}:{1}' not found", Host, id));
                return new ServerFullInfo();
                //return null;
            return _servers_server_full_info[server_id];
        }

        private int[] _configurated_ids_servers = null;
        public IList<int> GetConfiguratedIdsServers()
        {
            if (_configurated_ids_servers == null)
                _configurated_ids_servers = _service_configuration.ConfiguratedIdsServers();
            return _configurated_ids_servers;
        }
        
        public void SetTemporalStatus(int id, IEX.Server.Monitor.Client.MonitoringServiceReference.ServerState serverState)
        {
            var serverInfo = _service_monitoring.SetTemporalStatus(id, serverState);
            var serverFullInfo = new IEX.Server.Monitor.Client.MonitoringServiceReference.ServerFullInfo(serverInfo, null);
            _servers_server_full_info[id] = serverFullInfo;
            if (PropertyChanged != null)
                PropertyChanged(this, null);
        }

        public void StopPing()
        {
            _ping_timer.Stop();
        }

        public void StartPing()
        {
            _ping_timer.Start();
        }

        public void PingMonitor()
        {
            Tracer.Write(Tracer.TraceLevel.INFO, string.Format("Ping monitor '{0}'.", Host));
            _ping_timer.Stop();
            IEX.Server.Monitor.Client.MonitoringServiceReference.MonitorInfo monitor_info = _service_monitoring.PingMonitor();
            
            
            Status = monitor_info.Status;
            if (monitor_info.State.PerformanceCounters == null)
            {
                CPU = string.Empty;
                Memory = string.Empty;
            }
            else
            {
                foreach (var counter in monitor_info.State.PerformanceCounters)
                {
                    if (counter.PerformanceCounter.Name == "ComputerCPU")
                    {
                        CPU = counter.Value.ToString("#00.00", CultureInfo.InvariantCulture);
                    }
                    else if (counter.PerformanceCounter.Name == "Available MBytes")
                    {
                        Memory = counter.Value.ToString("#00.00", CultureInfo.InvariantCulture);
                    }
                }
                //string cpu;
                //if (monitor_info.Info.TryGetValue("% Processor Time", out cpu))
                //    CPU = cpu;
                //string memory;
                //if (monitor_info.Info.TryGetValue("Available MBytes", out memory))
                //    Memory = memory;
                if (SubsystemsMetadata == null)
                    SubsystemsMetadata = _service_configuration.SubsystemsMetadata();
            }
            MonitorVersion = monitor_info.MonitorVersion;

            foreach (int key in _servers_server_full_info.Keys.ToArray())
            {
                ServerData server_data = monitor_info.Servers.FirstOrDefault(item => item.State != null && item.State.IexServer.Instance == key);
                _servers_server_full_info[key] = new IEX.Server.Monitor.Client.MonitoringServiceReference.ServerFullInfo(_service_monitoring.GetServerInfo(key), (server_data == null || server_data.State == null) ? new Dictionary<string, string>() : server_data.State.PerformanceCounters.ToPerformanceCountersDictionary());
                //_servers_server_full_info[key] = new IEX.Server.Monitor.Client.MonitoringServiceReference.ServerFullInfo(_service_monitoring.GetServerInfo(key), server_data.State == null ? new Dictionary<string, string>() : server_data.State.PerformanceCounters.ToPerformanceCountersDictionary());
            }

            if (PropertyChanged != null)
                PropertyChanged(this, null);

            if (_ping_timer != null)
                _ping_timer.Start();
        }

        public void UpdateHost(string version)
        {
            _service_monitoring.RunAutomaticInstaller(version);
        }

        Dictionary<int, BackgroundWorker> _workers = new Dictionary<int, BackgroundWorker>();
        private enum Action { Start, Stop, Restart };
        public bool Start(int id)
        {
            if (!_workers[id].IsBusy)
            {
                _workers[id].RunWorkerAsync(new List<object>() { Action.Start, id });
                return true;
            }
            return false;
        }

        public bool Stop(int id)
        {
            if (!_workers[id].IsBusy)
            {
                _workers[id].RunWorkerAsync(new List<object>() { Action.Stop, id });
                return true;
            }
            return false;
        }

        public bool Restart(int id)
        {
            if (!_workers[id].IsBusy)
            {
                _workers[id].RunWorkerAsync(new List<object>() { Action.Restart, id });
                return true;
            }
            return false;
        }

        protected bool _disposed = false;
        ~HostData()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            if (!_disposed) 
            {
                Dispose(true);
                GC.SuppressFinalize(this);
                _disposed = true;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    _ping_timer.Stop();
                if (_ping_timer != null)
                {
                    _ping_timer.Dispose();
                    _ping_timer = null;
                }
            }
            _disposed = true;
        }
    }

    public class HostDataCollection : NotifyParentObservableCollection<HostData>
    {
    }
}

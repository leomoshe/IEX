using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Server.Monitor.WebClients
{
    using IEX.Server.Monitor.ServerServiceReference;
    using IEX.Utilities;
    using System.Collections.Concurrent;
    public partial class ServiceServer
    {
        private readonly ConcurrentDictionary<int, ServerManager> _server_managers = new ConcurrentDictionary<int, ServerManager>();
        private readonly ConcurrentDictionary<int, PingerManager> _pinger_managers = new ConcurrentDictionary<int, PingerManager>();
        public ServiceServer()
        {
            foreach (int id in IEX.Utilities.IEXConfiguration.GetServerIds())
            {
                _server_managers.TryAdd(id, new ServerManager(id));
                _pinger_managers.TryAdd(id, new PingerManager(id));
            }
        }

        internal ICollection<IEX.ManagementServer.Model.Monitoring.PerformanceCounterInstance> PingServer(int server_id, out bool has_changes, int hash_code)
        {
            has_changes = false;
            return _pinger_managers[server_id].PingServer(hash_code, out has_changes);
        }

        internal ServerInfo GetServerInfo(int server_id)
        {
            return _server_managers[server_id].GetServerInfo();
        }
    }
}

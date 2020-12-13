using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Server.Monitor
{
    using System.Collections.Concurrent;
    internal class ServerFullInfo
    {
        public ServerFullInfo()
        {
            ServerInfo = new ServerInfo();
            Performance = new List<IEX.ManagementServer.Model.Monitoring.PerformanceCounterInstance>();
        }
        public ServerFullInfo(ServerInfo server_info, ICollection<IEX.ManagementServer.Model.Monitoring.PerformanceCounterInstance> performance)
        {
            ServerInfo = server_info;
            Performance = performance;
        }
        public ServerInfo ServerInfo { get; set; }
        public ICollection<IEX.ManagementServer.Model.Monitoring.PerformanceCounterInstance> Performance { get; set; }
    }

    internal class ServerFullInfoCollection : ConcurrentDictionary<int, ServerFullInfo>
    {
        public ServerFullInfoCollection()
        {
            foreach (int id in IEX.Utilities.IEXConfiguration.GetServerIds())
                base.TryAdd(id, new ServerFullInfo(new ServerInfo(), null));
        }

        public int HashCode(int server_id)
        {
            int result = 0;
            ServerInfo server_info = ServerInfo(server_id);
            result = server_info.HashCode;
            return result;
        }

        public void ServerFullInfo(int server_id, ServerInfo server_info, ICollection<IEX.ManagementServer.Model.Monitoring.PerformanceCounterInstance> performance)
        {
            ServerInfo(server_id, server_info);
            Performance(server_id, performance);
        }

        public ICollection<IEX.ManagementServer.Model.Monitoring.PerformanceCounterInstance> Performance(int server_id)
        {
            ServerFullInfo value;
            if (!base.TryGetValue(server_id, out value))
                return new List<IEX.ManagementServer.Model.Monitoring.PerformanceCounterInstance>();
            return value.Performance;
        }

        public void Performance(int server_id, ICollection<IEX.ManagementServer.Model.Monitoring.PerformanceCounterInstance> performance)
        {
            ServerFullInfo value = base[server_id];
            ServerFullInfo new_value = new ServerFullInfo(value.ServerInfo, performance);
            base[server_id] = new_value;
        }

        public ServerInfo ServerInfo(int server_id)
        {
            ServerFullInfo value;
            if (!base.TryGetValue(server_id, out value))
                return new ServerInfo() { Status = ServerState.Unknown };
            return value.ServerInfo;
        }

        public void ServerInfo(int server_id, ServerInfo server_info)
        {
            ServerFullInfo value = base[server_id];
            ServerFullInfo new_value = new ServerFullInfo(server_info, value.Performance);
            base[server_id] = new_value;
        }
    }
}

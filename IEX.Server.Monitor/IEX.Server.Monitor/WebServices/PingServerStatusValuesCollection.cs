using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Server.Monitor
{
    using System.Collections.Concurrent;
    internal class PingServerStatusValuesCollection : ConcurrentDictionary<int, Tuple<ServerInfo, Dictionary<string, string>>>
    {
        public PingServerStatusValuesCollection()
        {
            for (int i = MinMax().Item1; i < MinMax().Item2+1; ++i)
                base.TryAdd(i, new Tuple<ServerInfo, Dictionary<string, string>>(new ServerInfo(), null));
        }

        public Tuple<int, int> MinMax() { return new Tuple<int, int>(1, 8); }

        public int HashCode(int server_id)
        {
            int result = 0;
            ServerInfo server_info = ServerInfo(server_id);
            result = server_info.HashCode;
            return result;
        }

        public Dictionary<string, string> Performance(int server_id)
        {
            Tuple<ServerInfo, Dictionary<string, string>> value;
            if (!base.TryGetValue(server_id, out value))
                return new Dictionary<string, string>();
            return value.Item2;
        }

        public void Performance(int server_id, Dictionary<string, string> performance)
        {
            Tuple< ServerInfo, Dictionary<string, string>> value = base[server_id];
            Tuple< ServerInfo, Dictionary<string, string>> new_value = new Tuple<ServerInfo, Dictionary<string, string>>(value.Item1, performance);
            base[server_id] = new_value;
        }

        public ServerInfo ServerInfo(int server_id)
        {
            Tuple<ServerInfo, Dictionary<string, string>> value;
            if (!base.TryGetValue(server_id, out value))
                return new ServerInfo() { Status = ServerState.Unknown };
            return value.Item1;
        }

        public void ServerInfo(int server_id, ServerInfo server_info)
        {
            Tuple<ServerInfo, Dictionary<string, string>> value = base[server_id];
            Tuple<ServerInfo, Dictionary<string, string>> new_value = new Tuple<ServerInfo, Dictionary<string, string>>(server_info, value.Item2);
            base[server_id] = new_value;
        }
    }
}

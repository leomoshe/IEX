using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.Client
{
    using IEX.Utilities;
    using System.Runtime.Serialization;
    using System.ComponentModel;
    [DataContract]
    public class Server
    {
        public Server(string host_id, string server_id)
        {
            HostId = host_id;
            ServerId = server_id;
            ServerInfo = new IEX.Server.Monitor.Client.MonitoringServiceReference.ServerInfo();
            Status = ServerDataState.Unknown;
        }

        [DataMember]
        public virtual string HostId { get; set; }
        [DataMember]
        public virtual string ServerId { get; private set; }

        public virtual IEX.Server.Monitor.Client.MonitoringServiceReference.ServerInfo ServerInfo { get; set; }
        public virtual ServerDataState Status { get; set; }
        public virtual string CPU { get; set; }
        public virtual string Memory { get; set; }

        public static void KeyToHostSever(string key, out string host_id, out string server_id)
        {
            string[] result = key.Split(':');
            host_id = result[0];
            server_id = result[1];
        }

        public override string ToString()
        {
            return string.Format("Host: '{0}', Server: '{1}'", HostId,  ServerId);
        }
    }

    public static class ServerStateExtensions
    {
        public static System.Drawing.Color ToColor(ServerDataState value)
        {
            System.Drawing.Color result = System.Drawing.Color.Gray;
            switch (value)
            {
                //case ServerDataState.Unknown: result = System.Drawing.SystemColors.Control; break;
                //case ServerDataState.NotRunning: result = System.Drawing.SystemColors.Control; break;
                case ServerDataState.Loading: result = System.Drawing.Color.Yellow; break; // System.Drawing.Color.FromArgb(0, 160, 0); break;
                case ServerDataState.Idle: result = System.Drawing.Color.FromArgb(0, 128, 0); break;
                case ServerDataState.Connected: result = System.Drawing.Color.FromArgb(47, 180, 200); break;
                //case ServerDataState.ShuttingDown: result = System.Drawing.SystemColors.Control; break;
                //case ServerDataState.Restarting: result = System.Drawing.SystemColors.Control; break;
            }
            return result;
        }

        public static System.Drawing.Color ToForeColor(ServerDataState value)
        {
            System.Drawing.Color result = System.Drawing.Color.White;
            switch (value)
            {
                case ServerDataState.Loading: result = System.Drawing.Color.Black; break;
                case ServerDataState.Idle: result = System.Drawing.Color.White; break;
                case ServerDataState.Connected: result = System.Drawing.Color.White; break;
            }
            return result;
        }

        public static ServerDataState ToServerDataState(IEX.Server.Monitor.Client.MonitoringServiceReference.ServerState value)
        {
            return (ServerDataState)value;
        }

        public static IEX.Server.Monitor.Client.MonitoringServiceReference.ServerState ToServerState(ServerDataState value)
        {
            IEX.Server.Monitor.Client.MonitoringServiceReference.ServerState result = IEX.Server.Monitor.Client.MonitoringServiceReference.ServerState.Unknown;
            switch (value)
            {
                case ServerDataState.Loading:
                case ServerDataState.Idle:
                    result = IEX.Server.Monitor.Client.MonitoringServiceReference.ServerState.Idle;
                    break;
                case ServerDataState.Connected:
                    result = IEX.Server.Monitor.Client.MonitoringServiceReference.ServerState.Connected;
                    break;
                case ServerDataState.ShuttingDown:
                case ServerDataState.NotRunning:
                    result = IEX.Server.Monitor.Client.MonitoringServiceReference.ServerState.NotRunning;
                    break;
            }
            return result;
        }
    }

    public enum ServerDataState
    {
        [Description("Unknown")]
        Unknown = IEX.Server.Monitor.Client.MonitoringServiceReference.ServerState.Unknown,
        [Description("Loading...")]
        Loading = IEX.Server.Monitor.Client.MonitoringServiceReference.ServerState.Loading,
        [Description("Idle")]
        Idle = IEX.Server.Monitor.Client.MonitoringServiceReference.ServerState.Idle,
        [Description("Connected")]
        Connected = IEX.Server.Monitor.Client.MonitoringServiceReference.ServerState.Connected,
        [Description("Not Running")]
        NotRunning = IEX.Server.Monitor.Client.MonitoringServiceReference.ServerState.NotRunning,
        [Description("Shutting Down...")]
        ShuttingDown,
        [Description("Restarting...")]
        Restarting
    }

    public class ServerList : List<Server>
    {
        public static ServerList Convert(IList<Server> item)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { item });
            ServerList result = new ServerList();
            if (item != null)
                result.AddRange(item);
            return result;
        }

        public new void AddRange(IEnumerable<Server> collection)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { collection });
            var add = collection.Except(this);
            base.AddRange(add);
        }

        public override string ToString()
        {
            return string.Join(",", this);
        }
    }
}

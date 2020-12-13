using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using IEX.ManagementServer.Model.Resources;
using IEX.ManagementServer.Model.Monitoring;

namespace IEX.Server.Monitor
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWatchdogService" in both code and config file together.
    [ServiceContract]
    public interface IMonitoringService
    {
        void Start();

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool StartIEX(int iexNumber, bool shutdownUponErrors, out ServerInfo server_info);
        
        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Wrapped)]
        void StopIEX(int iexNumber);
        
        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool RestartIEX(int iexNumber, bool shutdownUponErrors, out ServerInfo server_info);

        [OperationContract]
        MonitorInfo GetMonitorInfo(Dictionary<int, int> servers_hash_code);

        //[OperationContract]
        //Dictionary<string, string> PingServer(int iexNumber, int hash_code, out bool has_changes);

        [OperationContract]
        [WebInvoke(UriTemplate = "/GetServerInfo?iexNumber={iexNumber}", Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ServerInfo GetServerInfo(int iexNumber);

        [OperationContract()]
        void RunProcess(Dictionary<string, string> data);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "*")]
        void GetOptions();
    }

    [DataContract]
    public class MonitorInfo
    {
        public MonitorInfo()
        {
            MonitorVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        [DataMember]
        public string MonitorVersion { get; set; }

       // [DataMember]
       // public Dictionary<string, string> Info { get; set; }
        [DataMember]
        public ComputerState State { get; set; }

        [DataMember]
        public ServerData[] Servers { get; set; }

        public static implicit operator string(MonitorInfo item)
        {
            return IEX.Utilities.Tools.Serializer.DataContractSerialize(item);
        }

        public override string ToString()
        {
            return this;
        }
    }

    [DataContract]
    public class ServerData
    {
        public ServerData()
        {
        }

        //[DataMember]
        //public int Id { get; set; }

        //[DataMember]
        //public Dictionary<string, string> Info { get; set; }

        [DataMember]
        public IexServerState State { get; set; }

        [DataMember]
        public bool has_changes { get; set; }
    }

    [DataContract]
    public enum ServerState
    {
        [EnumMember]
        Unknown = ServerServiceReference.ServerState.Unknown,
        [EnumMember]
        Loading = ServerServiceReference.ServerState.Loading,
        [EnumMember]
        Idle = ServerServiceReference.ServerState.Idle,
        [EnumMember]
        Connected = ServerServiceReference.ServerState.Connected,
        [EnumMember]
        NotRunning,
        [EnumMember]
        ShuttingDown
    };

    public static class ServerServiceReferenceServerStateExtensions
    {
        public static ServerState ToServerState(this ServerServiceReference.ServerState value)
        {
            ServerState result = ServerState.Unknown;

            switch (value)
            {
                case ServerServiceReference.ServerState.Loading:
                    result = ServerState.Loading;
                    break;
                case ServerServiceReference.ServerState.Idle:
                    result = ServerState.Idle;
                    break;
                case ServerServiceReference.ServerState.Connected:
                    result = ServerState.Connected;
                    break;
            }
            return result;
        }
    }


    [DataContract]
    public class ServerInfo
    {
        public ServerInfo()
            : this(ServerState.NotRunning)
        {
        }

        public ServerInfo(ServerState server_state)
        {
            this.Status = server_state;
            this.Connections = new ServerServiceReference.ConnectionInfoCollection();
            this.Services = new ServerServiceReference.ServiceInfoCollection();
            this.UpTime = DateTime.MinValue;
            //switch (server_state)
            //{
            //    case ServerState.Unknown:
            //        this.HashCode = -1;
            //        break;
            //    case ServerState.NotRunning:
            //        this.HashCode = 0;
            //        break;
            //    case ServerState.Loading:
            //        this.HashCode = 1;
            //        break;
            //}
            this.HashCode = 0;
        }

        public ServerInfo(ServerServiceReference.ServerInfo data)
        {
            this.Status = data.Status.ToServerState();
            this.Connections = data.Connections;
            this.Services = data.Services;
            this.UpTime = data.UpTime;
            this.HashCode = data.HashCode;
        }

        [DataMember]
        public ServerState Status { get; set; }

        [DataMember]
        public ServerServiceReference.ConnectionInfoCollection Connections { get; set; }

        [DataMember]
        public ServerServiceReference.ServiceInfoCollection Services { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]//added properties for JSON
        public DateTime UpTime { get; set; }

        [DataMember]
        public int HashCode { get; set; }

        public static implicit operator string(ServerInfo item)
        {
            return IEX.Utilities.Tools.Serializer.DataContractSerialize(item);
        }

        public override string ToString()
        {
            return this;
        }
    }
}

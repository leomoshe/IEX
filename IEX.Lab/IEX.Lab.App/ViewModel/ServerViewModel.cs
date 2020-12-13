using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.App
{
    using System.ComponentModel;
    using IEX.Utilities;
    using IEX.Utilities.Tools;
    using IEX.Utilities.Types;
    using System.Runtime.Serialization;
    [DataContract]
    public class ServerViewModel : INotifyPropertyChanged
    {
        public ServerViewModel(string host_id, string server_id)
        {
            Computer = host_id;
            ServerId = server_id;
            Status = Client.ServerDataState.Unknown.ToString();
        }

        public ServerViewModel(Client.Server server)
        {
            Computer = server.HostId;
            ServerId = server.ServerId;
            Status = server.Status.GetDescription();
            CPU = server.CPU;
            Memory = server.Memory;
            ServerInfo = server.ServerInfo;     
        }

        public string User
        {
            get
            {
                return (ServerInfo != null && ServerInfo.Connections != null && ServerInfo.Connections.Count > 0) ? ServerInfo.Connections[0].User : "";
            }
        }

        public string HostName
        {
            get
            {
                return (ServerInfo != null && ServerInfo.Connections != null && ServerInfo.Connections.Count > 0) ? ServerInfo.Connections[0].HostName : "";
            }
        }

        public string IPAddress
        {
            get
            {
                return (ServerInfo != null && ServerInfo.Connections != null && ServerInfo.Connections.Count > 0) ? ServerInfo.Connections[0].Details : "";
            }
        }

        public Changes Update(Client.Server server)
        {
            Tracer.Write(Tracer.TraceLevel.DEBUG, string.Format("Old Values of '{0}:{1}': Status='{2}', CPU='{3}', Memory='{4}'", server.HostId, server.ServerId, Status, CPU, Memory));
            Changes result = Changes.None;
            ServerInfo = server.ServerInfo;
            string status = server.Status.GetDescription();
            if (status != Status)
            {
                Status = status;
                result |= Changes.Status;
            }
            string cpu = server.CPU;
            string memory = server.Memory;
            if (cpu != CPU || memory != Memory)
            {
                CPU = cpu;
                Memory = memory;
                result |= Changes.Performance;
            }
            string streaming_port = server.ServerInfo.StreamingPort();
            if (streaming_port != StreamingPort)
            {
                StreamingPort = streaming_port;
                result |= Changes.StreamingPort;
            }
            Tracer.Write(Tracer.TraceLevel.DEBUG, string.Format("New Values of '{0}:{1}': Status='{2}', CPU='{3}', Memory='{4}'", server.HostId, server.ServerId, Status, CPU, Memory));
            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [DataMember]
        public string Computer { get; set; }
        [DataMember]
        public string ServerId { get; set; }
        [DataMember]
        public string StreamingPort { get; set; }
        private string _status;
        [DataMember]
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            }
        }

        public string StatusExtended
        {
            get
            {
                if (HostName == "" && User == "" && IPAddress == "")
                    return _status;
                else
                {
                    string host = HostName != "" ? HostName : IPAddress;
                    return _status + " (" + (User != "" ? User : "") + (host != "" ? " from " + host : "") + ")";
                }
            }            
        }

        [DataMember]
        public string CPU { get; set; }
        [DataMember]
        public string Memory { get; set; }
        private IEX.Server.Monitor.Client.MonitoringServiceReference.ServerInfo _server_info;
        // Too large for serialization
        public IEX.Server.Monitor.Client.MonitoringServiceReference.ServerInfo ServerInfo 
        {
            get { return _server_info; }
            set
            {
                _server_info = value;
                ServicesStatus = _server_info.ServicesStatus.GetDescription();
            }
        }
        private string _services_status;
        [DataMember]
        public string ServicesStatus
        {
            get { return _services_status; }
            set
            {
                _services_status = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ServicesStatus"));
            }
        }

        //public override string ToString()
        //{
        //    return IEX.Utilities.Tools.Serializer.DataContractSerialize(this);
        //}

        [Flags]
        public enum Changes 
        {
            None = 0x0,
            Performance = 0x1,
            Status = 0x2,
            StreamingPort = 0x3,
        };
    }
}

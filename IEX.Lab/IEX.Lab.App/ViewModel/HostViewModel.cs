using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.App
{
    using IEX.Utilities;
    using System.ComponentModel;
    using IEX.Utilities.Controls;
    using IEX.Utilities.Types;
    using System.Runtime.Serialization;
    [DataContract]
    public class HostViewModel
    {
        public HostViewModel(Client.Host host, IEnumerable<Client.Server> servers)
        {
            Computer = host.HostId;
            _servers_status = new Dictionary<int, string>();
            foreach (var server in servers)
                _servers_status.Add(Convert.ToInt16(server.ServerId.Replace("IEX_", string.Empty)), IEX.Lab.Client.ServerDataState.NotRunning.ToString());
            Update(host);
        }

        public void Update(Client.Host host)
        {
            Tracer.Write(Tracer.TraceLevel.DEBUG, string.Format("Old Values of '{0}': Status='{1}', CPU='{2}', Memory='{3}', MonitorVersion='{4}'", host.HostId, Status, CPU, Memory, MonitorVersion));
            Status = host.Status.GetDescription();
            CPU = host.CPU;
            Memory = host.Memory;
            MonitorVersion = host.MonitorVersion;
            Tracer.Write(Tracer.TraceLevel.DEBUG, string.Format("New Values for '{0}': Status='{1}', CPU='{2}', Memory='{3}', MonitorVersion='{4}'", host.HostId, Status, CPU, Memory, MonitorVersion));
        }

        public void Update(Client.Server server)
        {
            int id = Convert.ToInt16(server.ServerId.Replace("IEX_", string.Empty));
            Tracer.Write(Tracer.TraceLevel.DEBUG, string.Format("Old Value: {0}='{1}'", server.ServerId, ServerStatus(id)));
            ServerStatus(id, server.Status.GetDescription());
            Tracer.Write(Tracer.TraceLevel.DEBUG, string.Format("New Value: {0}='{1}'", server.ServerId, server.Status.GetDescription()));
        }

        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string Computer { get; set; }
        [DataMember]
        public string CPU { get; set; }
        [DataMember]
        public string Memory { get; set; }
        [DataMember]
        public string MonitorVersion { get; set; }
        private Dictionary<int, string> _servers_status = new Dictionary<int,string>();
        public string ServerStatus(int server_id)
        {
            return _servers_status[server_id];
        }
        public void ServerStatus(int server_id, string status)
        {
            if (!_servers_status.ContainsKey(server_id) && status == IEX.Lab.Client.ServerDataState.Unknown.ToString())
                _servers_status.Add(server_id, status);
            else
                _servers_status[server_id] = status;

            System.Reflection.PropertyInfo property_info = typeof(HostViewModel).GetProperty("IEX_" + server_id);
            if (property_info != null)
                property_info.SetValue(this, status, null);
        }

        // Check TypeDescriptionProvider, ICustomTypeDescriptor, PropertyBag in C#, DynamicObject and EMIT instead a properties of all the servers
        [DataMember]
        public string IEX_1 { get; set; }
        [DataMember]
        public string IEX_2 { get; set; }
        [DataMember]
        public string IEX_3 { get; set; }
        [DataMember]
        public string IEX_4 { get; set; }
        [DataMember]
        public string IEX_5 { get; set; }
        [DataMember]
        public string IEX_6 { get; set; }
        [DataMember]
        public string IEX_7 { get; set; }
        [DataMember]
        public string IEX_8 { get; set; }
        [DataMember]
        public string IEX_9 { get; set; }

        [DataMember]
        public string IEX_10 { get; set; }
        [DataMember]
        public string IEX_11 { get; set; }
        [DataMember]
        public string IEX_12 { get; set; }
        [DataMember]
        public string IEX_13 { get; set; }
        [DataMember]
        public string IEX_14 { get; set; }
        [DataMember]
        public string IEX_15 { get; set; }
        [DataMember]
        public string IEX_16 { get; set; }
        [DataMember]
        public string IEX_17 { get; set; }
        [DataMember]
        public string IEX_18 { get; set; }
        [DataMember]
        public string IEX_19 { get; set; }
        
        //public override string ToString()
        //{
        //    return IEX.Utilities.Tools.Serializer.DataContractSerialize(this);
        //}
    }
}

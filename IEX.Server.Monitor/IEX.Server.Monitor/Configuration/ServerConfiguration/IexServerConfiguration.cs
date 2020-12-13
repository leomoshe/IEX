using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Server.Monitor.Configuration.ServerConfiguration
{
    using System.Runtime.Serialization;
    using IEX.Utilities.Collections;
    [DataContract]
    public class IexServerConfiguration
    {
        bool _supportLegacyServices = true;
        
        public IexServerConfiguration(IEX.Server.Configuration.IexServerConfiguration data)
        {
            if (data != null)
            {
                Services = new ServiceInfoCollection(data.Service);
                _supportLegacyServices = data.SupportLegacyServices;
            }                
        }

        public Server.Configuration.IexServerConfiguration ToConfiguration()
        {
            return new Server.Configuration.IexServerConfiguration() { Service = this.Services.ToConfiguration(), SupportLegacyServices = this.SupportLegacyServices };
        }

        [DataMember]
        public bool SupportLegacyServices
        {
            get { return _supportLegacyServices; }
            set { _supportLegacyServices = value; }
        }

        public int Index(string service_name)
        {
            return Services.IndexWhere(item => item.Name == service_name);
        }

        public ServiceInfo Find(string service_name)
        {
            return this.Services.Find(item => item.Name == service_name);
        }

        public InstanceInfo[] Find(string service_name, string implementation_name)
        {
            ServiceInfo service_info = Find(service_name);
            if (service_info != null)
                return service_info.Find(implementation_name);
            return null;
        }

        public void Add(string service_name, InstanceInfo instance_info)
        {
            int index = Index(service_name);
            if (index == -1)
            {
                this.Services.Add(new ServiceInfo(service_name));
                index = Index(service_name);
            }
            this.Services[index].Add(instance_info);
        }

        ServiceInfoCollection _services = new ServiceInfoCollection();
        [DataMember]
        public ServiceInfoCollection Services
        {
            get { return _services; }
            set { _services = value; }
        }
    }
}

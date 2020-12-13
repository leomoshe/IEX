using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Server.Monitor.Configuration.ServerConfiguration
{
    using System.Runtime.Serialization;
    [DataContract]
    public class ServiceInfo
    {
        public ServiceInfo(string name)
        {
            Name = name;
        }

        public ServiceInfo(IEX.Server.Configuration.ServiceInfo data)
        {
            Name = data.Name;
            Instances = new InstanceInfoCollection(data.Instance);
        }

        public InstanceInfo[] Find(string implementation_name)
        {
            return this.Instances.FindAll(item => item.Implementation == implementation_name).ToArray();
        }

        public void Add(InstanceInfo instance_info)
        {
            this.Instances.Add(instance_info);
        }

        public Server.Configuration.ServiceInfo ToConfiguration()
        {
            return new Server.Configuration.ServiceInfo() { Name = this.Name, Instance = this.Instances.ToConfiguration() };
        }

        [DataMember]
        public string Name { get; private set; }

        InstanceInfoCollection _instances = new InstanceInfoCollection();
        [DataMember]
        public InstanceInfoCollection Instances
        {
            get { return _instances; }
            set { _instances = value; }
        }
    }

    [CollectionDataContract]
    public class ServiceInfoCollection : List<ServiceInfo>
    {
        public ServiceInfoCollection() { }
        public ServiceInfoCollection(IEnumerable<IEX.Server.Configuration.ServiceInfo> items)
        {
            if (items != null)
                this.AddRange(items.Select(item => new ServiceInfo(item))); 
        }

        public Server.Configuration.ServiceInfo[] ToConfiguration()
        {
            return this.Select(item => item.ToConfiguration()).ToArray();
        }
    }
}

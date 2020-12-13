using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Server.Monitor.Configuration.ServerConfiguration
{
    using System.Runtime.Serialization;
    [DataContract]
    public class InstanceInfo
    {
        public InstanceInfo(string name, string implementation_name, ParamInfoCollection parameters)
        {
            Name = name;
            Active = true;
            Implementation = implementation_name;
            Parameters = parameters;
        }

        public InstanceInfo(IEX.Server.Configuration.InstanceInfo data)
        {
            Name = data.Name;
            Active = data.Active;
            Implementation = data.Implementation;
            if (data.Param != null)
                Parameters = new ParamInfoCollection(data.Param);
            else
                Parameters = new ParamInfoCollection();
        }

        public Server.Configuration.InstanceInfo ToConfiguration()
        {
            return new Server.Configuration.InstanceInfo() { Name = this.Name, Active = this.Active, Implementation = this.Implementation, Param = this.Parameters.ToConfiguration() };
        }

        [DataMember]
        public bool AutomaticConfiguration { get; set; }
        
        [DataMember]
        public bool Active { get; private set; }

        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public string Implementation { get; private set; }

        ParamInfoCollection _parameters = new ParamInfoCollection();
        [DataMember]
        public ParamInfoCollection Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }

    [CollectionDataContract]
    public class InstanceInfoCollection : List<InstanceInfo>
    {
        public InstanceInfoCollection() { }
        public InstanceInfoCollection(IEnumerable<IEX.Server.Configuration.InstanceInfo> items)
        {
            if (items != null)
                this.AddRange(items.Select(item => new InstanceInfo(item)));
        }

        public Server.Configuration.InstanceInfo[] ToConfiguration()
        {
            return this.Select(item => item.ToConfiguration()).ToArray(); ;
        }
    }
}

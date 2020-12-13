using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Server.Monitor.Configuration.ServerConfiguration
{
    using System.Runtime.Serialization;
    [DataContract]
    public class ParamInfo
    {
        public ParamInfo(string value)
        {
            Value = value;
        }

        public ParamInfo(IEX.Server.Configuration.ParamInfo data)
        {
            Value = data.Value;
        }

        public Server.Configuration.ParamInfo ToConfiguration()
        {
            return new Server.Configuration.ParamInfo() { Value = this.Value };
        }

        [DataMember]
        public string Value { get; private set; }
    }

    [CollectionDataContract]
    public class ParamInfoCollection : List<ParamInfo>
    {
        public ParamInfoCollection() { }

        public ParamInfoCollection(IEnumerable<IEX.Server.Monitor.Configuration.ServerConfiguration.ParamInfo> items)
        {
            if (items != null)
                this.AddRange(items.Select(item => new ParamInfo(item.Value)));
        }

        public ParamInfoCollection(IEnumerable<IEX.Server.Configuration.ParamInfo> items)
        {
            this.AddRange(items.Select(item => new ParamInfo(item))); 
        }

        public Server.Configuration.ParamInfo[] ToConfiguration()
        {
            return this.Select(item => item.ToConfiguration()).ToArray();
        }
    }
}

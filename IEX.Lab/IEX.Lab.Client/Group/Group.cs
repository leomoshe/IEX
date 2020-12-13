using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.Client
{
    using IEX.Utilities;
    using System.Runtime.Serialization;
    [DataContract]
    public class Group
    {
        public Group()
        {
            Servers = new ServerList();
        }

        public Group(string group_name)
        {
            Name = group_name;
            Servers = new ServerList();
        }

        public Group(string group_name, ServerList servers)
        {
            Name = group_name;
            Servers = servers;
        }

        [DataMember]
        public virtual string Name { get; private set; }

        [DataMember]
        public ServerList Servers { get; set; }

        public static implicit operator string(Group data)
        {
            return IEX.Utilities.Tools.Serializer.DataContractSerialize(data);
        }

        public static explicit operator Group(string data)
        {
            return IEX.Utilities.Tools.Serializer.DataContractDeserialize<Group>(data);
        }

        public override string ToString()
        {
            return string.Format("Group: '{0}', Servers: {1}", Name, Servers);
        }
    }

    public class GroupList : List<Group>
    {
        public static GroupList Convert(IList<Group> item)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { item });
            GroupList result = new GroupList();
            if (item != null)
                result.AddRange(item);
            return result;
        }
    }
}

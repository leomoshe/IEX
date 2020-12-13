using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.Client
{
    using IEX.Utilities;
    public partial class IexLabXmlConfiguration
    {
        public IexLabXmlConfiguration()
        {
            configurationVersionField = "1.0";
            this.Server = new ServerInfo[0];
            this.Group = new GroupInfo[0];
        }

        public static Server[] Convert(ServerInfo[] servers)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { servers });
            if (servers == null)
                return new Server[0];
            Server[] result = Array.ConvertAll<ServerInfo, Server>(servers, server_info => new Server(server_info.Host, "IEX_" + server_info.ServerNumber));
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + result);
            return result;
        }

        public static ServerList ConvertToServerList(ServerInfo[] servers)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { servers });
            Server[] array = Convert(servers);
            ServerList result = ServerList.Convert(array);
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + result);
            return result;
        }

        public static ServerInfo[] Convert(ServerList servers)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { servers });
            Server[] array = servers.ToArray();
            ServerInfo[] result = Convert(array);
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + result);
            return result;
        }

        public static ServerInfo[] Convert(Server[] servers)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { servers });
            if (servers == null)
                return new ServerInfo[0];
            ServerInfo[] result = Array.ConvertAll<Server, ServerInfo>(servers, server => new ServerInfo(server));
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + result);
            return result;
        }

        public static Group[] Convert(GroupInfo[] groups)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { groups });
            if (groups == null)
                return new Group[0];
            Group[] result = Array.ConvertAll<GroupInfo, Group>(groups, group_info => new Group(group_info.Name, ConvertToServerList(group_info.Server)));
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + result);
            return result;
        }

        public static GroupInfo[] Convert(Group[] groups)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { groups });
            if (groups == null)
                return new GroupInfo[0];
            GroupInfo[] result = Array.ConvertAll<Group, GroupInfo>(groups, group => new GroupInfo(group));
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + result);
            return result;
        }
    }

    public partial class ServerInfo
    {
        public ServerInfo()
        {
        }

        public ServerInfo(Server server)
        {
            this.Host = server.HostId;
            this.ServerNumber = server.ServerId.Replace("IEX_", string.Empty);
        }
    }

    public partial class GroupInfo
    {
        public GroupInfo()
        {
        }

        public GroupInfo(Group group)
        {
            this.Name = group.Name;
            this.Server = IexLabXmlConfiguration.Convert(group.Servers);
        }
    }
}

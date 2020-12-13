using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.Client
{
    public interface ICatalogProvider : System.ComponentModel.INotifyPropertyChanged
    {
        void UpdateHost(string host_id, string version);

        IList<Host> FindHost();
        void Add(string host_id, IEnumerable<string> servers_id);
        void DeleteHost(string host_id);

        IList<int> GetConfiguratedIdsServers(string host_id);
        IList<Server> FindServer();
        IList<Server> FindServer(string host_id);
        Server FindServer(string host_id, string server_id);
        bool ExistServer(string host_id, string server_id);
        void DeleteServer(IList<Tuple<string, string>> servers);
        void StartServer(IList<Server> servers);
        void StopServer(IList<Server> servers);
        void RestartServer(IList<Server> servers);
#if IEX_DEBUG
        void StatusServer(IList<Server> servers);
#endif
        IList<Group> FindGroup();
        Group FindGroup(string group_id);
        bool ExistGroup(string group_id);
        void AddGroup(string group_id, ServerList servers = null);
        void RemoveServersFromGroup(string group_name, ServerList servers);
        void SetGroup(string group_id, ServerList servers);
        void RenameGroup(string oldName, string newName);
        void DeleteGroup(string group_id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.Client
{
    using IEX.Utilities;
    using IEX.Utilities.Collections;
    using System.ComponentModel;
    public class CatalogProvider : ICatalogProvider
    {
        private ICatalogRepository _repository;

        private readonly Common.IHostFactory _host_factory;
        private readonly Common.IServerFactory _server_factory;
        private readonly Common.IGroupFactory _group_factory;

        private HostDataCollection _hosts_data = new HostDataCollection();
        
        private IList<Host> _hosts_cache = new List<Host>();
        private IList<Server> _servers_cache;
        private IList<Group> _groups_cache;

        public CatalogProvider(ICatalogRepository repository, Common.IHostFactory host_factory, Common.IServerFactory server_factory, Common.IGroupFactory group_factory)
        {
            _repository = repository;
            _host_factory = host_factory;
            _server_factory = server_factory;
            _group_factory = group_factory;
            _hosts_data.ChildPropertyChanged += new PropertyChangedEventHandler(host_data_PropertyChanged);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property_name = "", object data = null)
        {
            Tracer.Write(Tracer.TraceLevel.DEBUG, "entered", new object[] { property_name, data });
            int index = 0;
            if (PropertyChanged != null && index != -1)
                PropertyChanged(this, new PropertyValueChangedEventArgs(property_name, data));
        }

        public void Start()
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered");

            _servers_cache = _repository.FindServer().ToList();

            _hosts_cache.Clear();
            Tracer.Write(Tracer.TraceLevel.INFO, string.Format("Remove '{0}' HostData's from the catalog.", string.Join(",", _hosts_data.Select(item => item.Host))));
            for (int i = _hosts_data.Count - 1; i > -1; --i)
            {
                HostData host_data = _hosts_data[i];
                _hosts_data.RemoveAt(i);
                host_data.PropertyChanged -= new PropertyChangedEventHandler(host_data_PropertyChanged);
                host_data.Dispose();
            }

            IEnumerable<IGrouping<string, Server>> servers_group = _servers_cache.GroupBy(item => item.HostId);
            foreach (var server_group in servers_group)
            {
                string host_id = server_group.Key;
                Host host = new Host(host_id);
                _hosts_cache.Add(host);

                IEnumerable<int> servers_id = server_group.Select(item => Convert.ToInt32(item.ServerId.Replace("IEX_", string.Empty)));
                HostData host_data = new HostData(host_id, servers_id);
                _hosts_data.Add(host_data);
            }
            _groups_cache = _repository.FindGroup().ToList();

            OnPropertyChanged("HostsClear");
            OnPropertyChanged("ServersClear");
            OnPropertyChanged("GroupsClear");

            OnPropertyChanged("HostsAdded", _hosts_cache);
            OnPropertyChanged("ServersAdded", _servers_cache);
            OnPropertyChanged("GroupsAdded", _groups_cache);
        }

        public IList<int> GetConfiguratedIdsServers(string host_id)
        {
            HostData host_data = _hosts_data.FirstOrDefault(item => item.Host == host_id);
            if (host_data != null)
                return host_data.GetConfiguratedIdsServers();
            return new List<int>();
        }

        public IList<Server> FindServer(string host_id)
        {
            return _servers_cache.Where(item => item.HostId.ToUpper() == host_id.ToUpper()).ToList();
        }
        
        public Server FindServer(string host_id, string server_id)
        {
            return _servers_cache.FirstOrDefault(item => item.HostId.ToUpper() == host_id.ToUpper() && item.ServerId.ToUpper() == server_id.ToUpper());
        }

        public bool ExistServer(string host_id, string server_id)
        {
            return FindServer(host_id, server_id) != null;
        }

        public IList<Group> FindGroup()
        {
            return _groups_cache;
        }

        public void Add(string host_id, IEnumerable<string> servers_id)
        {
            if (!ExistHost(host_id))
            {
                Host host = _host_factory.Create(host_id);
                _hosts_cache.Add(host);

                HostData host_data = new HostData(host_id, servers_id.Select(item => Convert.ToInt32(item.Replace("IEX_", string.Empty))));
                _hosts_data.Add(host_data);

                OnPropertyChanged("HostAdded", host);
            }

            if (servers_id.Count() > 0)
            {
                List<Server> servers = new List<Server>();
                List<Tuple<string, string>> add = servers_id.Where(item => !ExistServer(host_id, item)).Select(item => new Tuple<string, string>(host_id, item)).ToList();
                foreach (Tuple<string, string> data in add)
                {
                    Server server = _server_factory.Create(data.Item1, data.Item2);
                    _repository.Add(server);
                    servers.Add(server);
                }
                _servers_cache = _repository.FindServer().ToList();
                OnPropertyChanged("ServersAdded", servers);
            }
        }

        public void UpdateHost(string host_id, string version)
        {
            HostData host_data = _hosts_data.FirstOrDefault(item => item.Host == host_id);
            if (host_data != null)
                host_data.UpdateHost(version);
        }

        public IList<Host> FindHost()
        {
            return _hosts_cache;
        }

        private bool ExistHost(string host_id)
        {
            return _servers_cache.FirstOrDefault(item => item.HostId.ToUpper() == host_id.ToUpper()) != null;
        }

        public void DeleteHost(string host_id)
        {
            if (!ExistHost(host_id))
                return;

            List<Tuple<string, string>> servers = _servers_cache.Where(item => item.HostId == host_id).Select(item => new Tuple<string, string>(host_id, item.ServerId)).ToList();
            DeleteServer(servers);

            _hosts_cache.Remove(item => item.HostId == host_id);

            int index = _hosts_data.IndexWhere(item => item.Host == host_id);
            HostData host_data = _hosts_data[index];
            _hosts_data.RemoveAt(index);
            host_data.PropertyChanged -= new PropertyChangedEventHandler(host_data_PropertyChanged);
            host_data.Dispose();

            OnPropertyChanged("HostRemoved", host_id);
        }

        public void DeleteServer(IList<Tuple<string, string>> servers)
        {
            if (servers.Count() > 0)
            {
                List<Tuple<string, string>> delete = servers.Where(item => ExistServer(item.Item1, item.Item2)).Select(item => new Tuple<string, string>(item.Item1, item.Item2)).ToList();
                for (int i = delete.Count - 1; i > -1; --i)
                    _repository.DeleteServer(delete[i].Item1, delete[i].Item2);

                _servers_cache = _repository.FindServer().ToList();

                OnPropertyChanged("ServersRemoved", delete);
            }
        }

        public IList<Server> FindServer()
        {
            return _servers_cache;
        }

        static object _lock = new object();

        private void host_data_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            HostData host_data = sender as HostData;

            if (host_data == null) return;

            Host host = _hosts_cache.FirstOrDefault(item => item.HostId == host_data.Host);
            if (host != null)
            {
                host.SubsystemsMetadata = host_data.SubsystemsMetadata;
                host.Status = host_data.Status;
                host.CPU = host_data.CPU;
                host.Memory = host_data.Memory;
                host.MonitorVersion = host_data.MonitorVersion;
                //host.ConfiguratedIdsServers = host_data.GetConfiguratedIdsServers();
                foreach (int id in host_data.ServersId)
                //foreach (int id in host_data.GetConfiguratedIdsServers())
                {
                    string server_id = "IEX_" + id;
                    Server server = _servers_cache.FirstOrDefault(item => item.HostId == host_data.Host && item.ServerId == server_id);
                    if (server != null)
                    {
                        //System.Reflection.PropertyInfo property_info = host_data.GetType().GetProperty(server_id);
                        //IEX.Server.Monitor.Client.MonitoringServiceReference.ServerFullInfo server_full_info = (IEX.Server.Monitor.Client.MonitoringServiceReference.ServerFullInfo)property_info.GetValue(host_data, null);
                        IEX.Server.Monitor.Client.MonitoringServiceReference.ServerFullInfo server_full_info = host_data.GetServerFullInfo(id);
                        if (server_full_info != null)
                        {
                            
                            ServerDataState server_data_state = ServerStateExtensions.ToServerDataState(server_full_info.ServerInfo.Status);
                            if (server.Status == ServerDataState.ShuttingDown && server_data_state == ServerDataState.Idle)
                            {
                            }
                            else
                                server.Status = server_data_state;
                            server.ServerInfo = server_full_info.ServerInfo;
                            if (server_full_info.Performance != null /* && server_full_info.Performance.Count == 2*/)
                            {
                                if (server_full_info.Performance.ContainsKey("% Processor Time"))
                                {
                                    server.CPU = server_full_info.Performance["% Processor Time"];
                                }
                                else
                                {
                                    server.CPU = string.Empty;
                                }
                                if (server_full_info.Performance.ContainsKey("Working Set"))
                                {
                                    server.Memory = server_full_info.Performance["Working Set"];
                                }
                                else
                                {
                                    server.Memory = string.Empty;
                                }
                            }
                            else
                            {
                                server.CPU = string.Empty;
                                server.Memory = string.Empty;
                            }
                        }
                    }
                }
                lock (_lock)
                {
                    OnPropertyChanged("HostChanged", host);
                }
            }
        }

        public void StartServer(IList<Server> servers)
        {
            IEnumerable<IGrouping<string, Server>> servers_group = servers.GroupBy(item => item.HostId);
            foreach (var server_group in servers_group)
            {
                HostData host_data = _hosts_data.FirstOrDefault(item => item.Host == server_group.Key);
                if (host_data != null)
                {
                    if (host_data.Status == IEX.Server.Monitor.Client.MonitoringServiceReference.MonitorState.Running)
                    {
                        host_data.StopPing();
                        foreach (Server server in server_group)
                        {
                            int id = Convert.ToInt16(server.ServerId.Replace("IEX_", string.Empty));
                            Server server_cache = _servers_cache.FirstOrDefault(item => item.HostId == host_data.Host && item.ServerId == server.ServerId);
                            if (server_cache != null && server_cache.Status == ServerDataState.NotRunning)
                            {
                                if (host_data.Start(id))
                                {
                                    server_cache.Status = ServerDataState.Loading;
                                    host_data.SetTemporalStatus(id, IEX.Server.Monitor.Client.MonitoringServiceReference.ServerState.Loading);
                                }
                            }
                        }
                        host_data.StartPing();
                    }
                }
            }
        }

        public void StopServer(IList<Server> servers)
        {
            IEnumerable<IGrouping<string, Server>> servers_group = servers.GroupBy(item => item.HostId);
            foreach (var server_group in servers_group)
            {
                HostData host_data = _hosts_data.FirstOrDefault(item => item.Host == server_group.Key);
                if (host_data != null)
                {
                    if (host_data.Status == IEX.Server.Monitor.Client.MonitoringServiceReference.MonitorState.Running)
                    {
                        host_data.StopPing();
                        foreach (Server server in server_group)
                        {
                            int id = Convert.ToInt16(server.ServerId.Replace("IEX_", string.Empty));
                            Server server_cache = _servers_cache.FirstOrDefault(item => item.HostId == host_data.Host && item.ServerId == server.ServerId);
                            if (server_cache != null && (server_cache.Status == ServerDataState.Connected || server_cache.Status == ServerDataState.Idle))
                            {
                                if (host_data.Stop(id))
                                {
                                    server_cache.Status = ServerDataState.ShuttingDown;
                                    host_data.SetTemporalStatus(id, IEX.Server.Monitor.Client.MonitoringServiceReference.ServerState.ShuttingDown);
                                }
                            }
                        }
                        host_data.StartPing();
                    }
                }
            }
        }

        public void RestartServer(IList<Server> servers)
        {
            IEnumerable<IGrouping<string, Server>> servers_group = servers.GroupBy(item => item.HostId);
            foreach (var server_group in servers_group)
            {
                HostData host_data = _hosts_data.FirstOrDefault(item => item.Host == server_group.Key);
                if (host_data != null)
                {
                    if (host_data.Status == IEX.Server.Monitor.Client.MonitoringServiceReference.MonitorState.Running)
                    {
                        host_data.StopPing();
                        foreach (Server server in server_group)
                        {
                            int id = Convert.ToInt16(server.ServerId.Replace("IEX_", string.Empty));
                            Server server_cache = _servers_cache.FirstOrDefault(item => item.HostId == host_data.Host && item.ServerId == server.ServerId);
                            if (server_cache != null && (server_cache.Status == ServerDataState.Connected || server_cache.Status == ServerDataState.Idle))
                            {
                                if (host_data.Restart(id))
                                {
                                    server_cache.Status = ServerDataState.Restarting;
                                    host_data.SetTemporalStatus(id, IEX.Server.Monitor.Client.MonitoringServiceReference.ServerState.Loading);
                                }
                            }
                        }
                        host_data.StartPing();
                    }
                }
            }
        }

#if IEX_DEBUG
        public void StatusServer(IList<Server> servers)
        {
            //_server_provider.StatusServer(servers);
        }
#endif

        public Group FindGroup(string group_id)
        {
            return _groups_cache.FirstOrDefault(item => item.Name.ToUpper() == group_id.ToUpper());
        }

        public bool ExistGroup(string group_id)
        {
            return FindGroup(group_id) != null;
        }

        public void RenameGroup(string oldName, string newName)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { oldName, newName });
            Group group = FindGroup(oldName);
            DeleteGroup(oldName);
            AddGroup(newName, group.Servers);
            OnPropertyChanged("GroupChanged", group);
        }

        public void SetGroup(string group_id, ServerList servers)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { group_id, servers });
            Group group = FindGroup(group_id);
            group.Servers = servers;
            _repository.SetGroup(group_id, group);
            _groups_cache = _repository.FindGroup().ToList();
            OnPropertyChanged("GroupChanged", group);
        }

        public void RemoveServersFromGroup(string group_id, ServerList servers)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { group_id, servers });
            Group group = FindGroup(group_id);
            foreach (Server s in servers)
            {

                int t = group.Servers.IndexWhere(b => (b.HostId==s.HostId) && (b.ServerId==s.ServerId ) );
                group.Servers.RemoveAt(t);
            }
            _repository.SetGroup(group_id, group);
            _groups_cache = _repository.FindGroup().ToList();
            OnPropertyChanged("GroupChanged", group);
        }

        public void AddGroup(string group_id, ServerList servers = null)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { group_id, servers });
            Group group = _group_factory.Create(group_id);
            if (servers != null)
                group.Servers = servers;
            _repository.Add(group);
            _groups_cache = _repository.FindGroup().ToList();
            OnPropertyChanged("GroupAdded", group);
        }

        public void DeleteGroup(string group_id)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { group_id });
            _repository.DeleteGroup(group_id);
            _groups_cache = _repository.FindGroup().ToList();
            OnPropertyChanged("GroupRemoved", group_id);
        }
        

        public void CatalogProvider_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyValueChangedEventArgs ea = e as PropertyValueChangedEventArgs;
                PropertyChanged(this, ea);
            }
        }
    }
}

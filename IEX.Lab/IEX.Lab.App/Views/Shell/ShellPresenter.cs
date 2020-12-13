using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.App.Views
{
    using IEX.Utilities;
    using IEX.Lab.Client;
    using System.ComponentModel;
    using IEX.Utilities.Collections;
    using IEX.Utilities.Types;
    public class ShellPresenter : Presenter<IShell>, INotifyPropertyChanged
    {
        private IEX.Lab.Client.ICatalogProvider _catalog_provider;
        private BindingListView<HostViewModel> _hosts_view_model;
        private BindingServerView _servers_view_model;
        public ShellPresenter(ICatalogProvider catalog_provider)
        {
            _catalog_provider = catalog_provider;
        }

        public void New(string full_path)
        {
            XmlCatalogRepository repository = (XmlCatalogRepository)StructureMap.ObjectFactory.GetInstance<IEX.Lab.Client.ICatalogRepository>();
            repository.New(full_path);
            repository.Open(full_path);

            CatalogProvider provider = (CatalogProvider)StructureMap.ObjectFactory.GetInstance<IEX.Lab.Client.ICatalogProvider>();
            provider.Start();
        }

        public void Open(string full_path)
        {
            XmlCatalogRepository repository = (XmlCatalogRepository)StructureMap.ObjectFactory.GetInstance<IEX.Lab.Client.ICatalogRepository>();
            try
            {
                repository.Open(full_path);
            }
            catch
            {
                Tracer.Write(Tracer.TraceLevel.INFO, string.Format("'{0}' not found", full_path));
                repository.New(full_path);
                repository.Open(full_path);
            }
            CatalogProvider provider = (CatalogProvider)StructureMap.ObjectFactory.GetInstance<IEX.Lab.Client.ICatalogProvider>();
            provider.Start();
        }

        public void SaveAs(string full_path)
        {
            //data_context.SaveChanges();
            XmlCatalogRepository repository = (XmlCatalogRepository)StructureMap.ObjectFactory.GetInstance<IEX.Lab.Client.ICatalogRepository>();
            repository.SaveAs(full_path);
        }

        private object _selected_content;
        public object SelectedContent
        {
            get { return _selected_content; }
            set
            {
                _selected_content = value;
                string content = (string)_selected_content;
                if (content.StartsWith("IEX Servers\\"))
                {
                    string filter = content.Substring(content.IndexOf("\\") + 1);
                    if (filter == IEX.Server.Monitor.Client.MonitoringServiceReference.ServicesStatus.Error.GetDescription())
                        _servers_view_model.Filter = "ServicesStatus~=" + filter;
                    else
                        _servers_view_model.Filter = "Status~=" + filter;
                }
                else if (content.StartsWith("Groups\\"))
                {
                    string group_name = content.Substring(content.IndexOf("\\") + 1);
                    Client.Group group = GetGroups().FirstOrDefault(item => item.Name == group_name);
                    if (group != null)
                        _servers_view_model.GroupFilter = group;
                }
                else
                    _servers_view_model.RemoveFilter();
                //OnPropertyChanged("SelectedContent");
            }
        }

        public override void OnViewReady()
        {
            base.OnViewReady();
            ((CatalogProvider)_catalog_provider).PropertyChanged += ShellPresenter_PropertyChanged;
            _hosts_view_model = new BindingListView<HostViewModel>(View as ISynchronizeInvoke);
            _servers_view_model = new BindingServerView(View as ISynchronizeInvoke);
            View.Hosts = _hosts_view_model;
            View.Servers = _servers_view_model;
        }

        private IList<Host> _selected_hosts = new IEX.Lab.Client.HostList();
        public IList<Host> SelectedHosts
        {
            get { return _selected_hosts; }
            set
            {
                _selected_hosts = value;
                //OnPropertyChanged("SelectedHosts");
            }
        }

        #region ShellPresenter_PropertyChanged
        public void ShellPresenter_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IEX.Lab.Client.PropertyValueChangedEventArgs ea = e as IEX.Lab.Client.PropertyValueChangedEventArgs;
            object data = ea.Data;
            string pn = ea.PropertyName;
            Tracer.Write(Tracer.TraceLevel.DEBUG, string.Format("PropertyChanged: PropertyName='{0}', Data='{1}'", pn, data));

            if (pn == "HostsClear")
                _hosts_view_model.Clear();
            if (pn == "ServersClear")
                _servers_view_model.Clear();
            //if (pn == "GroupsClear")
            //    _servers_view_model.Clear();

            if (pn == "HostsAdded")
                HostsAdded(data as IList<Host>);
            if (pn == "ServersAdded")
                ServersAdded(data as IList<Server>);
            if (pn == "GroupsAdded")
                ReloadGroups();

            if (pn == "HostAdded")
                HostsAdded(new List<Host>() { data as Host });
            if (pn == "HostRemoved")
            {
                HostRemoved(data as string);
                OnPropertyChanged("HostRemoved");
            }

            if (pn == "ServersRemoved")
            {
                ServersRemoved(data as IList<Tuple<string, string>>);
                OnPropertyChanged("ServersRemoved");
            }

            if (pn == "GroupAdded" || pn == "GroupRemoved" || pn == "GroupsRemoved")
                ReloadGroups();

            if (pn == "GroupChanged")
                _servers_view_model.GroupFilter = data as Group;
            // Updates
            if (pn == "HostChanged")
            {
                Host host = data as Host;
                HostViewModel host_view_model = _hosts_view_model.FirstOrDefault(item => item.Computer == host.HostId);
                if (host_view_model != null)
                    host_view_model.Update(host);
                IList<Server> servers = _catalog_provider.FindServer(host.HostId);
                List<Tuple<Server, ServerViewModel.Changes>> servers_changes = new List<Tuple<Server, ServerViewModel.Changes>>();
                foreach (Server server in servers)
                {
                    if (host_view_model != null)
                        host_view_model.Update(server);

                    ServerViewModel server_view_model = _servers_view_model.FirstOrDefault(item => item.Computer == server.HostId && item.ServerId == server.ServerId);
                    if (server_view_model != null)
                        servers_changes.Add(new Tuple<Server,ServerViewModel.Changes>(server, server_view_model.Update(server)));
                }
                OnPropertyChanged("HostChanged", servers_changes); // Invalidate the grids
            }
        }

        public void HostsAdded(IList<Host> hosts)
        {
            var hosts_view_model = hosts.Select(host => new HostViewModel(host, GetServers().Where(server => server.HostId == host.HostId)));
            foreach (var host_view_model in hosts_view_model) // try AddAll
                _hosts_view_model.Add(host_view_model);
        }

        public void ServersAdded(IList<Server> servers)
        {
            var servers_view_model = servers.Select(item => new ServerViewModel(item.HostId, item.ServerId));
            foreach (ServerViewModel server_view_model in servers_view_model)   // try AddAll
                _servers_view_model.Add(server_view_model);

            View.ColumnServers = _servers_view_model.Select(item => Convert.ToInt32(item.ServerId.Replace("IEX_", string.Empty))).Distinct().ToList();

            IEnumerable<IGrouping<string, Server>> servers_group = servers.GroupBy(item => item.HostId);
            foreach (var server_group in servers_group)
            {
                HostViewModel host_view_model = _hosts_view_model.FirstOrDefault(item => item.Computer == server_group.Key);
                if (host_view_model != null)
                {
                    foreach (Server server in server_group)
                    {
                        int id = Convert.ToInt16(server.ServerId.Replace("IEX_", string.Empty));
                        host_view_model.ServerStatus(id, server.Status.ToString());
                    }
                }
            }
        }

        public void ReloadGroups()
        {
            View.Groups = GroupList.Convert(_catalog_provider.FindGroup());
        }

        public void ServersRemoved(IList<Tuple<string, string>> servers)
        {
            foreach (Tuple<string, string> server in servers)
                _servers_view_model.RemoveAll(item => item.Computer == server.Item1 && item.ServerId == server.Item2);

            
            //_servers_view_model.Distinct(item => Convert.ToInt32(item.ServerId.Replace("IEX_", string.Empty)));

            //IEnumerable<int> values = _servers_view_model.SelectMany<ServerViewModel,int>(item => Convert.ToInt32(item.ServerId.Replace("IEX_", string.Empty)));
            //var values = _servers_view_model.SelectMany<ServerViewModel,int>(item => Convert.ToInt32(item.ServerId.Replace("IEX_", string.Empty))).Distinct();
            View.ColumnServers = _servers_view_model.Select(item => Convert.ToInt32(item.ServerId.Replace("IEX_", string.Empty))).Distinct().ToList();

            IEnumerable<IGrouping<string, Tuple<string, string>>> servers_group = servers.GroupBy(item => item.Item1);
            foreach (var server_group in servers_group)
            {
                HostViewModel host_view_model = _hosts_view_model.First(item => item.Computer == server_group.Key);
                if (host_view_model != null)
                {
                    foreach (Tuple<string, string> server in server_group)
                    {
                        System.Reflection.PropertyInfo property_info = typeof(HostViewModel).GetProperty(server.Item2);
                        property_info.SetValue(host_view_model, string.Empty, null);
                    }
                }
            }
        }

        public void HostRemoved(string host_id)
        {
            _hosts_view_model.RemoveAll(item => item.Computer == host_id);
        }

        public IList<Host> GetHosts()
        {
            return _catalog_provider.FindHost();
        }

        #endregion ShellPresenter_PropertyChanged

        public IList<int> GetConfiguratedIdsServers(string host_id)
        {
            return _catalog_provider.GetConfiguratedIdsServers(host_id);
        }

        public IList<Server> GetServers()
        {
            return _catalog_provider.FindServer();
        }

        public void Add(string host_id, IEnumerable<string> servers_id)
        {
            foreach (string server_id in servers_id)
            {
                if (_catalog_provider.ExistServer(host_id, server_id))
                    InformUserServerAlreadyListed(host_id, server_id);
            }
            _catalog_provider.Add(host_id, servers_id);
        }

        public void DeleteServer(IList<Tuple<string, string>> servers)
        {
            List<Tuple<string, string>> delete = new List<Tuple<string, string>>();
            foreach (Tuple<string, string> server in servers)
            {
                System.Windows.Forms.DialogResult res = System.Windows.Forms.MessageBox.Show(string.Format("Do you really want to remove the server '{0}:{1}'?", server.Item1, server.Item2), "", System.Windows.Forms.MessageBoxButtons.YesNo);
                if (res != System.Windows.Forms.DialogResult.No)
                    delete.Add(server);
            }
            _catalog_provider.DeleteServer(delete);
        }

        public void DeleteHost(string host_id)
        {
            System.Windows.Forms.DialogResult res = System.Windows.Forms.MessageBox.Show(string.Format("Do you really want to remove the computer '{0}'?", host_id), "", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (res != System.Windows.Forms.DialogResult.No)
                _catalog_provider.DeleteHost(host_id);
        }

        public void UpdateHost(string host_id, string version)
        {
            _catalog_provider.UpdateHost(host_id, version);
        }

        private IList<Server> _selected_servers = new IEX.Lab.Client.ServerList();
        public IList<Server> SelectedServers
        {
            get { return _selected_servers; }
            set
            {
                _selected_servers = value;
                View.SelectedServers = value;
            }
        }

        public void AddGroup(string group_name)
        {
            if (_catalog_provider.ExistGroup(group_name))
                InformUserGroupAlreadyListed(group_name);
            else
                _catalog_provider.AddGroup(group_name);
        }

        private void InformUserServerAlreadyListed(string host, string server_id)
        {
            System.Windows.Forms.MessageBox.Show(string.Format("A host,server is already listed {0} {1}", host, server_id));
        }

        public void StartServer(IList<Client.Server> servers)
        {
            _catalog_provider.StartServer(servers);
        }

        public void StopServer(IList<Client.Server> servers)
        {
            _catalog_provider.StopServer(servers);
        }

        public void RestartServer(IList<Client.Server> servers)
        {
            _catalog_provider.RestartServer(servers);
        }

#if IEX_DEBUG
        public void StatusServer(IList<Client.Server> servers)
        {
            _catalog_provider.StatusServer(servers);
        }
#endif
        public IList<Group> GetGroups()
        {
            return _catalog_provider.FindGroup();
        }

        public void SetGroup(string group_name, ServerList servers)
        {
            if (!_catalog_provider.ExistGroup(group_name))
                //InformUserGroupAlreadyListed(group_name);
                System.Windows.Forms.MessageBox.Show(string.Format("A groupr isn't listed '{0}'", group_name));
            else
                _catalog_provider.SetGroup(group_name, servers);
        }

        public void RenameGroup(string oldName, string newName)
        {
            if (!_catalog_provider.ExistGroup(oldName))
                //InformUserGroupAlreadyListed(group_name);
                System.Windows.Forms.MessageBox.Show(string.Format("A groupr isn't listed '{0}'", oldName));
            else
                _catalog_provider.RenameGroup(oldName, newName);
        }
        

        public void RemoveServersFromGroup(string group_name, ServerList servers)
        {
            _catalog_provider.RemoveServersFromGroup(group_name, servers);
        }

        public void DeleteGroup(string group_name)
        {
            System.Windows.Forms.DialogResult res = System.Windows.Forms.MessageBox.Show(string.Format("Do you really want to remove the group '{0}'?", group_name), "", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (res == System.Windows.Forms.DialogResult.No)
                return;

            _catalog_provider.DeleteGroup(group_name);
        }

        private IList<Group> _selected_groups = new IEX.Lab.Client.GroupList();
        public IList<Group> SelectedGroups
        {
            get { return _selected_groups; }
            set
            {
                _selected_groups = value;
                OnPropertyChanged("SelectedGroups");
            }
        }

        private void InformUserGroupAlreadyListed(string group_name)
        {
            System.Windows.Forms.MessageBox.Show(string.Format("A group is already listed '{0}'", group_name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property_name = "", object data = null)
        {
            Tracer.Write(Tracer.TraceLevel.DEBUG, "entered", new object[] { property_name, data });
            int index = 0;
            if (PropertyChanged != null && index != -1)
                PropertyChanged(this, new PropertyValueChangedEventArgs(property_name, data));
        }
    }
}

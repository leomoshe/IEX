using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IEX.Lab.App.Views
{
    using DevExpress.XtraBars.Docking;
    using IEX.Utilities.Controls;
    using IEX.Utilities.Collections;
    public partial class frmLab : DevExpress.XtraEditors.XtraForm, IShell
    {
        private ShellPresenter _presenter;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _presenter.OnViewReady();

            //if (System.IO.File.Exists("IEX.LAB.xml"))
            //    this.dockManagerMain.RestoreLayoutFromXml("IEX.LAB.xml");
            treeNavigator.SelectedNode = treeNavigator.Nodes[0];
            ActiveControl = treeNavigator;

            if (System.IO.File.Exists(RepositoryString))
                _mru_list.InsertElement(RepositoryString);
            _presenter.Open(_mru_list.ValueAt(0));
            this.Text = string.Format("Lab {0} - {1}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version, _mru_list.ValueAt(0));
        }

        public frmLab()
        {
            InitializeComponent();
        }

        MRUList _mru_list;

        DevExpress.XtraBars.Docking2010.Views.BaseDocument _doc_computers;
        DevExpress.XtraBars.Docking2010.Views.BaseDocument _doc_stbs_grid;
        DevExpress.XtraBars.Docking2010.Views.BaseDocument _doc_videos;
        public frmLab(ShellPresenter presenter)
        {
            _presenter = presenter;
            _presenter.PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e) =>
                {
                    IEX.Lab.Client.PropertyValueChangedEventArgs ea = e as IEX.Lab.Client.PropertyValueChangedEventArgs;
                    object data = ea.Data;
                    if (e.PropertyName == "HostChanged")
                    {
                        iexComputersGrid1.Invalidate();
                        iexStbGridServers.Invalidate();
                        List<Tuple<Client.Server, ServerViewModel.Changes>> servers_changes = data as List<Tuple<Client.Server, ServerViewModel.Changes>>;
                        bool any_status_changed = servers_changes.Any(item => (item.Item2 & ServerViewModel.Changes.Status) == ServerViewModel.Changes.Status);
                        if (any_status_changed == true)
                            ServersSelectedHandler(this, null);
                    }
                    else if (e.PropertyName == "HostRemoved")
                    {
                        SetSelectedGridHosts();
                        ContentHostSelectedHandler(this, null);
                    }
                    else if (e.PropertyName == "ServersRemoved")
                    {
                        SetSelectedServers(new IEX.Lab.Client.ServerList());
                        ServersSelectedHandler(this, null);
                    }
                };
            _presenter.View = this;

            InitializeComponent();

//            var servers = _presenter.GetServers().SelectMany(item => item.ServerId.Replace("IEX_", string.Empty)).Distinct().ToList();
            //iexComputersGrid1.ServersId = new int[] { 1, 2, 3, 4, 5, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
            //iexComputersGrid1.ServersId = new int[] { 1, 2, 3, 4, 5, 6};
            iexComputersGrid1.GotFocus += new EventHandler(iexComputersGrid1_GotFocus);
            iexStbGridServers.GotFocus += new EventHandler(iexStbGridServers_GotFocus);

            iexFlowLayoutPanel1.ControlType = typeof(IexStb);

            _doc_computers = tabbedView1.Controller.AddDocument(iexComputersGrid1);
            _doc_computers.Caption = "Computers";
            _doc_computers.Properties.AllowClose = DevExpress.Utils.DefaultBoolean.False;
            _doc_computers.Properties.AllowFloat = DevExpress.Utils.DefaultBoolean.False;

            _doc_stbs_grid = tabbedView1.Controller.AddDocument(iexStbGridServers);
            _doc_stbs_grid.Caption = "Grid";
            _doc_stbs_grid.Properties.AllowClose = DevExpress.Utils.DefaultBoolean.False;
            _doc_stbs_grid.Properties.AllowFloat = DevExpress.Utils.DefaultBoolean.False;
            
            _doc_videos = tabbedView1.Controller.AddDocument(iexFlowLayoutPanel1);
            _doc_videos.Caption = "Video";
            _doc_videos.Properties.AllowClose = DevExpress.Utils.DefaultBoolean.False;
            _doc_videos.Properties.AllowFloat = DevExpress.Utils.DefaultBoolean.False;
            
            _mru_list = new MRUList(mniFileRecent);
            _mru_list.ItemClicked += new EventHandler(MenuItemClickHandler);

            set_info_tree();

            InitializeCommandManager();
            
//#if IEX_DEBUG
//            btnStatus.Visible = true;
//#else
//            btnStatus.Visible = false;
//#endif
        }

        private void frmLab_Load(object sender, EventArgs e)
        {
            tabbedView1.DocumentActivated += new DevExpress.XtraBars.Docking2010.Views.DocumentEventHandler(tabbedView1_DocumentActivated);
            tabbedView1.DocumentDeactivated += new DevExpress.XtraBars.Docking2010.Views.DocumentEventHandler(tabbedView1_DocumentDeactivated);
        }

        void tabbedView1_DocumentDeactivated(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
            if (e.Document == _doc_videos)
            {
                foreach (IexStb iexStb in iexFlowLayoutPanel1.Controls)
                {
                    iexStb.Stop();
                }
            }
        }

        void tabbedView1_DocumentActivated(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
            if (e.Document == _doc_videos)
            {
                foreach (IexStb iexStb in iexFlowLayoutPanel1.Controls)
                {
                    iexStb.Play();
                }
            }
        }
      

        private void MenuItemClickHandler(object sender, EventArgs e)
        {
            string file_name = sender as string;
            _presenter.Open(file_name);
            _mru_list.InsertElement(file_name);
            this.Text = string.Format("Lab {0} - {1}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version, _mru_list.ValueAt(0));
        }

        private void treeNavigator_AfterSelect(object sender, TreeViewEventArgs e)
        {

            iexStbGridServers.DataSource = null;

            string content = treeNavigator.SelectedNode.FullPath;
            _presenter.SelectedContent = content;

            if (content.StartsWith("Groups\\"))
            {
                IList<Client.Group> groups = _presenter.GetGroups();
                string group_name = content.Split('\\')[1];
                IEnumerable<Client.Group> selected_groups = groups.Where(item => item.Name == group_name);
                _presenter.SelectedGroups = selected_groups.ToList();
            }
            else
                _presenter.SelectedGroups.Clear();

            iexStbGridServers.DataSource = myServers.ListItems.ToArray();
        }

        public string RepositoryString { get; set; }

        public delegate void UpdateGroupListDelegate(IEX.Lab.Client.GroupList groups);
        IEX.Lab.Client.GroupList IShell.Groups
        {
            set
            {
                if (InvokeRequired)
                    BeginInvoke(new UpdateGroupListDelegate(Update), new object[1] { value });
                else
                    Update(value);
            }
        }

        private void Update(IEX.Lab.Client.GroupList groups)
        {
            treeNavigator.SetGroups(groups);
        }

        BindingListView<HostViewModel> IShell.Hosts
        {
            set 
            {
                IEX.Utilities.Controls.Tools.SetControlPropertyValue(iexComputersGrid1, "DataSource", value);
            }
        }

        BindingListView<ServerViewModel> myServers;
        
        BindingListView<ServerViewModel> IShell.Servers
        {
            set                 
            {
                myServers = value;
                IEX.Utilities.Controls.Tools.SetControlPropertyValue(iexFlowLayoutPanel1, "DataSource", value);
                //IEX.Utilities.Controls.Tools.SetControlPropertyValue(iexStbGridServers, "DataSource", value);
            }
        }

        IList<IEX.Lab.Client.Server> IShell.SelectedServers
        {
            set
            {
                // Set Video
                Predicate<Control> predicate = control =>
                {
                    IexStb iex_stb = control as IexStb;
                    return value.Any(server => server.HostId == iex_stb.Computer && server.ServerId == iex_stb.ServerId);
                };
                iexFlowLayoutPanel1.SelectedControls(item => predicate(item));

                // Set Grid
                foreach (DataGridViewRow row in iexStbGridServers.Rows)
                {
                    string computer = row.Cells[iexStbGridServers.colServersComputer.Index].Value as string;
                    string iex = row.Cells[iexStbGridServers.colServersIEX.Index].Value as string;
                    bool selected = value.Any(server => server.HostId == computer && server.ServerId == iex);
                    if (row.Selected != selected)
                        row.Selected = selected;
                }
            }
        }

        public delegate void UpdateColumnServersDelegate(IList<int> column_servers);
        IList<int> IShell.ColumnServers
        {
            set
            {
                if (InvokeRequired)
                    BeginInvoke(new UpdateColumnServersDelegate(Update), new object[1] { value });
                else
                    Update(value);
            }
        }

        private void Update(IList<int> column_servers)
        {
            iexComputersGrid1.ServersId = column_servers.ToArray();
        }

        private void iexComputersGrid1_GotFocus(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in iexComputersGrid1.SelectedRows)
                iexComputersGrid1.Rows[row.Index].Selected = false;

            Point point = iexComputersGrid1.PointToClient(Cursor.Position);
            int selected_index = iexComputersGrid1.HitTest(point.X, point.Y).RowIndex;
            if (selected_index > -1 && selected_index < iexComputersGrid1.RowCount)
                iexComputersGrid1.Rows[selected_index].Selected = true;
            SetSelectedGridHosts();
        }


        private void iexComputersGrid1_KeyUp(object sender, KeyEventArgs e)
        {
            SetSelectedGridHosts();
        }

        private void iexComputersGrid1_MouseUp(object sender, MouseEventArgs e)
        {
            SetSelectedGridHosts();
        }

        private void SetSelectedGridHosts()
        {
            IList<Client.Host> servers = _presenter.GetHosts();
            IEX.Lab.Client.HostList selected_hosts = new IEX.Lab.Client.HostList();
            foreach (DataGridViewRow row in iexComputersGrid1.SelectedRows)
            {
                Client.Host host = servers.FirstOrDefault(item => item.HostId == row.Cells[iexComputersGrid1.colComputersName.Index].Value as string);
                if (host != null)
                    selected_hosts.Add(host);
            }
            _presenter.SelectedHosts = selected_hosts;
        }

        private void iexStbGridServers_GotFocus(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in iexStbGridServers.SelectedRows)
                iexStbGridServers.Rows[row.Index].Selected = false;

            Point point = iexStbGridServers.PointToClient(Cursor.Position);
            int selected_index = iexStbGridServers.HitTest(point.X, point.Y).RowIndex;
            if (selected_index > -1 && selected_index < iexStbGridServers.RowCount)
                iexStbGridServers.Rows[selected_index].Selected = true;
            SetSelectedGridServers();
        }

        private void iexStbGridServers_KeyUp(object sender, KeyEventArgs e)
        {
            SetSelectedGridServers();
        }

        private void iexStbGridServers_MouseUp(object sender, MouseEventArgs e)
        {
            SetSelectedGridServers();
        }

        private void SetSelectedGridServers()
        {
            IList<Client.Server> servers = _presenter.GetServers();
            IEX.Lab.Client.ServerList selected_servers = new IEX.Lab.Client.ServerList();
            foreach (DataGridViewRow row in iexStbGridServers.SelectedRows)
            {
                Client.Server server = servers.FirstOrDefault(item => item.HostId == row.Cells[iexStbGridServers.colServersComputer.Index].Value as string && item.ServerId == row.Cells[iexStbGridServers.colServersIEX.Index].Value as string);
                if (server != null)
                    selected_servers.Add(server);
            }
            SetSelectedServers(selected_servers);
        }

        private void iexFlowLayoutPanel1_SelectedIndexChanged(object sender, EventArgs e)
        {
            IList<Client.Server> servers = _presenter.GetServers();
            IEX.Lab.Client.ServerList selected_servers = new IEX.Lab.Client.ServerList();
            foreach (Control control in iexFlowLayoutPanel1.SelectedControls())
            {
                IexStb iex_stb = control as IexStb;
                Client.Server server = servers.FirstOrDefault(item => item.HostId == iex_stb.Computer as string && item.ServerId == iex_stb.ServerId as string);
                if (server != null)
                    selected_servers.Add(server);
            }
            SetSelectedServers(selected_servers);
        }

        private void SetSelectedServers(IEX.Lab.Client.ServerList selected_servers)
        {
            bool equal = _presenter.SelectedServers.OrderBy(presenter_server => presenter_server.HostId).ThenBy(presenter_server => presenter_server.ServerId).SequenceEqual(selected_servers.OrderBy(selected_server => selected_server.HostId).ThenBy(selected_server => selected_server.ServerId), new IEX.Utilities.Collections.LambdaEqualityComparer<Client.Server>((x, y) => x.HostId == y.HostId && x.ServerId == y.ServerId));
            if (!equal)
                _presenter.SelectedServers = selected_servers;
        }

        private void treeNavigator_DragOver(object sender, DragEventArgs e)
        {
            IEX.Utilities.Controls.Tools.DragOverEffect(e, typeof(IEX.Lab.Client.ServerList), DragDropEffects.Copy); 
        }

        private void treeNavigator_DragDrop(object sender, DragEventArgs e)
        {
            Point client_point = treeNavigator.PointToClient(new Point(e.X, e.Y));
            TreeNode node = treeNavigator.GetNodeAt(client_point);
            if (node != null && node.Parent != null && node.Parent.Text == "Groups")
            {
                object obj = IEX.Utilities.Controls.Tools.GetObjectOfDataObject((DataObject)e.Data, typeof(IEX.Lab.Client.ServerList));
                if (obj != null)
                {
                    IEX.Lab.Client.ServerList servers = obj as IEX.Lab.Client.ServerList;
                    Client.Group group = _presenter.GetGroups().FirstOrDefault(item => item.Name == node.Text);
                    if (group != null)
                        servers.AddRange(group.Servers);
                    _presenter.SetGroup(node.Text, servers);
                    treeNavigator.SelectedNode = node;
                }
            }
        }

        private void treeNavigator_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void set_info_tree()
        {
            IEX.Utilities.Controls.TreeNodeDataTemplate tn_service = new IEX.Utilities.Controls.TreeNodeDataTemplate() { Type = typeof(IEX.Server.Monitor.Client.ServiceViewModel), Key = "Binding Name", Text = "Binding Display", ItemsSource = "Binding Implementations" };
            IEX.Utilities.Controls.TreeNodeDataTemplate tn_implementation = new IEX.Utilities.Controls.TreeNodeDataTemplate() { Type = typeof(IEX.Server.Monitor.Client.ImplementationViewModel), Key = "Binding Name", Text = "Binding Display", ItemsSource = "Binding Instances" };
            IEX.Server.Monitor.Client.TreeNodeInstanceTemplate tn_instance = new IEX.Server.Monitor.Client.TreeNodeInstanceTemplate();

            List<IEX.Utilities.Controls.TreeNodeDataTemplate> data_templates = new List<IEX.Utilities.Controls.TreeNodeDataTemplate>();
            data_templates.Add(tn_service);
            data_templates.Add(tn_implementation);
            data_templates.Add(tn_instance);

            treeStatus.DataTemplates = data_templates;
        } 
    }
    public class MyButton : System.Windows.Forms.Button
    {
        public override void NotifyDefault(bool value)
        {
        }
    }
}

namespace IEX.Lab.App.Views
{
    partial class frmLab
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            //if (System.IO.File.Exists("IEX.LAB.xml"))
            //    this.dockManagerMain.SaveLayoutToXml("IEX.LAB.xml");
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLab));
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mniFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mniFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mniFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mniFileExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mniFileRecent = new System.Windows.Forms.ToolStripMenuItem();
            this.mniHosts = new System.Windows.Forms.ToolStripMenuItem();
            this.mniHostsAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mniHostsEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mniHostsRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.mniGroups = new System.Windows.Forms.ToolStripMenuItem();
            this.mniGroupsAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mniGroupsRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.mniGroupsRemoveSelectedServers = new System.Windows.Forms.ToolStripMenuItem();
            this.mniGroupsRenameGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.dockManagerMain = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.pnlFill = new DevExpress.XtraBars.Docking.DockPanel();
            this.pnlServersBox = new DevExpress.XtraBars.Docking.DockPanel();
            this.pnlServersBoxContainer = new DevExpress.XtraBars.Docking.ControlContainer();
            this.pnlServersGid = new DevExpress.XtraBars.Docking.DockPanel();
            this.pnlServersGidContainer = new DevExpress.XtraBars.Docking.ControlContainer();
            this.pnlComputers = new DevExpress.XtraBars.Docking.DockPanel();
            this.pnlComputersContainer = new DevExpress.XtraBars.Docking.ControlContainer();
            this.pnlGroups = new DevExpress.XtraBars.Docking.DockPanel();
            this.pnlGroupsContainer = new DevExpress.XtraBars.Docking.ControlContainer();
            this.pnlLeft = new DevExpress.XtraBars.Docking.DockPanel();
            this.pnlLeftContainer = new DevExpress.XtraBars.Docking.ControlContainer();
            this.treeNavigator = new IEX.Lab.App.NativeTreeView();
            this.pnlToolBox = new DevExpress.XtraBars.Docking.DockPanel();
            this.pnlToolBoxContainer = new DevExpress.XtraBars.Docking.ControlContainer();
            this.treeStatus = new IEX.Utilities.Controls.BTreeView();
            this.btnRestart = new IEX.Lab.App.Views.MyButton();
            this.btnStop = new IEX.Lab.App.Views.MyButton();
            this.btnStart = new IEX.Lab.App.Views.MyButton();
            this.iexComputersGrid1 = new IEX.Lab.App.IexComputersGridView();
            this.iexStbGridServers = new IEX.Lab.App.IexStbGridView();
            this.documentManagerMain = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.tabbedView1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
            this.iexFlowLayoutPanel1 = new IEX.Utilities.Controls.IEXFlowLayoutPanel();
            this.xafBarManager1 = new DevExpress.ExpressApp.Win.Templates.Controls.XafBarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.mainMenuItem1 = new DevExpress.ExpressApp.Win.Templates.MainMenuItem();
            this.chkRemote = new DevExpress.XtraBars.BarCheckItem();
            this.chkVideo = new DevExpress.XtraBars.BarCheckItem();
            this.chkSTBPower = new DevExpress.XtraBars.BarCheckItem();
            this.chkRFSwitch = new DevExpress.XtraBars.BarCheckItem();
            this.chkUSBHost = new DevExpress.XtraBars.BarCheckItem();
            this.chkManualTests = new DevExpress.XtraBars.BarCheckItem();
            this.btnRestoreLayout = new DevExpress.XtraBars.BarButtonItem();
            this.barLinkContainerItem1 = new DevExpress.XtraBars.BarLinkContainerItem();
            this.barStaticItem2 = new DevExpress.XtraBars.BarStaticItem();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.repositoryItemPopupContainerEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnInstall = new IEX.Lab.App.Views.MyButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.mnuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManagerMain)).BeginInit();
            this.pnlFill.SuspendLayout();
            this.pnlServersBox.SuspendLayout();
            this.pnlServersGid.SuspendLayout();
            this.pnlComputers.SuspendLayout();
            this.pnlGroups.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlLeftContainer.SuspendLayout();
            this.pnlToolBox.SuspendLayout();
            this.pnlToolBoxContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iexComputersGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iexStbGridServers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManagerMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xafBarManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPopupContainerEdit1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mnuMain.AutoSize = false;
            this.mnuMain.Dock = System.Windows.Forms.DockStyle.None;
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniFile,
            this.mniHosts,
            this.mniGroups});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(1020, 24);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "menuStrip1";
            // 
            // mniFile
            // 
            this.mniFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniFileNew,
            this.mniFileOpen,
            this.mniFileExport,
            this.mniFileRecent});
            this.mniFile.Name = "mniFile";
            this.mniFile.Size = new System.Drawing.Size(35, 20);
            this.mniFile.Text = "File";
            // 
            // mniFileNew
            // 
            this.mniFileNew.Name = "mniFileNew";
            this.mniFileNew.Size = new System.Drawing.Size(118, 22);
            this.mniFileNew.Text = "New";
            // 
            // mniFileOpen
            // 
            this.mniFileOpen.Name = "mniFileOpen";
            this.mniFileOpen.Size = new System.Drawing.Size(118, 22);
            this.mniFileOpen.Text = "Open...";
            // 
            // mniFileExport
            // 
            this.mniFileExport.Name = "mniFileExport";
            this.mniFileExport.Size = new System.Drawing.Size(118, 22);
            this.mniFileExport.Text = "Export...";
            // 
            // mniFileRecent
            // 
            this.mniFileRecent.Name = "mniFileRecent";
            this.mniFileRecent.Size = new System.Drawing.Size(118, 22);
            this.mniFileRecent.Text = "Recent";
            // 
            // mniHosts
            // 
            this.mniHosts.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniHostsAdd,
            this.mniHostsEdit,
            this.mniHostsRemove});
            this.mniHosts.Name = "mniHosts";
            this.mniHosts.Size = new System.Drawing.Size(46, 20);
            this.mniHosts.Text = "Hosts";
            // 
            // mniHostsAdd
            // 
            this.mniHostsAdd.Name = "mniHostsAdd";
            this.mniHostsAdd.Size = new System.Drawing.Size(113, 22);
            this.mniHostsAdd.Text = "Add...";
            // 
            // mniHostsEdit
            // 
            this.mniHostsEdit.Enabled = false;
            this.mniHostsEdit.Name = "mniHostsEdit";
            this.mniHostsEdit.Size = new System.Drawing.Size(113, 22);
            this.mniHostsEdit.Text = "Edit...";
            // 
            // mniHostsRemove
            // 
            this.mniHostsRemove.Enabled = false;
            this.mniHostsRemove.Name = "mniHostsRemove";
            this.mniHostsRemove.Size = new System.Drawing.Size(113, 22);
            this.mniHostsRemove.Text = "Remove";
            // 
            // mniGroups
            // 
            this.mniGroups.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniGroupsAdd,
            this.mniGroupsRemove,
            this.mniGroupsRemoveSelectedServers,
            this.mniGroupsRenameGroup});
            this.mniGroups.Name = "mniGroups";
            this.mniGroups.Size = new System.Drawing.Size(53, 20);
            this.mniGroups.Text = "Groups";
            // 
            // mniGroupsAdd
            // 
            this.mniGroupsAdd.Name = "mniGroupsAdd";
            this.mniGroupsAdd.Size = new System.Drawing.Size(195, 22);
            this.mniGroupsAdd.Text = "Add...";
            // 
            // mniGroupsRemove
            // 
            this.mniGroupsRemove.Enabled = false;
            this.mniGroupsRemove.Name = "mniGroupsRemove";
            this.mniGroupsRemove.Size = new System.Drawing.Size(195, 22);
            this.mniGroupsRemove.Text = "Remove whole group";
            // 
            // mniGroupsRemoveSelectedServers
            // 
            this.mniGroupsRemoveSelectedServers.Enabled = false;
            this.mniGroupsRemoveSelectedServers.Name = "mniGroupsRemoveSelectedServers";
            this.mniGroupsRemoveSelectedServers.Size = new System.Drawing.Size(195, 22);
            this.mniGroupsRemoveSelectedServers.Text = "Remove selected servers";
            // 
            // mniGroupsRenameGroup
            // 
            this.mniGroupsRenameGroup.Name = "mniGroupsRenameGroup";
            this.mniGroupsRenameGroup.Size = new System.Drawing.Size(195, 22);
            this.mniGroupsRenameGroup.Text = "Rename group";
            // 
            // dockManagerMain
            // 
            this.dockManagerMain.Form = this;
            this.dockManagerMain.HiddenPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.pnlFill});
            this.dockManagerMain.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.pnlLeft,
            this.pnlToolBox});
            this.dockManagerMain.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // pnlFill
            // 
            this.pnlFill.ActiveChild = this.pnlServersBox;
            this.pnlFill.Controls.Add(this.pnlServersBox);
            this.pnlFill.Controls.Add(this.pnlServersGid);
            this.pnlFill.Controls.Add(this.pnlComputers);
            this.pnlFill.Controls.Add(this.pnlGroups);
            this.pnlFill.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.pnlFill.FloatVertical = true;
            this.pnlFill.ID = new System.Guid("3d43c2ce-ca36-4bba-9647-ebadf1f1427c");
            this.pnlFill.Location = new System.Drawing.Point(200, 24);
            this.pnlFill.Name = "pnlFill";
            this.pnlFill.Options.AllowDockBottom = false;
            this.pnlFill.Options.AllowDockRight = false;
            this.pnlFill.Options.AllowDockTop = false;
            this.pnlFill.Options.AllowFloating = false;
            this.pnlFill.Options.FloatOnDblClick = false;
            this.pnlFill.Options.ShowAutoHideButton = false;
            this.pnlFill.Options.ShowCloseButton = false;
            this.pnlFill.Options.ShowMaximizeButton = false;
            this.pnlFill.OriginalSize = new System.Drawing.Size(151, 200);
            this.pnlFill.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.pnlFill.SavedIndex = 2;
            this.pnlFill.Size = new System.Drawing.Size(151, 477);
            this.pnlFill.Tabbed = true;
            this.pnlFill.TabsPosition = DevExpress.XtraBars.Docking.TabsPosition.Top;
            this.pnlFill.Text = "panelContainer1";
            this.pnlFill.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            // 
            // pnlServersBox
            // 
            this.pnlServersBox.Controls.Add(this.pnlServersBoxContainer);
            this.pnlServersBox.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.pnlServersBox.ID = new System.Guid("2cdca61f-6269-4f3d-b08b-7a635a7dbac2");
            this.pnlServersBox.Location = new System.Drawing.Point(3, 50);
            this.pnlServersBox.Name = "pnlServersBox";
            this.pnlServersBox.Options.AllowDockBottom = false;
            this.pnlServersBox.Options.AllowDockRight = false;
            this.pnlServersBox.Options.AllowDockTop = false;
            this.pnlServersBox.Options.AllowFloating = false;
            this.pnlServersBox.Options.FloatOnDblClick = false;
            this.pnlServersBox.Options.ShowAutoHideButton = false;
            this.pnlServersBox.Options.ShowCloseButton = false;
            this.pnlServersBox.Options.ShowMaximizeButton = false;
            this.pnlServersBox.OriginalSize = new System.Drawing.Size(355, 424);
            this.pnlServersBox.Size = new System.Drawing.Size(145, 424);
            this.pnlServersBox.TabsPosition = DevExpress.XtraBars.Docking.TabsPosition.Top;
            this.pnlServersBox.TabText = "Video";
            this.pnlServersBox.Text = "Servers";
            // 
            // pnlServersBoxContainer
            // 
            this.pnlServersBoxContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlServersBoxContainer.Name = "pnlServersBoxContainer";
            this.pnlServersBoxContainer.Size = new System.Drawing.Size(145, 424);
            this.pnlServersBoxContainer.TabIndex = 0;
            // 
            // pnlServersGid
            // 
            this.pnlServersGid.Controls.Add(this.pnlServersGidContainer);
            this.pnlServersGid.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.pnlServersGid.ID = new System.Guid("1497430b-2954-4ece-8e50-42407d9f8234");
            this.pnlServersGid.Location = new System.Drawing.Point(3, 50);
            this.pnlServersGid.Name = "pnlServersGid";
            this.pnlServersGid.Options.AllowDockBottom = false;
            this.pnlServersGid.Options.AllowDockRight = false;
            this.pnlServersGid.Options.AllowDockTop = false;
            this.pnlServersGid.Options.AllowFloating = false;
            this.pnlServersGid.Options.FloatOnDblClick = false;
            this.pnlServersGid.Options.ShowAutoHideButton = false;
            this.pnlServersGid.Options.ShowCloseButton = false;
            this.pnlServersGid.Options.ShowMaximizeButton = false;
            this.pnlServersGid.OriginalSize = new System.Drawing.Size(355, 424);
            this.pnlServersGid.Size = new System.Drawing.Size(145, 424);
            this.pnlServersGid.TabsPosition = DevExpress.XtraBars.Docking.TabsPosition.Top;
            this.pnlServersGid.TabText = "Grid";
            this.pnlServersGid.Text = "Servers";
            // 
            // pnlServersGidContainer
            // 
            this.pnlServersGidContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlServersGidContainer.Name = "pnlServersGidContainer";
            this.pnlServersGidContainer.Size = new System.Drawing.Size(145, 424);
            this.pnlServersGidContainer.TabIndex = 0;
            // 
            // pnlComputers
            // 
            this.pnlComputers.Controls.Add(this.pnlComputersContainer);
            this.pnlComputers.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.pnlComputers.ID = new System.Guid("8083179b-2a74-431a-8e36-ae1baa38e1d2");
            this.pnlComputers.Location = new System.Drawing.Point(3, 50);
            this.pnlComputers.Name = "pnlComputers";
            this.pnlComputers.Options.AllowDockBottom = false;
            this.pnlComputers.Options.AllowDockRight = false;
            this.pnlComputers.Options.AllowDockTop = false;
            this.pnlComputers.Options.AllowFloating = false;
            this.pnlComputers.Options.FloatOnDblClick = false;
            this.pnlComputers.Options.ShowAutoHideButton = false;
            this.pnlComputers.Options.ShowCloseButton = false;
            this.pnlComputers.Options.ShowMaximizeButton = false;
            this.pnlComputers.OriginalSize = new System.Drawing.Size(355, 424);
            this.pnlComputers.Size = new System.Drawing.Size(145, 424);
            this.pnlComputers.TabsPosition = DevExpress.XtraBars.Docking.TabsPosition.Top;
            this.pnlComputers.TabText = "Computers";
            this.pnlComputers.Text = "Computers";
            // 
            // pnlComputersContainer
            // 
            this.pnlComputersContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlComputersContainer.Name = "pnlComputersContainer";
            this.pnlComputersContainer.Size = new System.Drawing.Size(145, 424);
            this.pnlComputersContainer.TabIndex = 0;
            // 
            // pnlGroups
            // 
            this.pnlGroups.Controls.Add(this.pnlGroupsContainer);
            this.pnlGroups.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.pnlGroups.ID = new System.Guid("d3cac5b0-ec46-452a-8fc6-34d6be2aadee");
            this.pnlGroups.Location = new System.Drawing.Point(3, 50);
            this.pnlGroups.Name = "pnlGroups";
            this.pnlGroups.Options.AllowDockBottom = false;
            this.pnlGroups.Options.AllowDockRight = false;
            this.pnlGroups.Options.AllowDockTop = false;
            this.pnlGroups.Options.AllowFloating = false;
            this.pnlGroups.Options.FloatOnDblClick = false;
            this.pnlGroups.Options.ShowAutoHideButton = false;
            this.pnlGroups.Options.ShowCloseButton = false;
            this.pnlGroups.Options.ShowMaximizeButton = false;
            this.pnlGroups.OriginalSize = new System.Drawing.Size(355, 424);
            this.pnlGroups.Size = new System.Drawing.Size(145, 424);
            this.pnlGroups.TabsPosition = DevExpress.XtraBars.Docking.TabsPosition.Top;
            this.pnlGroups.TabText = "Groups";
            this.pnlGroups.Text = "Groups";
            // 
            // pnlGroupsContainer
            // 
            this.pnlGroupsContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlGroupsContainer.Name = "pnlGroupsContainer";
            this.pnlGroupsContainer.Size = new System.Drawing.Size(145, 424);
            this.pnlGroupsContainer.TabIndex = 0;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.pnlLeftContainer);
            this.pnlLeft.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.pnlLeft.ID = new System.Guid("22ad71e5-5c74-49a1-abb3-233c027d8e7c");
            this.pnlLeft.Location = new System.Drawing.Point(0, 58);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Options.AllowDockAsTabbedDocument = false;
            this.pnlLeft.Options.AllowDockBottom = false;
            this.pnlLeft.Options.AllowDockFill = false;
            this.pnlLeft.Options.AllowDockRight = false;
            this.pnlLeft.Options.AllowDockTop = false;
            this.pnlLeft.Options.AllowFloating = false;
            this.pnlLeft.Options.FloatOnDblClick = false;
            this.pnlLeft.Options.ShowCloseButton = false;
            this.pnlLeft.Options.ShowMaximizeButton = false;
            this.pnlLeft.OriginalSize = new System.Drawing.Size(200, 200);
            this.pnlLeft.Size = new System.Drawing.Size(200, 443);
            this.pnlLeft.TabsPosition = DevExpress.XtraBars.Docking.TabsPosition.Top;
            this.pnlLeft.TabText = "Navigator";
            this.pnlLeft.Text = "Navigator";
            // 
            // pnlLeftContainer
            // 
            this.pnlLeftContainer.Controls.Add(this.treeNavigator);
            this.pnlLeftContainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeftContainer.Location = new System.Drawing.Point(4, 23);
            this.pnlLeftContainer.Name = "pnlLeftContainer";
            this.pnlLeftContainer.Size = new System.Drawing.Size(192, 416);
            this.pnlLeftContainer.TabIndex = 0;
            // 
            // treeNavigator
            // 
            this.treeNavigator.AllowDrop = true;
            this.treeNavigator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.treeNavigator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeNavigator.HideSelection = false;
            this.treeNavigator.Location = new System.Drawing.Point(0, 0);
            this.treeNavigator.Name = "treeNavigator";
            this.treeNavigator.ShowLines = false;
            this.treeNavigator.Size = new System.Drawing.Size(192, 416);
            this.treeNavigator.TabIndex = 0;
            this.treeNavigator.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeNavigator_AfterSelect);
            this.treeNavigator.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeNavigator_DragDrop);
            this.treeNavigator.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeNavigator_DragEnter);
            this.treeNavigator.DragOver += new System.Windows.Forms.DragEventHandler(this.treeNavigator_DragOver);
            // 
            // pnlToolBox
            // 
            this.pnlToolBox.Controls.Add(this.pnlToolBoxContainer);
            this.pnlToolBox.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.pnlToolBox.ID = new System.Guid("7faef46d-3f79-46b1-8ede-55961218e748");
            this.pnlToolBox.Location = new System.Drawing.Point(820, 58);
            this.pnlToolBox.Name = "pnlToolBox";
            this.pnlToolBox.Options.AllowDockAsTabbedDocument = false;
            this.pnlToolBox.Options.AllowDockBottom = false;
            this.pnlToolBox.Options.AllowDockLeft = false;
            this.pnlToolBox.Options.AllowDockTop = false;
            this.pnlToolBox.Options.AllowFloating = false;
            this.pnlToolBox.Options.FloatOnDblClick = false;
            this.pnlToolBox.Options.ShowCloseButton = false;
            this.pnlToolBox.Options.ShowMaximizeButton = false;
            this.pnlToolBox.OriginalSize = new System.Drawing.Size(200, 200);
            this.pnlToolBox.Size = new System.Drawing.Size(200, 443);
            this.pnlToolBox.TabsPosition = DevExpress.XtraBars.Docking.TabsPosition.Top;
            this.pnlToolBox.TabText = "ToolBox";
            this.pnlToolBox.Text = "ToolBox";
            // 
            // pnlToolBoxContainer
            // 
            this.pnlToolBoxContainer.Controls.Add(this.treeStatus);
            this.pnlToolBoxContainer.Location = new System.Drawing.Point(4, 23);
            this.pnlToolBoxContainer.Name = "pnlToolBoxContainer";
            this.pnlToolBoxContainer.Size = new System.Drawing.Size(192, 416);
            this.pnlToolBoxContainer.TabIndex = 0;
            // 
            // treeStatus
            // 
            this.treeStatus.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeStatus.DataTemplates = null;
            this.treeStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.treeStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.treeStatus.ItemsSource = null;
            this.treeStatus.Location = new System.Drawing.Point(0, 0);
            this.treeStatus.Name = "treeStatus";
            this.treeStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.treeStatus.SelectedNode = null;
            this.treeStatus.Size = new System.Drawing.Size(192, 416);
            this.treeStatus.TabIndex = 7;
            // 
            // btnRestart
            // 
            this.btnRestart.Enabled = false;
            this.btnRestart.FlatAppearance.BorderSize = 0;
            this.btnRestart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestart.Image = global::IEX.Lab.App.Properties.Resources.Restart;
            this.btnRestart.Location = new System.Drawing.Point(87, 4);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(25, 25);
            this.btnRestart.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnRestart, "Restart");
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.FlatAppearance.BorderSize = 0;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Image = global::IEX.Lab.App.Properties.Resources.Stop;
            this.btnStop.Location = new System.Drawing.Point(48, 4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(25, 25);
            this.btnStop.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnStop, "Stop");
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Image = global::IEX.Lab.App.Properties.Resources.Start;
            this.btnStart.Location = new System.Drawing.Point(9, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(25, 25);
            this.btnStart.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnStart, "Start");
            // 
            // iexComputersGrid1
            // 
            this.iexComputersGrid1.AllowUserToAddRows = false;
            this.iexComputersGrid1.AllowUserToDeleteRows = false;
            this.iexComputersGrid1.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Silver;
            this.iexComputersGrid1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.iexComputersGrid1.AutoGenerateColumns = false;
            this.iexComputersGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.iexComputersGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.iexComputersGrid1.ConfigurationImage = global::IEX.Lab.App.Properties.Resources.Wheel_16x16;
            this.iexComputersGrid1.DataSource = null;
            this.iexComputersGrid1.Location = new System.Drawing.Point(360, 63);
            this.iexComputersGrid1.Name = "iexComputersGrid1";
            this.iexComputersGrid1.ReadOnly = true;
            this.iexComputersGrid1.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.iexComputersGrid1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.iexComputersGrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.iexComputersGrid1.ShowEditingIcon = false;
            this.iexComputersGrid1.Size = new System.Drawing.Size(355, 128);
            this.iexComputersGrid1.TabIndex = 0;
            this.iexComputersGrid1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.iexComputersGrid1_KeyUp);
            this.iexComputersGrid1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.iexComputersGrid1_MouseUp);
            // 
            // iexStbGridServers
            // 
            this.iexStbGridServers.AllowMultiRowDrag = true;
            this.iexStbGridServers.AllowUserToAddRows = false;
            this.iexStbGridServers.AllowUserToDeleteRows = false;
            this.iexStbGridServers.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            this.iexStbGridServers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.iexStbGridServers.AutoGenerateColumns = false;
            this.iexStbGridServers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.iexStbGridServers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.iexStbGridServers.ConfigurationImage = global::IEX.Lab.App.Properties.Resources.Wheel_16x16;
            this.iexStbGridServers.DataSource = null;
            this.iexStbGridServers.Location = new System.Drawing.Point(383, 220);
            this.iexStbGridServers.Name = "iexStbGridServers";
            this.iexStbGridServers.ReadOnly = true;
            this.iexStbGridServers.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.iexStbGridServers.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.iexStbGridServers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.iexStbGridServers.ShowEditingIcon = false;
            this.iexStbGridServers.Size = new System.Drawing.Size(292, 141);
            this.iexStbGridServers.TabIndex = 1;
            this.iexStbGridServers.KeyUp += new System.Windows.Forms.KeyEventHandler(this.iexStbGridServers_KeyUp);
            this.iexStbGridServers.MouseUp += new System.Windows.Forms.MouseEventHandler(this.iexStbGridServers_MouseUp);
            // 
            // documentManagerMain
            // 
            this.documentManagerMain.MdiParent = this;
            this.documentManagerMain.View = this.tabbedView1;
            this.documentManagerMain.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.tabbedView1});
            // 
            // iexFlowLayoutPanel1
            // 
            this.iexFlowLayoutPanel1.AutoScroll = true;
            this.iexFlowLayoutPanel1.ControlType = null;
            this.iexFlowLayoutPanel1.Location = new System.Drawing.Point(383, 381);
            this.iexFlowLayoutPanel1.Name = "iexFlowLayoutPanel1";
            this.iexFlowLayoutPanel1.SelectedControl = null;
            this.iexFlowLayoutPanel1.Size = new System.Drawing.Size(292, 100);
            this.iexFlowLayoutPanel1.TabIndex = 4;
            this.iexFlowLayoutPanel1.SelectedIndexChanged += new System.EventHandler(this.iexFlowLayoutPanel1_SelectedIndexChanged);
            // 
            // xafBarManager1
            // 
            this.xafBarManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2,
            this.bar3});
            this.xafBarManager1.DockControls.Add(this.barDockControlTop);
            this.xafBarManager1.DockControls.Add(this.barDockControlBottom);
            this.xafBarManager1.DockControls.Add(this.barDockControlLeft);
            this.xafBarManager1.DockControls.Add(this.barDockControlRight);
            this.xafBarManager1.Form = this;
            this.xafBarManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barStaticItem1,
            this.mainMenuItem1,
            this.chkRemote,
            this.chkVideo,
            this.chkSTBPower,
            this.chkRFSwitch,
            this.chkUSBHost,
            this.barLinkContainerItem1,
            this.btnRestoreLayout,
            this.barStaticItem2,
            this.chkManualTests});
            this.xafBarManager1.MaxItemId = 23;
            this.xafBarManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox1,
            this.repositoryItemComboBox2,
            this.repositoryItemButtonEdit1,
            this.repositoryItemPopupContainerEdit1});
            // 
            // bar2
            // 
            this.bar2.BarName = "Connect";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.OptionsBar.AllowRename = true;
            this.bar2.OptionsBar.DrawBorder = false;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Connect";
            // 
            // bar3
            // 
            this.bar3.BarName = "Session";
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 1;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar3.OptionsBar.AllowRename = true;
            this.bar3.OptionsBar.DrawBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Session";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1020, 58);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 501);
            this.barDockControlBottom.Size = new System.Drawing.Size(1020, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 58);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 443);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1020, 58);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 443);
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "Command Status";
            this.barStaticItem1.Id = 1;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // mainMenuItem1
            // 
            this.mainMenuItem1.Caption = "View";
            this.mainMenuItem1.Id = 2;
            this.mainMenuItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.chkRemote),
            new DevExpress.XtraBars.LinkPersistInfo(this.chkVideo),
            new DevExpress.XtraBars.LinkPersistInfo(this.chkSTBPower),
            new DevExpress.XtraBars.LinkPersistInfo(this.chkRFSwitch),
            new DevExpress.XtraBars.LinkPersistInfo(this.chkUSBHost),
            new DevExpress.XtraBars.LinkPersistInfo(this.chkManualTests),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnRestoreLayout, true)});
            this.mainMenuItem1.Name = "mainMenuItem1";
            // 
            // chkRemote
            // 
            this.chkRemote.Caption = "Remote";
            this.chkRemote.Checked = true;
            this.chkRemote.Id = 3;
            this.chkRemote.Name = "chkRemote";
            // 
            // chkVideo
            // 
            this.chkVideo.Caption = "Video";
            this.chkVideo.Checked = true;
            this.chkVideo.Id = 4;
            this.chkVideo.Name = "chkVideo";
            // 
            // chkSTBPower
            // 
            this.chkSTBPower.Caption = "STB Power";
            this.chkSTBPower.Checked = true;
            this.chkSTBPower.Id = 5;
            this.chkSTBPower.Name = "chkSTBPower";
            // 
            // chkRFSwitch
            // 
            this.chkRFSwitch.Caption = "RF Switch";
            this.chkRFSwitch.Checked = true;
            this.chkRFSwitch.Id = 6;
            this.chkRFSwitch.Name = "chkRFSwitch";
            // 
            // chkUSBHost
            // 
            this.chkUSBHost.Caption = "USB Host Selector";
            this.chkUSBHost.Checked = true;
            this.chkUSBHost.Id = 9;
            this.chkUSBHost.Name = "chkUSBHost";
            // 
            // chkManualTests
            // 
            this.chkManualTests.Caption = "Manual Tests";
            this.chkManualTests.Checked = true;
            this.chkManualTests.Id = 22;
            this.chkManualTests.Name = "chkManualTests";
            // 
            // btnRestoreLayout
            // 
            this.btnRestoreLayout.Caption = "Restore Layout";
            this.btnRestoreLayout.Id = 12;
            this.btnRestoreLayout.Name = "btnRestoreLayout";
            // 
            // barLinkContainerItem1
            // 
            this.barLinkContainerItem1.Caption = "barLinkContainerItem1";
            this.barLinkContainerItem1.Id = 10;
            this.barLinkContainerItem1.Name = "barLinkContainerItem1";
            // 
            // barStaticItem2
            // 
            this.barStaticItem2.Caption = "Server IP:";
            this.barStaticItem2.Id = 13;
            this.barStaticItem2.Name = "barStaticItem2";
            this.barStaticItem2.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // repositoryItemComboBox2
            // 
            this.repositoryItemComboBox2.AutoHeight = false;
            this.repositoryItemComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox2.Name = "repositoryItemComboBox2";
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // repositoryItemPopupContainerEdit1
            // 
            this.repositoryItemPopupContainerEdit1.AutoHeight = false;
            this.repositoryItemPopupContainerEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemPopupContainerEdit1.Name = "repositoryItemPopupContainerEdit1";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btnInstall);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.btnRestart);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Location = new System.Drawing.Point(2, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1014, 33);
            this.panel1.TabIndex = 7;
            // 
            // btnInstall
            // 
            this.btnInstall.Enabled = false;
            this.btnInstall.FlatAppearance.BorderSize = 0;
            this.btnInstall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInstall.Image = global::IEX.Lab.App.Properties.Resources.Install;
            this.btnInstall.Location = new System.Drawing.Point(126, 4);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(25, 25);
            this.btnInstall.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnInstall, "Update Hosts");
            // 
            // frmLab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 501);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.iexFlowLayoutPanel1);
            this.Controls.Add(this.iexStbGridServers);
            this.Controls.Add(this.iexComputersGrid1);
            this.Controls.Add(this.pnlToolBox);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.mnuMain);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mnuMain;
            this.Name = "frmLab";
            this.Text = "Lab";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmLab_Load);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManagerMain)).EndInit();
            this.pnlFill.ResumeLayout(false);
            this.pnlServersBox.ResumeLayout(false);
            this.pnlServersGid.ResumeLayout(false);
            this.pnlComputers.ResumeLayout(false);
            this.pnlGroups.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeftContainer.ResumeLayout(false);
            this.pnlToolBox.ResumeLayout(false);
            this.pnlToolBoxContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iexComputersGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iexStbGridServers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManagerMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xafBarManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPopupContainerEdit1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mniHosts;
        private System.Windows.Forms.ToolStripMenuItem mniGroups;
        private System.Windows.Forms.ToolStripMenuItem mniHostsAdd;
        private System.Windows.Forms.ToolStripMenuItem mniHostsRemove;
        private System.Windows.Forms.ToolStripMenuItem mniGroupsAdd;
        private System.Windows.Forms.ToolStripMenuItem mniGroupsRemove;
        private DevExpress.XtraBars.Docking.DockManager dockManagerMain;
        private DevExpress.XtraBars.Docking.DockPanel pnlComputers;
        private DevExpress.XtraBars.Docking.ControlContainer pnlComputersContainer;
        private DevExpress.XtraBars.Docking.DockPanel pnlLeft;
        private DevExpress.XtraBars.Docking.ControlContainer pnlLeftContainer;
        private NativeTreeView treeNavigator;
        private DevExpress.XtraBars.Docking.DockPanel pnlServersGid;
        private DevExpress.XtraBars.Docking.ControlContainer pnlServersGidContainer;
        private IexStbGridView iexStbGridServers;
        private DevExpress.XtraBars.Docking.DockPanel pnlServersBox;
        private DevExpress.XtraBars.Docking.ControlContainer pnlServersBoxContainer;
        private IexComputersGridView iexComputersGrid1;
        private DevExpress.XtraBars.Docking.DockPanel pnlFill;
        private DevExpress.XtraBars.Docking.DockPanel pnlToolBox;
        private DevExpress.XtraBars.Docking.ControlContainer pnlToolBoxContainer;
        private MyButton btnStart;
        private MyButton btnRestart;
        private MyButton btnStop;
        private DevExpress.XtraBars.Docking.DockPanel pnlGroups;
        private DevExpress.XtraBars.Docking.ControlContainer pnlGroupsContainer;
        private System.Windows.Forms.ToolStripMenuItem mniFile;
        private System.Windows.Forms.ToolStripMenuItem mniFileOpen;
        private System.Windows.Forms.ToolStripMenuItem mniFileExport;
        private System.Windows.Forms.ToolStripMenuItem mniHostsEdit;
        private System.Windows.Forms.ToolStripMenuItem mniGroupsRemoveSelectedServers;
        private System.Windows.Forms.ToolStripMenuItem mniFileRecent;
        private DevExpress.XtraBars.Docking2010.DocumentManager documentManagerMain;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView1;
        private System.Windows.Forms.ToolStripMenuItem mniFileNew;
        private Utilities.Controls.IEXFlowLayoutPanel iexFlowLayoutPanel1;
        private Utilities.Controls.BTreeView treeStatus;
        private System.Windows.Forms.ToolStripMenuItem mniGroupsRenameGroup;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.ExpressApp.Win.Templates.Controls.XafBarManager xafBarManager1;
        private DevExpress.ExpressApp.Win.Templates.MainMenuItem mainMenuItem1;
        private DevExpress.XtraBars.BarCheckItem chkRemote;
        private DevExpress.XtraBars.BarCheckItem chkVideo;
        private DevExpress.XtraBars.BarCheckItem chkSTBPower;
        private DevExpress.XtraBars.BarCheckItem chkRFSwitch;
        private DevExpress.XtraBars.BarCheckItem chkUSBHost;
        private DevExpress.XtraBars.BarCheckItem chkManualTests;
        private DevExpress.XtraBars.BarButtonItem btnRestoreLayout;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarLinkContainerItem barLinkContainerItem1;
        private DevExpress.XtraBars.BarStaticItem barStaticItem2;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox2;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit repositoryItemPopupContainerEdit1;
        private System.Windows.Forms.Panel panel1;
        private MyButton btnInstall;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
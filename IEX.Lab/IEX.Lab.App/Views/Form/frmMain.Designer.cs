namespace IEX.Lab.App.Views
{
    partial class frmMain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dockManagerMain = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.pnlLeft = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.pnlRight = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.documentManagerMain = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.tabbedView1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
            this.iexFlowLayoutPanel1 = new IEX.Utilities.Controls.IEXFlowLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.iexStbGridView1 = new IEX.Lab.App.IexStbGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dockManagerMain)).BeginInit();
            this.pnlLeft.SuspendLayout();
            this.pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.documentManagerMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iexStbGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dockManagerMain
            // 
            this.dockManagerMain.Form = this;
            this.dockManagerMain.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.pnlLeft,
            this.pnlRight});
            this.dockManagerMain.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.dockPanel1_Container);
            this.pnlLeft.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.pnlLeft.ID = new System.Guid("26987c35-8331-4a18-9fe0-20d488d91c0e");
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.OriginalSize = new System.Drawing.Size(200, 200);
            this.pnlLeft.Size = new System.Drawing.Size(200, 445);
            this.pnlLeft.Text = "Left";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(192, 418);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.dockPanel2_Container);
            this.pnlRight.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.pnlRight.ID = new System.Guid("c153b043-0a81-4f9a-be3a-8de4a3e19b00");
            this.pnlRight.Location = new System.Drawing.Point(719, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.OriginalSize = new System.Drawing.Size(200, 200);
            this.pnlRight.Size = new System.Drawing.Size(200, 445);
            this.pnlRight.Text = "Right";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(192, 418);
            this.dockPanel2_Container.TabIndex = 0;
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
            this.iexFlowLayoutPanel1.Location = new System.Drawing.Point(311, 83);
            this.iexFlowLayoutPanel1.Name = "iexFlowLayoutPanel1";
            this.iexFlowLayoutPanel1.SelectedControl = null;
            this.iexFlowLayoutPanel1.Size = new System.Drawing.Size(292, 100);
            this.iexFlowLayoutPanel1.TabIndex = 5;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(339, 236);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(240, 150);
            this.dataGridView1.TabIndex = 6;
            // 
            // iexStbGridView1
            // 
            this.iexStbGridView1.AllowMultiRowDrag = true;
            this.iexStbGridView1.AllowUserToAddRows = false;
            this.iexStbGridView1.AllowUserToDeleteRows = false;
            this.iexStbGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            this.iexStbGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.iexStbGridView1.AutoGenerateColumns = false;
            this.iexStbGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.iexStbGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.iexStbGridView1.Location = new System.Drawing.Point(611, 236);
            this.iexStbGridView1.Name = "iexStbGridView1";
            this.iexStbGridView1.ReadOnly = true;
            this.iexStbGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.iexStbGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.iexStbGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.iexStbGridView1.ShowEditingIcon = false;
            this.iexStbGridView1.Size = new System.Drawing.Size(240, 150);
            this.iexStbGridView1.TabIndex = 9;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 445);
            this.Controls.Add(this.iexStbGridView1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.iexFlowLayoutPanel1);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.IsMdiContainer = true;
            this.Name = "frmMain";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dockManagerMain)).EndInit();
            this.pnlLeft.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.documentManagerMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iexStbGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManagerMain;
        private DevExpress.XtraBars.Docking2010.DocumentManager documentManagerMain;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView1;
        private DevExpress.XtraBars.Docking.DockPanel pnlRight;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private DevExpress.XtraBars.Docking.DockPanel pnlLeft;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private System.Windows.Forms.DataGridView dataGridView1;
        private IEX.Utilities.Controls.IEXFlowLayoutPanel iexFlowLayoutPanel1;
        private IexStbGridView iexStbGridView1;
    }
}


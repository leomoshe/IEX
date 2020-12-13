using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.App
{
    using IEX.Utilities;
    using System.Windows.Forms;
    using System.ComponentModel;
    [Designer(typeof(System.Windows.Forms.Design.ControlDesigner))]
    public class IexComputersGridView: DataGridView
    {
        public IexComputersGridView()
        {
            if (!this.DesignMode)
                Init();
        }

        private ListChangedEventHandler _list_changed_handler;
        public System.Windows.Forms.DataGridViewImageColumn colComputersConfiguration;
        public System.Windows.Forms.DataGridViewTextBoxColumn colComputersName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colComputersStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colComputersCPUUsage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colComputersMemory;
        public System.Windows.Forms.DataGridViewTextBoxColumn colComputersMonitorVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colComputersDummy;
        private void Init()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();

            this.colComputersConfiguration = new System.Windows.Forms.DataGridViewImageColumn();
            this.colComputersName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colComputersStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colComputersCPUUsage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colComputersMemory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colComputersMonitorVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colComputersDummy = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.colComputersConfiguration.HeaderText = string.Empty;
            this.colComputersConfiguration.Name = "colComputersConfiguration";
            this.colComputersConfiguration.DataPropertyName = "Configuration";
            this.colComputersConfiguration.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            this.colComputersName.HeaderText = "Host";
            this.colComputersName.Name = "colComputersName";
            this.colComputersName.DataPropertyName = "Computer";
            this.colComputersName.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            this.colComputersStatus.HeaderText = "Status";
            this.colComputersStatus.Name = "colComputersStatus";
            this.colComputersStatus.DataPropertyName = "Status";
            this.colComputersStatus.Width = 84;

            this.colComputersCPUUsage.HeaderText = "CPU (%)";
            this.colComputersCPUUsage.Name = "colComputersCPUUsage";
            this.colComputersCPUUsage.DataPropertyName = "CPU"; 
            this.colComputersCPUUsage.Width = 90;

            this.colComputersMemory.HeaderText = "Memory (%)"; 
            this.colComputersMemory.Name = "colComputersMemory";
            this.colComputersMemory.DataPropertyName = "Memory";
            this.colComputersMemory.Width = 90;

            this.colComputersMonitorVersion.HeaderText = "Version";
            this.colComputersMonitorVersion.Name = "colComputersMonitorVersion";
            this.colComputersMonitorVersion.DataPropertyName = "MonitorVersion";
            this.colComputersMonitorVersion.Width = 90;

            this.colComputersDummy.HeaderText = string.Empty;
            this.colComputersDummy.Name = "colComputersDummy";
            this.colComputersDummy.DataPropertyName = "Dummy";
            this.colComputersDummy.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colComputersConfiguration, this.colComputersName, 
                this.colComputersStatus, this.colComputersCPUUsage, this.colComputersMemory, this.colComputersMonitorVersion,
                this.colComputersDummy });

            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToResizeRows = false;

            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            //AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            BorderStyle = System.Windows.Forms.BorderStyle.None;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            RowsDefaultCellStyle = dataGridViewCellStyle2;
            ShowEditingIcon = false;

            RowHeadersVisible = false;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            MultiSelect = true;
            AutoGenerateColumns = false;

            ReadOnly = true;
            ShowCellErrors = true;

            _list_changed_handler = new ListChangedEventHandler(DataManager_ListChanged);
        }

        [Browsable(true)]
        public System.Drawing.Image ConfigurationImage { get; set; }

        private int[] _servers_id;
        [Browsable(true)]
        public int[] ServersId
        {
            get { return _servers_id; }
            set
            {
                _servers_id = value;
                for (int i = Columns.Count-1; i > -1; i--)
                {
                    string name = Columns[i].Name;
                    if (name.StartsWith("colComputersIEX_"))
                        Columns.RemoveAt(i);
                }
                foreach (int id in _servers_id)
                {
                    System.Windows.Forms.DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                    column.HeaderText = id.ToString();
                    column.Name = "colComputersIEX_" + id;
                    column.DataPropertyName = "IEX_" + id;
                    column.Width = 94;
                    Columns.Insert(Columns.Count - 1, column);
                }
            }
        }
        //
        // Summary:
        //     Gets a collection that contains all the columns in the control.
        //
        // Returns:
        //     The System.Windows.Forms.DataGridViewColumnCollection that contains all the
        //     columns in the System.Windows.Forms.DataGridView control.
        //[Editor("System.Windows.Forms.Design.DataGridViewColumnCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public new DataGridViewColumnCollection Columns
        {
            get { return base.Columns; }
        }

        protected CurrencyManager DataManager { get; private set; }
        protected override void OnBindingContextChanged(EventArgs e)
        {
            this.UpdateDataBinding();
            base.OnBindingContextChanged(e);
        }

        //
        // Summary:
        //     Gets or sets the data source that the System.Windows.Forms.DataGridView is
        //     displaying data for.
        //
        // Returns:
        //     The object that contains data for the System.Windows.Forms.DataGridView to
        //     display.
        //
        // Exceptions:
        //   System.Exception:
        //     An error occurred in the data source and either there is no handler for the
        //     System.Windows.Forms.DataGridView.DataError event or the handler has set
        //     the System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException property
        //     to true. The exception object can typically be cast to type System.FormatException.
        [AttributeProvider(typeof(IListSource))]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DefaultValue("")]
        public new object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                base.DataSource = value;
                UpdateDataBinding();
            }
        }

        private void UpdateDataBinding()
        {
            if (this.DataSource == null || base.BindingContext == null)
                return;

            CurrencyManager concurrency_manager;
            try
            {
                concurrency_manager = (CurrencyManager)base.BindingContext[this.DataSource, this.DataMember];
            }
            catch (System.ArgumentException)
            {
                // If no CurrencyManager was found
                Tracer.Write(Tracer.TraceLevel.DEBUG, "Currency manage not found");
                return;
            }

            if (this.DataManager != concurrency_manager)
            {
                // Unwire the old CurrencyManager
                if (this.DataManager != null)
                    this.DataManager.ListChanged -= _list_changed_handler;
                this.DataManager = concurrency_manager;
                // Wire the new CurrencyManager
                if (this.DataManager != null)
                    this.DataManager.ListChanged += _list_changed_handler;
            }
        }

        private void DataManager_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.Reset)
            {
                Tracer.Write(Tracer.TraceLevel.DEBUG, string.Format("ListChanged (Reset)"));
                ClearSelection();
            }
            if (e.ListChangedType == ListChangedType.ItemMoved)
            {
                Tracer.Write(Tracer.TraceLevel.DEBUG, string.Format("ListChanged (Moved)"));
                ClearSelection();
            }
#if IEX_DEBUG
            else if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemChanged)
            {
                //Tracer.Write(Tracer.TraceLevel.DEBUG, string.Format("ListChanged ({0}): '{1}'", e.ListChangedType, DataManager.List[e.NewIndex]));
            }
            else if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
            }
#endif
        }

        protected override void OnCellFormatting(DataGridViewCellFormattingEventArgs e)
        {
            base.OnCellFormatting(e);
            if (e.ColumnIndex == colComputersConfiguration.Index)
                e.Value = ConfigurationImage;
            else if (e.ColumnIndex == colComputersCPUUsage.Index || e.ColumnIndex == colComputersMemory.Index)
            {
                string value = (string)e.Value;
                if (value == IEX.Utilities.Consts.Base.ERROR_PERFORMANCE_COUNTER)
                {
                    this.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = value;
                    e.Value = string.Empty;
                }
                else
                    this.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = string.Empty;
            }
            else if (
                e.ColumnIndex > colComputersMonitorVersion.Index &&
                e.ColumnIndex < colComputersDummy.Index)
            {
                IEX.Lab.Client.ServerDataState status_value = IEX.Utilities.Types.EnumExtensions.GetValueFromDescription<IEX.Lab.Client.ServerDataState>((string)e.Value);
                System.Drawing.Color color = IEX.Lab.Client.ServerStateExtensions.ToColor(status_value);
                e.CellStyle.BackColor = color;
                e.CellStyle.SelectionBackColor = color;
            }
        }

        public void PerformOnMouseUp(MouseEventArgs e)
        {
            this.OnMouseUp(e);
        }
    }
}
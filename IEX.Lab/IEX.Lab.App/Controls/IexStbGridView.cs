using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.App
{
    using System.Windows.Forms;
    using System.ComponentModel;
    using System.Drawing;
    [Designer(typeof(System.Windows.Forms.Design.ControlDesigner))]
    public class IexStbGridView: DataGridView
    {
        public IexStbGridView()
        {
            if (!this.DesignMode)
                Init();
        }

        private ListChangedEventHandler _list_changed_handler;
        public System.Windows.Forms.DataGridViewImageColumn colServersConfiguration;
        public System.Windows.Forms.DataGridViewTextBoxColumn colServersComputer;
        public System.Windows.Forms.DataGridViewTextBoxColumn colServersIEX;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServersStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServersCPU;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServersMemory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServersTest;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServersStep;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServersDummy;
        private void Init()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();

            this.colServersConfiguration = new System.Windows.Forms.DataGridViewImageColumn();
            this.colServersComputer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServersIEX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServersStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServersCPU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServersMemory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServersTest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServersStep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServersDummy = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.colServersConfiguration.HeaderText = string.Empty;
            this.colServersConfiguration.Name = "colServersConfiguration";
            this.colServersConfiguration.DataPropertyName = "Configuration";
            this.colServersConfiguration.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            this.colServersComputer.HeaderText = "Computer";
            this.colServersComputer.Name = "colServersComputer";
            this.colServersComputer.DataPropertyName = "Computer";
            this.colServersComputer.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            // 
            // colServersIEX
            // 
            this.colServersIEX.HeaderText = "IEX";
            this.colServersIEX.Name = "colServersIEX";
            this.colServersIEX.DataPropertyName = "ServerId";
            this.colServersIEX.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            // 
            // colServersStatus
            // 
            this.colServersStatus.HeaderText = "Status";
            this.colServersStatus.Name = "colServersStatus";
            this.colServersStatus.DataPropertyName = "StatusExtended";
            this.colServersStatus.Width = 94;
            // 
            // colServersCPU
            // 
            this.colServersCPU.HeaderText = "CPU (%)";
            this.colServersCPU.Name = "colServersCPU";
            this.colServersCPU.DataPropertyName = "CPU";
            this.colServersCPU.Width = 90;
            // 
            // colServersMemory
            // 
            this.colServersMemory.HeaderText = "Memory (M)";
            this.colServersMemory.Name = "colServersMemory";
            this.colServersMemory.DataPropertyName = "Memory";
            this.colServersMemory.Width = 90;
            // 
            // colServersTest
            // 
            this.colServersTest.HeaderText = "Test";
            this.colServersTest.Name = "colServersTest";
            this.colServersTest.DataPropertyName = "Test";
            this.colServersTest.Width = 53;
            // 
            // colServersStep
            // 
            this.colServersStep.HeaderText = "Step";
            this.colServersStep.Name = "colServersStep";
            this.colServersStep.DataPropertyName = "Step";
            this.colServersStep.Width = 54;
            // 
            // colServersDummy
            // 
            this.colServersDummy.HeaderText = string.Empty;
            this.colServersDummy.Name = "colServersDummy";
            this.colServersDummy.DataPropertyName = "Dummy";
            this.colServersDummy.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToResizeRows = false;

            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            //AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            BorderStyle = System.Windows.Forms.BorderStyle.None;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colServersConfiguration, this.colServersComputer, this.colServersIEX, this.colServersStatus, this.colServersCPU, this.colServersMemory, this.colServersTest, this.colServersStep, this.colServersDummy });

            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            RowsDefaultCellStyle = dataGridViewCellStyle2;
            ShowEditingIcon = false;

            RowHeadersVisible = false;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            MultiSelect = true;
            AutoGenerateColumns = false;

            AllowMultiRowDrag = true;
            ReadOnly = true;
            ShowCellErrors = true;

            _list_changed_handler = new ListChangedEventHandler(DataManager_ListChanged);
        }

        [Browsable(true)]
        public System.Drawing.Image ConfigurationImage { get; set; }

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
                IEX.Utilities.Tracer.Write(IEX.Utilities.Tracer.TraceLevel.DEBUG, "Currency manage not found");
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
                ClearSelection();
        }

        protected override void OnCellFormatting(DataGridViewCellFormattingEventArgs e)
        {
            base.OnCellFormatting(e);
            if (e.ColumnIndex == colServersConfiguration.Index)
                e.Value = ConfigurationImage;
            else if (e.ColumnIndex == colServersCPU.Index || e.ColumnIndex == colServersMemory.Index)
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
            else if (e.ColumnIndex == colServersStatus.Index)
            {
                //if (this.DataSource is IEX.Lab.App.BindingServerView)
                if (this.DataSource is ServerViewModel[])
                {
                    //object data_row = (this.DataSource as IEX.Lab.App.BindingServerView)[e.RowIndex];
                    object data_row = (this.DataSource as ServerViewModel[])[e.RowIndex];
                    if (data_row is ServerViewModel)
                    {
                        ServerViewModel server_view_model = data_row as ServerViewModel;
                        if (server_view_model.ServerInfo != null)
                        {
                            Server.Monitor.Client.MonitoringServiceReference.ServicesStatus services_status = server_view_model.ServerInfo.ServicesStatus;
                            this.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = services_status == Server.Monitor.Client.MonitoringServiceReference.ServicesStatus.OK ? string.Empty : "Error";
                        }

                        //IEX.Lab.Client.ServerDataState status_value = IEX.Utilities.Types.EnumExtensions.GetValueFromDescription<IEX.Lab.Client.ServerDataState>((string)e.Value);
                        IEX.Lab.Client.ServerDataState status_value = IEX.Utilities.Types.EnumExtensions.GetValueFromDescription<IEX.Lab.Client.ServerDataState>(server_view_model.Status);
                        System.Drawing.Color color = IEX.Lab.Client.ServerStateExtensions.ToColor(status_value);
                        e.CellStyle.BackColor = color;
                        e.CellStyle.SelectionBackColor = color;
                        e.CellStyle.SelectionForeColor = IEX.Lab.Client.ServerStateExtensions.ToForeColor(status_value);
                    }
                }
            }
        }

        #region Sort
        DataGridViewColumn _lastCol = null;
        protected override void OnColumnHeaderMouseClick(DataGridViewCellMouseEventArgs e)
        {
            base.OnColumnHeaderMouseClick(e);

            SortingGrid.SortGrid<ServerViewModel>(ref _lastCol, (ServerViewModel[])DataSource, this, e);
        }
        #endregion

        #region Drag & Drop Feature

        public bool AllowMultiRowDrag { get; set; }
        private int _clickedRow = -1;
        private Rectangle _dragBounds = Rectangle.Empty;
        private MouseEventArgs _mouseDownArgs;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (AllowMultiRowDrag)
            {
                var hitTest = this.HitTest(e.X, e.Y);
                int row = hitTest.RowIndex;
                int col = hitTest.ColumnIndex;
                if (col > 0 && row >= 0 && row < this.Rows.Count && (this.SelectedRows.Contains(this.Rows[row])))
                {
                    _clickedRow = row;
                    Size dragSize = SystemInformation.DragSize;
                    Point p = new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2));
                    _dragBounds = new Rectangle(p, dragSize);
                    _mouseDownArgs = e;
                    return;
                }
                else
                {
                    _dragBounds = Rectangle.Empty;
                }
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (AllowMultiRowDrag)
            {
                int row = this.HitTest(e.X, e.Y).RowIndex;

                if (row < 0)
                {
                    _dragBounds = Rectangle.Empty;
                    return;
                }

                if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                {
                    if (_dragBounds != Rectangle.Empty && !_dragBounds.Contains(e.X, e.Y))
                    {
                        ItemDragEventArgs dragArgs = new ItemDragEventArgs(MouseButtons.Left, this.SelectedRows);
                        OnRowDrag(dragArgs);
                    }
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_dragBounds.Contains(e.X, e.Y))
            {
                if (_clickedRow >= 0 && _clickedRow < this.Rows.Count)
                {
                    base.OnMouseDown(_mouseDownArgs);
                    _mouseDownArgs = null;
                }
            }

            base.OnMouseUp(e);
        }

        protected virtual void OnRowDrag(ItemDragEventArgs e)
        {
            IEX.Lab.Client.ServerList selected_servers = new IEX.Lab.Client.ServerList();
            foreach (DataGridViewRow selected_row in SelectedRows)
            {
                Client.Server server = new Client.Server(selected_row.Cells[colServersComputer.Index].Value as string, selected_row.Cells[colServersIEX.Index].Value as string);
                selected_servers.Add(server);
            }
            DoDragDrop(selected_servers, DragDropEffects.Copy);
        }

        #endregion
    }
}

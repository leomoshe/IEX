using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IEX.Lab.App
{
    using IEX.Utilities.Types;
    using IEX.Lab.Client;
    [Serializable]
    public partial class IexStb : UserControl, System.Runtime.Serialization.ISerializable//, IDisposable
    {
        //public event EventHandler Close;

        private System.Windows.Forms.DataGridViewTextBoxColumn colServersComputer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServersIEX;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServersStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServersCPU;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServersMemory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServersTest;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServersStep;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServersDummy;

        public IexStb()
        {
            InitializeComponent();
            
            this.colServersComputer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServersIEX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServersStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServersCPU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServersMemory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServersTest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServersStep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServersDummy = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.colServersComputer.HeaderText = "Computer";
            this.colServersComputer.Name = "colServersComputer";
            this.colServersComputer.DataPropertyName = "Computer";
            this.colServersComputer.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colServersComputer.ReadOnly = true;
            // 
            // colServersIEX
            // 
            this.colServersIEX.HeaderText = "IEX";
            this.colServersIEX.Name = "colServersIEX";
            this.colServersIEX.DataPropertyName = "ServerId";
            this.colServersIEX.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colServersIEX.ReadOnly = true;
            // 
            // colServersStatus
            // 
            this.colServersStatus.HeaderText = "Status";
            this.colServersStatus.Name = "colServersStatus";
            this.colServersStatus.DataPropertyName = "Status";
            this.colServersStatus.Width = 94;
            this.colServersStatus.ReadOnly = true;
            // 
            // colServersCPU
            // 
            this.colServersCPU.HeaderText = "CPU";
            this.colServersCPU.Name = "colServersCPU";
            this.colServersCPU.DataPropertyName = "CPU";
            this.colServersCPU.Width = 43;
            this.colServersCPU.ReadOnly = true;
            this.colServersCPU.Visible = false;
            // 
            // colServersMemory
            // 
            this.colServersMemory.HeaderText = "Memory";
            this.colServersMemory.Name = "colServersMemory";
            this.colServersMemory.DataPropertyName = "Memory";
            this.colServersMemory.Width = 43;
            this.colServersMemory.Visible = false;
            // 
            // colServersTest
            // 
            this.colServersTest.HeaderText = "Test";
            this.colServersTest.Name = "colServersTest";
            this.colServersTest.DataPropertyName = "Test";
            this.colServersTest.Width = 53;
            this.colServersTest.Visible = false;
            // 
            // colServersStep
            // 
            this.colServersStep.HeaderText = "Step";
            this.colServersStep.Name = "colServersStep";
            this.colServersStep.DataPropertyName = "Step";
            this.colServersStep.Width = 54;
            this.colServersStep.Visible = false;
            // 
            // colServersDummy
            // 
            this.colServersDummy.HeaderText = string.Empty;
            this.colServersDummy.Name = "colServersDummy";
            this.colServersDummy.DataPropertyName = "Dummy";
            this.colServersDummy.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colServersComputer, this.colServersIEX, this.colServersStatus, this.colServersCPU, this.colServersMemory, this.colServersTest, this.colServersStep, this.colServersDummy });
            dataGridView.Rows.Add();

            dataGridView.ShowCellErrors = true;
            dataGridView.RowHeadersVisible = false;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToOrderColumns = false;
            dataGridView.ReadOnly = true;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView.SelectionChanged += delegate(object sender, EventArgs e) { dataGridView.ClearSelection(); };
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.ColumnHeadersVisible = false;
        }
        
        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
        }

        public int Index
        {
            get
            {
                if (this.Parent == null)
                    return -1;
                return this.Parent.Controls.GetChildIndex(this);
            }
        }

        private string _computer;
        public string Computer 
        {
            get { return _computer; }
            private set
            {
                if (_computer != value)
                {
                    _computer = value;
                    DataGridViewRow row = dataGridView.Rows[0];
                    row.Cells[colServersComputer.Index].Value = _computer;
                    if (_playCalled)
                        Play();
                }
            }
        }

        private string _server_id;
        public string ServerId
        {
            get { return _server_id; }
            private set
            {
                if (_server_id != value)
                {
                    _server_id = value;
                    DataGridViewRow row = dataGridView.Rows[0];
                    row.Cells[colServersIEX.Index].Value = _server_id;
                }
            }
        }

        private string _streaming_port;
        public string StreamingPort
        {
            get { return _streaming_port; }
            private set
            {
                if (_streaming_port != value)
                {
                    _streaming_port = value;
                    if (_playCalled)
                        Play();
                }
            }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            private set
            {
                if (_status != value)
                {
                    _status = value;
                    UpdateServerStatus();
                }
            }
        }

        public IEX.Server.Monitor.Client.MonitoringServiceReference.ServerInfo _server_info;
        public IEX.Server.Monitor.Client.MonitoringServiceReference.ServerInfo ServerInfo
        {
            get { return _server_info; }
            private set
            {
                if (_server_info == null || _server_info.ServicesStatus != value.ServicesStatus)
                {
                    _server_info = value;
                    UpdateServerStatus();
                }
            }
        }

        private void UpdateServerStatus()
        {
            DataGridViewRow row = dataGridView.Rows[0];
            IEX.Lab.Client.ServerDataState status_value = EnumExtensions.GetValueFromDescription<IEX.Lab.Client.ServerDataState>(_status);
            if (status_value != Client.ServerDataState.Idle && status_value != Client.ServerDataState.Loading && status_value != Client.ServerDataState.Connected)
                IexVideoStream.Image = null;
            row.Cells[colServersStatus.Index].Style.BackColor = ServerStateExtensions.ToColor(status_value);

            if (ServerInfo != null)
                row.Cells[colServersStatus.Index].ErrorText = ServerInfo.ServicesStatus == IEX.Server.Monitor.Client.MonitoringServiceReference.ServicesStatus.OK ? string.Empty : "Error";
            row.Cells[colServersStatus.Index].Value = Status;
        }

        private string _cpu;
        public string CPU
        {
            get { return _cpu; }
            private set
            {
                if (_cpu != value)
                {
                    _cpu = value;
                    DataGridViewRow row = dataGridView.Rows[0];
                    row.Cells[colServersCPU.Index].Value = _cpu;
                }
            }
        }

        private string _memory;
        public string Memory
        {
            get { return _memory; }
            private set
            {
                if (_memory != value)
                {
                    _memory = value;
                    DataGridViewRow row = dataGridView.Rows[0];
                    row.Cells[colServersMemory.Index].Value = _memory;
                }
            }
        }
        
        public bool IsRunning { get { return IexVideoStream.IsRunning; } }

        bool _playCalled = false;
        public void Play()
        {
            _playCalled = true;
            if (!IexVideoStream.IsRunning && !string.IsNullOrEmpty(Computer) && !string.IsNullOrEmpty(StreamingPort))
            {
                int streaming_port;
                if (Int32.TryParse(StreamingPort, out streaming_port))
                {
                    string uri = string.Format(@"http://{0}:{1}", Computer, streaming_port.ToString());
                    IexVideoStream.Url = uri;
                    IexVideoStream.Play();
                }
            }
        }

        public void Stop()
        {
            _playCalled = false;
            IexVideoStream.Stop();
        }

        private int NId { get; set;}

        //private void btnClose_Click(object sender, EventArgs e)
        //{
        //    IexVideoStream.Stop();
        //    if (Close != null)
        //        Close(this, EventArgs.Empty);
        //}

        private void iexStb_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void iexStb_MouseDown(object sender, MouseEventArgs e)
        {
            this.OnMouseDown(e);
        }

        private void iexStb_KeyDown(object sender, KeyEventArgs e)
        {
            this.OnKeyDown(e);
        }

        //private bool is_disposed = false;
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!is_disposed)
        //    {
        //        if (disposing)
        //        {
        //            // Cleanup managed objects by calling their Dispose() methods.
        //            Stop();
        //        }
        //        // Cleanup unmanaged objects
        //    }
        //    is_disposed = true;
        //}

        //~IexStb()
        //{
        //    Dispose(false);
        //}
    }
}

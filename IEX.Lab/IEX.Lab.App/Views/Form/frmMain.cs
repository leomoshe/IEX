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
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        List<Data> _datas = new List<Data>();
        BindingDataView _datas_view;
        DevExpress.XtraBars.Docking2010.Views.BaseDocument _doc_grid;
        DevExpress.XtraBars.Docking2010.Views.BaseDocument _doc_stb_grid;
        DevExpress.XtraBars.Docking2010.Views.BaseDocument _doc_folw;
        List<IEX.NetworkMediaSource.NetworkSource> _streams = new List<IEX.NetworkMediaSource.NetworkSource>();

        private System.Threading.Timer _iexhot1_timer;
        private IEX.Server.Monitor.Client.ServiceMonitoring _iexhot1_monitoring;
        private System.Threading.Timer _iex_qc_2_timer;
        private IEX.Server.Monitor.Client.ServiceMonitoring _iex_qc_2_monitoring;
        private System.Threading.Timer _iex_qc_1xp_timer;
        private IEX.Server.Monitor.Client.ServiceMonitoring _iex_qc_1xp_monitoring;
        public frmMain()
        {
            InitializeComponent();

            //dataGridView1.AllowUserToAddRows = false;
            //dataGridView1.AllowUserToDeleteRows = false;
            //dataGridView1.AllowUserToResizeRows = false;
            //dataGridView1.AutoGenerateColumns = false;
            //dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //iexFlowLayoutPanel1.ControlType = typeof(StreamViewer);
            iexFlowLayoutPanel1.ControlType = typeof(IexStb);

            _doc_grid = tabbedView1.Controller.AddDocument(this.dataGridView1);
            _doc_grid.Caption = "Grid";
            _doc_grid.Properties.AllowClose = DevExpress.Utils.DefaultBoolean.False;
            _doc_grid.Properties.AllowFloat = DevExpress.Utils.DefaultBoolean.True;

            _doc_stb_grid = tabbedView1.Controller.AddDocument(this.iexStbGridView1);
            _doc_stb_grid.Caption = "Stb Grid";
            _doc_stb_grid.Properties.AllowClose = DevExpress.Utils.DefaultBoolean.False;
            _doc_stb_grid.Properties.AllowFloat = DevExpress.Utils.DefaultBoolean.True;

            _doc_folw = tabbedView1.Controller.AddDocument(iexFlowLayoutPanel1);
            _doc_folw.Caption = "Panel";
            _doc_folw.Properties.AllowClose = DevExpress.Utils.DefaultBoolean.False;
            _doc_folw.Properties.AllowFloat = DevExpress.Utils.DefaultBoolean.True;

            //_datas.Add(new Data("iexhot1", 4));
            //_datas.Add(new Data("iex-qc-2", 1));
            //_datas.Add(new Data("iex-qc-2", 2));
            //_datas.Add(new Data("iex-qc-1xp", 1));
            //_datas.Add(new Data("iex-qc-1xp", 2));

            _iexhot1_monitoring = new IEX.Server.Monitor.Client.ServiceMonitoring("iexhot1");
            foreach (int i in IEX.Utilities.IEXConfiguration.GetServerIds())
                _datas.Add(new Data("iexhot1", "IEX_"+i));
            _iex_qc_2_monitoring = new IEX.Server.Monitor.Client.ServiceMonitoring("iex-qc-2");
            foreach (int i in IEX.Utilities.IEXConfiguration.GetServerIds())
                _datas.Add(new Data("iex-qc-2", "IEX_" + i));
            _iex_qc_1xp_monitoring = new IEX.Server.Monitor.Client.ServiceMonitoring("iex-qc-1xp");
            foreach (int i in IEX.Utilities.IEXConfiguration.GetServerIds())
                _datas.Add(new Data("iex-qc-1xp", "IEX_" + i));
            
            _datas_view = new BindingDataView(this);
            for (int i = 0; i < _datas.Count; ++i)
            {
                _datas[i].PropertyChanged += new PropertyChangedEventHandler(data_PropertyChanged);

                //_datas_view.Add(new DataView(_datas[i].HostId, _datas[i].ServerId));
                _datas_view.Add(new DataView(_datas[i]));
            }

            dataGridView1.DataSource = _datas_view;
            iexStbGridView1.DataSource = _datas_view;
            iexFlowLayoutPanel1.DataSource = _datas_view;

            //iexFlowLayoutPanel1.Clear();
            //foreach(var data_view in _datas_view)
            //    iexFlowLayoutPanel1.Add(data_view);

            _iexhot1_timer = new System.Threading.Timer(Elapsed_iexhot1, null, 5000, 5000);
            _iex_qc_2_timer = new System.Threading.Timer(Elapsed_iex_qc_2, null, 5000, 5000);
            _iex_qc_1xp_timer = new System.Threading.Timer(Elapsed_iex_qc_1xp, null, 5000, 5000);
        }

        private void data_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Data data = sender as Data;
            DataView data_view = _datas_view.First(item => item.Computer == data.HostId && item.ServerId == data.ServerId);
            data_view.Update(data);
        }

        private void SetData(IEX.Server.Monitor.Client.MonitoringServiceReference.MonitorInfo info, string host, IEX.Server.Monitor.Client.ServiceMonitoring service)
        {
            foreach (var server_data in info.Servers)
            {
                string server_id = "IEX_" + server_data.State.IexServer.Instance;
                Data data = _datas.Find(item => item.HostId == host && item.ServerId == server_id);
                if (server_data.State.PerformanceCounters != null && server_data.State.PerformanceCounters.Count() == 2)
                {
                    foreach (var performanceCounter in server_data.State.PerformanceCounters)
                    {
                        if (performanceCounter.PerformanceCounter.Name == "ComputerCPU")
                        {
                            data.CPU = performanceCounter.Value.ToString();
                        }
                        else if (performanceCounter.PerformanceCounter.Name == "Available MBytes")
                        {
                            data.Memory = performanceCounter.Value.ToString();
                        }
                    }

                   // data.CPU = server_data.Info["% Processor Time"];
                   // data.Memory = server_data.Info["Working Set"];
                }
                else
                {
                    data.CPU = string.Empty;
                    data.Memory = string.Empty;
                }

                if (server_data.has_changes)
                {
                    IEX.Server.Monitor.Client.MonitoringServiceReference.ServerInfo server_info = service.GetServerInfo(server_data.State.IexServer.Instance);
                    data.Status = server_info.Status.ToString();
                }
            }
            iexStbGridView1.Invalidate();
            dataGridView1.Invalidate();
        }

        //private void PingElapsed(Object state)
        //{
        //    DateTime date_time = DateTime.Now;
        //    Random rand = new Random();
        //    foreach (Data data in _datas)
        //    {
        //        data.Time = date_time.ToString();
        //        data.Random = rand.Next().ToString();
        //    }
        //    dataGridView1.Invalidate();
        //}

        private void Elapsed_iexhot1(Object state)
        {
            try
            {
                IEX.Server.Monitor.Client.MonitoringServiceReference.MonitorInfo info = _iexhot1_monitoring.PingMonitor();
                SetData(info, "iexhot1", _iexhot1_monitoring);
                
            }
            catch (Exception exc)
            {
                string data = exc.Message;
            }
        }

        private void Elapsed_iex_qc_2(Object state)
        {
            try
            {
                IEX.Server.Monitor.Client.MonitoringServiceReference.MonitorInfo info = _iex_qc_2_monitoring.PingMonitor();
                SetData(info, "iex-qc-2", _iex_qc_2_monitoring);
            }
            catch (Exception exc)
            {
                string data = exc.Message;
            }
        }

        private void Elapsed_iex_qc_1xp(Object state)
        {
            try
            {
                IEX.Server.Monitor.Client.MonitoringServiceReference.MonitorInfo info = _iex_qc_1xp_monitoring.PingMonitor();
                SetData(info, "iex-qc-1xp", _iex_qc_1xp_monitoring);
            }
            catch (Exception exc)
            {
                string data = exc.Message;
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            for(int i = _datas.Count-1; i > -1; --i)
            {
                _datas[i].PropertyChanged -= new PropertyChangedEventHandler(data_PropertyChanged);
                _datas.RemoveAt(i);
                _datas_view.RemoveAt(i);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.App.Views
{
    using System.ComponentModel;
    using System.Runtime.Serialization;
    public class Data : INotifyPropertyChanged
    {
        public Data(string host_id, string server_id)
        {
            HostId = host_id;
            ServerId = server_id;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string property_name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property_name));
        }

        private string _host_id;
        public string HostId
        {
            get { return _host_id; }
            set
            {
                _host_id = value;
                NotifyPropertyChanged("HostId");
            }
        }
        private string _server_id;
        public string ServerId
        {
            get { return _server_id; }
            set
            {
                _server_id = value;
                NotifyPropertyChanged("ServerId");
            }
        }
        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }
        private string _cpu;
        public string CPU
        {
            get { return _cpu; }
            set
            {
                _cpu = value;
                NotifyPropertyChanged("CPU");
            }
        }
        private string _memory;
        public string Memory
        {
            get { return _memory; }
            set
            {
                _memory = value;
                NotifyPropertyChanged("Memory");
            }
        }
    }

    public class DataView
    {
        public DataView(string computer, string server_id)
        {
            Computer = computer;
            ServerId = server_id;
        }

        public DataView(Data data)
        {
            Computer = data.HostId;
            ServerId = data.ServerId;
            Update(data);
        }

        public void Update(Data data)
        {
            this.Status = data.Status;
            this.CPU = data.CPU;
            this.Memory = data.Memory;
        }

        [DataMember]
        public string Computer { get; set; }
        [DataMember]
        public string ServerId { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string CPU { get; set; }
        [DataMember]
        public string Memory { get; set; }
        
        //public override string ToString()
        //{
        //    return IEX.Utilities.Tools.Serializer.DataContractSerialize(this);
        //}
    }

    public class BindingDataView : IEX.Utilities.Collections.BindingListView<DataView>
    {
        public BindingDataView(System.ComponentModel.ISynchronizeInvoke invoke) : base(invoke) { }
    }
}

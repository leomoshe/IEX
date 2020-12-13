using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Server.Monitor.Client.MonitoringServiceReference
{
    using System.ComponentModel;
    public partial class MonitorInfo
    {
        public MonitorInfo()
        {
            Status = MonitorState.Unknown;
            State = new ComputerState(); 
            this.Servers = Enumerable.Repeat(new ServerData(), 8).ToArray();
        }

        public MonitorState Status { get; set; }

        public static implicit operator string(MonitorInfo item)
        {
            return IEX.Utilities.Tools.Serializer.DataContractSerialize(item);
        }

        public override string ToString()
        {
            return this;
        }
    }

    public enum MonitorState
    {
        [Description("Unknown")]
        Unknown,
        [Description("Running")]
        Running,
        [Description("Not Running")]
        NotRunning
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.Client
{
    using IEX.Utilities;
    public class Host
    {
        public Host(string host_id)
        {
            HostId = host_id;
        }

        public IEX.Server.Monitor.Client.ConfigurationServiceReference.ServiceCollection SubsystemsMetadata { get; set; }
        public virtual string HostId { get; set; }
        public IEX.Server.Monitor.Client.MonitoringServiceReference.MonitorState Status { get; set; }
        public string CPU { get; set; }
        public string Memory { get; set; }
        public string MonitorVersion { get; set; }
        public IList<int> ConfiguratedIdsServers { get; set; }
        public override string ToString()
        {
            return string.Format("Host: '{0}'", HostId);
        }
    }

    public class HostList : List<Host>
    {
        public static HostList Convert(IList<Host> item)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { item });
            HostList result = new HostList();
            if (item != null)
                result.AddRange(item);
            return result;
        }
    }
}

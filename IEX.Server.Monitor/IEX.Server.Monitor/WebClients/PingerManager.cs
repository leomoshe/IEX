using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Server.Monitor.WebClients
{
    using IEX.Utilities;
    using System.ServiceModel;
    using IEX.Server.Monitor.ServerServiceReference;
    public class PingerManager : ChannelFactoryManager<IPinger>
    {
        const string base_url = "http://localhost:8732/IEX.ServerX/Pinger/";
        private int _id;
        public PingerManager(int id): this(base_url + id + "/")
        {
            _id = id;
        }

        private PingerManager(string absolute_uri)
            : base(typeof(PingerClient), "WSHttpBinding_IPinger", new EndpointAddress(absolute_uri))
        {
        }

        private IPinger _channel;
        public IPinger Open()
        {
            IPinger result = null;
            try
            {
                result = new PingerClient("WSHttpBinding_IPinger", new EndpointAddress(base_url + _id + "/"));
            }
            catch (Exception exc)
            {
                Tracer.Write(Tracer.TraceLevel.ERROR, string.Format("'{0}' IPinger creation", _id), exc);
                throw;
            }
            return result;
            //if (_channel == null)
            //    _channel = CreateChannel(true, new TimeSpan(0, 0, 5));
            //return Open(_channel);
        }

        public void Close(IPinger channel = null)
        {
            if (channel == null)
                channel = _channel;
            base.Close(channel);
            channel = null;
        }

        public ICollection<IEX.ManagementServer.Model.Monitoring.PerformanceCounterInstance> PingServer(int hash_code, out bool has_changes)
        {
            ICollection<IEX.ManagementServer.Model.Monitoring.PerformanceCounterInstance> finalResult = new List<IEX.ManagementServer.Model.Monitoring.PerformanceCounterInstance>();
            Dictionary<string, string> result = null;
            has_changes = false;
            IPinger proxy = Open();
            try
            {
                result = proxy.PingServerStatus(out has_changes, hash_code);
                
                //TODO Ilay, convert the result to the new performance counters
                finalResult = result.ToPerformanceCounters();

            }
            catch (System.ServiceModel.EndpointNotFoundException exc)
            {
                Tracer.Write(Tracer.TraceLevel.INFO, string.Format("Server '{0}' is not running", _id));
            }
            catch (System.ServiceModel.FaultException exc)
            {
                Tracer.Write(Tracer.TraceLevel.ERROR, string.Format("PingServerStatus '{0}'", _id), exc);
                throw;
            }
            catch (Exception exc)
            {
                Tracer.Write(Tracer.TraceLevel.ERROR, string.Format("PingServerStatus '{0}'", _id), exc);
            }
            finally
            {
                Close(proxy);
            }
            return finalResult;
        }
    }
}

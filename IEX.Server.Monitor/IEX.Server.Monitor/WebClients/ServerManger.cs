using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Server.Monitor.WebClients
{
    using IEX.Utilities;
    using System.ServiceModel;
    using IEX.Server.Monitor.ServerServiceReference;
    public class ServerManager : ChannelFactoryManager<IServer>
    {
        const string base_url = "http://localhost:8732/IEX.ServerX/Server/";
        private int _id;
        public ServerManager(int id): this(base_url + id + "/")
        {
            _id = id;
        }

        private ServerManager(string absolute_uri)
            : base(typeof(ServerClient), "WSHttpBinding_IServer", new EndpointAddress(absolute_uri))
        {
        }

        private IServer _channel;
        public IServer Open()
        {
            IServer result = null;
            try
            {
                result = new ServerClient("WSHttpBinding_IServer", new EndpointAddress(base_url + _id + "/"));
            }
            catch (Exception exc)
            {
                Tracer.Write(Tracer.TraceLevel.ERROR, string.Format("'{0}' IServer creation", _id), exc);
                throw;
            }
            return result;
            //if (_channel == null)
            //    _channel = CreateChannel(false);
            //return Open(_channel);
        }

        public void Close(IServer channel = null)
        {
            if (channel == null)
                channel = _channel;
            base.Close(channel);
            channel = null;
        }

        public ServerInfo GetServerInfo()
        {
            ServerInfo result = null;
            IServer proxy = Open();
            try
            {
                result = proxy.GetServerStatus();
            }
            catch (System.ServiceModel.FaultException exc)
            {
                Tracer.Write(Tracer.TraceLevel.ERROR, string.Format("GetServerInfo '{0}'", _id), exc);
                throw;
            }
            catch (Exception exc)
            {
                Tracer.Write(Tracer.TraceLevel.ERROR, string.Format("GetServerInfo '{0}'", _id), exc);
            }
            finally
            {
                Close(proxy);
            }
            return result;
        }
    }
}

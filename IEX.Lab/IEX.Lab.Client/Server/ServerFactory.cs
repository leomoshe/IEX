using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.Client
{
    public class ServerFactory : IEX.Lab.Client.Common.IServerFactory
    {
        public Server Create(string host_id, string server_id)
        {
            return new Server(host_id, server_id);
        }
    }
}

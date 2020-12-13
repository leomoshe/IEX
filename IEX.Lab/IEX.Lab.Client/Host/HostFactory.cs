using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.Client
{
    public class HostFactory : IEX.Lab.Client.Common.IHostFactory
    {
        public Host Create(string host_id)
        {
            return new Host(host_id);
        }
    }
}

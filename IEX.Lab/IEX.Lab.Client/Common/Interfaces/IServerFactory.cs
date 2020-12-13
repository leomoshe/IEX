using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.Client.Common
{
    public interface IServerFactory
    {
        IEX.Lab.Client.Server Create(string host_id, string server_id);
    }
}

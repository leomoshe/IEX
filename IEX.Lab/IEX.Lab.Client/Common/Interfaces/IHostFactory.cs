using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.Client.Common
{
    public interface IHostFactory
    {
        IEX.Lab.Client.Host Create(string host_id);
    }
}

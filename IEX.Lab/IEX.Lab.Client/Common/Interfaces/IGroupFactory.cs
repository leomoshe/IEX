using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.Client.Common
{
    public interface IGroupFactory
    {
        IEX.Lab.Client.Group Create(string group_id);
    }
}

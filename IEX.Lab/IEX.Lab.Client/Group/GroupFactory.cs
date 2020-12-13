using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.Client
{
    public class GroupFactory : IEX.Lab.Client.Common.IGroupFactory
    {
        public Group Create(string group_id)
        {
            return new Group(group_id);
        }
    }
}

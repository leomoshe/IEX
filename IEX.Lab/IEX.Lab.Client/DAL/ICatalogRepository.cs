using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.Client
{
    public interface ICatalogRepository
    {
        IEnumerable<Server> FindServer();
        IEnumerable<Group> FindGroup();

        void Add(Server item);
        void DeleteServer(string host_id, string server_id);

        void Add(Group item);
        void DeleteGroup(string group_id);
        void SetGroup(string key, Group item);
    }
}

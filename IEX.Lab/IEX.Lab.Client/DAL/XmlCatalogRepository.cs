using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.Client
{
    using IEX.Utilities;
    using IEX.Utilities.Collections;
    public class XmlCatalogRepository : XmlRepository, ICatalogRepository
    {
        private List<Server> _servers = new List<Server>();
        private List<Group> _groups = new List<Group>();

        public XmlCatalogRepository()
            : base()
        {
        }

        public new void New(string full_path)
        {
            IexLabXmlConfiguration configuration = base.New(full_path);
            _servers = IexLabXmlConfiguration.Convert(configuration.Server).ToList();
            _groups = IexLabXmlConfiguration.Convert(configuration.Group).ToList();
        }

        public new void Open(string full_path)
        {
            IexLabXmlConfiguration configuration = base.Open(full_path);
            _servers = IexLabXmlConfiguration.Convert(configuration.Server).ToList();
            _groups = IexLabXmlConfiguration.Convert(configuration.Group).ToList();
        }

        public new void Save(string full_path)
        {
            IexLabXmlConfiguration configuration = base.Save(full_path);
            configuration.Server = IexLabXmlConfiguration.Convert(_servers.ToArray());
            configuration.Group = IexLabXmlConfiguration.Convert(_groups.ToArray());
            Serialize(FullPath, configuration);
        }

        public void SaveAs(string full_path)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { full_path });
            IexLabXmlConfiguration configuration = Deserialize(FullPath);
            configuration.Server = IexLabXmlConfiguration.Convert(_servers.ToArray());
            configuration.Group = IexLabXmlConfiguration.Convert(_groups.ToArray());
            Serialize(full_path, configuration);
        }

        public IEnumerable<Server> FindServer()
        {
            return _servers;
        }

        public IEnumerable<Group> FindGroup()
        {
            return _groups;
        }

        public void Add(Server item)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { item });
            _servers.Add(item);
            Save(FullPath);
        }

        public void DeleteServer(string host_id, string server_id)
        {
            _servers.Remove(item => item.HostId == host_id && item.ServerId == server_id);
            Save(FullPath);
        }

        public void Add(Group item)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { item });
            _groups.Add(item);
            Save(FullPath);
        }

        public void DeleteGroup(string group_id)
        {
            _groups.Remove(item => item.Name == group_id);
            Save(FullPath);
        }

        public void SetGroup(string group_id, Group item)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { group_id, item });
            int index = -1;
            index = _groups.IndexWhere(value => value.Name == group_id);
            _groups[index] = item;
            Save(FullPath);
        }
    }
}

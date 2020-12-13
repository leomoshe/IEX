using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.Client
{
    public class StructureMapRegistry : StructureMap.Configuration.DSL.Registry
    {
        public StructureMapRegistry()
        {
            For<IEX.Lab.Client.Common.IServerFactory>().Singleton().Use<IEX.Lab.Client.ServerFactory>();
            For<IEX.Lab.Client.Common.IGroupFactory>().Singleton().Use<IEX.Lab.Client.GroupFactory>();
            For<IEX.Lab.Client.Common.IHostFactory>().Singleton().Use<IEX.Lab.Client.HostFactory>();

            For<IEX.Lab.Client.ICatalogRepository>().Singleton().Use<IEX.Lab.Client.XmlCatalogRepository>();
            For<IEX.Lab.Client.ICatalogProvider>().Singleton().Use<IEX.Lab.Client.CatalogProvider>();
        }
    }
}

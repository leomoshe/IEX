using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.App.Views
{
    using IEX.Utilities.Collections;
    public interface IShell
    {
        string RepositoryString { get;  set; }

        BindingListView<ServerViewModel> Servers { set; }
        BindingListView<HostViewModel> Hosts { set; }
        IEX.Lab.Client.GroupList Groups { set; }
        IList<int> ColumnServers { set; }

        IList<IEX.Lab.Client.Server> SelectedServers { set; }
    }
}

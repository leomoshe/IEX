using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.App
{
    using System.ComponentModel;
    public class BindingServerView : IEX.Utilities.Collections.BindingListView<ServerViewModel>
    {
        public BindingServerView(ISynchronizeInvoke invoke) : base(invoke) {}

        private Client.Group _group_filter;
        public Client.Group GroupFilter
        {
            set
            {
                this.RemoveFilter();
                _group_filter = value;
                Items.Clear();
                AddAll(_original.FindAll(filter));
            }
        }

        private bool filter(ServerViewModel server_view_model)
        {
            if (_group_filter.Servers != null && _group_filter.Servers.Find(item => item.HostId == server_view_model.Computer && item.ServerId == server_view_model.ServerId) != null)
                return true;
            return false;
        }
    }
}

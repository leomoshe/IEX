using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.App.Views
{
    using IEX.Utilities.Controls;
    public class TreeNodeServerTemplate : TreeNodeDataTemplate
    {
        public TreeNodeServerTemplate()
        {
            Type = typeof(ServerViewModel);
            Key = "Binding Key";
            Text = "Binding Description";
        }

        override public IEX.Utilities.Controls.BTreeNode Set(IEX.Utilities.Controls.TreeViewItemViewModel item_source, System.Windows.Forms.TreeNodeCollection nodes, System.Windows.Forms.TreeView tree_view = null)
        {
            IEX.Utilities.Controls.BTreeNode result = base.Set(item_source, nodes);
            return result;
        }
    }
}

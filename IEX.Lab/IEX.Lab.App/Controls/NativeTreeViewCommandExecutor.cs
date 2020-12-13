using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.App
{
    using IEX.Utilities;
    using System.Windows.Forms;
    public class NativeTreeViewCommandExecutor : CommandExecutor
    {
        public override void InstanceAdded(object item, PCommand cmd)
        {
            NativeTreeView control = Control<NativeTreeView>(item);
            switch (EventName(item))
            {
                case "AfterSelect":
                    control.AfterSelect += delegate(object sender, TreeViewEventArgs e) { Execute<TreeViewEventArgs>(sender, "AfterSelect", e); };
                    break;
            }
            base.InstanceAdded(item, cmd);
        }

        // State setters
        public override void Enable(object item, bool bEnable)
        {
            NativeTreeView control = (NativeTreeView)item;
            control.Enabled = bEnable;
        }

        public override void Visible(object item, bool bVisible)
        {
            NativeTreeView control = (NativeTreeView)item;
            control.Visible = bVisible;
        }

        public override void Check(object item, bool bCheck)
        {
            throw new NotImplementedException();
        }
    }
}

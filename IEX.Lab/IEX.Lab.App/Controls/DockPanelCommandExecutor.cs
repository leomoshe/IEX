using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.App
{
    using IEX.Utilities;
    using DevExpress.XtraBars.Docking;
    public class DockPanelCommandExecutor : CommandExecutor
    {
        public override void InstanceAdded(object item, PCommand cmd)
        {
            DockPanel control = Control<DockPanel>(item);
            if (EventName(item) == "Click")
                control.Click += new System.EventHandler(this.control_Click);

            base.InstanceAdded(item, cmd);
        }

        // State setters
        public override void Enable(object item, bool bEnable)
        {
            DockPanel control = (DockPanel)item;
            control.Enabled = bEnable;
        }

        public override void Visible(object item, bool bVisible)
        {
            DockPanel control = (DockPanel)item;
            control.Visibility = bVisible == true ? DockVisibility.Visible : DockVisibility.Hidden;
        }

        public override void Check(object item, bool bCheck)
        {
            throw new NotImplementedException();
        }
    }
}

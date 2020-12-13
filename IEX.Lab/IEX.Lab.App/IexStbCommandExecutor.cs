using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.App
{
    using IEX.Utilities;
    public class IexStbCommandExecutor : CommandExecutor
    {
        public override void InstanceAdded(object item, PCommand cmd)
        {
            IexStb control = (IexStb)item;
            control.Click += new System.EventHandler(this.Item_Click);

            base.InstanceAdded(item, cmd);
        }

        // State setters
        public override void Enable(object item, bool bEnable)
        {
            IexStb control = (IexStb)item;
            control.Enabled = bEnable;
        }

        public override void Check(object item, bool bCheck)
        {
        }

        // Execution event handler
        private void Item_Click(object sender, System.EventArgs e)
        {
            PCommand cmd = GetCommandForInstance(sender);
            cmd.Execute(sender);
        }
    }
}

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
            IexStb control = Control<IexStb>(item);
            switch (EventName(item))
            {
                case "Click":
                    control.Click += new EventHandler(control_Click);
                    break;
            }

            base.InstanceAdded(item, cmd);
        }

        // State setters
        public override void Enable(object item, bool bEnable)
        {
            IexStb control = (IexStb)item;
            control.Enabled = bEnable;
        }

        public override void Visible(object item, bool bVisible)
        {
            IexStb control = (IexStb)item;
            control.Visible = bVisible;
        }

        public override void Check(object item, bool bCheck)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.App
{
    using IEX.Utilities;
    using DevExpress.XtraEditors;
    using IEX.Lab.App.Views;
    public class SimpleButtonCommandExecutor : CommandExecutor
    {
        public override void InstanceAdded(object item, PCommand cmd)
        {
            SimpleButton control = Control<SimpleButton>(item);

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
            SimpleButton control = (SimpleButton)item;
            IEX.Utilities.Controls.Tools.SetControlPropertyValue(control, "Enabled", bEnable);
        }

        public override void Visible(object item, bool bVisible)
        {
            SimpleButton control = (SimpleButton)item;
            IEX.Utilities.Controls.Tools.SetControlPropertyValue(control, "Visible", bVisible);
        }

        public override void Check(object item, bool bCheck)
        {
            throw new NotImplementedException();
        }
    }

    public class MyButtonCommandExecutor : CommandExecutor
    {
        public override void InstanceAdded(object item, PCommand cmd)
        {
            MyButton control = Control<MyButton>(item);

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
            MyButton control = (MyButton)item;
            IEX.Utilities.Controls.Tools.SetControlPropertyValue(control, "Enabled", bEnable);
        }

        public override void Visible(object item, bool bVisible)
        {
            MyButton control = (MyButton)item;
            IEX.Utilities.Controls.Tools.SetControlPropertyValue(control, "Visible", bVisible);
        }

        public override void Check(object item, bool bCheck)
        {
            throw new NotImplementedException();
        }
    }
}

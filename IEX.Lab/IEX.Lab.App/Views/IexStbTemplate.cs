using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.App.Views
{
    public class IexStbTemplate
    {
        public string Key { get; set; }
        public string Text { get; set; }
        public string Command { get; set; }

        virtual public void Set(ServerViewModel item_source, IexStb item)
        {
        }
    }
}

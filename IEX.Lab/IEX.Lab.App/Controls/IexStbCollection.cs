using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.App.Controls
{
    using System.Collections;
    class IexStbCollection : IEnumerable
    {
        ArrayList list;
               
        internal static IexStbCollection Empty = new IexStbCollection (null);

        public IexStbCollection(ArrayList list)
        {
            this.list = list;
        }

        public IEnumerator GetEnumerator ()
        {
            if (list != null)
                return list.GetEnumerator ();
            else
                return Type.EmptyTypes.GetEnumerator ();
        }

        public IexStb this [int n] 
        {
            get 
            { 
                if (list != null)
                    return (IexStb)list[n];
                else
                    throw new System.IndexOutOfRangeException ();
            }
        }

        public int IndexOfItem (string id)
        {
            for (int n=0; n<Count; n++) 
            {
                if (this [n].ServerId == id)
                    return n;
            }
            return -1;
        }

        public int Count 
        {
            get { return list != null ? list.Count : 0; }
        }

        public IexStbCollection Clone()
        {
            if (list != null)
                return new IexStbCollection((ArrayList)list.Clone());
            else
                return Empty;
        }
    }
}

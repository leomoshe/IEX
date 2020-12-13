using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.Client
{
    public class PropertyValueChangedEventArgs : System.ComponentModel.PropertyChangedEventArgs
    {
        private readonly object _data;
        public PropertyValueChangedEventArgs(/*[CallerMemberName]*/string property_name, object data)
            : base(property_name)
        {
            _data = data;
        }

        public virtual object Data
        {
            get
            {
                return _data;
            }
        }
    }

    public class PropertyValuesChangedEventArgs : System.ComponentModel.PropertyChangedEventArgs
    {
        private readonly IEnumerable<object> _datas;
        public PropertyValuesChangedEventArgs(/*[CallerMemberName]*/string property_name, IEnumerable<object> datas)
            : base(string.Empty)
        {
            _datas = datas;
        }

        public virtual IEnumerable<object> Datas
        {
            get
            {
                return _datas;
            }
        }
    }

    public class PropertiesValuesChangedEventArgs : System.ComponentModel.PropertyChangedEventArgs
    {
        private readonly ProperyValueCollection _data;
        public PropertiesValuesChangedEventArgs(ProperyValueCollection data): base(string.Empty)
        {
            _data = data;
        }

        public virtual object Data
        {
            get
            {
                return _data;
            }
        }
    }

    public class ProperyValueCollection: List<ProperyValue>
    {
    }

    public class ProperyValue
    {
        public ProperyValue(string propery_name, object data) 
        {
            PropertyName = propery_name;
            Data = data;
        }

        public string PropertyName { get; set; }
        public object Data { get; set; }
    }

    
}
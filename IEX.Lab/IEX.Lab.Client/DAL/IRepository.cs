using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.Client
{
    public interface IRepository<T> where T : class
    {
        void Add(T item);
        void Add(IEnumerable<T> item);
        void Delete(T item);
        void Delete(IEnumerable<T> item);
        T Find(string key);
        void Set(string key, T item);
        IEnumerable<T> Find();
    }
}

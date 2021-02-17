using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject
{
    interface IBasicDb<T> where T : IPoco
    {
        T Get(int id);
        IList<T> GetAll();
        void Add(T t);
        void Remove(T t);
        void Update(T t);
    }
}

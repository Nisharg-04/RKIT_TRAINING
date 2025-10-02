using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsDemo
{    public class Box<T>
    {
        private T _value;

        public void Add(T value)
        {
            _value = value;
        }

        public T Get()
        {
            return _value;
        }
    }
}

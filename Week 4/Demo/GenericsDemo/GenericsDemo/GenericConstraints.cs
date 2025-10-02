using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsDemo
{

    public class Repository<T> where T : new()
    {
        public T CreateInstance()
        {
            return new T(); // Requires parameterless constructor
        }
    }

    class Sample
    {
        public int Id { get; set; }
        public Sample() { Id = 100; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtentionMethodsDemo
{
    public static class IntExtensions
    {
        public static bool IsEven(this int n) => n % 2 == 0;
    }
}

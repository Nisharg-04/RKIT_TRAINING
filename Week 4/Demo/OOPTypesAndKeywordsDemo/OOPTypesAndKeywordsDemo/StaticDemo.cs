using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPTypesAndKeywordsDemo
{
    public class MathHelper
    {
        public static double Pi = 3.14159;

        public static double Square(double number)
        {
            return number * number;
        }
    }

    public static class Utilities
    {
        public static void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }
    }

}

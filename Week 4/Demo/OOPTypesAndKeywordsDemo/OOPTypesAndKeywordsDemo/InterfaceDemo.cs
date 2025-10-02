using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPTypesAndKeywordsDemo
{
    public interface IShape
    {
        double Area();
        double Perimeter();
    }

    public class Circle : IShape
    {
        public double Radius { get; set; }

        public Circle(double radius)
        {
            Radius = radius;
        }

        public double Area()
        {
            return Math.PI * Radius * Radius;
        }

        public double Perimeter()
        {
            return 2 * Math.PI * Radius;
        }
    }

}

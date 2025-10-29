using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }

        // One-to-Many: One Student -> Many Enrollments
        public List<Enrollment> Enrollments { get; set; } = new();
    }
}

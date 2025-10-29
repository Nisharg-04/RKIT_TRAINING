using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string Name { get; set; } = "";

        // One-to-Many: One Teacher teaches many Courses
        public List<Course> Courses { get; set; } = new();
    }
}

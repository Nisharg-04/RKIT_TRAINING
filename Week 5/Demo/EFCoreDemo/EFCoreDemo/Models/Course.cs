using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = "";
        public int Credits { get; set; }

        // One-to-Many: One Course -> Many Enrollments
        public List<Enrollment> Enrollments { get; set; } = new();
    }
}

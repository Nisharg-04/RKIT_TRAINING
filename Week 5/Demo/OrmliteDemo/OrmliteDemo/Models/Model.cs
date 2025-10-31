using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmliteDemo.Models
{
    public class Department
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class Student
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [ForeignKey(typeof(Department))]
        public int DepartmentId { get; set; }

        public DateTime EnrolledDate { get; set; }

        [References(typeof(Department))]
        public Department Department { get; set; }
    }
    public class Course
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }

        public string Title { get; set; }

        public int Credits { get; set; }
    }
    public class StudentCourse
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }

        [References(typeof(Student))]
        public int StudentId { get; set; }

        [References(typeof(Course))]
        public int CourseId { get; set; }
    }
}

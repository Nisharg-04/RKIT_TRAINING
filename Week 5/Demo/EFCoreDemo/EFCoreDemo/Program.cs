using EFCoreDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new SchoolContext();

     

     

          
            var teacher1 = new Teacher { Name = "Dr. Smith" };
            var teacher2 = new Teacher { Name = "Prof. Johnson" };

            var course1 = new Course { Title = "Math", Credits = 3, };
            var course2 = new Course { Title = "Physics", Credits = 4 };
            var course3 = new Course { Title = "Chemistry", Credits = 4 };

            teacher1.Courses.Add(course1);
            teacher1.Courses.Add(course2);
            teacher2.Courses.Add(course3);

            var student1 = new Student { Name = "Alice", Age = 20 };
            var student2 = new Student { Name = "Bob", Age = 22 };

          
            var e1 = new Enrollment { Student = student1, Course = course1, Grade = "A" };
            var e2 = new Enrollment { Student = student1, Course = course2, Grade = "B+" };
            var e3 = new Enrollment { Student = student2, Course = course2, Grade = "A-" };

            context.AddRange(teacher1, teacher2, student1, student2, e1, e2, e3);
            context.SaveChanges();


            var students = context.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .ToList();

  
            foreach (var s in students)
            {
                Console.WriteLine($"Student: {s.Name}");
                foreach (var enr in s.Enrollments)
                {
                    Console.WriteLine($"   -> {enr.Course.Title} ({enr.Grade})");
                }
            }

    
            var studentToUpdate = context.Students.First();
            studentToUpdate.Age = 21;
            context.SaveChanges();
            Console.WriteLine($"\nUpdated {studentToUpdate.Name}'s age to {studentToUpdate.Age}");

    
            var courseToDelete = context.Courses.FirstOrDefault(c => c.Title == "Chemistry");
            if (courseToDelete != null)
            {
                context.Courses.Remove(courseToDelete);
                context.SaveChanges();
                Console.WriteLine("\nDeleted 'Chemistry' course successfully!");
            }


     
        
    }
    }
}

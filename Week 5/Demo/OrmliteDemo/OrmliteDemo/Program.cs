using OrmliteDemo.Models;
using ServiceStack.OrmLite;
using System.Data;

namespace OrmliteDemo
{
    internal class Program
    {
        static void seedData(IDbConnection db) {
            var dept1 = new Models.Department { Name = "Computer Science" };
            var dept2 = new Models.Department { Name = "Mathematics" };
            db.Insert(dept1);
            db.Insert(dept2);
            var s1 = new Student { FullName = "Alice", DepartmentId = 1, EnrolledDate = DateTime.Now };
            var s2 = new Student { FullName = "Bob", DepartmentId = 1, EnrolledDate = DateTime.Now };
            var s3 = new Student { FullName = "Charlie", DepartmentId = 2, EnrolledDate = DateTime.Now };
            db.InsertAll(new[] { s1, s2, s3 });
            var course1 = new Models.Course { Title = "Data Structures" };
            var course2 = new Models.Course { Title = "Calculus" };
            db.Insert(course1);
            db.Insert(course2);
            var sc1 = new Models.StudentCourse { StudentId = s1.Id, CourseId = course1.Id };
            var sc2 = new Models.StudentCourse { StudentId = s2.Id, CourseId = course2.Id };
            db.Insert(sc1);
            db.Insert(sc2);
            Console.WriteLine("Sample data inserted successfully.");
        }

        static void curd(IDbConnection db) {
            
            var students = db.Select<Student>();
            Console.WriteLine("Students:");
            foreach (var student in students) {
                Console.WriteLine($"ID: {student.Id}, Name: {student.FullName}, DepartmentId: {student.DepartmentId}, EnrolledDate: {student.EnrolledDate}");
            }
            
            var studentToUpdate = db.SingleById<Student>(1);
            if (studentToUpdate != null) {
                studentToUpdate.FullName = "Alice Updated";
                db.Update(studentToUpdate);
                Console.WriteLine("Student updated successfully.");
            }
             students = db.Select<Student>();
            Console.WriteLine("Students:");
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.Id}, Name: {student.FullName}, DepartmentId: {student.DepartmentId}, EnrolledDate: {student.EnrolledDate}");
            }

            var studentToDelete = db.SingleById<Student>(2);
            if (studentToDelete != null) {
                db.Delete(studentToDelete);
                Console.WriteLine("Student deleted successfully.");
            }

        }

        static void transactionDemo(IDbConnection db) {
           
            
                using (var trans = db.OpenTransaction())
                {
                    try
                    {
                        db.Insert(new Course { Title = "Database Systems", Credits = 4 });
                        db.Insert(new Course { Title = "Operating Systems", Credits = 3 });
                        trans.Commit();
                        Console.WriteLine("Transaction committed.");
                    }
                    catch
                    {
                        trans.Rollback();
                        Console.WriteLine("Transaction rolled back.");
                    }
                }
            

        }
        static void Main(string[] args)
        {
            var dbfactory = new OrmLiteConnectionFactory("Data Source = student.db", SqliteDialect.Provider);
            using (var db=dbfactory.Open()) {
                
                Console.WriteLine("Database connected successfully.");

                db.DropAndCreateTable<Models.Department>();
                db.DropAndCreateTable<Models.Student>();
                db.DropAndCreateTable<Models.Course>();
                db.DropAndCreateTable<Models.StudentCourse>();
                Console.WriteLine("Tables created successfully.");

                seedData(db);
                curd(db);
                transactionDemo(db);

            }
        }
    }
}

using Dapper;
using MySql.Data.MySqlClient;

namespace DapperDemo
{
    internal class Program
    {
        public class Department
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int DepartmentId { get; set; }

            // For JOIN result
            public Department Department { get; set; }
        }
        static string connectionString = "Server=localhost;Database=companydb;User Id=root;Password=****;";
        static void Main(string[] args)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (var tran = connection.BeginTransaction())
                {
                    try
                    {
                        string insertDept = "INSERT INTO Department (Name) VALUES (@Name);";
                        connection.Execute(insertDept, new { Name = "IT" }, tran);
                        connection.Execute(insertDept, new { Name = "HR" }, tran);

                        string insertEmp = "INSERT INTO Employee (Name, DepartmentId) VALUES (@Name, @DeptId);";
                        connection.Execute(insertEmp, new { Name = "Alice", DeptId = 1 }, tran);
                        connection.Execute(insertEmp, new { Name = "Bob", DeptId = 1 }, tran);
                        connection.Execute(insertEmp, new { Name = "Charlie", DeptId = 2 }, tran);

                        tran.Commit();
                        Console.WriteLine("Transaction committed successfully!");
                    }
                    catch
                    {
                        tran.Rollback();
                        Console.WriteLine("Transaction failed, rolled back.");
                    }
                }

                var departments = connection.Query<Department>("SELECT * FROM Department").ToList();
                foreach (var d in departments)
                    Console.WriteLine($"{d.Id}: {d.Name}");
                departments = connection.Query<Department>("SELECT * FROM Department WHERE Id = 1").ToList();
                foreach (var d in departments)
                    Console.WriteLine($"{d.Id}: {d.Name}");


                string joinSql = @"SELECT e.Id, e.Name, e.DepartmentId, d.Id, d.Name 
                                   FROM Employee e 
                                   JOIN Department d ON e.DepartmentId = d.Id;";

                var joined = connection.Query<Employee, Department, Employee>(
                    joinSql,
                    (emp, dept) =>
                    {
                        emp.Department = dept;
                        return emp;
                    },
                    splitOn: "Id"
                ).ToList();

                foreach (var e in joined)
                    Console.WriteLine($"{e.Name} works in {e.Department.Name} department.");


                string multiSql = "SELECT * FROM Department; SELECT * FROM Employee;";
                using (var multi = connection.QueryMultiple(multiSql))
                {
                    var deptList = multi.Read<Department>().ToList();
                    var empList = multi.Read<Employee>().ToList();

                    Console.WriteLine($"Departments count: {deptList.Count}");
                    Console.WriteLine($"Employees count: {empList.Count}");
                }

            }

        }
    }
}

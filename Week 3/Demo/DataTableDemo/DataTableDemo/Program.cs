using System;
using System.Data;  
namespace DataTableDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            DataTable employees = new DataTable("Employees");

            DataColumn idCol = new DataColumn("EmpId", typeof(int));
            idCol.AutoIncrement = true;
            idCol.AutoIncrementSeed = 1;
            idCol.AutoIncrementStep = 1;
            idCol.Unique = true;

            DataColumn nameCol = new DataColumn("Name", typeof(string));
            DataColumn deptCol = new DataColumn("Department", typeof(string));
            DataColumn salaryCol = new DataColumn("Salary", typeof(decimal));

            employees.Columns.Add(idCol);
            employees.Columns.Add(nameCol);
            employees.Columns.Add(deptCol);
            employees.Columns.Add(salaryCol);

            employees.PrimaryKey = new DataColumn[] { idCol };


            employees.Rows.Add(null, "Alice", "IT", 55000);
            employees.Rows.Add(null, "Bob", "HR", 45000);
            employees.Rows.Add(null, "Charlie", "Finance", 60000);
            employees.Rows.Add(null, "David", "IT", 70000);

            PrintTable(employees, "Initial Employees Data");


            employees.Rows[1]["Salary"] = 48000;
            PrintTable(employees, "After Updating Bob Salary");


            employees.Rows[2].Delete();
            employees.AcceptChanges();
            PrintTable(employees, "After Deleting Charlie");


            Console.WriteLine("Employees in IT Dept:");
            DataRow[] itRows = employees.Select("Department = 'IT'");
            foreach (DataRow row in itRows)
                Console.WriteLine($" - {row["Name"]} earns {row["Salary"]}");

            Console.WriteLine();


            object maxSalary = employees.Compute("MAX(Salary)", "");
            Console.WriteLine($"Max Salary = {maxSalary}");

            object avgSalary = employees.Compute("AVG(Salary)", "Department='IT'");
            Console.WriteLine($"Average Salary in IT = {avgSalary}\n");



            DataTable copy = employees.Copy();   // Structure + Data
            DataTable clone = employees.Clone(); // Only Structure
            PrintTable(copy, "Copied Table");
            PrintTable(clone, "Cloned Table");

            DataTable newEmployees = employees.Clone();
            newEmployees.Rows.Add(null, "Eve", "Finance", 50000);
            employees.Merge(newEmployees);
            PrintTable(employees, "After Merging New Employees");

        }
        static void PrintTable(DataTable dt, string title)
        {
            Console.WriteLine($"\n{title}");
            foreach (DataColumn col in dt.Columns)
                Console.Write($"{col.ColumnName}\t");
            Console.WriteLine() ;

            foreach (DataRow row in dt.Rows)
            {
                foreach (var item in row.ItemArray)
                    Console.Write(item + "\t");
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

    
}

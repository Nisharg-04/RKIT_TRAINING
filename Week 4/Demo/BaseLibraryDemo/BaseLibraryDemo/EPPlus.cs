using OfficeOpenXml;

using OfficeOpenXml.Style;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPPlusDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ExcelPackage.License.SetNonCommercialPersonal("NishargSoni");

            string filePath = "StudentReport.xlsx";
            FileInfo file = new FileInfo(filePath);

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet sheet = package.Workbook.Worksheets.Add("Students");

                sheet.Cells[1, 1].Value = "ID";
                sheet.Cells[1, 2].Value = "Name";
                sheet.Cells[1, 3].Value = "Score";
                sheet.Cells[1, 4].Value = "Grade";

                
                using (var range = sheet.Cells[1, 1, 1, 4])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                List<Student> students = new List<Student>()
                {
                    new Student { Id = 1, Name = "Alice", Score = 95 },
                    new Student { Id = 2, Name = "Bob", Score = 82 },
                    new Student { Id = 3, Name = "Charlie", Score = 74 },
                    new Student { Id = 4, Name = "David", Score = 60 },
                    new Student { Id = 5, Name = "Eve", Score = 45 },
                };

                int row = 2;
                foreach (var s in students)
                {
                    sheet.Cells[row, 1].Value = s.Id;
                    sheet.Cells[row, 2].Value = s.Name;
                    sheet.Cells[row, 3].Value = s.Score;
                    sheet.Cells[row, 4].Formula = $"IF(C{row}>=90,\"A\",IF(C{row}>=75,\"B\",IF(C{row}>=60,\"C\",\"F\")))";
                    row++;
                }
                package.SaveAs(file);
            }

            Console.WriteLine("Excel file created successfully!");

      
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet sheet = package.Workbook.Worksheets["Students"];

                int startRow = sheet.Dimension.Start.Row;
                int endRow = sheet.Dimension.End.Row;
                int startColumn = sheet.Dimension.Start.Column;
                int endColumn = sheet.Dimension.End.Column;

                for (int row = startRow; row <= endRow; row++)
                {
                    for (int col = startColumn; col <= endColumn; col++)
                    {
                        Console.Write($"{sheet.Cells[row, col].Value}\t");
                    }
                    Console.WriteLine();
                }
            }
        }
    }


    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
    }
}


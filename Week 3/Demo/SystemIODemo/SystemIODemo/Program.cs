using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Text.Json;
using System.Data;

namespace SystemIODemo
{
    class Program
    {
        static void Main()
        {

            Console.WriteLine(Directory.GetCurrentDirectory());
            string baseDir = Path.Combine(Directory.GetCurrentDirectory(), "DemoFiles");
            Directory.CreateDirectory(baseDir);

            string textFile = Path.Combine(baseDir, "sample.txt");
            string csvFile = Path.Combine(baseDir, "data.csv");
            string binFile = Path.Combine(baseDir, "data.bin");
            string jsonFile = Path.Combine(baseDir, "data.json");

          
                //FileDemo(textFile);
               // FileInfoDemo(textFile);
                //DirectoryDemo(baseDir);
              //  StreamDemo(textFile);
            //    BinaryDemo(binFile);
              // CsvDemo(csvFile);
                //JsonDemo(jsonFile);
               DriveDemo();

            static void FileDemo(string filePath)
            {
                Console.WriteLine("\nFile Demo");

                File.WriteAllText(filePath, "Hello, World!\nThis is a demo file."); 
                Console.WriteLine(File.ReadAllText(filePath));

                File.AppendAllText(filePath, "\nAppended line.");
                Console.WriteLine("After append:");
                Console.WriteLine(File.ReadAllText(filePath));

                bool exists = File.Exists(filePath);
                Console.WriteLine($"File exists: {exists}");
            }

            static void FileInfoDemo(string filePath)
            {
                Console.WriteLine("\nFileInfo Demo");
                FileInfo fi = new FileInfo(filePath);
                Console.WriteLine($"Name: {fi.Name}");
                Console.WriteLine($"Extension: {fi.Extension}");
                Console.WriteLine($"Size: {fi.Length} bytes");
                Console.WriteLine($"Created: {fi.CreationTime}");
                Console.WriteLine($"Directory: {fi.DirectoryName}");
            }


            static void DirectoryDemo(string baseDir)
            {
                Console.WriteLine("\nDirectory Demo");

                string subDir = Path.Combine(baseDir, "SubFolder");
                Directory.CreateDirectory(subDir);

                Console.WriteLine($"Created directory: {subDir}");

                string[] files = Directory.GetFiles(baseDir);
                Console.WriteLine("Files in base directory:");
                foreach (var f in files) Console.WriteLine(f);

                DirectoryInfo di = new DirectoryInfo(baseDir);
                Console.WriteLine($"Directory Full Name: {di.FullName}");
                Console.WriteLine($"Parent: {di.Parent}");
                Console.WriteLine($"Created: {di.CreationTime}");
            }

            static void StreamDemo(string filePath)
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine("Written via StreamWriter.");
                }

       
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (StreamReader sr = new StreamReader(fs))
                {
                    Console.WriteLine("Reading with StreamReader:");
                    Console.WriteLine(sr.ReadToEnd());
                }
            }

 
            static void BinaryDemo(string binFile)
            {
                using (BinaryWriter bw = new BinaryWriter(File.Open(binFile, FileMode.Create)))
                {
                    bw.Write(42);         
                    bw.Write(3.14);       
                    bw.Write("HelloBin");  
                }

                using (BinaryReader br = new BinaryReader(File.Open(binFile, FileMode.Open)))
                {
                    int i = br.ReadInt32();
                    double d = br.ReadDouble();
                    string s = br.ReadString();
                    Console.WriteLine($"Read from binary: {i}, {d}, {s}");
                }
            }
            
            static void CsvDemo(string csvFile)
            {
                Console.WriteLine("\nCSV Demo");
                var rows = new List<string>
                {
                    "Id,Name,Age",
                    "1,Alice,30",
                    "2,Bob,25",
                    "3,Charlie,35"
                };

                File.WriteAllLines(csvFile, rows);
                string[] csvLines = File.ReadAllLines(csvFile);
                if (csvLines.Length == 0)
                {
                    Console.WriteLine("CSV file is empty.");
                    return;
                }

                foreach (var line in csvLines)
                    Console.WriteLine(line);

                DataTable dataTable = new DataTable();

            
                string[] headers = csvLines[0].Split(',');
                foreach (string header in headers)
                {
                    dataTable.Columns.Add(header);
                }

                foreach (string line in csvLines.Skip(1))
                {
                    string[] data = line.Split(',');
                    DataRow dataRow = dataTable.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dataRow[i] = data[i];
                    }
                    dataTable.Rows.Add(dataRow);
                }
                PrintTable(dataTable);  
            }
            static void PrintTable(DataTable dt)
            {
                foreach (DataColumn col in dt.Columns)
                    Console.Write($"{col.ColumnName}\t");
                Console.WriteLine();

                foreach (DataRow row in dt.Rows)
                {
                    foreach (var item in row.ItemArray)
                        Console.Write(item + "\t");
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            
            static void JsonDemo(string jsonFile)
            {
                Console.WriteLine("\nJSON Demo");
                var person = new { Id = 1, Name = "Alice", Age = 30 };
                string json = JsonSerializer.Serialize(person);
                File.WriteAllText(jsonFile, json);

                string readJson = File.ReadAllText(jsonFile);   
                Console.WriteLine(readJson);
            }

            static void DriveDemo()
            {
                Console.WriteLine("\nDriveInfo Demo");
                foreach (var drive in DriveInfo.GetDrives())
                {
                    Console.WriteLine($"Drive {drive.Name} - {drive.DriveType}");
                    if (drive.IsReady)
                    {
                        Console.WriteLine($"  Volume: {drive.VolumeLabel}");
                        Console.WriteLine($"  Format: {drive.DriveFormat}");
                        Console.WriteLine($"  Size: {drive.TotalSize / (1024 * 1024 * 1024)} GB");
                        Console.WriteLine($"  Free: {drive.AvailableFreeSpace / (1024 * 1024 * 1024)} GB");
                    }
                }
            }
        }
    }
}
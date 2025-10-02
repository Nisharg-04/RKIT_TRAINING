using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemDemo
{
    public static class DirectoryProperties
    {
        public static void ShowDirectoryProperties(string path)
        {
            Console.WriteLine("Directory Properties Demo\n");

            Console.WriteLine($"Exists: {Directory.Exists(path)}");
            Console.WriteLine($"Full Path: {Path.GetFullPath(path)}");
            Console.WriteLine($"Name: {Path.GetFileName(path)}");
            Console.WriteLine($"Parent: {Directory.GetParent(path)?.FullName}");
        }
    }
}

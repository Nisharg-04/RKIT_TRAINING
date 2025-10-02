using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemDemo
{
    public static class DirectoryInfoProperties
    {
        public static void ShowDirectoryInfoProperties(string path)
        {
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Directory does not exist.");
                return;
            }

            DirectoryInfo dirInfo = new DirectoryInfo(path);

            Console.WriteLine("DirectoryInfo Properties Demo\n");

            Console.WriteLine($"Name: {dirInfo.Name}");
            Console.WriteLine($"FullName: {dirInfo.FullName}");
            Console.WriteLine($"Parent: {dirInfo.Parent}");
            Console.WriteLine($"Exists: {dirInfo.Exists}");
            Console.WriteLine($"Root: {dirInfo.Root}");
            Console.WriteLine($"CreationTime: {dirInfo.CreationTime}");
            Console.WriteLine($"LastAccessTime: {dirInfo.LastAccessTime}");
            Console.WriteLine($"LastWriteTime: {dirInfo.LastWriteTime}");
            Console.WriteLine($"Attributes: {dirInfo.Attributes}");
        }
    }
}

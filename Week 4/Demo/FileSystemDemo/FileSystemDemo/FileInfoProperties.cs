using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemDemo
{
    public static class FileInfoProperties
    {
        public static void ShowFileInfoProperties(string path)
        {

            FileInfo fi = new FileInfo(path);
            Console.WriteLine("FileInfo Properties Demo\n");
            Console.WriteLine($"Name: {fi.Name}");
            Console.WriteLine($"FullName: {fi.FullName}");
            Console.WriteLine($"DirectoryName: {fi.DirectoryName}");
            Console.WriteLine($"Directory: {fi.Directory}");
            Console.WriteLine($"Extension: {fi.Extension}");
            Console.WriteLine($"Exists: {fi.Exists}");
            Console.WriteLine($"IsReadOnly: {fi.IsReadOnly}");
            Console.WriteLine($"Length: {fi.Length} bytes");
            Console.WriteLine($"CreationTime: {fi.CreationTime}");
            Console.WriteLine($"LastAccessTime: {fi.LastAccessTime}");
            Console.WriteLine($"LastWriteTime: {fi.LastWriteTime}");
            Console.WriteLine($"Attributes: {fi.Attributes}");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemDemo
{
    public static class FileProperties
    {
        public static void ShowFileProperties(string path)
        {
            Console.WriteLine("File Properties Demo\n");

            Console.WriteLine($"Exists: {File.Exists(path)}");

            if (File.Exists(path))
            {
                FileInfo fi = new FileInfo(path);
                Console.WriteLine($"Creation Time: {File.GetCreationTime(path)}");
                Console.WriteLine($"Last Access Time: {File.GetLastAccessTime(path)}");
                Console.WriteLine($"Last Write Time: {File.GetLastWriteTime(path)}");
                Console.WriteLine($"Length: {fi.Length} bytes");
                Console.WriteLine($"Attributes: {File.GetAttributes(path)}");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemDemo
{
    public static class DirectoryMethods
    {
        public static void ShowDirectoryMethods(string path)
        {
            Console.WriteLine("Directory Methods Demo\n");

            // Create Directory
            string newDirPath = Path.Combine(path, "NewDirectory");
            Directory.CreateDirectory(newDirPath);
            Console.WriteLine($"Created directory: {newDirPath}");

            // GetDirectories
            string[] directories = Directory.GetDirectories(path);
            Console.WriteLine($"Directories in {path}:");
            foreach (var dir in directories)
            {
                Console.WriteLine(dir);
            }

            // GetFiles
            string[] files = Directory.GetFiles(path);
            Console.WriteLine($"Files in {path}:");
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }

            // GetLogicalDrives
            string[] drives = Directory.GetLogicalDrives();
            Console.WriteLine("Logical Drives:");
            foreach (var drive in drives)
            {
                Console.WriteLine(drive);
            }

            // Move Directory
            string movedDirPath = Path.Combine(path, "MovedDirectory");
            Directory.Move(newDirPath, movedDirPath);
            Console.WriteLine($"Moved directory to: {movedDirPath}");

            // Delete Directory
            Directory.Delete(movedDirPath, true);
            Console.WriteLine($"Deleted directory: {movedDirPath}");

            // SetCreationTime
            Directory.SetCreationTime(path, DateTime.Now);
            Console.WriteLine($"Updated creation time of {path}.");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemDemo
{
    public static class DirectoryInfoMethods
    {
        public static void ShowDirectoryInfoMethods(string path)
        {
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Directory does not exist.");
                return;
            }

            DirectoryInfo dirInfo = new DirectoryInfo(path);

            Console.WriteLine("DirectoryInfo Methods Demo\n");

            // Create Directory
            string newDirPath = Path.Combine(path, "NewDirectoryInfo");
            DirectoryInfo newDir = dirInfo.CreateSubdirectory("NewDirectoryInfo");
            Console.WriteLine($"Created directory: {newDir.FullName}");

            // GetDirectories
            DirectoryInfo[] directories = dirInfo.GetDirectories();
            Console.WriteLine($"Directories in {path}:");
            foreach (var dir in directories)
            {
                Console.WriteLine(dir.FullName);
            }

            // GetFiles
            FileInfo[] files = dirInfo.GetFiles();
            Console.WriteLine($"Files in {path}:");
            foreach (var file in files)
            {
                Console.WriteLine(file.FullName);
            }

            // MoveTo
            string movedPath = Path.Combine(path, "MovedDirectoryInfo");
            newDir.MoveTo(movedPath);
            Console.WriteLine($"Moved directory to: {movedPath}");

            // Delete
            DirectoryInfo movedDir = new DirectoryInfo(movedPath);
            movedDir.Delete(true);
            Console.WriteLine($"Deleted directory: {movedPath}");

            // Refresh
            dirInfo.Refresh();
            Console.WriteLine($"Refreshed DirectoryInfo for: {dirInfo.FullName}");

            // SetCreationTime
            dirInfo.CreationTime = DateTime.Now;
            Console.WriteLine($"Updated creation time for {dirInfo.FullName}.");
        }
    }
}

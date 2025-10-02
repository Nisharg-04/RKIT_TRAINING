using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemDemo
{
    public static class FileMethods
    {
        public static void ShowFileMethods(string path)
        {
            Console.WriteLine("File Methods Demo\n");

            
            string copyPath = Path.Combine(Path.GetDirectoryName(path), "copy_" + Path.GetFileName(path));
            File.Copy(path, copyPath, true);
            Console.WriteLine($"Copied to: {copyPath}");

            // Create a file
            string newFilePath = Path.Combine(Path.GetDirectoryName(path), "newfile.txt");
            File.WriteAllText(newFilePath, "Hello, this is a test file!");
            Console.WriteLine($"Created: {newFilePath}");

            // Append text
            File.AppendAllText(newFilePath, "\nAppended text.");
            Console.WriteLine("Appended text to file.");

            // Read all text
            string content = File.ReadAllText(newFilePath);
            Console.WriteLine($"Content of {newFilePath}:\n{content}");

            // Read all lines
            string[] lines = File.ReadAllLines(newFilePath);
            Console.WriteLine($"Lines in {newFilePath}: {lines.Length}");

            // Delete file
            File.Delete(newFilePath);
            Console.WriteLine($"Deleted: {newFilePath}");

            // Move file
            string movedPath = Path.Combine(Path.GetDirectoryName(path), "moved_" + Path.GetFileName(path));
            File.Move(copyPath, movedPath, true);
            Console.WriteLine($"Moved file to: {movedPath}");

            // Get creation time
            Console.WriteLine($"Creation Time: {File.GetCreationTime(path)}");

            // Get last access time
            Console.WriteLine($"Last Access Time: {File.GetLastAccessTime(path)}");

            // Get last write time
            Console.WriteLine($"Last Write Time: {File.GetLastWriteTime(path)}");

            // Set last write time
            File.SetLastWriteTime(path, DateTime.Now);
            Console.WriteLine("Updated Last Write Time to now.");
        }
    }

}

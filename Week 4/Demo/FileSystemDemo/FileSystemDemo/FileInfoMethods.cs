using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemDemo
{
    public static class FileInfoMethods
    {
        public static void ShowFileInfoMethods(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("File does not exist.");
                return;
            }

            FileInfo fi = new FileInfo(path);

            Console.WriteLine("FileInfo Methods Demo\n");

            // CopyTo
            string copyPath = Path.Combine(fi.DirectoryName!, "copy_" + fi.Name);
            fi.CopyTo(copyPath, true);
            Console.WriteLine($"Copied file to: {copyPath}");

            // Create
            string newFilePath = Path.Combine(fi.DirectoryName!, "newfile.txt");
            using (FileStream fs = fi.Create())
            {
                byte[] info = new System.Text.UTF8Encoding(true).GetBytes("Hello from FileInfo!");
                fs.Write(info, 0, info.Length);
            }
            Console.WriteLine($"Created file: {newFilePath}");

            // Delete
            if (File.Exists(newFilePath))
            {
                FileInfo newFile = new FileInfo(newFilePath);
                newFile.Delete();
                Console.WriteLine($"Deleted file: {newFilePath}");
            }

            // MoveTo
            string movePath = Path.Combine(fi.DirectoryName!, "moved_" + fi.Name);
            fi.MoveTo(movePath);
            Console.WriteLine($"Moved file to: {movePath}");

            // Refresh
            fi.Refresh();
            Console.WriteLine($"Refreshed FileInfo for: {fi.FullName}");

            // OpenRead
            using (FileStream fs = fi.OpenRead())
            {
                Console.WriteLine($"Opened {fi.Name} for reading. Length: {fs.Length} bytes.");
            }

            // OpenWrite
            using (FileStream fs = fi.OpenWrite())
            {
                Console.WriteLine($"Opened {fi.Name} for writing.");
            }

            // Set attributes example
            fi.Attributes = FileAttributes.ReadOnly;
            Console.WriteLine("Set file attribute to ReadOnly.");
        }
    }
}

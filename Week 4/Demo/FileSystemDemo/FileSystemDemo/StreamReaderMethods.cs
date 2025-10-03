using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemDemo
{
    public static class StreamReaderMethods
    {
        public static void ShowStreamReaderMethods(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist.");
                return;
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                Console.WriteLine("StreamReader Methods Demo\n");

                // Peek
                int peekChar = reader.Peek();
                Console.WriteLine($"Peek: {(peekChar != -1 ? (char)peekChar : ' ')}");

                // Read
                char[] buffer = new char[10];
                int readCount = reader.Read(buffer, 0, buffer.Length);
                Console.WriteLine($"Read {readCount} characters: {new string(buffer, 0, readCount)}");

                // ReadLine
                reader.DiscardBufferedData(); // Reset 
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                string line = reader.ReadLine() ?? "";
                Console.WriteLine($"ReadLine: {line}");

                // ReadToEnd
                reader.DiscardBufferedData();
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                string content = reader.ReadToEnd();
                Console.WriteLine($"ReadToEnd:\n{content}");

                // Close
                reader.Close();
                Console.WriteLine("StreamReader closed.");
            }
        }
    }
}

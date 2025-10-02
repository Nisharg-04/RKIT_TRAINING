using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemDemo
{
    public static class StreamReaderProperties
    {
        public static void ShowStreamReaderProperties(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist.");
                return;
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                Console.WriteLine("StreamReader Properties Demo\n");

                Console.WriteLine($"BaseStream: {reader.BaseStream}");
                Console.WriteLine($"CurrentEncoding: {reader.CurrentEncoding}");
                Console.WriteLine($"EndOfStream: {reader.EndOfStream}");
                Console.WriteLine($"BaseStream CanRead: {reader.BaseStream.CanRead}");
                Console.WriteLine($"BaseStream CanSeek: {reader.BaseStream.CanSeek}");
                Console.WriteLine($"BaseStream Length: {reader.BaseStream.Length} bytes");
            }
        }
    }
}

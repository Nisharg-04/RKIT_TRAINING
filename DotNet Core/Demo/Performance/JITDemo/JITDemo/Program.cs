using System.Diagnostics;
namespace JITDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
           

            var sw = Stopwatch.StartNew();

            Console.WriteLine("Hello from JIT app");

            sw.Stop();
            Console.WriteLine($"Startup time: {sw.ElapsedMilliseconds} ms");

            Console.ReadLine();

        }
    }
}
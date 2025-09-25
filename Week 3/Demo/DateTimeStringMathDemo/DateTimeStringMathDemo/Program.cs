using System.Text;

namespace DateTimeStringMathDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Year, Month, Day
            DateTime dt1 = new DateTime(2025, 9, 23);

            // Year, Month, Day, Hour, Minute, Second
            DateTime dt2 = new DateTime(2025, 9, 23, 18, 30, 45);

            Console.WriteLine(dt1); 
            Console.WriteLine(dt2);

            DateTime now = DateTime.Now;        
            DateTime utcNow = DateTime.UtcNow;  
            DateTime today = DateTime.Today;

            Console.WriteLine($"Now: {now}");
            Console.WriteLine($"UTC Now: {utcNow}");
            Console.WriteLine($"Today: {today}");

            Console.WriteLine($"Day of Week: {now.DayOfWeek}");
            Console.WriteLine($"Day of Year: {now.DayOfYear}");


            Console.WriteLine(now.AddDays(5)); // 5 days later
            Console.WriteLine(now.AddMonths(-2)); // 2 months back
            Console.WriteLine(now.ToString("yyyy-MM-dd HH:mm:ss")); // Custom format

            string s1 = "Hello";
            string s2 = "World";

            string s3 = s1 + " " + s2;
            string str = "Hello World";
            Console.WriteLine(str.Length);    
            Console.WriteLine(str[0]);         
            Console.WriteLine(string.Empty);    
            Console.WriteLine(string.IsNullOrEmpty(str)); 
            Console.WriteLine(string.IsNullOrWhiteSpace(" "));

            string s = "Hello";
            s.Replace("H", "J");
            Console.WriteLine(s); // "Hello"
            s = s.Replace("H", "J"); 
            Console.WriteLine(s); // "Jello"

            StringBuilder sb = new StringBuilder("Hello");
            sb.Append(" World");
            sb.Replace("Hello", "Hi");
            Console.WriteLine(sb); // "Hi World"

            double a = -7.5;
            double b = 16;

            Console.WriteLine("Abs: " + Math.Abs(a));          
            Console.WriteLine("Max: " + Math.Max(a, b));     
            Console.WriteLine("Pow: " + Math.Pow(2, 3));    
            Console.WriteLine("Sqrt: " + Math.Sqrt(b));     
            Console.WriteLine("Round: " + Math.Round(a));    
            Console.WriteLine("Ceiling: " + Math.Ceiling(a));
            Console.WriteLine("Floor: " + Math.Floor(a));    
            Console.WriteLine("Sin(π/2): " + Math.Sin(Math.PI / 2));
            Console.WriteLine("Log(100): " + Math.Log10(100));     


        }
    }
}

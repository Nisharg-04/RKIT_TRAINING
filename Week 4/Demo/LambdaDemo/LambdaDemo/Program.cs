namespace LambdaDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Func<int, int> square = x => x * x;
            Console.WriteLine(square(5)); // 25

            Func<int, int, int> add = (a, b) => a + b;
            Console.WriteLine(add(3, 4)); //  7

            Func<string> greet = () => "Hello, World!";
            Console.WriteLine(greet()); // Hello, World!

            Action<string> printMessage = msg =>
            {
                string decorated = $"*** {msg} ***";
                Console.WriteLine(decorated);
            };
            printMessage("Welcome Lambda"); // *** Welcome Lambda ***

            Func<int, string> evenOdd = x =>
            {
                if (x % 2 == 0) return "Even";
                return "Odd";
            };
            Console.WriteLine(evenOdd(5)); //  Odd


            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };

            var evens = numbers.Where(n => n % 2 == 0);
            Console.WriteLine(string.Join(", ", evens)); // 2, 4, 6


            Action<string> print = msg => Console.WriteLine(msg);
            print("Action Lambda"); // Output: Action Lambda



            Predicate<int> isPositive = x => x > 0;
            Console.WriteLine(isPositive(10)); // Output: True

            int factor = 3;
            Func<int, int> multiply = x => x * factor;

            Console.WriteLine(multiply(5)); // 15

            factor = 5; // Changing captured variable affects lambda
            Console.WriteLine(multiply(5)); // 25

            for(int i = 0; i < 3; i++)
            {
                int loopVar = i; // Capture loop variable
                Task.Run(() => Console.WriteLine(loopVar));
            }









        }
    }
}

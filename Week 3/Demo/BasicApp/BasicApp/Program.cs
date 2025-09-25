using System;
using System.Text;

namespace BasicApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int age = 30;
            Console.WriteLine("Age: " + age);
            Console.WriteLine("Size of int: " + sizeof(int));
            Console.WriteLine("Range of int: " + int.MinValue + " to " + int.MaxValue);
            Console.WriteLine();

            long nationalDebt = 33170000000000L;
            Console.WriteLine("National Debt: " + nationalDebt);
            Console.WriteLine("Size of long: " + sizeof(long));
            Console.WriteLine();

            short temperature = -5;
            Console.WriteLine("Temperature: " + temperature);
            Console.WriteLine("Size of short: " + sizeof(short));
            Console.WriteLine();

            byte red = 255;
            Console.WriteLine("Red Color Value: " + red);
            Console.WriteLine("Size of byte: " + sizeof(byte));
            Console.WriteLine();

            double pi = 3.1415926535;
            Console.WriteLine("Pi value: " + pi);
            Console.WriteLine("Size of double: " + sizeof(double));
            Console.WriteLine();

            float gravity = 9.8f;
            Console.WriteLine("Gravity: " + gravity);
            Console.WriteLine("Size of float: " + sizeof(float));
            Console.WriteLine();

            decimal balance = 1500.99m;
            Console.WriteLine("Account Balance: " + balance);
            Console.WriteLine("Size of decimal: " + sizeof(decimal));
            Console.WriteLine(); 
            //f - float
            //m - decimal
            //L - long
            //u - unsigned



            double d1 = 0.1, d2 = 0.2;
            Console.WriteLine("Double (0.1 + 0.2): " + (d1 + d2));
            decimal m1 = 0.1m, m2 = 0.2m;
            Console.WriteLine("Decimal (0.1 + 0.2): " + (m1 + m2));
            Console.WriteLine();

            bool isLoggedIn = true;
            Console.WriteLine("Is Logged In: " + isLoggedIn);
            Console.WriteLine("Size of bool: " + sizeof(bool));
            Console.WriteLine();

            char firstInitial = 'J';
            Console.WriteLine("First Initial: " + firstInitial);
            Console.WriteLine("Size of char: " + sizeof(char));
            Console.WriteLine();

            int original = 100;
            int copy = original;
            Console.WriteLine("Original: " + original + ", Copy: " + copy);
            copy = 200;
            Console.WriteLine("After changing copy → Original: " + original + ", Copy: " + copy);
            Console.WriteLine();

            string greeting = "Hello, World!";
            Console.WriteLine("Greeting: " + greeting);
            Console.WriteLine();

            string name = "John";
            Console.WriteLine("Name before: " + name);
            name = name + " Doe";
            Console.WriteLine("Name after: " + name);
            Console.WriteLine();

            object someValue = 123;
            Console.WriteLine("Boxed object: " + someValue);
            int unboxedValue = (int)someValue;
            Console.WriteLine("Unboxed value: " + unboxedValue);
            Console.WriteLine();

            StringBuilder builder1 = new StringBuilder("Start");
            StringBuilder builder2 = builder1;
            Console.WriteLine("Before change → " + builder1);
            builder2.Append(" End");
            Console.WriteLine("After change → " + builder1);
            Console.WriteLine();

            //int optionalScore = null; // error - cannot be null
            int? optionalScore = null;
            Console.WriteLine("Optional Score: " + optionalScore);
            optionalScore = 95;
            Console.WriteLine("Updated Score: " + optionalScore);
            int currentScore = optionalScore ?? 0;
            Console.WriteLine("Score using ?? operator: " + currentScore);
            Console.WriteLine();

            int myInt = 100;
            long myLong = myInt;
            Console.WriteLine("Implicit: int " + myInt + " to long " + myLong);

            double myDouble = 12345.6789;
            int myIntFromDouble = (int)myDouble;
            Console.WriteLine("Explicit: double " + myDouble + " to int " + myIntFromDouble);

            string numberStr = "500";
            int convertedInt = Convert.ToInt32(numberStr);
            Console.WriteLine("Convert string to int: " + convertedInt);

            try
            {
                string badStr = "RKIT";
                Convert.ToInt32(badStr);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Invalid conversion: " + ex.Message);
            }
        }
    }
}

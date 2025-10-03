using System.Reflection;

namespace ReflectionDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Console.WriteLine($"Full Name: {asm.FullName}");
            Console.WriteLine();

            Console.WriteLine("Types in Assembly");
            foreach (Type type in asm.GetTypes())
            {
                Console.WriteLine(type.FullName);
            }
            Console.WriteLine();

            Type personType = typeof(Person);
            Console.WriteLine("Type Info: Person");
            Console.WriteLine($"Name: {personType.Name}");
            Console.WriteLine($"Namespace: {personType.Namespace}");
            Console.WriteLine();

            Console.WriteLine("Properties");
            foreach (PropertyInfo prop in personType.GetProperties())
            {
                Console.WriteLine($"{prop.Name} ({prop.PropertyType})");
            }
            Console.WriteLine();


            Console.WriteLine("Method");
            foreach (MethodInfo method in personType.GetMethods(
                     BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                Console.WriteLine($"{method.Name} (Return: {method.ReturnType.Name})");
            }
            Console.WriteLine();


            Console.WriteLine("Creating Instance Dynamically");
            object personObj = Activator.CreateInstance(personType, "Alice", 25);


            PropertyInfo nameProp = personType.GetProperty("Name");
            Console.WriteLine($"Current Name: {nameProp.GetValue(personObj)}");

            nameProp.SetValue(personObj, "Bob");
            Console.WriteLine($"Updated Name: {nameProp.GetValue(personObj)}");
            Console.WriteLine();

            MethodInfo greetMethod = personType.GetMethod("Greet");
            greetMethod.Invoke(personObj, null);
            Console.WriteLine();

            //  private method
            MethodInfo secretMethod = personType.GetMethod("SecretMethod",
                BindingFlags.NonPublic | BindingFlags.Instance);
            secretMethod.Invoke(personObj, null);
            Console.WriteLine();

        }
    }
}

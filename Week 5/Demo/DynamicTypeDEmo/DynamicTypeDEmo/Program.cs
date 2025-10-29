namespace DynamicTypeDEmo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            dynamic x = 10;
            Console.WriteLine(x.GetType());  // System.Int32

            x = "Hello";
            Console.WriteLine(x.GetType());  // System.String

            x = new { Name = "Nisharg", Age = 21 };
            Console.WriteLine(x.Name);       // Works fine

            string json = "{\"Name\": \"Nisharg\", \"Age\": 21}";
            dynamic person = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            Console.WriteLine(person.Name);
            Console.WriteLine(person.Age);

        }
    }
}

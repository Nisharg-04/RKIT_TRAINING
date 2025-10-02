namespace GenericsDemo
{
   
   
    

    internal class Program
    {
        static void Main(string[] args)
        {
            //generic class
            Box<int> intBox = new Box<int>();
            intBox.Add(100);
            Console.WriteLine(intBox.Get()); // 100

            Box<string> strBox = new Box<string>();
            strBox.Add("Hello World");
            Console.WriteLine(strBox.Get()); // Hello World

            //genrric method
            int x = 5, y = 10;
            Utilities.Swap(ref x, ref y);
            Console.WriteLine($"x: {x}, y: {y}"); // x: 10, y: 5

            string s1 = "A", s2 = "B";
            Utilities.Swap(ref s1, ref s2);
            Console.WriteLine($"s1: {s1}, s2: {s2}"); // s1: B, s2: A

            //generic constraint
            Repository<Sample> repo = new Repository<Sample>();
            Sample s = repo.CreateInstance();
            Console.WriteLine(s.Id); // 100
        }
    }
}

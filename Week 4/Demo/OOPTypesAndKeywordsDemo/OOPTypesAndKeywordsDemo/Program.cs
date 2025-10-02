namespace OOPTypesAndKeywordsDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //class
            Person p = new Person("Nisharg", 21);
            p.Greet();

            //struct
            Point p1 = new Point(5, 10);
            p1.Display();

            //interface
            IShape shape = new Circle(5);
            Console.WriteLine($"Area: {shape.Area()}");
            Console.WriteLine($"Perimeter: {shape.Perimeter()}");

            //static
            Console.WriteLine(MathHelper.Pi);
            Console.WriteLine(MathHelper.Square(4));

            Utilities.PrintMessage("Hello World");

            //abstract
            Animal animal = new Dog();
            animal.MakeSound(); // Bark
            animal.Eat();       // Eating

            // virtual override
            SocialAnimal a = new Cat();
            a.MakeSound(); // Bark

            //new method hiding
            ParentClass obj1 = new ChildClass();
            obj1.Display(); // Base class display

            ChildClass obj2 = new ChildClass();
            obj2.Display(); // Derived class display

            //partial class
            Person1 per = new Person1 { Name = "Nisharg", Age = 25 };
            per.Display();
        }
    }
}

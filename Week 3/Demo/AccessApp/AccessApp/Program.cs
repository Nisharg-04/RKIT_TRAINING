using AccessLib;

namespace AccessApp
{
    class Program
    {
        static void Main()
        {
            DemoBase baseObj = new DemoBase();
            baseObj.ShowAccess();

            Console.WriteLine("\nAccessing from Main Program:");
            Console.WriteLine(baseObj.PublicField);             
            // Console.WriteLine(baseObj._privateField);       
            // Console.WriteLine(baseObj.ProtectedField);      
            // Console.WriteLine(baseObj.InternalField);       
            //Console.WriteLine(baseObj.ProtectedInternalField);
            // Console.WriteLine(baseObj.PrivateProtectedField);

            Console.WriteLine("\nTesting DerivedDemo:");
            DerivedDemo derived = new DerivedDemo();
            derived.ShowInherited();

            Console.WriteLine("\nInternal Class Test:");
            // InternalDemo demo = new InternalDemo();   
           

            DerivedInOtherAsssembly derivedInOtherAsssembly = new DerivedInOtherAsssembly();
            derivedInOtherAsssembly.CheckAccess();
        }
    }

    class DerivedInOtherAsssembly : DemoBase
    {
        public void CheckAccess() {
            Console.WriteLine("\nDerivedInOtherAsssembly ");

            Console.WriteLine(ProtectedInternalField);
            //Console.WriteLine(._privateField);       
             Console.WriteLine(ProtectedField);      
            //Console.WriteLine(InternalField);      
            // Console.WriteLine(PrivateProtectedField);
        }
    }
}

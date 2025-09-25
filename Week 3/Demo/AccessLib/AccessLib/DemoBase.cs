namespace AccessLib
{
    
    public class DemoBase
    {
        public string PublicField = "Public Field";
        private string _privateField = "Private Field";
        protected string ProtectedField = "Protected Field";
        internal string InternalField = "Internal Field";
        protected internal string ProtectedInternalField = "Protected Internal Field";
        private protected string PrivateProtectedField = "Private Protected Field";

        public void ShowAccess()
        {
            Console.WriteLine("Inside DemoBase:");
            Console.WriteLine(PublicField);
            Console.WriteLine(_privateField);
            Console.WriteLine(ProtectedField);
            Console.WriteLine(InternalField);
            Console.WriteLine(ProtectedInternalField);
            Console.WriteLine(PrivateProtectedField);
        }
    }

    // 🔸 Internal class – accessible only inside this assembly
    internal class InternalDemo
    {
        public string Info = "Internal Class Field";
    }

    // 🔸 Derived class to test inheritance
    public class DerivedDemo : DemoBase
    {
        public void ShowInherited()
        {
            Console.WriteLine("Inside DerivedDemo:");
            Console.WriteLine(PublicField);                
            // Console.WriteLine(_privateField);            // not allowed
            Console.WriteLine(ProtectedField);              
            Console.WriteLine(InternalField);               
            Console.WriteLine(ProtectedInternalField);                  
            Console.WriteLine(PrivateProtectedField);       
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPTypesAndKeywordsDemo
{
    public class SocialAnimal
    {
        public virtual void MakeSound()
        {
            Console.WriteLine("Generic Animal Sound");
        }
    }

    public class Cat : SocialAnimal
    {
        public override void MakeSound()
        {
            Console.WriteLine("Meow");
        }
    }
}

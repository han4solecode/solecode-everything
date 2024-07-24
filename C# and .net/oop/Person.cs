using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop
{
    public class Person
    {
        public virtual void PrintName()
        {
            Console.WriteLine("I am a person!");
        }
    }

    public class Trainer : Person
    {
        public override void PrintName()
        {
            Console.WriteLine("I am a trainer!");
        }
    }

    public class Student : Person
    {
        public override void PrintName()
        {
            Console.WriteLine("I am a student!");
        }
    }
}

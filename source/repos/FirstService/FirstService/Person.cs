using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstService
{
    public class Person
    {
        public string name;
        public void Introduce(string to)
        {
            Console.WriteLine("Hi {0}, I am {1}", name, to);
        }

        public static Person parse(string str)
        {
            var person = new Person() { name = str };
            return person;
        }
    }
}

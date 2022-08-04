using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheNIR
{
    internal class Dog : GameObject
    {
        public Dog(Coordinates location) : base(location, '@')
        {

        }

        public override void Speech()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Гаф, Блеадь");
            Console.ForegroundColor = ConsoleColor.Cyan;
        }
    }
}

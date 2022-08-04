using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheNIR
{
    internal class Comission : GameObject
    {
        private bool _alive;
        public Comission(Coordinates location) : base(location, 'W')
        {
            _alive = true;
        }

        public override void Speech()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Отойди дурак");
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        internal bool IsAlive()
        {
            return _alive;
        }

        internal void Pass()
        {
            _alive = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheNIR
{
    internal class Scientific : GameObject
    {

        public Scientific(Coordinates location) : base(location, '$')
        {

        }
        public override void Speech()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Ты пришёл за подписью? А у тебя всё готово, сынок ?");
            Console.ForegroundColor = ConsoleColor.Cyan;
        }
    }
}

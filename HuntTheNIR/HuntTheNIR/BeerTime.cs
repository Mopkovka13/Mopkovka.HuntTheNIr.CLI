using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheNIR
{
    internal class BeerTime : GameObject
    {
        public BeerTime(Coordinates location) : base(location, 'O')
        {
        }

        public override void Speech()
        {
            Random random = new Random();
            List<string> Fraze = new List<string>()
            {
                "По стопарику, братишка ?!",
                "От одной банки пива ничего не будет",
                "Да нормально, не очкуй, я так 100 раз делал",
                "Давно не видели тебя на кострище, ты заходи если чо"
            };

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Fraze[random.Next(0, Fraze.Count)]);
            Console.ForegroundColor = ConsoleColor.Cyan;
        }
    }
}

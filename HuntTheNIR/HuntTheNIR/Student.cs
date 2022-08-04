using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheNIR
{
    public class Student : GameObject
    {
        const byte COUNT_SIGNATURE = 3;
        private bool _alive;
        private byte _signature;

        public Student(Coordinates location) : base(location, '♂')
        {
            _alive = true;
            _signature = 0;
        }

        public bool IsAlive()
        {
            return _alive;
        }
        public override void Speech()
        {
            Console.WriteLine("Ну дайте сдать ;(");
        }
        internal void Move(Map map, Comission comission) //Карта в параметрах только для проверки корректности хода
        {
            ConsoleKeyInfo UserInput = Console.ReadKey(true);
            switch (UserInput.Key)
            {
                case ConsoleKey.W:
                    if(map.CorrectMove(Location.Y, Location.X - 1)) //Если ход корректен
                    {
                        Location.X--; //Ходим
                        //map.SpeechNear(Location.Y, Location.X); //Слушаем, что происходит вокруг
                        MetSomebody(map.WhoIsHere(Location.Y, Location.X), map); //Встретил кого-то (кого ?!)
                    }
                    break;
                case ConsoleKey.S:
                    if (map.CorrectMove(Location.Y, Location.X + 1))
                    {
                        Location.X++;
                        //map.SpeechNear(Location.Y, Location.X);
                        MetSomebody(map.WhoIsHere(Location.Y, Location.X), map);
                    }  
                    break;
                case ConsoleKey.A:
                    if (map.CorrectMove(Location.Y - 1, Location.X))
                    {
                        Location.Y--;
                        //map.SpeechNear(Location.Y, Location.X);
                        MetSomebody(map.WhoIsHere(Location.Y, Location.X), map);
                    }
                    break;
                case ConsoleKey.D:
                    if (map.CorrectMove(Location.Y + 1, Location.X))
                    {
                        Location.Y++;
                        //map.SpeechNear(Location.Y, Location.X);
                        MetSomebody(map.WhoIsHere(Location.Y, Location.X), map);
                    }
                    break;
                case ConsoleKey.Spacebar:
                    if(_signature < COUNT_SIGNATURE) 
                    {
                        Console.WriteLine("Недостаточно подписей");
                    }
                    else
                    {
                        PassNIR(map, comission);
                    }
                    break;
            }
        }
        private void PassNIR(Map map, Comission comission)
        {
            ConsoleKeyInfo UserInput = Console.ReadKey(true);
            switch (UserInput.Key)
            {
                case ConsoleKey.W:
                    Location.X--;
                    if(map.WhoIsHere(Location.Y, Location.X) =='W')
                    {
                        comission.Pass();
                    }
                    else
                    {
                        _signature = 0;
                        map.AddScientific();
                    }
                    break;
                case ConsoleKey.S:
                    Location.X++;
                    if (map.WhoIsHere(Location.Y, Location.X) == 'W')
                    {
                        comission.Pass();
                    }
                    else
                    {
                        _signature = 0;
                        map.AddScientific();
                    }
                    break;
                case ConsoleKey.A:
                    Location.Y--;
                    if (map.WhoIsHere(Location.Y, Location.X) == 'W')
                    {
                        comission.Pass();
                    }
                    else
                    {
                        _signature = 0;
                        map.AddScientific();
                    }
                    break;
                case ConsoleKey.D:
                    Location.Y++;
                    if (map.WhoIsHere(Location.Y, Location.X) == 'W')
                    {
                        comission.Pass();
                    }
                    else
                    {
                        _signature = 0;
                        map.AddScientific();
                    }
                    break;
            }
            Console.SetCursorPosition(1, 30);
            Console.WriteLine($"Подписей: {_signature} / 3");
            Console.SetCursorPosition(0, 0);
        }
        private void MetSomebody(char character, Map map) //
        {
            if (character == 'W')
            {
                if(_signature<3)
                {
                    _alive = false;
                    Console.Clear();
                    Console.WriteLine("Вы пришли на комиссию без подписей.\n" +
                                      "Это провал. Вы отчислены!"); // Продумать текст
                }else
                {
                    _alive = false;
                    Console.Clear();
                    Console.WriteLine("Вы собрали все подписи, но незачем было подходить так близко.\n" + 
                                      "Комиссия подумала, что вы хотите дать взятку.\n" +
                                      "\n" +
                                      "ВЫ ОТЧИСЛЕНЫ !"); // Продумать текст
                }
                
            }
            else if(character=='$')
            {
                _signature++;
                Console.SetCursorPosition(1, 30);
                Console.WriteLine($"Подписей: {_signature} / 3");
                Console.SetCursorPosition(0, 0);
            }
            else if(character=='@')
            {
                while(_signature>0)
                {
                    _signature--;
                    map.AddScientific();
                    Console.SetCursorPosition(1, 30);
                    Console.WriteLine($"Подписей: {_signature} / 3");
                    Console.SetCursorPosition(0, 0);
                }
            }
            else if(character=='O')
            {
                Location = map.GetCorrectCoordinates();
            }
                
        }
    }
}

using SlashScreen;
using System.Media;
using System.Windows.Media;

namespace HuntTheNIR
{
    internal class Game
    {
        private Map _map;
        private Student _student;
        private Comission _comission;
        public Game()
        {
            _map = new Map();
        }
        public void Start()
        {
            Console.WriteLine("Поставь шрифт 18\n" + "Консоль во весь экран\n" + "После чего нажми любую кнопку!");
            Console.ReadKey();
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Привет, дружище, надеюсь, ты сегодня сдашь научку (;");
            SplashScreen splashScreen = new SplashScreen(@"C:\Users\Evgen\OneDrive\Рабочий стол\HuntTheNIR\SlashScreen\CcdP.gif", @"C:\Users\Evgen\OneDrive\Рабочий стол\HuntTheNIR\SlashScreen\welcome.wav");
            splashScreen.Start();
            Console.ReadKey(true);
            Console.CursorVisible = false;

            ShowRules(); //Показать правила
            

            if (OperatingSystem.IsWindows()) //Музыка на шиндовсе
            {
                SoundPlayer player = new SoundPlayer("sound8bit.wav");
                player.Load();
                player.PlayLooping();
               
            }

            _map.Create();
            FillMap();
            

            Console.SetCursorPosition(1, 30);
            Console.WriteLine($"Подписей: 0 / 3");
            Console.SetCursorPosition(0, 0);

            var left1 = Console.CursorLeft; //
            var top1 = Console.CursorTop;
            
            do //Цикл игры
            {
                Console.SetCursorPosition(left1, top1);
                Console.WriteLine("Добро пожаловать на Матфак!");
                PrintMap(_map.RenderMap()); 
                _student.Move(_map, _comission);
                _map.SpeechNear(_student.Location.Y, _student.Location.X);
            } while (_student.IsAlive() && _comission.IsAlive());
            
            if(_comission.IsAlive()==false)
            {
                Console.Clear();
                Console.WriteLine("Happy END");
            }
            else
            {

            }
        }

        public void FillMap()
        {
            

            _comission = new Comission(_map.GetCorrectCoordinates());
            _map.AddObject(_comission);

            for (int i = 0; i < 3; i++)
                _map.AddScientific();


            int countDog = ((_map.Height()+1) * (_map.Width()+1)) / 50;
            for(int i = 0; i < countDog; i++)
                _map.AddObject(new Dog(_map.GetCorrectCoordinates()));

            int countBeerTime = ((_map.Height() + 1) * (_map.Width() + 1)) / 100;
            for (int i = 0; i < countBeerTime; i++)
                _map.AddObject(new BeerTime(_map.GetCorrectCoordinates()));

            _student = new Student(_map.GetCorrectCoordinates());
            _map.AddObject(_student);
        }


        public void PrintOpenMap(char[,] field)
        {   
            for (int i = 0; i < field.GetLength(0);i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                    Console.Write($"[{field[i, j]}]");

                Console.WriteLine();
            }
        }
        public void PrintMap(char[,] field)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if(field[i, j]!= '♂')
                        Console.Write("[ ]");
                    else
                        Console.Write($"[{field[i, j]}]");
                }
                Console.WriteLine();
            }

        }

        public static void ShowRules()
        {
            Console.WriteLine(@"
                                                                                                                        
                                                                                                                        
  ____    ____                                       __________ ___                            ___      ______________    
  `MM'    `MM'                                       MMMMMMMMMM `MM                            `MM\     `M'`MM`MMMMMMMb.  
   MM      MM                      /                 /   MM   \  MM                             MMM\     M  MM MM    `Mb  
   MM      MM ___   ___ ___  __   /M                     MM      MM  __     ____                M\MM\    M  MM MM     MM  
   MM      MM `MM    MM `MM 6MMb /MMMMM                  MM      MM 6MMb   6MMMMb               M \MM\   M  MM MM     MM  
   MMMMMMMMMM  MM    MM  MMM9 `Mb MM                     MM      MMM9 `Mb 6M'  `Mb              M  \MM\  M  MM MM    .M9  
   MM      MM  MM    MM  MM'   MM MM                     MM      MM'   MM MM    MM              M   \MM\ M  MM MMMMMMM9'  
   MM      MM  MM    MM  MM    MM MM                     MM      MM    MM MMMMMMMM              M    \MM\M  MM MM  \M\    
   MM      MM  MM    MM  MM    MM MM                     MM      MM    MM MM                    M     \MMM  MM MM   \M\   
   MM      MM  YM.   MM  MM    MM YM.  ,                 MM      MM    MM YM    d9              M      \MM  MM MM    \M\  
  _MM_    _MM_  YMMM9MM__MM_  _MM_ YMMM9                _MM_    _MM_  _MM_ YMMMM9              _M_      \M _MM_MM_    \M\_
                                                                                                                        
                                                                                                                        
                                                                                                                        
");

            Console.WriteLine(" Правила: \n" +
                              " 1. Тебе необходимо собрать все подписи, прежде чем ты попадешься комиссии.\n" +
                              " 2. Если ты собрал все подписи, найди комиссию и сдай уже этот НИР.\n" +
                              " 3. Не попадись Хулиганам, иначе они испортят работу и придётся заново собирать подписи.\n");

            Console.WriteLine(" Управление: \n"+
                              " WASD - для перемещения\n" +
                              " SPACE - для сдачи научной работы,\n" +
                              "         но не подходи слишком близко,\n" +
                              "         а-то подумают что-то не то ;(");

            Console.ReadKey();
            Console.Clear();
        }
    }
}

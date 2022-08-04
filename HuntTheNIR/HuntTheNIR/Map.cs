using System.Text;


namespace HuntTheNIR
{
    internal class Map
    {
        const int MaxWidth = 20;
        const int MinWidth = 10;
        const int MaxHeight = 20;
        const int MinHeight = 10;

        private int _width;
        private int _height;
        private char[,] _field;
        private List<GameObject> _gameObjects = new List<GameObject>();

        public int Height() { return _height; }
        public int Width() { return _width; }
        public static bool CorrectSize(int width, int height)
        {
            if(width < MinWidth || height < MinHeight || width > MaxWidth || height > MaxHeight)
                return false;
            return true;
        }
        public bool CorrectMove(int width, int height)
        {
            if (width < 0 || height < 0 || width > _width || height > _height)
                return false;
            return true;
        }
        public Coordinates GetCorrectCoordinates()
        {
            Coordinates result = new Coordinates();
            Random random = new Random();
            bool success = false;

            do
            {
                result.Y = random.Next(0, _width);
                result.X = random.Next(0, _height);
                if (_gameObjects.All(s => s.Location.Y != result.Y && s.Location.X != result.X))
                    success = true;
            }while (success==false);
            return result;
        }
        public char[,] RenderMap()
        {
            for (int x = 0; x <= _height; x++)
            {
                for (int y = 0; y <= _width; y++)
                    _field[x, y] = ' ';
            }
            foreach (var gameObject in _gameObjects)
            {
                _field[gameObject.Location.X, gameObject.Location.Y] = gameObject.Avatar;
            }
            return _field;
        }
        /// <summary>
        /// Это функция не должна иметь доп.логики, но я устал уже делать это приложение
        /// Поэтому помимо того, что она возвращает кто это, она удаляет с карты научников после подписи
        /// </summary>
        public char WhoIsHere(int width, int height) //Если кто-то есть в клетке - true
        {
            var left1 = Console.CursorLeft;
            var top1 = Console.CursorTop;
            GameObject SearchGameObject;
            foreach (var gameObject in _gameObjects)
                if (gameObject.Avatar!='♂' && gameObject.Location.X == height && gameObject.Location.Y == width)
                {
                    SearchGameObject = gameObject;
                    if (gameObject.Avatar == '$')
                        _gameObjects.Remove(SearchGameObject);
                    return SearchGameObject.Avatar;
                }
            return '♂';
        }
        public void SpeechNear(int width, int height)//кто вокруг клетки (голоса)
        {//X == height , Y == width
            var left1 = Console.CursorLeft;
            var top1 = Console.CursorTop;
            Console.SetCursorPosition(left1, top1);

            StringBuilder speechCleaning = new StringBuilder(""); //Пустая строка на ширину консоли
            for (int i = 0; i < Console.WindowWidth; i++)
                speechCleaning.Append(" ");
            for(int i = 0; i < 5;i++) //Очистка после карты (желательно убрать хард код)
            {
                Console.WriteLine(speechCleaning);
            }
            Console.SetCursorPosition(left1, _height + 2);

            if (height == _height || _field[height + 1, width] == ' ') { } //низ
            else
            {
                foreach (var gameObject in _gameObjects)
                    if (gameObject.Location.X == height + 1 && gameObject.Location.Y == width)
                        gameObject.Speech();
            }

            if (width == _width || height == _height || _field[height + 1, width + 1] == ' ') { } //правый нижний
            else
            {
                foreach (var gameObject in _gameObjects)
                    if (gameObject.Location.X == height + 1 && gameObject.Location.Y == width + 1)
                        gameObject.Speech();
            }

            if (width == _width || _field[height, width + 1] == ' ') { } //правый
            else
            {
                foreach (var gameObject in _gameObjects)
                    if (gameObject.Location.X == height && gameObject.Location.Y == width + 1)
                        gameObject.Speech();
            }

            if (width == _width || height == 0 || _field[height - 1, width + 1] == ' ') { } //правый верхний
            else
            {
                foreach (var gameObject in _gameObjects)
                    if (gameObject.Location.X == height - 1 && gameObject.Location.Y == width + 1)
                        gameObject.Speech();
            }

            if (height == 0 || _field[height - 1, width] == ' ') { } //верхний
            else
            {
                foreach (var gameObject in _gameObjects)
                    if (gameObject.Location.X == height - 1 && gameObject.Location.Y == width)
                        gameObject.Speech();
            }

            if (height == 0 || width == 0 || _field[height - 1, width - 1] == ' ') { } //левый верхний
            else
            {
                foreach (var gameObject in _gameObjects)
                    if (gameObject.Location.X == height - 1 && gameObject.Location.Y == width - 1)
                        gameObject.Speech();
            }

            if (width == 0 || _field[height, width - 1] == ' ') { } //левый
            else
            {
                foreach (var gameObject in _gameObjects)
                    if (gameObject.Location.X == height && gameObject.Location.Y == width - 1)
                        gameObject.Speech();
            }

            if (width == 0 || height == _height || _field[height + 1, width - 1] == ' ') { } // левый нижний
            else
            {
                foreach (var gameObject in _gameObjects)
                    if (gameObject.Location.X == height + 1 && gameObject.Location.Y == width - 1)
                        gameObject.Speech();
            }
        }

        public void AddObject(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }


        public void AddScientific()
        {
            AddObject(new Scientific(GetCorrectCoordinates()));
        }
        public void Create()
        {
            bool CorrectWidth, CorrectHeight;
            int heightMap, widthMap;
            do
            {
                Console.WriteLine($"Введите значения от {MinWidth} до {MaxWidth}: ");
                Console.Write("Количество этажей: ");
                CorrectWidth = Int32.TryParse(Console.ReadLine(), out heightMap);
                Console.Write("Сколько кабинетов на каждом: ");
                CorrectHeight = Int32.TryParse(Console.ReadLine(), out widthMap);
                Console.Clear();
            } while (!CorrectWidth || !CorrectHeight || !Map.CorrectSize(widthMap, heightMap));

            _width = widthMap - 1;
            _height = heightMap - 1;
            _field = new char[heightMap, widthMap];
            for (int i = 0; i < heightMap; i++)
                for (int j = 0; j < widthMap; j++)
                    _field[i, j] = ' ';

            Console.Clear();
        }
    }
}

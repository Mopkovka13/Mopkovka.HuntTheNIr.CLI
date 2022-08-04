using System.Drawing.Imaging;
using System.Media;

namespace SlashScreen
{
    public class SplashScreen
    {
        const double WIDTH_OFFSET = 2.5;
        const int MAX_WIDTH = 130;
        private Bitmap _gif;
        private SoundPlayer? _player;
        private bool _workStatus = false; //false - выключен / true - включен
        public SplashScreen(string gifPath, string playerPath)
        {
            _gif = new Bitmap(gifPath);
            _player = new SoundPlayer(playerPath);
        }
        public SplashScreen(string gifPath)
        {
            _gif = new Bitmap(gifPath);
        }
        public void Start()
        {
            int countRepeat = 0;
            Image[] bitMaps = CreateFramesArray(_gif);
            Console.CursorVisible = false;
            if (OperatingSystem.IsWindows() && _player!=null) //Музыка на шиндовсе
            {
                _player.Load();
                _player.Play();
            }
            do
            {
                for (int i = 0; i < bitMaps.Length; i++) //Плохо что константа)
                {
                    Console.SetCursorPosition(0, 1);
                    Bitmap bitmap = new Bitmap(bitMaps[i]);
                    ShowFrame(ref bitmap);
                    Thread.Sleep(35);
                }
                countRepeat++;
            } while (countRepeat != 10);
            
            Console.Clear();
        }

        internal void Stop()
        {
            if (_workStatus == false) //Если уже остановлен - исключение
            {
                throw new InvalidOperationException();
            }
            else
            {
                _workStatus = false;
            }
        }
        private static void ShowFrame(ref Bitmap bitmap)
        {
            bitmap = ResizeBitmap(bitmap);
            bitmap.ToGrayScale();
            var convertor = new BitmapToASCIIConverter(bitmap);
            Console.Write(convertor.Convert());
            bitmap.Dispose();
            Console.SetCursorPosition(0, 0);

        }

        private static Image[] CreateFramesArray(Image img)
        {
            List<Image> images = new List<Image>();
            int length = img.GetFrameCount(FrameDimension.Time);
            for (int i = 0; i < length; i++)
            {
                img.SelectActiveFrame(FrameDimension.Time, i);
                images.Add(new Bitmap(img));
            }
            return images.ToArray();
        }

        private static Bitmap ResizeBitmap(Bitmap bitmap)
        {
            var newHeight = bitmap.Height / WIDTH_OFFSET * MAX_WIDTH / bitmap.Width;
            if (bitmap.Width > MAX_WIDTH || bitmap.Height > newHeight)
                bitmap = new Bitmap(bitmap, new Size(MAX_WIDTH, (int)newHeight));
            return bitmap;
        }
    }
}
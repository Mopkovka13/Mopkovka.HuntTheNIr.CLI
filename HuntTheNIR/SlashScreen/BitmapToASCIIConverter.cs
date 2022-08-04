using System.Drawing.Imaging;
using System.Media;


namespace SlashScreen
{
    public class BitmapToASCIIConverter
    {
        private readonly char[] _asciiTable = { '.', ',', ':', '+', '*', '?', '%', '$', '#', '@' };
        private readonly Bitmap _bitmap;
        public BitmapToASCIIConverter(Bitmap bitmap)
        {
            _bitmap = bitmap;
        }
        public string Convert()
        {
            var result = "";
            int mapIndex;

            for(int y = 0;y < _bitmap.Height;y++)
            {
                for(int x = 0; x < _bitmap.Width;x++)
                {
                    mapIndex = (int)Map(_bitmap.GetPixel(x, y).R, 0, 255, 0, _asciiTable.Length - 1);
                    result += _asciiTable[mapIndex];
                }
                result += "\n";
            }
            return result;
        }
        private float Map(float valueToMap, float start1, float stop1, float start2, float stop2)
        {
            return ((valueToMap - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
        }
    }
}

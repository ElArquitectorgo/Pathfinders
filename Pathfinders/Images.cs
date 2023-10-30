using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Pathfinders
{
    public static class Images
    {
        public readonly static ImageSource Empty = LoadImage("Empty.png");
        public readonly static ImageSource Start = LoadImage("Start.png");
        public readonly static ImageSource End = LoadImage("End.png");
        public readonly static ImageSource Path = LoadImage("Path.png");
        public readonly static ImageSource Wall = LoadImage("Wall.png");

        private static ImageSource LoadImage(string fileName)
        {
            return new BitmapImage(new Uri($"Assets/{fileName}", UriKind.Relative));
        }
    }
}

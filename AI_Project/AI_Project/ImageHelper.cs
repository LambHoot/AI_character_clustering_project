using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AI_Project
{
    class ImageHelper
    {
        public static Bitmap normalizeImage(string path)
        {
            Bitmap myBitmap = new Bitmap(path);
            return normalizeImageSize(normalizeImageColor(myBitmap));
        }

        public static Bitmap normalizeImageColor(Bitmap myBitmap)
        {
            for (int i = 0; i < myBitmap.Height; i++)
            {
                for (int j = 0; j < myBitmap.Width; j++)
                {
                    if (myBitmap.GetPixel(i, j) != Color.White)
                        myBitmap.SetPixel(i, j, Color.Black);
                }
            }
            return myBitmap;
        }

        //from http://www.deltasblog.co.uk/code-snippets/c-resizing-a-bitmap-image/
        public static Bitmap normalizeImageSize(Bitmap myBitmap)
        {
            Bitmap result = new Bitmap(42, 42);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(myBitmap, 0, 0, 42, 42);
            return result;
        }


    }
}

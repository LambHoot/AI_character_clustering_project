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
        public static Bitmap normalizeImage_42(string path)
        {
            Bitmap myBitmap = new Bitmap(path);
            return normalizeImageSize(normalizeImageColor(myBitmap), 42);
        }

        public static Bitmap normalizeImageColor(Bitmap myBitmap)
        {
            for (int i = 0; i < myBitmap.Width; i++)
            {
                for (int j = 0; j < myBitmap.Height; j++)
                {
                    //if (myBitmap.GetPixel(i, j).R < Color.White.R)
                        //myBitmap.SetPixel(i, j, Color.Black);
                }
            }
            return myBitmap;
        }

        //from http://www.deltasblog.co.uk/code-snippets/c-resizing-a-bitmap-image/
        public static Bitmap normalizeImageSize(Bitmap myBitmap, int size)
        {
            Bitmap result = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(myBitmap, 0, 0, size, size);
            return result;
        }
    }
}
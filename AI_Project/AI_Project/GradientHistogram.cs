using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AI_Project
{
    class GradientHistogram
    {
    }




    public class SectionedImage
    {
        Bitmap imageBitmap;
        List<ImageBlock> blocks;

        public SectionedImage(Bitmap bm)
        {
            imageBitmap = bm;
            generateBlocks();
        }


        public void generateBlocks()
        {
            for(int i =0; i < imageBitmap.Height; i += 7)
            {
                for (int j = 0; j < imageBitmap.Width; j += 7)
                {
                    ImageBlock ib = new ImageBlock(Copy(imageBitmap, new Rectangle(i,j,7,7)), i%7, j%7);
                    blocks.Add(ib);
                }
            }
        }

        //from https://msdn.microsoft.com/en-us/library/aa457087.aspx
        static public Bitmap Copy(Bitmap srcBitmap, Rectangle section)
        {
            // Create the new bitmap and associated graphics object
            Bitmap bmp = new Bitmap(section.Width, section.Height);
            Graphics g = Graphics.FromImage(bmp);

            // Draw the specified section of the source bitmap to the new one
            g.DrawImage(srcBitmap, 0, 0, section, GraphicsUnit.Pixel);

            // Clean up
            g.Dispose();

            // Return the bitmap
            return bmp;
        }


    }



    public class ImageBlock
    {
        Bitmap blockBitmap;
        int x, y;//coordinates in parent image
        List<float> gxs, gys, magnitudes, directions;

        public ImageBlock(Bitmap bm, int x, int y)
        {
            this.x = x;
            this.y = y;
            blockBitmap = bm;
            calculateGradientStuff();
        }

        public void calculateGradientStuff()
        {
            for (int i = 0; i < blockBitmap.Height; i++)
            {
                for (int j = 0; j < blockBitmap.Width; j++)
                {
                    
                }
            }

        }


    }
}

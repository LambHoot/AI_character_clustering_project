﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace AI_Project
{
    class GradientHistogram
    {

        public static List<SectionedImage> ExtractFeaturesForImages(List<string> paths)
        {
            List<SectionedImage> sectionedImages = new List<SectionedImage>();
            foreach (string p in paths)
            {
                sectionedImages.Add(new SectionedImage(ImageHelper.normalizeImage_42(p), p));
            }
            return sectionedImages;
        }



    }


    public class SectionedImage
    {
        Bitmap imageBitmap;
        public List<ImageBlock> blocks = new List<ImageBlock>();
        public float x = 0, y = 0;//for entire image vector
        public string path = "";

        public SectionedImage(Bitmap bm, string path)
        {
            imageBitmap = bm;
            generateBlocks();
            GetTotalVector();
            this.path = path;
        }


        public void generateBlocks()
        {
            float maxMag = 0, minMag = 10000000;
            for(int i =0; i < imageBitmap.Width; i += 7)
            {
                for (int j = 0; j < imageBitmap.Height; j += 7)
                {
                    ImageBlock ib = new ImageBlock(Copy(imageBitmap, new Rectangle(i,j,7,7)), i%7, j%7);
                    blocks.Add(ib);
                    if (ib.blockMagnitude > maxMag)
                        maxMag = ib.blockMagnitude;
                    if (ib.blockMagnitude < minMag)
                        minMag = ib.blockMagnitude;
                }
            }
            //normalize all blockVectors
            foreach(ImageBlock ib in blocks)
            {
                ib.NormalizeBlockMagnitude(minMag, maxMag);
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

        public void GetTotalVector()
        {
            foreach (ImageBlock ib in blocks)
            {
                x += ib.gxSum;
                y += ib.gySum;
            }
        }


    }



    public class ImageBlock
    {
        Bitmap blockBitmap;
        int x, y;//coordinates in parent image
        List<float> gxs = new List<float>() , gys = new List<float>(), magnitudes = new List<float>(), directions = new List<float>();
        public float gxSum, gySum, blockMagnitude, blockDirection;

        public ImageBlock(Bitmap bm, int x, int y)
        {
            this.x = x;
            this.y = y;
            blockBitmap = bm;
            calculateGradientStuff();
        }

        public void calculateGradientStuff()
        {
            for (int i = 0; i < blockBitmap.Width; i++)
            {
                for (int j = 0; j < blockBitmap.Height; j++)
                {
                    float gx = GetPixelCustom(i - 1, j + 1) + 2 * GetPixelCustom(i, j + 1) + GetPixelCustom(i + 1, j + 1)
                        - GetPixelCustom(i - 1, j - 1) - 2 * GetPixelCustom(i, j - 1) - GetPixelCustom(i + 1, j - 1);
                    float gy = GetPixelCustom(i - 1, j - 1) + 2 * GetPixelCustom(i-1, j) + GetPixelCustom(i - 1, j + 1)
                        - GetPixelCustom(i + 1, j - 1) - 2 * GetPixelCustom(i + 1, j) - GetPixelCustom(i + 1, j + 1);
                    float g = (float)Math.Sqrt((gx*gx)+(gy*gy));
                    float dir = (float)Math.Atan(gy / gx);
                    gxs.Add(gx);
                    gys.Add(gy);
                    magnitudes.Add(g);
                    directions.Add(dir);
                }
            }
            //you got all the lists filled out!
            gxSum = gxs.Sum();
            gySum = gys.Sum();
            blockMagnitude = (float)Math.Sqrt((gxSum * gxSum) + (gySum * gySum));
            blockDirection = (float)Math.Atan(gySum / gxSum);
        }


        public int GetPixelCustom(int i, int j)
        {
            if (i < 0 || i >= blockBitmap.Width)
                return 0;
            if (j < 0 || j >= blockBitmap.Height)
                return 0;
            //if (blockBitmap.GetPixel(i, j) != Color.White)
            //return 1;
            Color pixelColor = blockBitmap.GetPixel(i, j);
            int colorDistance = -(pixelColor.R - Color.White.R);
            return colorDistance;
        }

        public void NormalizeBlockMagnitude(float min, float max)
        {
            blockMagnitude = (blockMagnitude - min) / (max - min);
        }


    }
}

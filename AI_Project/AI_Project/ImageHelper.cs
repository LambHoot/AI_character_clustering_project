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
                    if (myBitmap.GetPixel(i, j) != Color.White)
                        myBitmap.SetPixel(i, j, Color.Black);
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

        public static void support_vector_recognition(Bitmap bm)
        {
            #region Feature Extraction
            int[] top = new int[bm.Size.Width], bottom = new int[bm.Size.Width], left = new int[bm.Size.Width], right = new int[bm.Size.Width];
            #region Edge Detection Scans
            //bottom: 
            // i: left-to-right, 
            // j: bottom-to-top
            for (int i = 0; i < bm.Size.Width; i++)
            {
                for (int j = 0; j < bm.Height; j++)
                {
                    if (bm.GetPixel(i, j).Equals(Color.Black))
                    {
                        bottom[i] = j;
                        break;
                    }
                }
            }

            //top: 
            // i: left-to-right, 
            // j: top-to-bottom
            for (int i = 0; i < bm.Size.Width; i++)
            {
                for (int j = bm.Height - 1; j >= 0; j--)
                {
                    if (bm.GetPixel(i, j).Equals(Color.Black))
                    {
                        top[i] = j;
                        break;
                    }
                }
            }

            //left: 
            // j: top-to-bottom
            // i: left-to-right, 
            for (int j = bm.Height - 1; j >= 0; j--)
            {
                for (int i = 0; i < bm.Size.Width; i++)
                {
                    if (bm.GetPixel(i, j).Equals(Color.Black))
                    {
                        left[j] = i;
                        break;
                    }
                }
            }

            //right: 
            // j: top-to-bottom
            // i: right-to-left, 
            for (int j = bm.Height - 1; j >= 0; j--)
            {
                for (int i = bm.Size.Width; i >= 0; i--)
                {
                    if (bm.GetPixel(i, j).Equals(Color.Black))
                    {
                        right[j] = i;
                        break;
                    }
                }
            }
            #endregion

            /*
             * For each signal (curve), after some smoothing by median filtering (with window size 3
            pixel), one-dimensional derivative is computed (Figure 4), smoothed, and sampled. Here the sampling
            rate of 1/4 is used
            */

            #region Median Filter
            OneD_win3_MedianFilter(ref top);
            OneD_win3_MedianFilter(ref bottom);
            OneD_win3_MedianFilter(ref left);
            OneD_win3_MedianFilter(ref right);
            #endregion

            #region TODO: 1-D Derivative

            #endregion

            #region TODO: Smoothing

            #endregion

            #region TODO: Sampling
            //rate 1/4
            #endregion

            #endregion

            #region SVM

            #endregion
        }

        public static void OneD_win3_MedianFilter(ref int[] signal)
        {
            //Previous, Current, Next
            int[] window = new int[3];
            for (int i = 0; i < signal.Length; i++)
            {
                window[0] = signal[i == 0 ? i : i - 1];
                window[1] = signal[i];
                window[2] = signal[i == signal.Length ? i : i + 1];

                Array.Sort(window);
                signal[i] = window[1]; //take the median
            }
        }



    }
}
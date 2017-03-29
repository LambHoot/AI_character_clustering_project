using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AI_Project
{
    /**
     * NOTE: XOR IMAGES and test that way
     * ALSO, IMPLEMENT STRATEGY PATTEN
     */

    static class XOR
    {
        public static void runXOR(string[] trainingPaths, string[] testingPaths)
        {
            #region Training
            Dictionary<string, float[,]> trained = new Dictionary<string, float[,]>();
            string currentNum = "-1";
            foreach (string path in trainingPaths)
            {
                Bitmap b = ImageHelper.normalizeImageSize(new Bitmap(path), 64);
                if (path.Substring(42, 1).Equals(currentNum))
                {
                    var toSmash = trained[currentNum];
                    smash(ref toSmash, b);
                    trained[currentNum] = toSmash;
                }
                else
                {
                    currentNum = path.Substring(42, 1);
                    var toSmash = new float[64, 64];

                    smash(ref toSmash, b);

                    trained.Add(currentNum, toSmash);
                }
            }
            //Normalization
            List<string> trainKeys = new List<string>();
            foreach (string key in trained.Keys)
            {
                trainKeys.Add(key);
            }

            foreach (string key in trainKeys)
            {
                trained[key] = normalize(trained[key]);
            }

            #endregion

            #region Testing

            #endregion
        }

        public static void smash(ref float[,] map, Bitmap bm)
        {
            for(int y = 0; y < bm.Size.Height; y++)
            {
                for(int x = 0; x < bm.Size.Width; x++)
                {
                    map[x, y] += bm.GetPixel(x, y).R > Color.Gray.R? 1 : 0;
                }
            }
        }

        public static float[,] normalize(float[,] map)
        {
            //acquire sum
            float total = 0;
            for(int x = 0; x < map.GetLength(0); x++)
            {
                for(int y = 0; y < map.GetLength(1); y++)
                {
                    total += map[x, y];
                }
            }
            //normalize by sum
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] /= total;
                }
            }

            return map;
        }
    }
}

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
            Console.WriteLine("Training...");

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
            Console.WriteLine("Testing...");


            currentNum = "0";
            var success = 0.0f;
            var sum = 1.0f;
            foreach (string path in trainingPaths)
            {
                Bitmap b = ImageHelper.normalizeImageSize(new Bitmap(path), 64);
                var tuple = test(ref trained, b);
                
                Console.WriteLine("File {0}\n appears to be a {1} with confidence {2}", path, tuple.Item1, tuple.Item2);

                if (path.Substring(42, 1).Equals(currentNum))
                {
                    sum++;
                    success += path.Substring(42, 1).Equals(tuple.Item1) ? 1 : 0;
                }
                else
                {
                    Console.WriteLine("\n==={0} recognized with accuracy {1}===\n", currentNum, success / sum);
                    currentNum = path.Substring(42, 1);
                    sum = 1.0f;
                    success = path.Substring(42, 1).Equals(tuple.Item1) ? 1 : 0;
                }
            }
            Console.WriteLine("\n==={0} recognized with accuracy {1}===\n", currentNum, success / sum);
            #endregion
        }

        private static Tuple<string, float> test(ref Dictionary<string, float[,]> training, Bitmap test)
        {
            Tuple<string, float> choice = new Tuple<string, float>("none", -1f);
            foreach (string key in training.Keys)
            {
                var current = getConfidence(training[key], ref test);
                if (current > choice.Item2)
                    choice = new Tuple<string, float>(key, current);
            }
            return choice;
        }

        private static float getConfidence(float[,] map, ref Bitmap test)
        {
            float confidence = 0.0f;
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    confidence += test.GetPixel(x, y).R > Color.Gray.R ? map[x, y] : 0.0f;
                }
            }
            return confidence;
        }

        private static void smash(ref float[,] map, Bitmap bm)
        {
            for (int y = 0; y < bm.Size.Height; y++)
            {
                for (int x = 0; x < bm.Size.Width; x++)
                {
                    map[x, y] += bm.GetPixel(x, y).R > Color.Gray.R ? 1 : 0;
                }
            }
        }

        private static float[,] normalize(float[,] map)
        {
            //acquire sum
            float total = 0;
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
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

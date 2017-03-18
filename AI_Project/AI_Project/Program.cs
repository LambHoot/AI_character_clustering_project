using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] testing = Directory.GetFiles("..\\..\\Resources\\Testing", "*.*", SearchOption.AllDirectories);
            string[] training = Directory.GetFiles("..\\..\\Resources\\Training", "*.*", SearchOption.AllDirectories);
            GradientHistogram.ExtractFeaturesForImages(testing.ToList<string>());
            KNN knn = new KNN();
            knn.RunKNN(training.ToList<string>(), testing.ToList<string>());
        }
    }
}

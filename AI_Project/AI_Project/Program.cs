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

            XOR.runXOR(training, testing);

            /**
            GradientHistogram.ExtractFeaturesForImages(testing.ToList<string>());
            KNN knn = new KNN();
            knn.RunKNN(training.ToList<string>(), testing.ToList<string>());
            float acc = HeuristicAccuracy(knn.ClassifiedImagePairs);

            KNNwithBayes knnb = new KNNwithBayes();
            knnb.RunKNNwithBayes(training.ToList<string>(), testing.ToList<string>());
            */
        }

        public static float HeuristicAccuracy(List<ClassifiedTestingImage> ClassifiedImagePairs)
        {
            float acc = 0;
            foreach (ClassifiedTestingImage cti in ClassifiedImagePairs)
            {
                string[] training = cti.classifiedTrainingImage.path.Split('\\');
                string[] testing = cti.testingImage.path.Split('\\');
                if (training[5].Equals(testing[5]))
                    acc++;
            }
            acc = acc / ClassifiedImagePairs.Count();
            return acc;
        }
    }
}

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
            //KNN knn = new KNN();
            //knn.RunKNN(training.ToList<string>(), testing.ToList<string>());
            //float acc = HeuristicAccuracy(knn.ClassifiedImagePairs);

            KNNwithBayes knnb = new KNNwithBayes();
            knnb.RunKNNwithBayes(training.ToList<string>(), testing.ToList<string>());


            //CLUSTERING
            ClusteringStrategy edc = new EuclideanDistanceClustering();
            ClusteringStrategy hdc = new HammingDistanceClustering();
            ClusteringStrategy shdc = new SuperHammingDistanceClustering();
            string[] allCharacters = Directory.GetFiles("..\\..\\Resources\\Training", "*.*", SearchOption.AllDirectories);
            Clustering.DoClustering(edc, GradientHistogram.ExtractFeaturesForImages(allCharacters.ToList<string>()), 1000f);
            //Clustering.DoClustering(hdc, GradientHistogram.ExtractFeaturesForImages(allCharacters.ToList<string>()), 1080f);
            //Clustering.DoClustering(shdc, GradientHistogram.ExtractFeaturesForImages(allCharacters.ToList<string>()), 12500f);



        }






        public static float HeuristicAccuracy(List<ClassifiedTestingImage> ClassifiedImagePairs)
        {
            float acc = 0;

            string outputPath = "..\\..\\Resources\\Output\\Classification";
            //clear all files in directory
            //taken from http://stackoverflow.com/questions/1288718/how-to-delete-all-files-and-folders-in-a-directory
            DirectoryInfo di = new DirectoryInfo(outputPath);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
            string newPath = "";
            for (int i = 0; i < 10; i++)
            {
                newPath = outputPath + "\\" + i;
                Directory.CreateDirectory(newPath);
            }

            int iterator = 0;
            foreach (ClassifiedTestingImage cti in ClassifiedImagePairs)
            {
                string[] training = cti.classifiedTrainingImage.path.Split('\\');
                string[] testing = cti.testingImage.path.Split('\\');

                File.Copy(cti.testingImage.path, outputPath + "\\" + training[5] + "\\"+ iterator + ".tif", true);

                if (training[5].Equals(testing[5]))
                    acc++;
            }
            acc = acc / ClassifiedImagePairs.Count();
            return acc;
        }
    }
}

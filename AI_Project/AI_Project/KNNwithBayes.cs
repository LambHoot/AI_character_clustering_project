using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI_Project;
using System.IO;

namespace AI_Project
{
    public class KNNwithBayes
    {
        public List<SectionedImage> trainingImages = new List<SectionedImage>(), testingImages = new List<SectionedImage>();
        public List<ClassifiedTestingImage> ClassifiedImagePairs = new List<ClassifiedTestingImage>();
        public float KnnBaccuracy = 0;
        //trainingImages one for each character

        public void RunKNNwithBayes(List<string> trainingImagesPaths, List<string> testingImagesPaths)
        {
            ExtractFeatures(trainingImagesPaths, testingImagesPaths);
        }

        public void ExtractFeatures(List<string> trainingImagesPaths, List<string> testingImagesPaths)
        {
            trainingImages = GradientHistogram.ExtractFeaturesForImages(trainingImagesPaths);
            List<List<ImageBlockFeature>> trainingFeatures = new List<List<ImageBlockFeature>>();

            testingImages = GradientHistogram.ExtractFeaturesForImages(testingImagesPaths);
            List<List<ImageBlockFeature>> testingFeatures = new List<List<ImageBlockFeature>>();

            //make list of training features
            //organized in lists of lists of imageblocks with classes, based on block order
            //ex) one list for all images top left block, one list for all images top right block, etc...
            int blockNum = 0;
            while (blockNum < 36) {
                List<ImageBlockFeature> blockList = new List<ImageBlockFeature>();
                foreach (SectionedImage si in trainingImages)
                {
                    blockList.Add(new ImageBlockFeature(si.blocks[blockNum], si.path.Split('\\')[5]));
                }
                blockNum++;
                trainingFeatures.Add(blockList);
            }

            //get testing scores
            //create output list
            List<string[]> outputList = new List<string[]>();
            foreach (SectionedImage si in testingImages)
            {
                List<string> currentClasses = new List<string>();
                for (int b = 0; b < trainingFeatures.Count(); b++)
                {
                    float minEucDist = 999999999;
                    string currentClass = "";
                    foreach (ImageBlockFeature ibf in trainingFeatures.ElementAt(b))
                    {
                        float newEucDist = getEuclideanDistance(si.blocks.ElementAt(b), ibf.block);
                        if (newEucDist < minEucDist)
                        {
                            minEucDist = newEucDist;
                            currentClass = ibf.classifiedDigit;
                        }
                    }
                    currentClasses.Add(currentClass);
                }
                //which number appears the most
                //from http://stackoverflow.com/questions/355945/find-the-most-occurring-number-in-a-listint
                string mostCommonClass = currentClasses.GroupBy(i => i).OrderByDescending(grp => grp.Count())
                    .Select(grp => grp.Key).First();
                //add number and path tupple to output list
                outputList.Add(new string[] { si.path.Split('\\')[5], mostCommonClass, si.path });
            }
            //compute accuracy in output list
            float accuracy = HeuristicAccuracy(outputList);

        }

        public float HeuristicAccuracy(List<string[]> testingAndTrainingPairs)
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
            foreach (string[] cti in testingAndTrainingPairs)
            {
                File.Copy(cti[2], outputPath + "\\" + cti[1] + "\\"+ iterator + ".tif", true);
                if (cti[0].Equals(cti[1]))
                    acc++;
                iterator++;
            }
            KnnBaccuracy = acc / testingAndTrainingPairs.Count();
            return KnnBaccuracy;
        }

        public float getEuclideanDistance(ImageBlock i1, ImageBlock i2)
        {
            return (float)Math.Sqrt(Math.Pow(i1.gxSum + i2.gxSum, 2) + Math.Pow(i1.gySum + i2.gySum, 2));
        }

        public int getProbOfClass(List<List<ImageBlockFeature>> features, string classDigit)
        {
            int countClass = 0;
            int countTotal = 0;
            foreach(List<ImageBlockFeature> lbf in features)
            {
                foreach (ImageBlockFeature ibf in lbf)
                {
                    if (ibf.classifiedDigit.Equals(classDigit))
                        countClass++;
                    countTotal++;
                }
            }
            return countClass / countTotal;
        }


    }


    public class ImageBlockFeature {
        public ImageBlock block;
        public string classifiedDigit;

        public ImageBlockFeature(ImageBlock ib, string digit)
        {
            this.block = ib;
            this.classifiedDigit = digit;
        }

    }




}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI_Project;

namespace AI_Project
{
    public class KNNwithBayes
    {
        public List<SectionedImage> trainingImages = new List<SectionedImage>(), testingImages = new List<SectionedImage>();
        public List<ClassifiedTestingImage> ClassifiedImagePairs = new List<ClassifiedTestingImage>();
        //trainingImages one for each character

        public List<ClassifiedTestingImage> RunKNNwithBayes(List<string> trainingImagesPaths, List<string> testingImagesPaths)
        {
            return null;
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

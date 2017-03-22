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

        public List<ClassifiedTestingImage> RunKNN(List<string> trainingImagesPaths, List<string> testingImagesPaths)
        {

        }

        public void ExtractFeatures(List<string> trainingImagesPaths, List<string> testingImagesPaths)
        {
            trainingImages = GradientHistogram.ExtractFeaturesForImages(trainingImagesPaths);
            List<ImageBlockFeature> trainingFeatures = new List<ImageBlockFeature>();

            testingImages = GradientHistogram.ExtractFeaturesForImages(testingImagesPaths);
            List<ImageBlockFeature> testingFeatures = new List<ImageBlockFeature>();

            //make list of training features
            foreach(SectionedImage si in trainingImages)
            {
                foreach(ImageBlock)


                trainingFeatures.Add(new ImageBlockFeature(si));


                si.path;
            }




        }


    }


    public class ImageBlockFeature{
        public ImageBlock block;
        public string classifiedDigit;

        ImageBlockFeature(ImageBlock ib, string digit)
        {
            this.block = ib;
            this.classifiedDigit = digit;
        }

    }



}

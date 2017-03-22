using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Project
{
    public class KNN
    {
        public List<SectionedImage> trainingImages = new List<SectionedImage>(), testingImages = new List<SectionedImage>();
        public List<ClassifiedTestingImage> ClassifiedImagePairs = new List<ClassifiedTestingImage>();
        //trainingImages one for each character

        public List<ClassifiedTestingImage> RunKNN(List<string> trainingImagesPaths, List<string> testingImagesPaths)
        {
            ExtractFeatures(trainingImagesPaths, testingImagesPaths);
            //have all training and testing sectionedImages with features

            //for each testingImage, calculate its euclidean distance to every trainingImage
            //the trainingImage that it has the shortest distance to is the one it will be classified as
            ClassifiedTestingImage pair = new ClassifiedTestingImage();
            foreach(SectionedImage testImage in testingImages)
            {
                pair = new ClassifiedTestingImage(testImage, trainingImages.First());
                foreach(SectionedImage trainImage in trainingImages)
                {
                    if (getEuclideanDistance(testImage, trainImage) < getEuclideanDistance(pair.testingImage, pair.classifiedTrainingImage))
                        pair.classifiedTrainingImage = trainImage;
                }
                ClassifiedImagePairs.Add(pair);
            }
            return ClassifiedImagePairs;
        }

        public void ExtractFeatures(List<string> trainingImagesPaths, List<string> testingImagesPaths)
        {
            trainingImages = GradientHistogram.ExtractFeaturesForImages(trainingImagesPaths);
            testingImages = GradientHistogram.ExtractFeaturesForImages(testingImagesPaths);
        }

        public float getEuclideanDistance(SectionedImage i1, SectionedImage i2)
        {
            return (float)Math.Sqrt(Math.Pow(i1.x + i2.x, 2) + Math.Pow(i1.y + i2.y, 2));
        }
    }
    public class ClassifiedTestingImage
    {
        public SectionedImage testingImage, classifiedTrainingImage;

        public ClassifiedTestingImage()
        {
        }

        public ClassifiedTestingImage(SectionedImage ti, SectionedImage classified)
        {
            this.testingImage = ti;
            this.classifiedTrainingImage = classified;
        }
    }



}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AI_Project
{
    class Clustering
    {


        public static void DoClustering(ClusteringStrategy cs, List<SectionedImage> sectionedImages, float minSimilarity)
        {
            List<Cluster> clusterList = new List<Cluster>();
            foreach(SectionedImage image in sectionedImages)
            {
                int similarClusterIndex = -1;
                float currentClusterSimilarity = minSimilarity + 1;
                for(int i = 0; i < clusterList.Count(); i++)
                {
                    float nextSimilarity = cs.getSimilarity(image, clusterList[i].center);
                    if (nextSimilarity < currentClusterSimilarity && nextSimilarity <= minSimilarity)
                    {
                        currentClusterSimilarity = nextSimilarity;
                        similarClusterIndex = i;
                    }
                }
                //for each image
                if(similarClusterIndex != -1)
                {
                    //if the image matched a cluster, add to it and update the cluster
                    clusterList[similarClusterIndex].addImage(image, cs);
                    //then see if any clusters can be merged
                    for(int i = 0; i < clusterList.Count(); i++)
                    {
                        for (int j = i+1; j < clusterList.Count(); j++)
                        {
                            if(cs.getSimilarity(clusterList[i].center, clusterList[j].center) <= minSimilarity)
                            {
                                clusterList.Add(Cluster.mergeClusters(clusterList[i], clusterList[j], cs));
                                clusterList.RemoveAt(j);
                                clusterList.RemoveAt(i);
                            }
                        }
                    }
                }
                else
                {
                    Cluster newImageCluster = new Cluster(image);
                    clusterList.Add(newImageCluster);
                }
            }
            //all images processed and added to clusters at this point

            var x = clusterList;

            //pass something to draw it as a graph, gg done
            ClusteringOutput(clusterList);
        }

        public static void ClusteringOutput(List<Cluster> clusters)
        {
            string outputPath = "..\\..\\Resources\\Output\\Clustering";
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

            int iterator = 0;
            string newPath = "";
            foreach(Cluster c in clusters)
            {
                newPath = outputPath + "\\" + iterator;
                Directory.CreateDirectory(newPath);
                int imageIterator = 0;
                foreach (SectionedImage image in c.images)
                {
                    File.Copy(image.path, newPath + "\\" + imageIterator + ".tif", true);
                    imageIterator++;
                }
                iterator++;

            }

        }

        public class Cluster
        {
            public SectionedImage center;
            public List<SectionedImage> images;

            public Cluster(SectionedImage center)
            {
                this.center = center;
                images = new List<SectionedImage>();
                images.Add(center);
            }

            public void addImage(SectionedImage si, ClusteringStrategy cs)
            {
                images.Add(si);
                UpdateCenter(cs);
            }

            public void UpdateCenter(ClusteringStrategy cs)
            {
                float currentCenterSimilarity = 0;
                int bestCenterIndex = images.IndexOf(center);
                var x = bestCenterIndex;
                foreach(SectionedImage image in images)
                {
                    currentCenterSimilarity += cs.getSimilarity(center, image);
                }

                for(int i = 0; i < images.Count(); i++)
                {
                    float iImageSimilarity = 0;
                    for(int j = 0; j < images.Count(); j++)
                    {
                        iImageSimilarity += cs.getSimilarity(images[i], images[j]);
                    }
                    if(iImageSimilarity < currentCenterSimilarity)
                    {
                        currentCenterSimilarity = iImageSimilarity;
                        bestCenterIndex = i;
                    }
                }
                center = images[bestCenterIndex];
            }

            public static Cluster mergeClusters(Cluster c1, Cluster c2, ClusteringStrategy cs)
            {
                Cluster returnedCluster = new Cluster(c1.images[0]);
                for (int i = 1; i < c1.images.Count(); i++)
                    returnedCluster.images.Add(c1.images[i]);
                for (int i = 0; i < c2.images.Count(); i++)
                    returnedCluster.images.Add(c2.images[i]);
                returnedCluster.UpdateCenter(cs);
                return returnedCluster;
            }


        }

    }
}

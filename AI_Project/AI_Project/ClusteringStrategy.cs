using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Project
{
    interface ClusteringStrategy
    {
        float getSimilarity(SectionedImage i1, SectionedImage i2);//lower score means more similar
    }


    class EuclideanDistanceClustering : ClusteringStrategy
    {

        public float getSimilarity(SectionedImage i1, SectionedImage i2)
        {
            return (float)Math.Sqrt(Math.Pow(i1.x + i2.x, 2) + Math.Pow(i1.y + i2.y, 2));
        }
    }

}

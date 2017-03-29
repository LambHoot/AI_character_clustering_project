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


    class HammingDistanceClustering : ClusteringStrategy
    {

        public float getSimilarity(SectionedImage i1, SectionedImage i2)
        {
            float hammingDistance = 0;
            for(int i = 0; i < i1.blocks.Count(); i++)
            {
                ImageBlock i1Block = i1.blocks[i];
                ImageBlock i2Block = i2.blocks[i];
                hammingDistance += Math.Abs(i1Block.GetPixelCustom(4, 4) - i2Block.GetPixelCustom(4, 4));
            }
            return hammingDistance;
        }
    }

    class SuperHammingDistanceClustering : ClusteringStrategy
    {
        float alpha = 0.5f;
        float beta = 3f;
        public float getSimilarity(SectionedImage i1, SectionedImage i2)
        {
            float superHammingDistance = 0;
            for (int i = 0; i < i1.blocks.Count(); i++)
            {
                ImageBlock i1Block = i1.blocks[i];
                ImageBlock i2Block = i2.blocks[i];
                for(int w = 0; w < 7; w++)
                {
                    for (int h = 0; h < 7; h++)
                    {
                        superHammingDistance += Math.Abs(i1Block.GetPixelCustom(w, h) - i2Block.GetPixelCustom(w, h));
                    }
                }
            }
            return superHammingDistance;
        }
    }

}

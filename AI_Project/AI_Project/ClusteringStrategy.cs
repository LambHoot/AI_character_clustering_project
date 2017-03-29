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
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykovProject.Model.Data
{
    public class CostableObject
    {
        public int cost;
        public float saleCoefficient;

        public CostableObject(int cost, float saleCoefficient)
        {
            this.cost = cost;
            this.saleCoefficient = saleCoefficient;
        }
    }
}

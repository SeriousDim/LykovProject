using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykovProject.Model.Data.InfraClasses
{
    public class FurnaceInfra : AbstractInfrastucture
    {
        public FurnaceInfra(string name, Sprite sprite) : base(name, InfraType.LAND, sprite, "buildFurnace")
        {

        }

        public override void OnTick()
        {

        }
    }
}

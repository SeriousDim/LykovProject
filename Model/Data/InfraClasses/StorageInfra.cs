using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykovProject.Model.Data.InfraClasses
{
    public class StorageInfra : AbstractInfrastucture
    {
        public StorageInfra(string name, Sprite sprite) : base(name, InfraType.LAND, sprite, "buildStorage", 100)
        {
            this.amount = 10000;
        }

        public override void OnTick()
        {
            
        }

        public override void AddRawMaterial(Material mat)
        {
            lock (locker)
            {
                if (mat != null)
                {
                    if (rawMaterials.Count < amount)
                    {
                        rawMaterials.Add(mat);
                        if (owner != null)
                            owner.money += mat.relativeValue;
                    }
                }
            }
        }

        // логика этого здания
    }
}

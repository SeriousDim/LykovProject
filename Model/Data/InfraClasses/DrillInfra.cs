using LykovProject.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LykovProject.Model.Data.InfraClasses
{
    public class DrillInfra : AbstractInfrastucture, IProducing
    {
        private Timer timer;

        private int speedInMs;
        private Random random;

        public DrillInfra(string name, Sprite sprite, int speed) : base(name, InfraType.LAND, sprite, "buildDrill", 15)
        {
            this.amount = 100;
            this.speedInMs = speed;
            this.random = new Random();

            this.timer = new Timer(
                    new TimerCallback((obj) =>
                    {
                        ExtractMaterial();
                        if (rawMaterials.Count > 0)
                            Send(rawMaterials.Last());
                    }), null, 0, speedInMs);
        }

        public void ExtractMaterial()
        {
            if (bottomLeftPosition != null && bottomLeftPosition.ore != null)
                AddRawMaterial(Produce(null));
        }

        public override void OnTick()
        {
            
        }

        public Material Produce(Material[] from)
        {
            return (Material)bottomLeftPosition.ore.resultMaterial.Clone();
        }

        public void Send(AbstractInfrastucture to, Material mat)
        {
            if (to != null)
            {
                to.AddRawMaterial(mat);
                rawMaterials.Remove(mat);
            }
        }

        public void Send(Material mat)
        {
            if (neighbors.Count > 0)
            {
                if (neighbors.Count > 0)
                {
                    var cell = neighbors.Keys.ElementAt(random.Next(0, neighbors.Count));
                    var toInfra = neighbors[cell];

                    if (toInfra != null && !(toInfra is DrillInfra))
                        Send(toInfra, mat);
                }
            }
        }
    }
}

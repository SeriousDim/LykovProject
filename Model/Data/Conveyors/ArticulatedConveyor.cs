using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykovProject.Model.Data.Conveyors
{
    public class ArticulatedConveyor : Conveyor
    {
        public ArticulatedConveyor(string name, Sprite sprite, float speed, System.Drawing.PointF dir) : base(InfraType.LAND, name, sprite, speed, dir)
        {

        }
    }
}

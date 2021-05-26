using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LykovProject.Data.Interfaces;
using LykovProject.View;

namespace LykovProject.Model.Data
{
    public class Conveyor : AbstractInfrastucture
    {
        public static PointF LEFT = new PointF(1, 0);
        public static PointF RIGHT = new PointF(-1, 0);
        public static PointF UP = new PointF(0, -1);
        public static PointF DOWN = new PointF(0, 1);

        public Dictionary<Material, PointF> matCoords;

        private float transportSpeedInPxPerMs;
        private PointF direction;

        public Conveyor(InfraType type, string name, Sprite sprite, float speed, PointF direction) : base(name, type, sprite, 10, "")
        {
            this.type = type;
            this.name = name;
            this.sprite = sprite;
            this.transportSpeedInPxPerMs = speed;

            this.matCoords = new Dictionary<Material, PointF>();
            this.direction = direction;
        }

        /*public Conveyor(InfraType type, string name, Sprite sprite, float speed, PointF direction) : base(name, type, sprite, "")
        {
            this.type = type;
            this.name = name;
            this.sprite = sprite;
            this.transportSpeedInPxPerMs = speed;

            this.matCoords = new Dictionary<Material, PointF>();
            this.direction = direction;
        }*/

        public override void OnTick()
        {
            foreach (var m in rawMaterials)
            {
                MoveMaterial(m);
            }
        }

        public PointF GetStartMaterialWorldPoint()
        {
            var x = bottomLeftPosition.x;
            var y = bottomLeftPosition.y;
            var cs = Graphx.CellSize;

            if (direction.Equals(LEFT))
                return new PointF(x * cs, (y + 0.5f) * cs);
            if (direction.Equals(RIGHT))
                return new PointF((x + 1) * cs, (y + 0.5f) * cs);
            if (direction.Equals(UP))
                return new PointF((x + 0.5f) * cs, (y + 1) * cs);
            return new PointF((x + 0.5f) * cs, y * cs);
        }

        public void MoveMaterial(Material mat)
        {
            if (matCoords.ContainsKey(mat))
            {
                matCoords[mat] += new Size((int)(direction.X * transportSpeedInPxPerMs), (int)(direction.Y * transportSpeedInPxPerMs));
            }
        }

        public override void AddRawMaterial(Material mat)
        {
            if (mat != null)
            {
                if (rawMaterials.Count < amount)
                {
                    rawMaterials.Add(mat);
                    matCoords.Add(mat, GetStartMaterialWorldPoint());
                }
            }
        }
    }
}

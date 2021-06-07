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
    public class Conveyor : AbstractInfrastucture, IProducing
    {
        public static PointF LEFT = new PointF(0, -1);
        public static PointF RIGHT = new PointF(0, 1);
        public static PointF UP = new PointF(-1, 0);
        public static PointF DOWN = new PointF(1, 0);

        private float transportSpeedInPxPerMs;
        private PointF direction;

        public Conveyor(InfraType type, string name, Sprite sprite, float speed, PointF direction) : base(name, type, sprite, 10, "")
        {
            this.type = type;
            this.name = name;
            this.sprite = sprite;
            this.transportSpeedInPxPerMs = speed;

            this.price = 15;

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
            lock (locker)
            {
                foreach (var m in IterateMaterials())
                {
                    if (IsPointInBounds(m.point))
                        MoveMaterial(m);
                }

                var toRemove = new List<Material>();
                foreach (var m in IterateMaterials())
                {
                    if (!IsPointInBounds(m.point))
                        Send(m, toRemove);
                }

                foreach (var m in toRemove)
                    rawMaterials.Remove(m);
            }
        }

        public PointF GetStartMaterialWorldPoint()
        {
            var x = bottomLeftPosition.x;
            var y = bottomLeftPosition.y;
            var cs = (float)Graphx.CellSize;

            if (direction.Equals(LEFT))
                return new PointF((x + 1) * cs - 2, (y + 0.5f) * cs);
            if (direction.Equals(RIGHT))
                return new PointF(x * cs + 2, (y + 0.5f) * cs);
            if (direction.Equals(UP))
                return new PointF((x + 0.5f) * cs, (y + 1) * cs - 2);
            return new PointF((x + 0.5f) * cs, y * cs + 2);
        }

        public bool IsPointInBounds(PointF p)
        {
            var cs = Graphx.CellSize;
            var x = this.bottomLeftPosition.x * cs;
            var y = this.bottomLeftPosition.y * cs;

            return p.X >= x && p.X < x + cs &&
                   p.Y >= y && p.Y < y + cs;
        }

        public void MoveMaterial(Material mat)
        {
            var old = mat.point;
            mat.SetPosition(new PointF() { X = old.X + direction.Y * transportSpeedInPxPerMs, Y = old.Y + direction.X * transportSpeedInPxPerMs });
        }

        public override void AddRawMaterial(Material mat)
        {
            lock (locker)
            {
                if (mat != null)
                {
                    if (rawMaterials.Count < amount)
                    {
                        mat.SetPosition(GetStartMaterialWorldPoint());
                        rawMaterials.Add(mat);
                    }
                }
            }
        }

        public Material Produce(Material[] from)
        {
            return null;
        }

        public void Send(AbstractInfrastucture to, Material mat)
        {
            throw new NotImplementedException("Use Send(Material) method instead this");
        }

        public void Send(Material mat, List<Material> toRemove)
        {
            if (neighbors.Count > 0)
            {
                var sx = bottomLeftPosition.x + direction.Y;
                var sy = bottomLeftPosition.y + direction.X;

                var cellKey = neighbors.Keys.Where(k => k.x == sx && k.y == sy).FirstOrDefault();

                if (cellKey != null)
                {
                    var infra = neighbors[cellKey];

                    if (infra != null)
                    {
                        infra.AddRawMaterial(mat);
                        toRemove.Add(mat);
                    }
                }
            }
        }

        public void Send(Material mat)
        {
            
        }
    }
}

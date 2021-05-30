using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykovProject.Model.Data
{
    public class Material
    {
        public PointF point;

        public int relativeValue;
        public string name;
        public string description;
        public Sprite sprite;
        public MatType type;

        public Material(
            int relativeValue, string name, string description, 
            Sprite sprite, MatType type)
        {
            this.relativeValue = relativeValue;
            this.name = name;
            this.description = description;
            this.sprite = sprite;
            this.type = type;
        }

        public void SetPosition(float x, float y)
        {
            point = new PointF(x, y);
        }

        public void SetPosition(PointF p)
        {
            point = p;
        }
    }
}

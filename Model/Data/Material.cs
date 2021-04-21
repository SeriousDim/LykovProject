using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykovProject.Model.Data
{
    public class Material
    {
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
    }
}

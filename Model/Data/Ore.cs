using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykovProject.Model.Data
{
    public class Ore
    {
        public string name;
        public string desc;
        public Sprite sprite;
        public Material resultMaterial;

        public Ore(string name, Sprite sprite, Material resultMaterial)
        {
            this.name = name;
            this.sprite = sprite;
            this.resultMaterial = resultMaterial;
        }
    }
}

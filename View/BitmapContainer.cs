using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using LykovProject.Model.Data;

namespace LykovProject
{
    public class BitmapContainer
    {
        private Dictionary<string, Sprite> sprites;

        public BitmapContainer()
        {
            this.sprites = new Dictionary<string, Sprite>();

            Add("grass", "grass.png");
        }

        public Sprite this[string key]
        {
            get
            {
                return sprites[key];
            }

            set
            {
                sprites.Add(key, value);
            }
        }

        public void Add(string key, string path)
        {
            this[key] = new Sprite(path);
        }

        public Bitmap Get(string key)
        {
            return this[key].Bitmap;
        }

        public void SetResolution(int size)
        {
            foreach (var key in sprites.Keys)
                sprites[key].Bitmap = new Bitmap(sprites[key].Bitmap, new Size(size, size));
        }

    }
}

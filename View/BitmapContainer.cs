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

            Add("grass", "grass24.png");
            Add("storage", "storage24.png");
            Add("iron_ore", "iron_ore.png");
            Add("coal_ore", "coal_ore.png");

            Add("furnace", "furnace.png");
            Add("router", "router.png");

            Add("conv", "conv_left.png");
            Add("conv_right", "conv_right.png");
            Add("conv_up", "conv_up.png");
            Add("conv_down", "conv_down.png");

            Add("conv_mine", "conv_mine.png");

            Add("drill", "drill.png");
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

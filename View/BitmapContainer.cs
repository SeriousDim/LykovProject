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

            /*Add("grass", "grass24.png");
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

            Add("drill", "drill.png");*/
            Add("grass", Properties.Resources.grass24);
            Add("storage", Properties.Resources.storage24);
            Add("iron_ore", Properties.Resources.iron_ore);
            Add("coal_ore", Properties.Resources.coal_ore);

            Add("furnace", Properties.Resources.furnace);
            Add("router", Properties.Resources.router);

            Add("conv", Properties.Resources.conv_left);
            Add("conv_right", Properties.Resources.conv_right);
            Add("conv_up", Properties.Resources.conv_up);
            Add("conv_down", Properties.Resources.conv_down);

            Add("conv_mine", Properties.Resources.conv_mine);

            Add("drill", Properties.Resources.drill);
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

        public void Add(string key, string bmp)
        {
            this[key] = new Sprite(bmp);
        }

        public void Add(string key, Bitmap bmp)
        {
            this[key] = new Sprite(bmp);
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

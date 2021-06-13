using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace LykovProject.Model.Data
{
    public class Sprite
    {
        private Bitmap bitmap;
        private string path;

        public Bitmap Bitmap
        {
            get
            {
                return bitmap;
            }
            set
            {
                bitmap = value;
            }
        }

        public Sprite(string path)
        {
            this.path = path;
            this.Bitmap = LoadSprite(path);
        }

        public Sprite(Bitmap bitmap)
        {
            this.Bitmap = bitmap;
        }

        public Sprite(Color color, int size)
        {
            this.bitmap = new Bitmap(size, size);
            using (Graphics gfx = Graphics.FromImage(bitmap))
            using (SolidBrush brush = new SolidBrush(color))
            {
                gfx.FillRectangle(brush, 0, 0, size, size);
            }
        }

        // загружает спрайт из path и сохраняет его в bitmap
        public Bitmap LoadSprite(string path)
        {
            var curDir = System.Environment.CurrentDirectory;
            return new Bitmap(curDir + "\\..\\..\\Assets\\" + path);
        }
    }
}

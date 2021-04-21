using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;s

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
        }

        public Sprite(Bitmap bitmap)
        {
            this.Bitmap = bitmap;
        }

        // загружает спрайт из path и сохраняет его в bitmap
        public void LoadSprite()
        {

        }
    }
}

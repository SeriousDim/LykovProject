using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using unvell.D2DLib;
using unvell.D2DLib.WinForm;

namespace LykovProject
{
    public partial class Form1 : Form
    {
        private float x = 0;
        private float scale = 1;

        private PictureBox box;
        private Pen penGray = new Pen(Color.Gray);
        private SolidBrush brushRed = new SolidBrush(Color.Red);

        private const int WIDTH = 1000;
        private const int HEIGHT = 100;
        private const int CELL_SIZE = 48;

        private Point cursor;

        private Bitmap grass;

        private void RotateBitmap(Graphics g, Bitmap bmp, PointF p, float angleDeg)
        {
            g.TranslateTransform(p.X, p.Y);
            g.RotateTransform(angleDeg);
            g.TranslateTransform(-p.X, -p.Y);

            g.DrawImage(bmp, p);

            g.TranslateTransform(p.X, p.Y);
            g.RotateTransform(-angleDeg);
            g.TranslateTransform(-p.X, -p.Y);
        }

        private void RenderField(Graphics g, PointF point, int width, int height, Bitmap sprite)
        {
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    g.DrawImage(sprite, new PointF(point.X + sprite.Width * i, point.Y + sprite.Height * j));
                }
            }
        }

        private void ShowHoveredCell(Graphics g)
        {
            var indX = (int)Math.Floor(((double)cursor.X - x) / CELL_SIZE * scale);
            var indY = (int)Math.Floor((double)cursor.Y / CELL_SIZE * scale);
            g.FillRectangle(brushRed, CELL_SIZE * indX, CELL_SIZE * indY, CELL_SIZE, CELL_SIZE);
        }

        private void RenderCells(Graphics g, int cellSize, int width, int height)
        {
            for (var i = 0; i < width; i++)
                g.DrawLine(penGray, 0 + cellSize * i, 0, 0 + cellSize * i, height * cellSize);

            for (var j = 0; j < height; j++)
                g.DrawLine(penGray, 0, 0 + cellSize * j, width * cellSize, 0 + cellSize * j);
        }

        private void RenderGame(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.TranslateTransform(x, 0);
            g.ScaleTransform(scale, scale);

            /*var colors = new Color[] { Color.Blue, Color.Green, Color.Yellow, Color.Red, Color.OrangeRed };

            for (var i = 0; i < 10; i++)
            {
                g.DrawRectangle(new Pen(colors[i % 5]), 200 + 200 * i, 285, 150, 150);
            }*/

            //g.DrawImage(grass, new PointF(1000, 285));

            ShowHoveredCell(g);

            RenderCells(g, grass.Width, WIDTH, HEIGHT);
            RenderField(g, new PointF(200, 500), 200, 50, grass);
            RotateBitmap(g, grass, new PointF { X = 150, Y = 100 }, 45);
            g.DrawImage(grass, new PointF(100, 100));
        }

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //var img = Image.FromFile(@"..\..\Assets\grass.png");
            //var grassG = Graphics.FromImage(grass);
            grass = new Bitmap(@"..\..\..\Assets\grass.png");

            box = new PictureBox()
            {
                Dock = DockStyle.Fill,
            };
            box.Paint += new PaintEventHandler(this.RenderGame);
            box.MouseMove += new MouseEventHandler(this.Form1_MouseHover);

            Controls.Add(box);
            KeyDown += new KeyEventHandler(this.Form1_KeyDown);
        }

        private void Form1_MouseHover(object sender, MouseEventArgs e)
        {
            cursor = e.Location;
            box.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                x -= 45;
                box.Invalidate();
            }

            if (e.KeyCode == Keys.Left)
            {
                x += 45;
                box.Invalidate();
            }

            if (e.KeyCode == Keys.Up)
            {
                scale += 0.05f;
                box.Invalidate();
            }

            if (e.KeyCode == Keys.Down)
            {
                scale -= 0.05f;
                box.Invalidate();
            }
        }
    }
}

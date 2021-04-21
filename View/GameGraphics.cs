using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LykovProject.View
{
    internal class GameGraphics
    {
        // Константные поля
        private static readonly Pen GRAY_PEN = new Pen(Color.Gray);
        private static readonly SolidBrush RED_BRUSH = new SolidBrush(Color.Red);

        private const int WIDTH = 1000;
        private const int HEIGHT = 100;
        private const int CELL_SIZE = 48;

        // Остальные приватные поля
        private static float x = 0;
        private static float scale = 1;

        private static Point cursor;

        public static void RotateBitmap(Graphics g, Bitmap bmp, PointF p, float angleDeg)
        {
            g.TranslateTransform(p.X, p.Y);
            g.RotateTransform(angleDeg);
            g.TranslateTransform(-p.X, -p.Y);

            g.DrawImage(bmp, p);

            g.TranslateTransform(p.X, p.Y);
            g.RotateTransform(-angleDeg);
            g.TranslateTransform(-p.X, -p.Y);
        }

        public static void RenderField(Graphics g, PointF point, int width, int height, Bitmap sprite)
        {
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    g.DrawImage(sprite, new PointF(point.X + sprite.Width * i, point.Y + sprite.Height * j));
                }
            }
        }

        public static void ShowHoveredCell(Graphics g)
        {
            var indX = (int)Math.Floor(((double)cursor.X - x) * (scale) / CELL_SIZE);
            var indY = (int)Math.Floor((double)cursor.Y * (scale) / CELL_SIZE);
            g.FillRectangle(RED_BRUSH, CELL_SIZE * indX, CELL_SIZE * indY, CELL_SIZE, CELL_SIZE);
        }

        public static void DebugCells(Graphics g, int cellSize, int width, int height)
        {
            for (var i = 0; i < width; i++)
                g.DrawLine(GRAY_PEN, 0 + cellSize * i, 0, 0 + cellSize * i, height * cellSize);

            for (var j = 0; j < height; j++)
                g.DrawLine(GRAY_PEN, 0, 0 + cellSize * j, width * cellSize, 0 + cellSize * j);
        }

    }
}

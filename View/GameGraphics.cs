using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LykovProject.View
{
    public class GameGraphics
    {
        // Константные поля
        private static readonly Pen GRAY_PEN = new Pen(Color.Gray);
        private static readonly SolidBrush RED_BRUSH = new SolidBrush(Color.Red);

        private const int WIDTH = 1000;
        private const int HEIGHT = 100;
        private const int CELL_SIZE = 48;

        // Остальные приватные поля
        public static float DeltaX = 0;
        public static float DeltaY = 0;
        public static float Scale = 1;

        public static Point Cursor { get; set; }

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

        public static void RenderField(Graphics g, PointF point, Bitmap sprite)
        {
            for (var i = 0; i < WIDTH; i++)
            {
                for (var j = 0; j < HEIGHT; j++)
                {
                    g.DrawImage(sprite, new PointF(point.X + sprite.Width * i, point.Y + sprite.Height * j));
                }
            }
        }

        public static void RenderCells(Graphics g, int cellSize)
        {
            for (var i = 0; i < WIDTH; i++)
                g.DrawLine(GRAY_PEN, 0 + cellSize * i, 0, 0 + cellSize * i, HEIGHT * cellSize);

            for (var j = 0; j < HEIGHT; j++)
                g.DrawLine(GRAY_PEN, 0, 0 + cellSize * j, WIDTH * cellSize, 0 + cellSize * j);
        }

        public static void ShowHoveredCell(Graphics g)
        {
            var worldC = ToWorldCoords(new PointF { X = Cursor.X, Y = Cursor.Y });
            g.FillRectangle(RED_BRUSH, CELL_SIZE * worldC.X, CELL_SIZE * worldC.Y, CELL_SIZE, CELL_SIZE);
        }

        public static PointF ToWorldCoords(PointF screenCoords)
        {
            return new PointF(
                (int)Math.Floor(((double)screenCoords.X - DeltaX) * (1 / Scale) / CELL_SIZE),
                (int)Math.Floor(((double)screenCoords.Y - DeltaY) * (1 / Scale) / CELL_SIZE));
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

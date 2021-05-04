using LykovProject.Model.Data;
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

        //private const int WIDTH = 1000;
        //private const int HEIGHT = 100;
        private const int CELL_SIZE = 16;
        private const int CAMERA_SPEED = 20;
        private const float SCALE_SPEED = 0.001f;

        // Остальные приватные поля
        public static float DeltaX = 0;
        public static float DeltaY = 0;
        public static float Scale = 1;
        
        public static Point Cursor { get; set; }

        // Свойства
        public static int CellSize => CELL_SIZE;
        public static int CameraSpeed => CAMERA_SPEED;
        public static float ScaleSpeed => SCALE_SPEED;

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

        public static void RenderGameWorld(Graphics g, GameWorld world, Bitmap landBitmap)
        {
            for (var i = 0; i < world.Height; i++)
            {
                for (var j = 0; j < world.Width; j++)
                {
                    if (world.gameMap[i, j].land)
                        g.DrawImage(landBitmap, new PointF(CellSize * j, CellSize * i));
                }
            }
        }

        public static void RenderField(Graphics g, PointF point, Bitmap sprite, int width, int height)
        {
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    g.DrawImage(sprite, new PointF(point.X + sprite.Width * i, point.Y + sprite.Height * j));
                }
            }
        }

        public static void RenderCells(Graphics g, GameWorld world)
        {
            for (var i = 0; i < world.Width; i++)
                g.DrawLine(GRAY_PEN, 0 + CellSize * i, 0, 0 + CellSize * i, world.Height * CellSize);

            for (var j = 0; j < world.Height; j++)
                g.DrawLine(GRAY_PEN, 0, 0 + CellSize * j, world.Width * CellSize, 0 + CellSize * j);
        }

        public static void ShowHoveredCell(Graphics g)
        {
            var worldC = ToWorldCoords(new PointF { X = Cursor.X, Y = Cursor.Y });
            g.FillRectangle(RED_BRUSH, CellSize * worldC.X, CellSize * worldC.Y, CellSize, CellSize);
        }

        public static PointF ToWorldCoords(PointF screenCoords)
        {
            return new PointF(
                (int)Math.Floor(((double)screenCoords.X - DeltaX) * (1 / Scale) / CellSize),
                (int)Math.Floor(((double)screenCoords.Y - DeltaY) * (1 / Scale) / CellSize));
        }

    }
}

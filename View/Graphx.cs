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
    public class Graphx
    {
        // Константные поля
        private static readonly Pen GRAY_PEN = new Pen(Color.Gray);
        public static readonly SolidBrush STANDART_BRUSH = new SolidBrush(Color.FromArgb(100, 0, 0, 0));
        private static readonly SolidBrush BUILD_BRUSH = new SolidBrush(Color.FromArgb(100, 0, 255, 0));
        private static readonly SolidBrush DEBUILD_BRUSH = new SolidBrush(Color.FromArgb(100, 255, 0, 0));

        //private const int WIDTH = 1000;
        //private const int HEIGHT = 100;
        private const int CELL_SIZE = 24;
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
                    var cell = world.gameMap[i, j];
                    if (cell.land)
                    {
                        if (cell.ore == null)
                            g.DrawImage(landBitmap, new PointF(CellSize * j, CellSize * i));
                        else
                            g.DrawImage(cell.ore.sprite.Bitmap, new PointF(CellSize * j, CellSize * i));
                    }

                    if (cell.infra != null)
                        g.DrawImage(cell.infra.sprite.Bitmap, new PointF(CellSize * j, CellSize * i));
                }
            }

            for (var i = 0; i < world.Height; i++)
            {
                for (var j = 0; j < world.Width; j++)
                {
                    var cell = world.gameMap[i, j];

                    if (cell.infra != null)
                    {
                        if (cell.infra is Conveyor)
                        {
                            var convey = ((Conveyor)cell.infra);

                            foreach (var mat in convey.rawMaterials)
                            {
                                g.DrawImage(mat.sprite.Bitmap, mat.point);
                            }
                        }
                    }
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

        public static Brush GetBrushByState(GameState state)
        {
            switch (state)
            {
                case GameState.BUILDING:
                    return BUILD_BRUSH;
                case GameState.DEBUILDING:
                    return DEBUILD_BRUSH;
            }
            return STANDART_BRUSH;
        }

        public static void ShowWorldCoords(Graphics g)
        {
            var worldC = CursorToWorldCoords();
            g.DrawString("X, Y: "+worldC.X+" "+worldC.Y, new Font("Tahoma", 10), Brushes.Black, -DeltaX + 20, -DeltaY + 20);
        }

        public static void ShowHoveredCell(Graphics g, Brush b)
        {
            var worldC = CursorToWorldCoords();
            g.FillRectangle(b, CellSize * worldC.X, CellSize * worldC.Y, CellSize, CellSize);
        }

        public static PointF CursorToWorldCoords()
        {
            return ToWorldCoords(new PointF { X = Cursor.X, Y = Cursor.Y });
        }

        public static PointF ToWorldCoords(PointF screenCoords)
        {
            return new PointF(
                (int)Math.Floor(((double)screenCoords.X * (1 / Scale) - DeltaX) / CellSize),
                (int)Math.Floor(((double)screenCoords.Y * (1 / Scale) - DeltaY)  / CellSize));
        }

    }
}

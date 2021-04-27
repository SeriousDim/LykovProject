using LykovProject.Controller;
using LykovProject.View;
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
        public PictureBox box;
        private TableLayoutPanel panel;
        private TableLayoutPanel console;

        private BitmapContainer cont;
        private InputController<Form1> input;

        private void RenderGame(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.TranslateTransform(GameGraphics.DeltaX, GameGraphics.DeltaY);
            g.ScaleTransform(GameGraphics.Scale, GameGraphics.Scale);

            GameGraphics.ShowHoveredCell(g);

            GameGraphics.RenderCells(g, 48);
            GameGraphics.RenderField(g, new PointF(200, 500), cont.Get("grass"));
            GameGraphics.RotateBitmap(g, cont.Get("grass"), new PointF { X = 150, Y = 100 }, 45);
            g.DrawImage(cont.Get("grass"), new PointF(100, 100));
        }

        private Action GetBoxUpdater(Action a)
        {
            return () => { a.Invoke(); box.Invalidate(); };
        }

        private void AddActions()
        {
            input.AddAction(Keys.Right, GetBoxUpdater(() => GameGraphics.DeltaX -= 45));
            input.AddAction(Keys.Left, GetBoxUpdater(() => GameGraphics.DeltaX += 45));
            input.AddAction(Keys.Up, GetBoxUpdater(() => GameGraphics.Scale += 0.05f));
            input.AddAction(Keys.Down, GetBoxUpdater(() => GameGraphics.Scale -= 0.05f));
        }

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            cont = new BitmapContainer();
            input = new InputController<Form1>(this);

            AddActions();

            box = new PictureBox()
            {
                Dock = DockStyle.Fill,
            };

            box.Paint += new PaintEventHandler(this.RenderGame);
            box.MouseMove += new MouseEventHandler(this.Form1_MouseHover);

            panel = new TableLayoutPanel();
            console = new TableLayoutPanel();
            panel.RowStyles.Clear();
            console.RowStyles.Clear();

            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85));

            console.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            console.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            console.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            console.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            console.Controls.Add(new Button { Text = "Построить хранилище", Dock = DockStyle.Fill }, 0, 0);
            console.Controls.Add(new Button { Text = "Кнопка 1", Dock = DockStyle.Fill }, 0, 1);
            console.Controls.Add(new Button { Text = "Кнопка 2", Dock = DockStyle.Fill }, 0, 2);

            console.Dock = DockStyle.Fill;
            panel.Controls.Add(console, 0, 0);
            panel.Controls.Add(box, 1, 0);

            panel.Dock = DockStyle.Fill;
            Controls.Add(panel);

            KeyDown += new KeyEventHandler(this.Form1_KeyDown);
        }

        private void Form1_MouseHover(object sender, MouseEventArgs e)
        {
            GameGraphics.Cursor = e.Location;
            box.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            input.ProcessKey(e.KeyCode);
        }
    }
}

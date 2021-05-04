using LykovProject.Controller;
using LykovProject.Model.Data;
using LykovProject.Model.Logic;
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

        private GameLoop loop;
        private GameWorld world;

        private BitmapContainer btms;

        public Form1()
        {
            world = new GameWorld("Joe Doe", 5000, 20, 500, 100);
            GameGraphics.DeltaX = -world.Width * GameGraphics.CellSize * 0.5f;

            KeyPreview = true;
            InitializeComponent();
        }

        public void OnLoopInvalidation()
        {
            box.Invalidate();
        }

        private void RenderGame(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.TranslateTransform(GameGraphics.DeltaX, GameGraphics.DeltaY);
            g.ScaleTransform(GameGraphics.Scale, GameGraphics.Scale);

            GameGraphics.RenderCells(g, world);
            GameGraphics.RenderGameWorld(g, world, btms.Get("grass"));
            GameGraphics.ShowHoveredCell(g);
        }

        private void AddActions(InputController<Form1> input)
        {
            input.AddAction(Keys.Right, () => GameGraphics.DeltaX -= GameGraphics.CameraSpeed);
            input.AddAction(Keys.Left, () => GameGraphics.DeltaX += GameGraphics.CameraSpeed);
            input.AddAction(Keys.Up, () => GameGraphics.DeltaY += GameGraphics.CameraSpeed);
            input.AddAction(Keys.Down, () => GameGraphics.DeltaY -= GameGraphics.CameraSpeed);
            //input.AddAction(Keys.Up, () => GameGraphics.Scale += 0.05f);
            //input.AddAction(Keys.Down, () => GameGraphics.Scale -= 0.05f);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            loop.cont.EnqueueKey(keyData);
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            FormClosed += Form1_FormClosed;
            MouseWheel += Form1_MouseWheel;

            btms = new BitmapContainer();
            btms.SetResolution(GameGraphics.CellSize);

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

            world.GenMap();
            loop = new GameLoop(world, this);
            loop.Start();

            AddActions(loop.cont);
        }

        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            GameGraphics.Scale += GameGraphics.ScaleSpeed * e.Delta;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            loop.Finish();
        }

        private void Form1_MouseHover(object sender, MouseEventArgs e)
        {
            GameGraphics.Cursor = e.Location;
            box.Invalidate();
        }
    }
}

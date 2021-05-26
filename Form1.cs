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
        public GameLoop loop;
        public GameWorld world;

        public BitmapContainer btms;

        private GUIContainer gui;

        public Form1()
        {
            world = new GameWorld("Joe Doe", 5000, 20, 500, 100);
            Graphx.DeltaX = -world.Width * Graphx.CellSize * 0.5f;
            gui = new GUIContainer(this);

            KeyPreview = true;
            InitializeComponent();
        }

        public void OnLoopInvalidation()
        {
            gui.box.Invalidate();
        }

        public void UpdateNotifier(string s)
        {
            BeginInvoke(new Action(() => { gui.notificationField.Text = s; }));
        }

        public void RenderGame(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.ScaleTransform(Graphx.Scale, Graphx.Scale);
            g.TranslateTransform(Graphx.DeltaX, Graphx.DeltaY);

            Graphx.RenderCells(g, world);
            Graphx.RenderGameWorld(g, world, btms.Get("grass"));
            Graphx.ShowHoveredCell(g, Graphx.GetBrushByState(loop.gameState));
            Graphx.ShowWorldCoords(g);
        }

        public void AddActions(InputController<Form1> input)
        {
            input.AddAction(Keys.Right, () => Graphx.DeltaX -= Graphx.CameraSpeed);
            input.AddAction(Keys.Left, () => Graphx.DeltaX += Graphx.CameraSpeed);
            input.AddAction(Keys.Up, () => Graphx.DeltaY += Graphx.CameraSpeed);
            input.AddAction(Keys.Down, () => Graphx.DeltaY -= Graphx.CameraSpeed);

            input.AddAction(Keys.Escape, () => loop.ProcessEsc());
            input.AddAction("build", () => {
                world.Build(world.playerCompanyName, Graphx.CursorToWorldCoords(), loop.infraBuilder.Invoke());
            });
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            loop.cont.EnqueueKey(keyData);
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            btms = new BitmapContainer();
            btms.SetResolution(Graphx.CellSize);
            Prefabs.btms = btms;

            gui.GenerateGUI();

            world.GenMap();
            loop = new GameLoop(world, this);
            loop.Start();

            AddActions(loop.cont);
        }
    }
}

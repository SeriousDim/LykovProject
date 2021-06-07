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
using System.Threading;
using unvell.D2DLib;
using unvell.D2DLib.WinForm;

namespace LykovProject
{
    public partial class Form1 : Form
    {
        public System.Threading.Timer timer;
        public const long timeIntervalMs = 500;
        public long timeInMs;
        public object timeLocker;
        public Action intervalAction;

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

            timeInMs = 10 * 60 * 1000;
        }

        public void UpdateTimeLabel()
        {
            var min = timeInMs / 60000;
            var sec = (timeInMs - min * 60000) / 1000;
            BeginInvoke(new Action(() => { gui.timerLabel.Text = min + ":" + sec; }));
        }

        public void OnLoopInvalidation()
        {
            gui.box.Invalidate();
        }

        public void UpdateNotifier(string s)
        {
            BeginInvoke(new Action(() => { gui.notificationField.Text = s; }));
        }

        public void UpdateMoney()
        {
            BeginInvoke(new Action(() => { gui.money.Text = "$" + world.playerData[world.playerCompanyName].money + ""; }));
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
                var c = Graphx.CursorToWorldCoords();

                if (world.playerData[world.playerCompanyName].money > 0)
                {
                    world.Build(world.playerCompanyName, Graphx.CursorToWorldCoords(), loop.infraBuilder.Invoke());
                    world.playerData[world.playerCompanyName].money -= loop.infraBuilder.Invoke().price;
                    UpdateMoney();
                }
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

            loop = new GameLoop(world, this, timeInMs);
            loop.Start();

            AddActions(loop.cont);

            var tm = new TimerCallback(new Action<object>((obj) =>
            {
                timeInMs -= timeIntervalMs;

                UpdateTimeLabel();
                UpdateMoney();

                if (loop.gameState == GameState.FINISHED)
                {
                    timer.Dispose();
                    MessageBox.Show("Вы смогли достаточно заработать. Вы обогнали конкурентов!", "Победа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (timeInMs <= 0)
                {
                    loop.Finish();
                    timer.Dispose();
                    MessageBox.Show("Вы не смогли заработать за отведенное время. Ваша компания обанкротилась!", "Поражение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }));
            timer = new System.Threading.Timer(tm, null, 0, timeIntervalMs);
        }
    }
}

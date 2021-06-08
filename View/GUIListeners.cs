using LykovProject.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LykovProject.View
{
    public class GUIListeners
    {
        private readonly Form1 form;
        private readonly PictureBox box;
        private readonly Label inventoryField;
        private readonly Label notificationField;
        private readonly Label money;

        private GUIContainer container;
        private PlayerData playerData;

        private System.Threading.Timer inventoryFieldTimer;

        public GUIListeners(Form1 form, GUIContainer cont)
        {
            this.container = cont;
            this.form = form;
            this.box = cont.box;
            this.inventoryField = cont.inventoryField;
            this.notificationField = cont.notificationField;

            this.playerData = form.world.playerData[form.world.playerCompanyName];
        }

        public void UpdateTimerLabel(long timeInMs)
        {
            var min = timeInMs / 60000;
            var sec = timeInMs / 1000;

            container.timerLabel.Text = min + ":" + sec;
        }

        public void UseMoney(int sum)
        {
            playerData.money -= sum;
            money.Text = playerData.money + "";
        }

        public void StorageButton_Click(object sender, EventArgs e)
        {
            var prefab = Prefabs.StorageInfra;
            form.loop.SetBuidlingState(() => Prefabs.StorageInfra);
            notificationField.Text = "ЛКМ - построить, ESC - закончить строительство\nХранилище. СТОИМОСТЬ: "+prefab.price;
        }

        public void DrillButton_Click(object sender, EventArgs e)
        {
            var prefab = Prefabs.DrillInfra;
            form.loop.SetBuidlingState(() => Prefabs.DrillInfra);
            notificationField.Text = "ЛКМ - построить, ESC - закончить строительство\nБур для добычи руды. СТОИМОСТЬ: "+prefab.price;
        }

        public void ConvButton_Click(object sender, EventArgs e)
        {
            var prefab = Prefabs.ArticulatedConveyorLeft;
            form.loop.SetBuidlingState(() => Prefabs.ArticulatedConveyorLeft);
            notificationField.Text = "ЛКМ - построить, ESC - закончить строительство\nКонвеер. СТОИМОСТЬ: "+prefab.price;
        }

        public void ConvRightButton_Click(object sender, EventArgs e)
        {
            var prefab = Prefabs.ArticulatedConveyorRight;
            form.loop.SetBuidlingState(() => Prefabs.ArticulatedConveyorRight);
            notificationField.Text = "ЛКМ - построить, ESC - закончить строительство\nКонвеер. СТОИМОСТЬ: " + prefab.price;
        }

        public void ConvUpButton_Click(object sender, EventArgs e)
        {
            var prefab = Prefabs.ArticulatedConveyorUp;
            form.loop.SetBuidlingState(() => Prefabs.ArticulatedConveyorUp);
            notificationField.Text = "ЛКМ - построить, ESC - закончить строительство\nКонвеер. СТОИМОСТЬ: " + prefab.price;
        }

        public void ConvDownButton_Click(object sender, EventArgs e)
        {
            var prefab = Prefabs.ArticulatedConveyorDown;
            form.loop.SetBuidlingState(() => Prefabs.ArticulatedConveyorDown);
            notificationField.Text = "ЛКМ - построить, ESC - закончить строительство\nКонвеер. СТОИМОСТЬ: " + prefab.price;
        }

        public void Box_MouseClick(object sender, MouseEventArgs e)
        {
            if (form.loop.gameState == GameState.BUILDING)
            {
                form.loop.cont.EnqueueAction("build");
            }
            if (form.loop.gameState == GameState.PLAYING)
            {
                var c = Graphx.CursorToWorldCoords();
                var b = form.world.gameMap[(int)c.Y, (int)c.X];
                if (b != null && b.infra != null)
                {
                    if (inventoryFieldTimer != null)
                        inventoryFieldTimer.Dispose();
                    inventoryFieldTimer = new System.Threading.Timer((obj) => 
                    { form.BeginInvoke(new Action(() => { inventoryField.Text = b.infra.RawMatToString(); })); }, null, 0, 100);
                }
                else
                {
                    if (inventoryFieldTimer != null)
                        inventoryFieldTimer.Dispose();
                    inventoryFieldTimer = null;
                    inventoryField.Text = "Нажмите на здание, чтобы посмотреть информацию об его инвентаре";
                }
            }
        }

        public void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            Graphx.Scale += Graphx.ScaleSpeed * e.Delta;
        }

        public void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            form.loop.Finish();
        }

        public void Form1_MouseHover(object sender, MouseEventArgs e)
        {
            Graphx.Cursor = e.Location;
            box.Invalidate();
        }

        internal void FurnaceButton_Click(object sender, EventArgs e)
        {
            form.loop.SetBuidlingState(() => Prefabs.FurnaceInfra);
            notificationField.Text = "ЛКМ - построить, ESC - закончить строительство";
        }
    }
}

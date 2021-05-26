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

        private GUIContainer container;

        private System.Threading.Timer inventoryFieldTimer;

        public GUIListeners(Form1 form, GUIContainer cont)
        {
            this.container = cont;
            this.form = form;
            this.box = cont.box;
            this.inventoryField = cont.inventoryField;
            this.notificationField = cont.notificationField;
        }

        public void StorageButton_Click(object sender, EventArgs e)
        {
            form.loop.SetBuidlingState(() => Prefabs.StorageInfra);
            notificationField.Text = "ЛКМ - построить, ESC - закончить строительство";
        }

        public void DrillButton_Click(object sender, EventArgs e)
        {
            form.loop.SetBuidlingState(() => Prefabs.DrillInfra);
            notificationField.Text = "ЛКМ - построить, ESC - закончить строительство";
        }

        public void ConvButton_Click(object sender, EventArgs e)
        {
            form.loop.SetBuidlingState(() => Prefabs.ArticulatedConveyorLeft);
            notificationField.Text = "ЛКМ - построить, стрелки NUMPAD - повернуть, ESC - закончить строительство";
        }

        public void Box_MouseClick(object sender, MouseEventArgs e)
        {
            if (form.loop.gameState == GameState.BUILDING)
                form.loop.cont.EnqueueAction("build");
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

using LykovProject.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LykovProject.View
{
    public class GUIListeners
    {
        private readonly Form1 form;
        private readonly PictureBox box;

        public GUIListeners(Form1 form, PictureBox box)
        {
            this.form = form;
            this.box = box;
        }

        public void StorageButton_Click(object sender, EventArgs e)
        {
            form.loop.SetBuidlingState(Prefabs.StorageInfra);
        }

        public void Box_MouseClick(object sender, MouseEventArgs e)
        {
            if (form.loop.gameState == GameState.BUILDING)
                form.loop.cont.EnqueueAction("build");
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

    }
}

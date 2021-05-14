using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LykovProject.View
{
    public class GUIContainer
    {
        private readonly Form1 form;
        private readonly GUIListeners listeners;

        public PictureBox box;
        private TableLayoutPanel panel;
        private TableLayoutPanel console;

        private TableLayoutPanel tabControlPanel;
        private TabControl tabControl;

        public GUIContainer(Form1 form)
        {
            this.form = form;

            box = new PictureBox()
            {
                Dock = DockStyle.Fill,
            };

            listeners = new GUIListeners(form, box);
        }

        public TabPage CreateTabMain()
        {
            var tab = new TabPage("Главная") { Dock = DockStyle.Fill };

            var table = new TableLayoutPanel() { Dock = DockStyle.Fill };
            table.RowStyles.Clear();

            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            table.Controls.Add(new Label() { Dock = DockStyle.Fill, Text = "Joe Doe", Font = new System.Drawing.Font("Tahoma", 20) }, 0, 0);

            var storageButton = new Button { Dock = DockStyle.Fill, Image = form.btms.Get("storage") };
            storageButton.Click += listeners.StorageButton_Click;
            table.Controls.Add(storageButton, 0, 1);

            tab.Controls.Add(table);

            return tab;
        }

        public TabPage CreateTabManagement()
        {
            var tab = new TabPage("Управление") { Dock = DockStyle.Fill };



            return tab;
        }

        public TabPage CreateTabInfo()
        {
            var tab = new TabPage("Инфо") { Dock = DockStyle.Fill };



            return tab;
        }

        public TabPage CreateTabDebug()
        {
            var tab = new TabPage("Debug") { Dock = DockStyle.Fill };



            return tab;
        }

        public void CreateTabControl()
        {
            tabControl = new TabControl() { Dock = DockStyle.Fill };

            tabControl.TabPages.Add(CreateTabMain());
            tabControl.TabPages.Add(CreateTabManagement());
            tabControl.TabPages.Add(CreateTabInfo());
            tabControl.TabPages.Add(CreateTabDebug());

            tabControlPanel = new TableLayoutPanel() { Dock = DockStyle.Fill };
            tabControlPanel.RowStyles.Clear();

            tabControlPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            tabControlPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            tabControlPanel.Controls.Add(tabControl);
        }

        public void CreateConsolePanel()
        {
            console = new TableLayoutPanel();
            console.RowStyles.Clear();

            console.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            console.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            console.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            console.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            var storageButton = new Button { Dock = DockStyle.Fill, Image = form.btms.Get("storage") };
            storageButton.Click += listeners.StorageButton_Click;
            console.Controls.Add(storageButton, 0, 0);
            console.Controls.Add(new Button { Text = "Кнопка 1", Dock = DockStyle.Fill }, 0, 1);
            console.Controls.Add(new Button { Text = "Кнопка 2", Dock = DockStyle.Fill }, 0, 2);

            console.Dock = DockStyle.Fill;
        }

        public void CreatePanel()
        {
            panel = new TableLayoutPanel();
            panel.RowStyles.Clear();

            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80));

            //panel.Controls.Add(console, 0, 0);
            panel.Controls.Add(tabControlPanel, 0, 0);
            panel.Controls.Add(box, 1, 0);

            panel.Dock = DockStyle.Fill;
        }

        public void GenerateGUI()
        {
            form.FormClosed += listeners.Form1_FormClosed;
            form.MouseWheel += listeners.Form1_MouseWheel;

            box.Paint += new PaintEventHandler(form.RenderGame);
            box.MouseMove += new MouseEventHandler(listeners.Form1_MouseHover);
            box.MouseClick += listeners.Box_MouseClick;

            CreateTabControl();
            CreateConsolePanel();
            CreatePanel();

            form.Controls.Add(panel);
        }

    }
}

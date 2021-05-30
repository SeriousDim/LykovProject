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
        public Label notificationField;
        public Label inventoryField;

        public TableLayoutPanel table;

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
            
            inventoryField = new Label() { Dock = DockStyle.Fill, Text = "Нажмите на здание, чтобы посмотреть информацию об его инвентаре", Font = new System.Drawing.Font("Tahoma", 10) };
            notificationField = new Label() { Dock = DockStyle.Fill, Text = "Готово", Font = new System.Drawing.Font("Tahoma", 10) };

            listeners = new GUIListeners(form, this);
        }

        public void UpdateNotifier(string s)
        {
            notificationField.Text = s;
            //notificationField.Invalidate();
        }

        public TabPage CreateTabMain()
        {
            var tab = new TabPage("Главная") { Dock = DockStyle.Fill };

            table = new TableLayoutPanel() { Dock = DockStyle.Fill };
            table.RowStyles.Clear();

            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            table.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            table.Controls.Add(new Label() { Dock = DockStyle.Fill, Text = "Joe Doe", Font = new System.Drawing.Font("Tahoma", 20) }, 0, 0);

            // ====== Информация о компании ========
            var companyInfo = new TableLayoutPanel() { /*Dock = DockStyle.Fill*/ };
            companyInfo.RowStyles.Clear();
            companyInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            companyInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            companyInfo.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            companyInfo.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            companyInfo.Controls.Add(new Label() { Dock = DockStyle.Left, Text = "$5000" }, 0, 0);
            companyInfo.Controls.Add(new Label() { Dock = DockStyle.Left, Text = "+ $50" }, 0, 1);
            companyInfo.Controls.Add(new Label() { Dock = DockStyle.Right, Text = "LVL 1" }, 1, 0);
            companyInfo.Controls.Add(new Label() { Dock = DockStyle.Right, Text = "" }, 1, 1);

            table.Controls.Add(companyInfo);

            // ======== Здания ========
            var storageButton = new Button() { Dock = DockStyle.Fill, Image = form.btms.Get("storage") };
            storageButton.Click += listeners.StorageButton_Click;
            var furnaceButton = new Button() { Dock = DockStyle.Fill, Image = form.btms.Get("furnace") };
            furnaceButton.Click += listeners.FurnaceButton_Click;

            var buildings = new TableLayoutPanel() { /*Dock = DockStyle.Fill*/ };
            buildings.RowStyles.Clear();

            buildings.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            buildings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            buildings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            buildings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            buildings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            buildings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));

            buildings.Controls.Add(storageButton, 0, 0);
            buildings.Controls.Add(furnaceButton, 1, 0);
            for (var i = 2; i <= 4; i++)
            {
                var emptyButton = new Button() { Dock = DockStyle.Fill, Text = " " };
                buildings.Controls.Add(emptyButton, i, 0);
            }

            table.Controls.Add(new Label() { Dock = DockStyle.Fill, Text = "Здания" });
            table.Controls.Add(buildings);

            // ========== Конвееры ===========
            var conv = new TableLayoutPanel() { /*Dock = DockStyle.Fill*/ };
            conv.RowStyles.Clear();

            var artiConvButton = new Button() { Dock = DockStyle.Fill, Image = form.btms.Get("conv") };
            artiConvButton.Click += listeners.ConvButton_Click;
            var artiConvRightButton = new Button() { Dock = DockStyle.Fill, Image = form.btms.Get("conv_right") };
            artiConvRightButton.Click += listeners.ConvRightButton_Click;
            var artiConvUpButton = new Button() { Dock = DockStyle.Fill, Image = form.btms.Get("conv_up") };
            artiConvUpButton.Click += listeners.ConvUpButton_Click;
            var artiConvDownButton = new Button() { Dock = DockStyle.Fill, Image = form.btms.Get("conv_down") };
            artiConvDownButton.Click += listeners.ConvDownButton_Click;

            conv.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            conv.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            conv.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            conv.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            conv.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            conv.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));

            conv.Controls.Add(artiConvButton, 0, 0);
            conv.Controls.Add(artiConvRightButton, 1, 0);
            conv.Controls.Add(artiConvUpButton, 2, 0);
            conv.Controls.Add(artiConvDownButton, 3, 0);
            conv.Controls.Add(new Button() { Dock = DockStyle.Fill, Text = " " }, 4, 0);

            table.Controls.Add(new Label() { Dock = DockStyle.Fill, Text = "Конвееры" });
            table.Controls.Add(conv);

            // ========= Шахты ========
            var mines = new TableLayoutPanel() { /*Dock = DockStyle.Fill*/ };
            mines.RowStyles.Clear();

            var routerButton = new Button() { Dock = DockStyle.Fill, Image = form.btms.Get("router") };
            var mineButton = new Button() { Dock = DockStyle.Fill, Image = form.btms.Get("conv_mine") };
            var drillButton = new Button() { Dock = DockStyle.Fill, Image = form.btms.Get("drill") };
            drillButton.Click += listeners.DrillButton_Click;

            mines.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            mines.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            mines.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            mines.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            mines.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            mines.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            mines.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));

            mines.Controls.Add(routerButton, 0, 0);
            mines.Controls.Add(drillButton, 1, 0);
            for (var i = 2; i <= 4; i++)
            {
                var emptyButton = new Button() { Dock = DockStyle.Fill, Text = " " };
                mines.Controls.Add(emptyButton, i, 0);
            }

            mines.Controls.Add(mineButton, 0, 1);
            for (var i = 1; i <= 4; i++)
            {
                var emptyButton = new Button() { Dock = DockStyle.Fill, Text = " " };
                mines.Controls.Add(emptyButton, i, 1);
            }

            table.Controls.Add(new Label() { Dock = DockStyle.Fill, Text = "Шахты" });
            table.Controls.Add(mines);

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

        public TabPage CreateTabInventory()
        {
            var tab = new TabPage("Инвентарь") { Dock = DockStyle.Fill };

            tab.Controls.Add(inventoryField);

            return tab;
        }

        public void CreateTabControl()
        {
            tabControl = new TabControl() { Dock = DockStyle.Fill };

            tabControl.TabPages.Add(CreateTabMain());
            tabControl.TabPages.Add(CreateTabManagement());
            tabControl.TabPages.Add(CreateTabInfo());
            tabControl.TabPages.Add(CreateTabInventory());

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


            var gameField = new TableLayoutPanel() { Dock = DockStyle.Fill };
            gameField.RowStyles.Clear();

            gameField.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            gameField.RowStyles.Add(new RowStyle(SizeType.Percent, 5));
            gameField.RowStyles.Add(new RowStyle(SizeType.Percent, 95));

            gameField.Controls.Add(notificationField, 0, 0);
            gameField.Controls.Add(box, 0, 1);
            

            panel.Controls.Add(tabControlPanel, 0, 0);
            panel.Controls.Add(gameField, 1, 0);

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

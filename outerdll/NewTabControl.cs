using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace Tabpanel
{
    public partial class NewTabControl : System.Windows.Forms.TabControl
    {
        public NewTabControl()
        {
            InitializeComponent();
        }

        public NewTabControl(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }
        protected override void WndProc(ref Message m)  //Скрываем стандартные кнопки
        {            
            if (m.Msg == 0x1328 && !DesignMode) m.Result = (IntPtr)1;
            else base.WndProc(ref m);
        }
    }
    
 
    public class NewTabPanel : System.Windows.Forms.Panel                                //Новый контрол
    {
        public int width = 0;
        public NewTabControl tabControl;  
        private System.Windows.Forms.Panel panel2;
      
        Dictionary<TabPage, PanelTP> ListTabButton = new Dictionary<TabPage, PanelTP>(); //Набор вкладок
        
        public TabControl.TabPageCollection TabPages{
            get
            {
                return this.tabControl.TabPages;
            }           
        }


        public NewTabPanel()
        {      
            InitializeComponent();
        }

        private void InitializeComponent()
        {
           
            this.panel2 = new System.Windows.Forms.Panel();                             //Панель с вкладками
            this.tabControl = new NewTabControl();
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panel2);
            this.Size = new System.Drawing.Size(311, 361);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl.ControlAdded += new ControlEventHandler(tc_ControlAdded);        //Событие на создание новой вкладки
            tabControl.ControlRemoved += new ControlEventHandler(tc_ControlRemoved);    //Событие удаления вкладки
            tabControl.Selected += new TabControlEventHandler(tc_Selected);             //Событие выделения вкладки

        }

        private void tc_ControlAdded(object sender, ControlEventArgs e)
        {

            this.AddItem(((TabPage)e.Control), ((TabPage)e.Control).Text);              //Добавляем вкладку по событию из NewTabControl
            
        }

        private void tc_ControlRemoved(object sender, ControlEventArgs e)
        {
            this.DellItem((TabPage)e.Control);                                          //Удаляем вкладку по событию из NewTabControl
        }

        private void tc_Selected(object sender, TabControlEventArgs e)
        {
            this.Selection();                                                           //Выделяем вкладку по событию из NewTabControl
        }

        private void AddItem(TabPage keyid, string name)
        {
            PanelTP TPanel = new PanelTP(keyid, name);
            this.panel2.Controls.Add(TPanel);
            panel2.Size = new System.Drawing.Size(TPanel.Width, 361);

            if (!ListTabButton.ContainsKey(keyid)) ListTabButton.Add(keyid, TPanel);   //Добавляем в набор новую вкладку
            this.Sort();                                                               //Перестраиваем и перекрашиваемм вкладки по номому событию
        }

        private void DellItem(TabPage keyid)
        {

            this.panel2.Controls.Remove((PanelTP)ListTabButton[keyid]);                 //Удаляем вкладку с панели
            if (ListTabButton.ContainsKey(keyid)) ListTabButton.Remove(keyid);          //Удаляем вкладку из набора
            this.Sort();
        }
        private void Selection()
        {
            Sort();
        }
        private void Sort()
        {
            int i = 0;
            foreach (PanelTP panel in this.ListTabButton.Values)
            {
                panel.BackgroundImage = Tabpanel.Properties.Resources.tab_c_74;  //Задаем фоном, что вкладка не активна
                panel.Top = i;
                i += panel.Height;
            }

            if (tabControl.SelectedTab!=null && this.ListTabButton.ContainsKey(tabControl.SelectedTab))
            ((PanelTP)this.ListTabButton[tabControl.SelectedTab]).BackgroundImage = Tabpanel.Properties.Resources.tab_c_73; //Красим выделенную вкладку
        }

    }


    public class PanelTP : System.Windows.Forms.Panel
    {

        private string name; //Имя вкладки
        private TabPage key; //Соответствующая вкладке TabPage
        public PanelTP(TabPage key, string name)
        {    
            this.key=key;
            this.name=name;

            InitializeComponent();
        }


        private void InitializeComponent()
        {
            
            this.BackColor = Color.Transparent;
            //Красим и задаем разммеры вкладки
            this.Height = 27;
            this.Width = 128;
            this.BackgroundImage = Tabpanel.Properties.Resources.tab_c_74;
 
            this.Click += new EventHandler(Select_Item); //Событие клие по вкладке
            
            //Добавляем иконку на вкладку
            PictureBox Icon;
            Icon = new PictureBox();
            Icon.Width = 25;
            Icon.Height = 26;
            Icon.Left = 3;
            Icon.Top = 5;
            Icon.Image = Tabpanel.Properties.Resources.green_dialbut_611;
            this.Controls.Add(Icon);

            //Добавляем надпись на вкладку
            Label lname;
            lname = new Label();
            lname.Width = 95;
            lname.Height = 25;
            lname.Left = 28;
            lname.Top = 5;
            lname.Font = new System.Drawing.Font("Times New Roman", 8f, FontStyle.Regular);
            lname.Text = this.name;
            lname.Click += new EventHandler(Select_Item);            
            this.Controls.Add(lname);
            
            
        }

        void Select_Item(object sender, EventArgs e) //Событие клик по вкладке
        {
            //Через родителей добираемся до нужного таба и выбираем его
            if (sender is Label)
            {
                ((NewTabControl)((PanelTP)((Label)sender).Parent).key.Parent).SelectTab(((PanelTP)((Label)sender).Parent).key);
            }
            if (sender is PanelTP)
            {
                ((NewTabControl)((PanelTP)sender).key.Parent).SelectTab(((PanelTP)sender).key);
            }
            
        }
    }
}

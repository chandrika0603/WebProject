using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestoDesktopApp.Model;
using RestoDesktopApp.View;

namespace RestoDesktopApp
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        static FrmMain _obj;

        public static FrmMain Instance

        {
            get { if (_obj == null) { _obj = new FrmMain(); } return _obj;}
            
        }

        //Create a method to add controls in main form inside panel3

        public void AddControls(Form f)
        {
            panel3.Controls.Clear();
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            panel3.Controls.Add(f);
            f.Show();
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            lblUserName.Text = MainClass.USER;
            _obj = this;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
            
            }

        private void btnMin_Click(object sender, EventArgs e)
        {
            WindowState=FormWindowState.Normal;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            AddControls(new FrmHome1());
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            AddControls(new frmCategoryView());
        }

        private void btnTables_Click(object sender, EventArgs e)
        {
            AddControls(new frmTableView());
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            AddControls(new frmStaffView());
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            AddControls(new frmProductView());
        }
    }
}

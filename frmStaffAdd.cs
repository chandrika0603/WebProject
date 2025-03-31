using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestoDesktopApp.Model
{
    public partial class frmStaffAdd : SampleAdd
    {
        public frmStaffAdd()
        {
            InitializeComponent();
        }
        public int id = 0;

        private void frmStaffAdd_Load(object sender, EventArgs e)
        {

        }
        public override void btnSave_Click(object sender, EventArgs e)
        {
            string qr = "";
            if (id == 0)
            {
                qr = "sp_addsatff";//insert category
            }
            else
            {
                qr = "sp_updatesatff";//Upadte category

                Hashtable ht = new Hashtable();
                ht.Add("staffid", id);
                ht.Add("@sname", txtName.Text);
                ht.Add("@sphone",txtMobile.Text);
                ht.Add("@srole", cmbRole.Text);
                if (MainClass.SQL(qr, ht) > 0)
                {
                    MessageBox.Show("Saved Successfully", "Restaurant Management System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    id = 0; txtName.Clear();
                    txtMobile.Clear();
                    cmbRole.SelectedIndex = -1;
                    txtName.Focus();

                }




            }

        }
      }
    }

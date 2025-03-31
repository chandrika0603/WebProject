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
    public partial class frmTableAdd : SampleAdd
    {
        public frmTableAdd()
        {
            InitializeComponent();
        }
        public int id = 0;

        public override void btnSave_Click(object sender, EventArgs e)
        {
            string qr = "";
            if (id == 0)
            {
                qr = "sp_AddTable";//insert category
            }
            else
            {
                qr = "sp_UpdateTable";//Upadte category

                Hashtable ht = new Hashtable();
                ht.Add("@tID", id);
                ht.Add("@tName", txtName.Text);

                if (MainClass.SQL(qr, ht) > 0)
                {
                    MessageBox.Show("Saved Successfully", "Restaurant Management System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    id = 0; txtName.Clear();
                    txtName.Focus();
                }

            }

            }
            
        }
}

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
    public partial class frmCategoryAdd : SampleAdd
    {
        public frmCategoryAdd()
        {
            InitializeComponent();
        }
        public int id = 0;
        public override void btnSave_Click(object sender, EventArgs e)
        {
            string qr = "";
            if (id == 0)
            {
                qr = "sp_AddCategory";//insert category
            }
            else
            {
                qr = "sp_UpadteCategory";//Upadte category

                Hashtable ht = new Hashtable();
                ht.Add("@catID",id);
                ht.Add("@catName",txtName.Text);

                if(MainClass.SQL(qr,ht)>0)
                {
                    MessageBox.Show("Saved Successfully", "Restaurant Management System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    id = 0; txtName.Clear();
                    txtName.Focus();
                }




            }
        }

        private void frmCategoryAdd_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

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
using RestoDesktopApp.Model;

namespace RestoDesktopApp.View
{
    public partial class frmProductView : SampleView
    {
        public frmProductView()
        {
            InitializeComponent();
        }

        private void frmProductView_Load(object sender, EventArgs e)
        {
            GetData();
        }
        public void GetData()
        {
            string qr = @"select p.pID,p.pName,p.pPrice,p.CategoryID,c.catName from tblProduct p inner join tblcategory c on c.catID=p.CategoryID where p.pName like '%" + txtSearch.Text + "%'";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvName);
            lb.Items.Add(dgvPrice);
            lb.Items.Add(dgvCatID);
            lb.Items.Add(dgvCat);
            MainClass.LoadData(qr, dataGridView1, lb);
        }
        public override void pictureBox1_Click(object sender, EventArgs e)
        {
            MainClass.BlueBackground(new frmProductAdd());
            GetData();
        }
        public override void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1_CellContentClick(sender, e, "dgvid");

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e, string v)
        {
            if (dataGridView1.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                frmProductAdd frm = new frmProductAdd();
                frm.id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["dgvid"].Value);
                frm.cID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["dgvcatID"].Value);
               
                MainClass.BlueBackground(frm);
                GetData();
            }
            //Check if the current cell belongs to the "dgvDel" column
            if (dataGridView1.CurrentCell.OwningColumn.Name == "dgvDel")
            {
                if (DialogResult.Yes == MessageBox.Show("Do You Want Delete ?", "Confrimation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["dgvid"].Value);
                    string qr = "sp_deleteProduct";
                    Hashtable ht = new Hashtable();
                    ht.Add("@pID", id);
                    if (MainClass.SQL(qr, ht) > 0)
                    {
                        MessageBox.Show("Delete Successfully", "Restaurant Management System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetData();
                    }
                    else
                    {
                        MessageBox.Show("Deletion Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }
    }
}

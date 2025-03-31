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
using System.Xml.Linq;
using RestoDesktopApp.Model;

namespace RestoDesktopApp.View
{
    public partial class frmCategoryView : SampleView
    {
        public frmCategoryView()
        {
            InitializeComponent();
        }
        public void GetData()
        {
            string qr = "select catID,catName from tblcategory where catName like '%"+txtSearch.Text+"%'";
            ListBox lb=new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvName);

            MainClass.LoadData(qr,dataGridView1,lb);
        }

        private void frmCategoryView_Load(object sender, EventArgs e)
        {
            GetData();
        }

        public override void pictureBox1_Click(object sender, EventArgs e)
        {
            MainClass.BlueBackground(new frmCategoryAdd());
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
            if(dataGridView1.CurrentCell.OwningColumn.Name =="dgvedit")
            {
                frmCategoryAdd frm = new frmCategoryAdd();
                frm.id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["dgvid"].Value);
                frm.txtName.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["dgvName"].Value);
                //frm.ShowDialog();
                MainClass.BlueBackground(frm);
                GetData();
            }
                //Check if the current cell belongs to the "dgvDel" column
                if(dataGridView1.CurrentCell.OwningColumn.Name == "dgvDel")
            {
                  if(DialogResult.Yes == MessageBox.Show("Do You Want Delete ?", "Confrimation", MessageBoxButtons.YesNo,MessageBoxIcon.Warning))
                {
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["dgvid"].Value);
                    string qr = "sp_DeleteCategory";
                    Hashtable ht = new Hashtable();
                    ht.Add("@catID", id);
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


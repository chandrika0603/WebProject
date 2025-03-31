using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestoDesktopApp.Model
{
    public partial class frmProductAdd : SampleAdd
    {
        public frmProductAdd()
        {
            InitializeComponent();
        }

        public int id = 0;
        public int cID = 0;
        private void frmProductAdd_Load(object sender, EventArgs e)
        {
            string qr = "select catID as id,catName as name from tblcategory";
            MainClass.CBFill(qr,cmbCategory);
            if (cID>0)
            { 
              cmbCategory.SelectedValue = cID;
            }
            if (id>0)
            {
                ForUpadateLoadData();
            }
        }
        string filepath;
        Byte[] ImageBytesArray;
        private void btnBrowseImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images(.jpg,.png)|*.png;*.jpg";
            if (ofd.ShowDialog()==DialogResult.OK )
            { 
              filepath = ofd.FileName;
              txtImage.Image= new Bitmap(filepath);
            }
        }
        public override void btnSave_Click(object sender, EventArgs e)
        {
            string qr = "";
            if (id == 0)
            {
                qr = "sp_addProduct";//insert category
            }
            else
            {
                qr = "sp_addProduct";//Upadte category
            }
            //for Image upload
            Image temp = new Bitmap(txtImage.Image);
            MemoryStream ms = new MemoryStream();
            temp.Save(ms,System.Drawing.Imaging.ImageFormat.Png);
            ImageBytesArray=ms.ToArray();



                Hashtable ht = new Hashtable();
                ht.Add("pID", id);
                ht.Add("@pName", txtName.Text);
                ht.Add("@pPrice", Convert.ToDouble(txtPrice.Text));
                ht.Add("@CategoryID", Convert.ToInt32(cmbCategory.SelectedValue));
                ht.Add("@pImage",ImageBytesArray);

                if (MainClass.SQL(qr, ht) > 0)
                {
                    MessageBox.Show("Saved Successfully", "Restaurant Management System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    id = 0; 
                    cID = 0;
                    txtName.Clear();
                    txtPrice.Clear();
                    cmbCategory.SelectedIndex = -1;
                    txtName.Focus();

                }
        }
        private void ForUpadateLoadData()
        {
            SqlCommand cmd = new SqlCommand("select * from tblProduct where pID=" + id + "", MainClass.Connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0) ;
            {
                txtName.Text = dt.Rows[0]["pName"].ToString();
                txtPrice.Text = dt.Rows[0]["pPrice"].ToString();
                Byte[] imageArray = (byte[])(dt.Rows[0]["pImage"]);
                byte[] imageByteArray = imageArray;
                txtImage.Image= Image.FromStream(new MemoryStream(imageByteArray));
            }
        }

    }
}

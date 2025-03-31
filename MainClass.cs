using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestoDesktopApp
{
    internal class MainClass
    {
        public static readonly string cs = @"Data Source=DESKTOP-8L8K79U;Initial Catalog=RestoDB;Integrated Security=True;";
        public static SqlConnection Connection = new SqlConnection(cs);

        //Check User Validation
        public static bool IsValidUser(string user, string pass)
        {
            bool isValid = false;
            //string qr = @"select userid,username,upass,uname,uphone from tbluser where username='"+ user + "' and upass='"+ pass + "'";
            //SqlCommand cmd = new SqlCommand(qr, Connection);
            SqlCommand cmd = new SqlCommand("sp_UserLogin", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@username", user);
            cmd.Parameters.AddWithValue("@upass", pass);

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                isValid = true;
                USER = dt.Rows[0]["uname"].ToString();
            }
            return isValid;
        }

        // create property for username
        public static string user;

        public static string USER
        {
            get { return user; }
            private set
            {
                user = value;
            }
        }
        //Create reusable methode for Insert Update and Delete operation
        public static int SQL(string qr, Hashtable ht)
        {
            int res = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(qr, Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (DictionaryEntry item in ht)
                {
                    cmd.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                }
                if (Connection.State == ConnectionState.Closed) { Connection.Open(); }
                res = cmd.ExecuteNonQuery();
                if (Connection.State == ConnectionState.Open) { Connection.Close(); }


            }
            catch (Exception ex)
            {
                Connection.Close(); MessageBox.Show(ex.Message);
            }
            return res;
        }
        //Load Data from SQL Database

        public static void LoadData(string qr, DataGridView gv, ListBox lb)
        {
            gv.CellFormatting += new DataGridViewCellFormattingEventHandler(gv_CellFormatting);
            try
            {
                SqlCommand cmd = new SqlCommand(qr, Connection);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < lb.Items.Count; i++)
                {
                    string cloName = ((DataGridViewColumn)lb.Items[i]).Name;
                    gv.Columns[cloName].DataPropertyName = dt.Columns[i].ToString();
                }
                gv.DataSource = dt;
            }
            catch (Exception ex)
            {

                Connection.Close(); MessageBox.Show(ex.Message);
            }


        }
        private static void gv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView gv = (DataGridView)sender;
            int count = 0;
            foreach (DataGridViewRow row in gv.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        public static void BlueBackground(Form Model)
        {
            Form Background = new Form();
            using (Model)
            {
                 Background.StartPosition = FormStartPosition.Manual;
                 Background.FormBorderStyle = FormBorderStyle.None;
                 Background.Opacity = 0.5d;
                 Background.BackColor = Color.Black;
                 Background.Size = FrmMain.Instance.Size;
                 Background.Location = FrmMain.Instance.Location;
                 Background.ShowInTaskbar = false;
                 Background.Show();
                 Model.Owner = Background;
                 Model.ShowDialog(Background);
                 Background.Dispose();
                
            }
        }

        public static void CBFill(string qr, ComboBox cb)
      {
            SqlCommand cmd = new SqlCommand(qr, Connection);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cb.DisplayMember = "name";
            cb.ValueMember = "id";
            cb.DataSource = dt;
            cb.SelectedIndex = -1;
        }
    }
}

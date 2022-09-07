using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace TravelR
{
    public partial class TAR : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["cu"].ConnectionString;
        public TAR()
        {
            InitializeComponent();
        }

        private void TAR_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            TAL i = new TAL();
            i.Show();
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool status = checkBox1.Checked;
            switch (status)
            {
                case true:
                    textBox2.UseSystemPasswordChar = false;
                    break;
                default:
                    textBox2.UseSystemPasswordChar = true;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Image";
            ofd.Filter = "JPG/PNG File (*.jpg; *.png)| *.jpg; *.png";
            //ofd.ShowDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(ofd.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection sc = new SqlConnection(cs);
            string query = "insert into TA values (@username, @pass, @addr, @loc,  @mob, @web, @place, @amnt, @img)";
            SqlCommand cmd = new SqlCommand(query, sc);
            cmd.Parameters.AddWithValue("@username", textBox1.Text);
            cmd.Parameters.AddWithValue("@pass", textBox2.Text);
            cmd.Parameters.AddWithValue("@addr", textBox3.Text);
            cmd.Parameters.AddWithValue("@loc", comboBox1.Text);
            cmd.Parameters.AddWithValue("@mob", textBox4.Text);
            cmd.Parameters.AddWithValue("@web", textBox6.Text);
            cmd.Parameters.AddWithValue("@place", textBox5.Text);
            cmd.Parameters.AddWithValue("@amnt", textBox7.Text);
            cmd.Parameters.AddWithValue("@img", SaveImage());
            sc.Open();
            var A = cmd.ExecuteNonQuery();
            if (A > 0)
            {
                TAL g = new TAL();
                g.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Data not inserted......");
            }
            sc.Close();
        }

        private byte[] SaveImage()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
        }
    }
}

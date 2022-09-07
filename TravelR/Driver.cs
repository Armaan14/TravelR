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
    public partial class Driver : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["cu"].ConnectionString;

        public Driver()
        {
            InitializeComponent();
            BindGridView();
            BindGridView2();
        }

        

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool status = checkBox1.Checked;
            switch (status)
            {
                case true:
                    textBox4.UseSystemPasswordChar = false;
                    break;
                default:
                    textBox4.UseSystemPasswordChar = true;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DriverL d = new DriverL();
            d.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection sc = new SqlConnection(cs);
            string query = "update DRIVE set username=@username, pass=@pass, addr=@addr, loc=@loc, mob=@mob, age=@age, carno=@carno, ctype=@ctype, cmodel=@cmodel, img=@img where username=@username";
            SqlCommand cmd = new SqlCommand(query, sc);
            cmd.Parameters.AddWithValue("@username", textBox1.Text);
            cmd.Parameters.AddWithValue("@pass", textBox4.Text);
            cmd.Parameters.AddWithValue("@addr", textBox5.Text);
            cmd.Parameters.AddWithValue("@loc", comboBox1.Text);
            cmd.Parameters.AddWithValue("@mob", textBox2.Text);
            cmd.Parameters.AddWithValue("@age", numericUpDown1.Text);
            cmd.Parameters.AddWithValue("@carno", textBox11.Text);
            cmd.Parameters.AddWithValue("@ctype", textBox10.Text);
            cmd.Parameters.AddWithValue("@cmodel", textBox8.Text);
            cmd.Parameters.AddWithValue("@img", SaveImage());
            sc.Open();
            var A = cmd.ExecuteNonQuery();
            if (A > 0)
            {
                MessageBox.Show("Data updated successfully......", "Successful", MessageBoxButtons.OK,MessageBoxIcon.Information);
                BindGridView();
            }
            else
            {
                MessageBox.Show("Data not updated......","ERROR",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            sc.Close();
        }
        private byte[] SaveImage()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Image";
            ofd.Filter = "JPG File (*.jpg)| *.jpg";
            //ofd.ShowDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(ofd.FileName);
            }
        }

        private void Driver_Load(object sender, EventArgs e)
        {
            label5.Text = DriverL.lname;
        }
        void BindGridView()
        {
            //Connection
            SqlConnection sql = new SqlConnection(cs);
            string q = "select * from DRIVE where username='" + DriverL.lname + "'"+ "and pass='" + DriverL.pname + "'";
            SqlDataAdapter sda = new SqlDataAdapter(q, sql);

            //data Grid view display
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

            //image layout fit
            DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)dataGridView1.Columns[9];
            dgv.ImageLayout = DataGridViewImageCellLayout.Zoom;

            //Table layout fit
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //TAble Height
            dataGridView1.RowTemplate.Height = 50;

        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            numericUpDown1.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            textBox11.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            textBox10.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            textBox8.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
            pictureBox1.Image = GetPhoto((byte[])dataGridView1.SelectedRows[0].Cells[9].Value);
        }

        private Image GetPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection sc = new SqlConnection(cs);
            string query = "delete from DRIVE where username=@username";
            SqlCommand cmd = new SqlCommand(query, sc);
            cmd.Parameters.AddWithValue("@username", textBox1.Text);
            sc.Open();
            int A = cmd.ExecuteNonQuery();
            if (A > 0)
            {
                MessageBox.Show("Data deleted successfully......", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindGridView();
            }
            else
            {
                MessageBox.Show("Data not deleted......", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            sc.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection sc = new SqlConnection(cs);
            string query = "delete from Book where sname=@sname";
            SqlCommand cmd = new SqlCommand(query, sc);
            cmd.Parameters.AddWithValue("@username", dataGridView2.SelectedRows[0].Cells[1].Value.ToString());
            sc.Open();
            int A = cmd.ExecuteNonQuery();
            if (A > 0)
            {
                MessageBox.Show("Data deleted successfully......", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindGridView();
            }
            else
            {
                MessageBox.Show("Data not deleted......", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            sc.Close();
        }
        void BindGridView2()
        {
            //Connection
            SqlConnection sql = new SqlConnection(cs);
            string q = "select * from Book where sname='" + DriverL.lname + "'";
            SqlDataAdapter sda = new SqlDataAdapter(q, sql);

            //data Grid view display
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView2.DataSource = data;



            //Table layout fit
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //TAble Height
            dataGridView2.RowTemplate.Height = 50;
        }
    }
}

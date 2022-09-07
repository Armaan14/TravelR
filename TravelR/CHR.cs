﻿using System;
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

namespace TravelR
{
    public partial class CHR : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["cu"].ConnectionString;

        public CHR()
        {
            InitializeComponent();
            BindGridView();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CS_MAIN c = new CS_MAIN();
            c.Show();
            this.Hide();
        }
        void BindGridView()
        {
            //Connection
            SqlConnection sql = new SqlConnection(cs);
            string q = "select USERNAME,ADDR, LOC, MOB, WEB, HR,IMG from HOTEL";
            SqlDataAdapter sda = new SqlDataAdapter(q, sql);

            //data Grid view display
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

            //image layout fit
            DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)dataGridView1.Columns[6];
            dgv.ImageLayout = DataGridViewImageCellLayout.Zoom;

            //Table layout fit
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //TAble Height
            dataGridView1.RowTemplate.Height = 50;

        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(cs);
            string q = "select USERNAME,ADDR, LOC, MOB, WEB, HR,IMG from HOTEL where LOC='" + comboBox1.Text + "'";
            sql.Open();
            SqlDataAdapter sda = new SqlDataAdapter(q, sql);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;
            sql.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string SNAME, MOB, LOC, INFO1, INFO2, img;
            SNAME = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            LOC = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            MOB = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            INFO1 = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            INFO2 = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            SqlConnection sc = new SqlConnection(cs);
            string query = "insert into book values (@username, @sname, @mob, @loc, @info1, @info2,@occupation)";
            SqlCommand cmd = new SqlCommand(query, sc);
            cmd.Parameters.AddWithValue("@username", Customer.loginuser);
            cmd.Parameters.AddWithValue("@sname", SNAME);
            cmd.Parameters.AddWithValue("@mob", MOB);
            cmd.Parameters.AddWithValue("@loc", LOC);
            cmd.Parameters.AddWithValue("@info1", INFO1);
            cmd.Parameters.AddWithValue("@info2", INFO2);
            cmd.Parameters.AddWithValue("@OCCUPATION", label1.Text);

            sc.Open();
            var A = cmd.ExecuteNonQuery();
            if (A > 0)
            {
                MessageBox.Show("Booked for you......");

            }
            else
            {
                MessageBox.Show("Can not book......");
            }
            sc.Close();
        }

        
    }
}

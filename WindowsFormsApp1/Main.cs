using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Main : Form
    {
        string txt;
        public Main(string text)
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.todo;
            txt = text;
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bogda\Documents\Data.mdf;Integrated Security=True;Connect Timeout=30");
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataSet ds = new DataSet();
        
        private void label1_Click(object sender, EventArgs e)
        {

        }
        public void getRecords()
        {
            ds = new DataSet();
            adapter = new SqlDataAdapter("select * from tasks", con);
            adapter.Fill(ds, "tasks");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "tasks";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ds = new DataSet();

            adapter = new SqlDataAdapter("insert into tasks (taskName,username,deadline) values ('" + textBox1.Text.ToString() + "','" +txt + "','" +dateTimePicker1.Value.Date +"')" , con);
            
            adapter.Fill(ds, "tasks");
            textBox1.Clear();
            getRecords();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //show tasks
            ds = new DataSet();
            con.Open();
            SqlCommand countRows = new SqlCommand("Select COUNT(*) from tasks",con);
            
            int result = int.Parse(countRows.ExecuteScalar().ToString());
            if (result == 0)
            {
                MessageBox.Show("no more tasks to do");
            }
            else
            {
                adapter = new SqlDataAdapter("select  * from tasks  ", con);
                adapter.Fill(ds, "tasks");
                textBox1.Clear();
                getRecords();
            }
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ds = new DataSet();
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bogda\Documents\Data.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            if (string.IsNullOrWhiteSpace(textBox1.Text) != null){
                adapter = new SqlDataAdapter("Delete from tasks where taskName = '" + textBox1.Text.ToString() + "'", con);
                adapter.Fill(ds, "tasks");
               
            }
            else
            { 
                SqlCommand cmd = new SqlCommand("SELECT TOP(1) ID FROM tasks ORDER BY 1 DESC",con);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                int id = Int32.Parse(reader["ID"].ToString());
                reader.Close();
                adapter = new SqlDataAdapter("Delete from tasks where ID = '" + id + "'", con);
                adapter.Fill(ds, "tasks");
            }
            getRecords();
            con.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f = new Form2();
            f.Show();
        }
    }
}

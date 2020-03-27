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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
            this.BackgroundImage = Properties.Resources.index;
        }

        public void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public void button2_Click(object sender, EventArgs e)
        {
            string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bogda\Documents\Data.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection con = new SqlConnection(conString);
            try
            {
                con.Open();   
            }
            catch(Exception)
            {
                MessageBox.Show("couldn't open database");
            }
            SqlDataAdapter sda = new SqlDataAdapter("Select  Count(*) FROM login where username ='"+ textBox1.Text +
                "'and password ='" + textBox2.Text+ "'",con ) ;

            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                this.Hide();
                Main ss = new Main(textBox1.Text.ToString());

                ss.Show();
            }
            else
            {
                MessageBox.Show("This account doesn't exist.Please signup!");
                this.Hide();
                Signup sign = new Signup();
                sign.Show();
            }

        }

        public void Form1_Load(object sender, EventArgs e)
        {

        }

        public void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public string tbText
        {
            get
            {
                return textBox1.Text.ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Signup sgn = new Signup();
            this.Hide();
            sgn.Show();
        }
    }
}

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
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
            loadcaptchaimage();
            textBox2.PasswordChar = '*';
            this.BackgroundImage = Properties.Resources.index;
        }
        int number = 0;
        private void loadcaptchaimage()
        {
            Random r = new Random();
            number = r.Next(100, 200);
            var img = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            var font = new Font("TimesNewRoman", 25, FontStyle.Bold, GraphicsUnit.Pixel);
            var graphics = Graphics.FromImage(img);
            graphics.DrawString(number.ToString(), font, Brushes.Black, new Point(0, 0));
            pictureBox1.Image = img;
        }
        string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bogda\Documents\Data.mdf;Integrated Security=True;Connect Timeout=30";
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Signup_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
                MessageBox.Show("please fill fields");
            else
            {
                using (SqlConnection sqlcon = new SqlConnection(conString))
                {
                    sqlcon.Open();

                    if (textBox3.Text == number.ToString())
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO login (username,password) values (@user,@pass)", sqlcon);
                        cmd.Parameters.AddWithValue("@user", textBox1.Text.ToString());
                        cmd.Parameters.AddWithValue("@pass", textBox2.Text.ToString());

                        cmd.ExecuteNonQuery();
                        sqlcon.Close();
                        MessageBox.Show("registration succeeded");
                        Main todo = new Main(textBox1.Text.ToString());
                        todo.Show();
                        this.Hide(); // vreau sa ascund signup cs
                    }
                    else
                    {
                        MessageBox.Show("wrong captcha");
                        this.Close();
                    }
                    Clear();
                }
            }
          
        }
        private void Clear()
        {
            textBox1.Text = textBox2.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

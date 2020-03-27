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
using System.IO;


namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.travelling_background_design_1262_2532;
            load();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Image.FromFile(@"D:\login_system\WindowsFormsApp1\WindowsFormsApp1\Imagini\" + str);
            string s = comboBox2.SelectedItem.ToString();
            if(s.Contains("D: \\\ \\"))
            pictureBox1.Image = new Bitmap(s);

        }

        public class Info
        {
            public string numeLoc;
            public List<string> fisierLoc = new List<string>();
            
            public string NumeLoc { get; set; }
            public List<string> FisierLoc { get; set; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bogda\Documents\Data.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
          

            string line;
            Info info = new Info();
            StreamReader file = new StreamReader(@"C:\Users\bogda\Desktop\judet\Resurse_C#\Resurse\planificari.txt");
            SqlCommand sqlCmd;
      
            string queryLoc = "insert into Localitati (Nume) values (@nume)";
            string queryImg = "insert into Imagini (IDLocalitate,CaleFisier) values (@id,@path)";

            while (( line = file.ReadLine()) != null)
            {
                string[] fields = line.Split('*');
                info.numeLoc = fields[0].ToString();
                int index = 0;
                if (fields[1].ToString() == "ocazional")
                    index = 5;
                else
                    index = 3;
                while ( index < fields.Length)
                {
                    info.fisierLoc.Add(fields[index].ToString());
                    index++;
                }
                line = file.ReadLine();
                sqlCmd = new SqlCommand(queryLoc, con);
                sqlCmd.Parameters.Add("@nume", SqlDbType.VarChar);
                sqlCmd.Parameters["@nume"].Value = info.numeLoc;
                sqlCmd.ExecuteNonQuery();

                sqlCmd = new SqlCommand("select IDLocalitate from Localitati where Nume = '"+info.numeLoc+"'",con);
                int id = Int32.Parse(sqlCmd.ExecuteScalar().ToString());
                
                int k = 0;
            
                while(k < info.fisierLoc.Count)
                {
                    
                    sqlCmd = new SqlCommand(queryImg, con);
                    sqlCmd.Parameters.Add("@id",id );
                     sqlCmd.Parameters.Add("@path", @"D:\login_system\WindowsFormsApp1\WindowsFormsApp1\Imagini\" + info.fisierLoc[k]);
                    k++;
                    sqlCmd.ExecuteNonQuery();
                   
                }
                
            }
            con.Close();

        }

        public void load()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bogda\Documents\Data.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();

            string query = "select * from Localitati";
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da;
            DataTable dt;
            da = new SqlDataAdapter(query, con);
            dt = new DataTable();
            da.Fill(dt);
            DataRow dr = dt.NewRow();
            comboBox1.ValueMember = "IDLocalitate";
            comboBox1.DisplayMember = "Nume";
            comboBox1.DataSource = dt;
          


            con.Close();
        }
    
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //combobox cascade on youtube

            if(comboBox1.SelectedValue.ToString() != null)
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bogda\Documents\Data.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();

                string query = "select * from Imagini where IDLocalitate = @id";
                SqlCommand cmd = new SqlCommand(query,con);
                SqlDataAdapter da;
                cmd.Parameters.AddWithValue("@id", comboBox1.SelectedValue.ToString());
                DataTable dt;
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                
                comboBox2.ValueMember = "IDImagine";
                comboBox2.DisplayMember = "CaleFisier";
                comboBox2.DataSource = dt;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            pictureBox1_Click(sender, e);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}

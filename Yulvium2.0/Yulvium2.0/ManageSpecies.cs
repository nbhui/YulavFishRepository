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

namespace Yulvium2._0
{
    public partial class ManageSpecies : Form
    {
        public ManageSpecies()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\שחר כהן\Documents\Fishdb.mdf"";Integrated Security=True;Connect Timeout=30");


        void populate()
        {
            try
            {
                con.Open();
                string myQuerry = "select * from fishTable order by speciesID";
                SqlDataAdapter da = new SqlDataAdapter(myQuerry, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                fishGV.DataSource = ds.Tables[0];
                con.Close();
            }
            catch
            {

            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into fishTable values('" + speciesNameTB.Text + "','" + 
                speciesID.Text + "')", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Species successfully added.");
                con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void fishMenagment_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (speciesNameTB.Text == "")
            {
                MessageBox.Show("Please enter the subspecies id you want to delete, YULAV.");
            }
            else
            {
                con.Open();
                string myQury = "delete from fishTable where speciesName = '" + speciesNameTB.Text + "';";
                SqlCommand cmd = new SqlCommand(myQury, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Fish successfully deleted.");
                con.Close();
                populate();
            }
        }
        private void fishGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            speciesNameTB.Text = fishGV.SelectedRows[0].Cells[0].Value.ToString();
            speciesID.Text = fishGV.SelectedRows[0].Cells[1].Value.ToString();
        }

      
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update fishTable set speciesName = '" + speciesNameTB.Text + "' where speciesID = '" + speciesID.Text + "'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Fish successfully updated.");
                con.Close();
                populate();
            }
            catch
            {

            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }
    }
}




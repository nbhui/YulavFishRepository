using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yulvium2._0
{
    public partial class ManageFish : Form
    {
        public ManageFish()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\שחר כהן\Documents\Fishdb.mdf"";Integrated Security=True;Connect Timeout=30");


        void fillcategory()
        {
            string querry = "select * from fishTable";
            SqlCommand cmd = new SqlCommand(querry, con);
            SqlDataReader rdr;
            try
            {
                con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("speciesName", typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                subCombo.ValueMember = "speciesName";
                subCombo.DataSource = dt;
                searchCombo.ValueMember = "speciesName";
                searchCombo.DataSource = dt;
                con.Close();
            }
            catch
            {

            }
        }
        void fillFishCategory()
        {
            string querry = "select * from productTBL";
            SqlCommand cmd = new SqlCommand(querry, con);
            SqlDataReader rdr;
            try
            {
                con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("Fname", typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                fishSearchCombo.ValueMember = "Fname";
                fishSearchCombo.DataSource = dt;
                con.Close();
            }
            catch
            {

            }
        }

        private void storeData_Load(object sender, EventArgs e)
        {
            fillcategory();
            fillFishCategory();
            populate();
        }

        void populate()
        {
            try
            {
                con.Open();
                string myQuerry = "select Fname,Fquantity,FsellPrice,FbuyPrice,Fsize,Fspecies from productTBL";
                SqlDataAdapter da = new SqlDataAdapter(myQuerry, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                productGV.DataSource = ds.Tables[0];
                con.Close();
            }
            catch
            {

            }
        }

        void filterBySpecies()
        {
            try
            {
                con.Open();
                string myQuerry = "select * from productTBL where Fspecies = '"+searchCombo.SelectedValue.ToString()+"'";
                SqlDataAdapter da = new SqlDataAdapter(myQuerry, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                productGV.DataSource = ds.Tables[0];
                con.Close();
            }
            catch
            {

            }
        }

        void filterByFish()
        {
            try
            {
                con.Open();
                string myQuerry = "select * from productTBL where Fname = '" + fishSearchCombo.SelectedValue.ToString() + "'";
                SqlDataAdapter da = new SqlDataAdapter(myQuerry, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                productGV.DataSource = ds.Tables[0];
                con.Close();
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into productTBL values('"+nameTB.Text+"','"+quantityTB.Text+"','"+sellPriceTB.Text+"','"+buyPriceTB.Text+"','"+sizeTB.Text+"','"+subCombo.Text.ToString()+ "','" +null+"')",con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Fish successfully added");
                con.Close();
                populate();
            }
            finally
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (nameTB.Text == "")
            {
                MessageBox.Show("Please enter the fish name, YULAV.");
            }
            con.Open();
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete fish from repository, Yulav?", "Delete Fish", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string myQury = "delete from productTBL where Fname = '" + nameTB.Text + "';";
                SqlCommand cmd = new SqlCommand(myQury, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Fish successfully deleted.");
            }
            else
            {
                con.Close();
                populate();
            }
            con.Close();
            populate();
        }

        private void productGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            nameTB.Text = productGV.SelectedRows[0].Cells[0].Value.ToString();
            quantityTB.Text = productGV.SelectedRows[0].Cells[1].Value.ToString();
            sellPriceTB.Text = productGV.SelectedRows[0].Cells[2].Value.ToString();
            buyPriceTB.Text = productGV.SelectedRows[0].Cells[3].Value.ToString();
            sizeTB.Text = productGV.SelectedRows[0].Cells[4].Value.ToString();
            subCombo.Text = productGV.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
             try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update productTBL set Fquantity = '" + quantityTB.Text + "',FsellPrice = '" +sellPriceTB.Text+ "',FbuyPrice = '" +buyPriceTB.Text+"',Fsize = '"+sizeTB.Text+ "', Fspecies = '"+subCombo.SelectedValue.ToString()+ "' where Fname = '" + nameTB.Text + "'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Update Complete");
                con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            filterBySpecies();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void searchCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            filterByFish();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            nameTB.Clear();
            quantityTB.Clear();
            buyPriceTB.Clear();
            sizeTB.Clear();
            sellPriceTB.Clear();
            
        }
    }
}

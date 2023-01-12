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
using Guna.UI2.AnimatorNS;


namespace Yulvium2._0
{
    public partial class ManageDeath : Form
    {
        public ManageDeath()
        {
            InitializeComponent();
        }

        int death,quantity;
        double deathPct;
        private void button1_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\שחר כהן\Documents\Fishdb.mdf"";Integrated Security=True;Connect Timeout=30");
        void populate()
        {
            try
            {
                con.Open();
                string myQuerry = "select Fname,Fquantity,Losts from productTBL order by Fname";
                SqlDataAdapter da = new SqlDataAdapter(myQuerry, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                DeathGV.DataSource = ds.Tables[0];
                con.Close();
            }
            catch
            {

            }
        }

        private void ManageDeath_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void DeathGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            fishName.Text = DeathGV.SelectedRows[0].Cells[0].Value.ToString();
            qty.Text = DeathGV.SelectedRows[0].Cells[1].Value.ToString();
            quantity = Convert.ToInt32(DeathGV.SelectedRows[0].Cells[1].Value.ToString());
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (fishName.Text == "" || qty.Text == "") MessageBox.Show("Please Select Fish");
            DialogResult dialogResult = MessageBox.Show(deathBox.Text + " " + fishName.Text + " Fishes are dead?", "Fish death", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                updateStock();
                MessageBox.Show(fishName.Text + "'s Quantity Updated.");
                fishName.Clear();
            }
            else if (dialogResult == DialogResult.No)
            {
                con.Close();
                populate();
            }
            con.Close();
            populate();
        }

        void updateStock()
        {
            con.Open();
            int newQty = quantity - Convert.ToInt32(deathBox.Text);
            deathPct = Convert.ToDouble(deathBox.Text) / quantity * 100;
            string querry1 = "update productTBL set Fquantity = '" + newQty + "'where Fname='" + fishName.Text + "';";
            SqlCommand cmd = new SqlCommand(querry1, con);
            cmd.ExecuteNonQuery();
            string querry2 = "update productTBL set Losts = '" + deathPct + "'where Fname='" + fishName.Text + "';";
            cmd = new SqlCommand(querry2, con);
            cmd.ExecuteNonQuery();
            qty.Clear();
            deathBox.Clear();
            con.Close();
            populate();
        }
    }
}

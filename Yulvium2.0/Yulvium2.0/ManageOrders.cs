using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yulvium2._0
{
    public partial class ManageOrders : Form
    {
        public ManageOrders()
        {
            InitializeComponent();
        }

        int price, quantity, sum, flag = 0, num = 0;
        int currentTotal = 0,totalItems = 0;
        int profit = 0,buyPrice = 0,totalProfit=0;
        string product;
        int stock;
        Random orderID = new Random();
        int returnedOrderID;
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\שחר כהן\Documents\Fishdb.mdf"";Integrated Security=True;Connect Timeout=30");

        void fillSpeciesCategory()
        {
            string querry = "select * from productTBL";
            SqlCommand cmd = new SqlCommand(querry, con);
            SqlDataReader rdr;
            try
            {
                con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("Fspecies", typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                searchCombo.ValueMember = "Fspecies";
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
                searchFish.ValueMember = "Fname";
                searchFish.DataSource = dt;
                con.Close();
            }
            catch
            {

            }
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
                string myQuerry = "select * from productTBL where Fspecies = '" + searchCombo.SelectedValue.ToString() + "'";
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
                string myQuerry = "select Fname,Fquantity,FsellPrice,FbuyPrice,Fsize,Fspecies from productTBL where Fname = '" + searchFish.SelectedValue.ToString() + "'";
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

        private void ManageOrders_Load(object sender, EventArgs e)
        {
            fillSpeciesCategory();
            fillFishCategory();
            populate();
        }

        private void searchCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            filterByFish();
        }

        private void button7_Click(object sender, EventArgs e)
        {
           
           if (flag == 0)
            {
                MessageBox.Show("Cart is empty");
            }
            else
            {
                con.Open();
                returnedOrderID = generateRandID(orderID);
                dayliOrder.Text = (returnedOrderID).ToString();
                SqlCommand cmd = new SqlCommand("insert into OrdersTable values('" + dayliOrder.Text + "','" + totalitemsBox.Text + "','" + totalBox.Text + "','" + profitBox.Text + "','" + orderDate.Text + "')", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Order successfully added");
                con.Close();
                try
                {

                }
                catch
                {

                }
            }
            
        }

        private void orderGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            ViewOrders view = new ViewOrders();
            view.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void orderDate_ValueChanged(object sender, EventArgs e)
        {
        }

        private void button10_Click(object sender, EventArgs e)
        {
            orderName.Clear();
            qty.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            filterBySpecies();
        }
        private void productGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            orderName.Text = productGV.SelectedRows[0].Cells[0].Value.ToString();
            product = productGV.SelectedRows[0].Cells[0].Value.ToString();
            stock = Convert.ToInt32(productGV.SelectedRows[0].Cells[1].Value.ToString());
            price = Convert.ToInt32(productGV.SelectedRows[0].Cells[2].Value.ToString());
            buyPrice = Convert.ToInt32(productGV.SelectedRows[0].Cells[3].Value.ToString());
        }
        private void button1_Click(object sender, EventArgs e)
        {
            flag = 1;
            if (qty.Text == "") MessageBox.Show("Please insert quantity");
            else if (orderName.Text == "") MessageBox.Show("Please select product");
            else if (Convert.ToInt32(qty.Text) > stock) MessageBox.Show("Not enough stock available");
            else
            {
                num += 1;
                quantity = Convert.ToInt32(qty.Text);
                sum = quantity * price;
                totalItems += quantity;
                profit = sum - (quantity * buyPrice);
                orderGV.Rows.Add(num, product, quantity, price, sum, profit);
              //  flag = 0;
                currentTotal = currentTotal + sum;
                totalProfit += profit;
                totalBox.Text = currentTotal.ToString();
                totalitemsBox.Text = totalItems.ToString();
                profitBox.Text = totalProfit.ToString();
                updateStock();
                qty.Clear();
                orderName.Clear();
            }
            
        }
        void updateStock()
        {
            con.Open();
            int newQty = stock - Convert.ToInt32(qty.Text);
            string querry = "update productTBL set Fquantity = '" + newQty + "'where Fname='" + orderName.Text + "';";
            SqlCommand cmd = new SqlCommand(querry,con);
            cmd.ExecuteNonQuery();
            con.Close();
            populate();
        }

        int generateRandID(Random id)
        {
            return id.Next(1,20000);
        }
       


        private void deleteButton_Click(object sender, EventArgs e)
        {

        }
    }

    


}

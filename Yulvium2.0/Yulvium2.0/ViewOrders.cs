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
    public partial class ViewOrders : Form
    {
        public ViewOrders()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\שחר כהן\Documents\Fishdb.mdf"";Integrated Security=True;Connect Timeout=30");

        void populate()
        {
            try
            {
                con.Open();
                string myQuerry = "select * from OrdersTable";
                SqlDataAdapter da = new SqlDataAdapter(myQuerry, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                viewOrdersGV.DataSource = ds.Tables[0];
                con.Close();
            }
            catch
            {

            }
        }

       

        private void ViewOrders_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button8_Click(object sender, EventArgs e)//delete order
        {
            con.Open();
            //Ask user if he sure he wants to delete order.
            if (orderID.Text == "")
            {
                MessageBox.Show("Please Select Order");
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete order?", "Delete Order", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string myQury = "delete from OrdersTable where OrderNum = '" + orderID.Text + "';";
                    SqlCommand cmd = new SqlCommand(myQury, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Order successfully deleted.");
                }
                else if (dialogResult == DialogResult.No)
                {
                    con.Close();
                    populate();
                }
            }
            
            con.Close();
            populate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void viewOrdersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            orderID.Text = viewOrdersGV.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            orderID.Clear();
        }
    }
}

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
using static Yulvium2._0.login;

namespace Yulvium2._0
{
    public partial class changePassword : Form
    {
        public changePassword()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\שחר כהן\Documents\Fishdb.mdf"";Integrated Security=True;Connect Timeout=30");
        login login = new login();

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to change password?", "Change Password", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Select count(*) from UserTBL where userName = '" + userNameTxt.Text + "'and password = '" + oldPassTxt.Text + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows[0][0].ToString() == "1")
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update UserTBL set password = '" + newPassTxt.Text + "' where userName = '" + userNameTxt.Text + "'and password = '" + oldPassTxt.Text + "'", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Password Successfully Changed");
                    con.Close();
                    login.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("User Name or Password Incorrect");
                    newPassTxt.Clear();
                    oldPassTxt.Clear();
                }
            }
            else
            {
                newPassTxt.Clear();
                oldPassTxt.Clear();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            login.Show();
            this.Hide();
        }
    }
}

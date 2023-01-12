using System.Data;
using System.Data.SqlClient;

namespace Yulvium2._0
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }


        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\שחר כהן\Documents\Fishdb.mdf"";Integrated Security=True;Connect Timeout=30");

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) from UserTBL where userName = '"+txtUserName.Text+ "'and password = '"+txtPassword.Text+"'",con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                Home home = new Home();
                home.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong Username Or Password.");
                txtPassword.Clear();
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            new changePassword().Show();
            this.Hide();
            
        }
        
    }
}
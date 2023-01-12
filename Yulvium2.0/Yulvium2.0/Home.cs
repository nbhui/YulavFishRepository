using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yulvium2._0
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ManageOrders orders = new ManageOrders();
            orders.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ManageFish fish = new ManageFish();
            fish.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ManageSpecies spe = new ManageSpecies();
            spe.Show();
            this.Hide();
        }

       
        /*
        */
        private void button3_Click(object sender, EventArgs e)
        {
            login log = new login();
            log.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ManageDeath death = new ManageDeath();
            death.Show();
            this.Hide();
        }
    }
}

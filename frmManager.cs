
using Bakery.DashBoard;
using Bakery.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bakery
{
    public partial class frmManager: Form
    {
        private string role;
        public frmManager() { }
        public frmManager(string Role) //Phân quyền
        {
            InitializeComponent();
            this.role = Role;
            if (role == "Employee")
            {
                btnOverview.Visible = false;
                btnManage.Visible = true;
                btnAdmin.Visible = false;
                btnOrder.Visible = true;
                btnGift.Visible = true;
            }
        }
        private void btnLogout_Click(object sender, EventArgs e) //btnLogout
        {
            DialogResult dlr = MessageBox.Show("Do you want to log out?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dlr == DialogResult.Yes)
            {
                frmLogin frmLogin = new frmLogin();
                this.Hide();
                frmLogin.ShowDialog();
            }
        }
        private void btnManage_Click(object sender, EventArgs e) //Manage
        {
                frmManage frmManage = new frmManage(role);
                this.Hide();
                frmManage.ShowDialog();
                this.Show();
        }
        private void btnOverview_Click_1(object sender, EventArgs e) //Overview
        {
                frmOverview frmOverview = new frmOverview();
                this.Hide();
                frmOverview.ShowDialog();
                this.Show();
        }
        private void btnAdmin_Click(object sender, EventArgs e) //Admin
        {
                frmAdmin frmAdmin = new frmAdmin(role);
                this.Hide();
                frmAdmin.ShowDialog();
                this.Show();
        }
        private void btnGift_Click(object sender, EventArgs e) //Gift
        {
            frmGift frmGift = new frmGift(role);
            this.Hide();
            frmGift.ShowDialog();
            this.Show();
        }
        private void button1_Click(object sender, EventArgs e) //Order
        {
            frmOrders frmOrders = new frmOrders(role);
            this.Hide();
            frmOrders.ShowDialog();
            this.Show();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

using Bakery.DashBoard;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bakery
{
    public partial class frmOverview: Form
    {
        private string role;
        public frmOverview()
        {
            InitializeComponent();
        }
        private void frmOverview_Load(object sender, EventArgs e) //Load form
        {
            soldProducts1.Visible = false;
            totalInvoice1.Visible = false;
            general1.Visible = false;
            this.panelProducts.Click += new System.EventHandler(this.panelProducts_Click);
            this.panelRevenue.Click += new System.EventHandler(this.panelRevenue_Click);
            this.guna2Panel2.Click += new System.EventHandler(this.guna2Panel2_Click);

            TotalRevenue();
            Product();
        }
        
        private void button16_Click(object sender, EventArgs e) //Btn Return
        {
            frmManager frmManager = new frmManager(role);
            this.Hide();
            frmManager.ShowDialog();
            this.Show();
        }
        private void panelRevenue_Click(object sender, EventArgs e) //General 
        {
            totalInvoice1.Visible = false;
            soldProducts1.Visible = false;
            general1.Visible = true;

            if (!guna2Panel4.Controls.Contains(soldProducts1))
            {
                soldProducts1.Dock = DockStyle.Fill;
                guna2Panel4.Controls.Add(soldProducts1);
            }
            general1.LoadRevenueByMonthChart();
        }
        private void guna2Panel1_Click(object sender, EventArgs e) //By Week
        {
            soldProducts1.Visible = false;
            totalInvoice1.Visible = true;
            general1.Visible = false;

            if (!guna2Panel4.Controls.Contains(totalInvoice1))
            {
                totalInvoice1.Dock = DockStyle.Fill;
                guna2Panel4.Controls.Add(totalInvoice1);
            }
        }
        private void panelProducts_Click(object sender, EventArgs e) //Products
        {
            totalInvoice1.Visible = false;
            soldProducts1.Visible = true;
            general1.Visible = false;

            if (!guna2Panel4.Controls.Contains(soldProducts1))
            {
                soldProducts1.Dock = DockStyle.Fill;
                guna2Panel4.Controls.Add(soldProducts1);
            }

            soldProducts1.LoadChart(@"D:\BAKERY\Bakery\bin\Debug\DATA\Products.txt");
        }
        public void TotalRevenue() //Hiển thị tổng doanh thu lên label
        {
            try
            {
                string filepath = @"D:\BAKERY\Bakery\bin\Debug\DATA\Invoices.txt";
                long total = 0;

                if (File.Exists(filepath))
                {
                    string[] lines = File.ReadAllLines(filepath);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length >= 5)
                        {
                            if (long.TryParse(parts[4], out long value))
                            {
                                total += value;
                            }
                        }
                    }
                }
                lblTotal.Text = $"{total:N0} vnd";
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error loading total revenue: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Product() //Hiển thị số lượng sản phẩm bán và tổng sản phẩm lên label
        {
            try
            {
                string filepath = @"D:\BAKERY\Bakery\bin\Debug\DATA\Products.txt";
                int total = 0;
                int sold = 0;

                if (File.Exists(filepath))
                {
                    string[] lines = File.ReadAllLines(filepath);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length >= 5)
                        {
                            if (int.TryParse(parts[3], out int quan) && int.TryParse(parts[4], out int solds))
                            {
                                total += quan;
                                sold += solds;
                            }
                        }
                    }
                }
                lblTotalP.Text = $"{total:N0} Total";
                lblSold.Text = $"{sold:N0} Sold";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void panelRevenue_Paint(object sender, PaintEventArgs e)
        {

        }
        private void guna2Panel2_Click(object sender, EventArgs e)
        {

        }
        private void dbInvoices1_Load(object sender, EventArgs e)
        {

        }

        private void guna2Panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void totalInvoice1_Load(object sender, EventArgs e)
        {

        }
        private void panelProducts_Paint(object sender, PaintEventArgs e)
        {

        }
        private void txbTime_TextChanged(object sender, EventArgs e) 
        {

        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {

        }
        private void general1_Load(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox4_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void guna2Panel2_Paint(object sender, PaintEventArgs e) 
        {

        }
    }
}

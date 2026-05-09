using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bakery
{
    public partial class frmLogin: Form
    {
      
        public frmLogin()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private Dictionary<string, (string password, string Role)> LoadCredentials()
        {
            Dictionary<string, (string Password, string Role)> credentials = new Dictionary<string, (string Password, string Role)>();
            string filePath = @"D:\BAKERY\Bakery\bin\Debug\DATA\Account.txt";

            try
            {
                foreach (string line in File.ReadLines(filePath))
                {
                    string[] parts = line.Split(',');

                    if (parts.Length == 3) 
                    {
                        string username = parts[0].Trim();
                        string password = parts[1].Trim();
                        string Role = parts[2].Trim();
                        credentials[username] = (password, Role);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Data not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return credentials;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            var credentials = LoadCredentials();

            string username = txbusername.Text.Trim();
            string password = txbpassword.Text.Trim();
            string role= cbRole.Text.Trim();

            if (credentials.ContainsKey(username) && credentials[username].password == password && credentials[username].Role == role)
            {
                frmManager manager = new frmManager(role);
                this.Hide();
                manager.ShowDialog();
            }
            else
            {
                lblerror.Visible = true;
                txbusername.Clear();
                txbpassword.Clear();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Do you want to exit?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dlr == DialogResult.Yes)  Application.Exit();
        }

        private void lblerror_Click(object sender, EventArgs e)
        {
            lblerror.Visible = false;
        }

        private void txbusername_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txbRole_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void txbpassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

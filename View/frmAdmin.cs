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
    public partial class frmAdmin: Form
    {
        public static List<Admin> accountList = new List<Admin>();
        private string role;
        public frmAdmin() { }
        public frmAdmin(string role)
        {
            InitializeComponent();
            this.role = role;
        }

        private void btnreturn_Click(object sender, EventArgs e)
        {
            frmManager frmManager = new frmManager(role);
            this.Hide();
            frmManager.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e) //Add
        {

                try
                {
                    string username = txbusername.Text;
                    string password = txbpassword.Text;
                    string Role = cbRole.Text;
                

                Admin.CreateAccount(username, password, Role, dgvAccount);
                txbusername.Clear();
                txbpassword.Clear();
                

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private void btnViewAd_Click(object sender, EventArgs e) //View
        {
                string filepath = @"D:\BAKERY\Bakery\bin\Debug\DATA\Account.txt";
                Admin.loadDataAccount(filepath, dgvAccount);
        }

        private void btnSaveAd_Click(object sender, EventArgs e) //save
        {
            string filepath = @"D:\BAKERY\Bakery\bin\Debug\DATA\Account.txt";
            Admin.saveDataAccount(filepath, dgvAccount);

        }

        private void btnDelete_Click(object sender, EventArgs e) //Delete
        {
            if (dgvAccount.SelectedRows.Count > 0)
            {
               
                int rowIndex = dgvAccount.SelectedRows[0].Index;

                
                dgvAccount.Rows.RemoveAt(rowIndex);

                txbusername.Clear();
                txbpassword.Clear();
                
            }
            else
            {
                MessageBox.Show("Please choose a row", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void dgvAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow selectedRow = dgvAccount.Rows[e.RowIndex];

                if (selectedRow.Cells.Count > 0)
                {
                    
                    txbusername.Text = selectedRow.Cells[0].Value != null ? selectedRow.Cells[0].Value.ToString() : "";
                    txbpassword.Text = selectedRow.Cells[1].Value != null ? selectedRow.Cells[1].Value.ToString() : "";
                    cbRole.Text = selectedRow.Cells[2].Value != null ? selectedRow.Cells[1].Value.ToString() : "";
                }
            }
        }
        

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (dgvAccount.SelectedRows.Count > 0)
            {
              
                string username = txbusername.Text;
                string newPassword = txbpassword.Text;
                string role = cbRole.Text;

                Admin.changeAccount(username, newPassword, role, dgvAccount);

                foreach (DataGridViewRow row in dgvAccount.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == username)
                    {
                        row.Cells[1].Value = newPassword;
                       
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please choose a row!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {
            this.dgvAccount.CellClick += new DataGridViewCellEventHandler(this.dgvAccount_CellClick);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}

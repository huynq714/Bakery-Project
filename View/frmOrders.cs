using Bakery.Components;
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
using System.IO;


namespace Bakery
{
    public partial class frmOrders : Form
    {
        private Dictionary<string, int> stock = new Dictionary<string, int>();
        private string role;
      
        public frmOrders() { }
        public frmOrders(string Role)
        {
            InitializeComponent();
            this.role = Role;
        }

        public void AddItem(string name, string cost, Image icon) //Thêm item
        {
            Tiramisu item = new Tiramisu()
            {
                Title = name,
                Cost = cost,
                Icon = icon
            };

            item.Click += Item_Click;

            foreach (Control c in item.Controls)
            {
                c.Click += (s, e) => Item_Click(item, e);
            }

            flowLayoutPanel1.Controls.Add(item);
        }
        private void Item_Click(object sender, EventArgs e) //sự kiện click vào item
        {
           
            if (sender is Tiramisu item)
            {
                string productName = item.Title;
                if (stock.ContainsKey(item.Title) && stock[item.Title] <= 0)
                {
                    MessageBox.Show("Product is sold out!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                foreach (DataGridViewRow row in dgvOrder.Rows)
                {
                    if (row.Cells[0].Value?.ToString() == productName)
                    {
                        int count = Convert.ToInt32(row.Cells[1].Value);
                        if (stock.ContainsKey(productName) && count + 1 > stock[productName])
                        {
                            MessageBox.Show("Not enough stock available!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        count++;
                        row.Cells[1].Value = count;

                        row.Cells[3].Value = count * Convert.ToInt32(item.Cost);
                        UpdateTotal();
                        return;
                    }
                }
                    dgvOrder.Rows.Add(item.Title, 1, item.Cost, item.Cost);
                UpdateTotal();
            }
        }
        private void UpdateTotal() //tính tổng tiền
        {
            int total = 0;
            foreach (DataGridViewRow row in dgvOrder.Rows)
            {
                if (row.Cells[3].Value != null)
                {
                    total += Convert.ToInt32(row.Cells[3].Value);
                }
            }
            lblTotal.Text = "TOTAL AMOUNT: " + total.ToString("N0"); // Hiển thị tổng tiền
        }
        private void btnAddO_Click(object sender, EventArgs e) //Check out
        {
            DataTable orderData = new DataTable();

            foreach (DataGridViewColumn col in dgvOrder.Columns)
            {
                DataColumn dataColumn = new DataColumn(col.Name, col.ValueType ?? typeof(string));
                orderData.Columns.Add(col.Name, col.ValueType ?? typeof(string));
            }

            foreach (DataGridViewRow row in dgvOrder.Rows)
            {
                if (!row.IsNewRow)
                {
                    DataRow dr = orderData.NewRow();
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        dr[i] = row.Cells[i].Value ?? DBNull.Value;
                    }
                    orderData.Rows.Add(dr);
                }
            }
            string totalText = lblTotal.Text.Replace("TOTAL AMOUNT: ", "").Replace(" $", "").Replace(",", "");
            int total = int.TryParse(totalText, out int result) ? result : 0;


      
            frmCheckout frmCheckout = new frmCheckout(orderData, dgvOrder, total, role);
            this.Hide();
            frmCheckout.ShowDialog();
        }
        private void dgvOrder_CellValueChanged(object sender, DataGridViewCellEventArgs e) //thay đổi số  lượng bằng cách sửa trong datagrid
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                var row = dgvOrder.Rows[e.RowIndex];
                var countCell = row.Cells[1];
                var priceCell = row.Cells[2];
                var amountCell = row.Cells[3];
                var productName = row.Cells[0].Value?.ToString();
                if (countCell.Value != null && int.TryParse(countCell.Value.ToString(), out int count))
                {
                    if (count == 0)
                    {
                        MessageBox.Show("Quantity is 0! Product will be removed.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvOrder.Rows.RemoveAt(e.RowIndex);
                        UpdateTotal();
                        return;
                    }
                    if (stock.ContainsKey(productName) && count > stock[productName])
                    {
                        MessageBox.Show("Not enough stock available!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        row.Cells[1].Value = stock[productName];
                        count = stock[productName];
                    }
                    var cost = Convert.ToInt32(row.Cells[2].Value);
                    amountCell.Value = count * cost;
                    UpdateTotal();
                }
            }
        }
        private void frmOrders_Load(object sender, EventArgs e)
        {
        }
        private void frmOrders_Shown(object sender, EventArgs e)
        {
            string filePath = @"D:\BAKERY\Bakery\bin\Debug\DATA\Products.txt";
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length == 6) 
                    {
                        string productName = parts[1].Trim();
                        string price = parts[2].Trim();
                        int quantity = int.Parse(parts[3].Trim());
                        string imagePath = parts[5].Trim();

                        stock[productName] = quantity;
                        AddItem(productName, price, Image.FromFile(imagePath));
                    }
                }
            }
            else
            {
                MessageBox.Show("File not found!");
            }
        }

       

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

      


        private void btnreturn_Click(object sender, EventArgs e)
        {
            frmManager frmManager = new frmManager(role);
            this.Hide();
            frmManager.ShowDialog();
            this.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

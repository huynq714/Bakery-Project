using Bakery.Components;
using Bakery.DTO;
using System;
using System.Collections;
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
using System.Xml.Linq;

namespace Bakery
{
    public partial class frmGift: Form
    {
        private Dictionary<string, int> stock = new Dictionary<string, int>();
        public string role;
        private List<Customers> customerList;
        public frmGift()
        {
            
        }
        public frmGift(string Role)
        {
            InitializeComponent();
            this.role = Role;
            this.customerList = Customers.LoadCustomerList(@"D:\BAKERY\Bakery\bin\Debug\DATA\Customers.txt");
        }
        private void flowLayoutPanel1_Scroll(object sender, ScrollEventArgs e)
        {

        }
        private void frmGift_Load(object sender, EventArgs e)
        {

        }
        public void AddItem(string name, string cost, Image image)
        {
            Gift item = new Gift()
            {
                Title = name,
                Cost = cost,
                Icon = image
            };
            foreach (Control c in item.Controls)
            {
                c.Click += (s, e) =>
                {
                    Item_Click(item, e);
                };
            }
            item.Click += (s, e) => Item_Click(item, e);
            flowLayoutPanel1.Controls.Add(item);
        }
        private void Item_Click(object sender, EventArgs e) //Click vào item
        {
            Gift item = null;

            if (sender is Gift gift)
            {
                item = gift;
            }
            else if (sender is Control ctrl && ctrl.Parent is Gift parentGift)
            {
                item = parentGift;
            }

            if (item == null) return;
            string productName = item.Title;
                if (stock.ContainsKey(item.Title) && stock[item.Title] <= 0)
                {
                    MessageBox.Show("Product is sold out!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            foreach (DataGridViewRow row in dgvGift.Rows)
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
            dgvGift.Rows.Add(item.Title, 1);
            UpdateTotal();
        }
        private void UpdateTotal() //hiện tổng tiền =0
        {
            int total = 0;
            lblTotal.Text = "TOTAL AMOUNT: " + total.ToString("N0"); 
        }
        private void frmGift_Shown(object sender, EventArgs e)
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
                        string imagePath = parts[5].Trim();
                        string price = parts[2].Trim();
                        int quantity = int.Parse(parts[3].Trim());
                        
                        stock[productName] = quantity;
                        AddItem(productName,price, Image.FromFile(imagePath));
                    }
                }
            }
            else
            {
                MessageBox.Show("File not found!");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            frmManager frmManager = new frmManager(role);
            this.Hide();
            frmManager.Show();
        }

        private void txbSearch_TextChanged(object sender, EventArgs e)
        {
            string filePath = @"D:\BAKERY\Bakery\bin\Debug\DATA\Customers.txt";
            string searchPhone = txbSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchPhone))
            {
                txbName.Clear();
                txbPhone.Clear();
                txbPoint.Clear();
                return;
            }

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Không tìm thấy file Customers.txt!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');

                if (parts.Length >= 4)
                {
                    string name = parts[1].Trim();
                    string phone = parts[2].Trim();
                    string point = parts[3].Trim();

                    if (phone == searchPhone)
                    {
                        txbName.Text = name;
                        txbPhone.Text = phone;
                        txbPoint.Text = point;
                        return; 
                    }
                }
            }
            txbName.Clear();
            txbPhone.Clear();
            txbPoint.Clear();
        }

        private void button1_Click(object sender, EventArgs e) //Print
        {
            Invoices invoice = new Invoices();
            string name = txbName.Text.Trim();
            string phone = txbPhone.Text.Trim();

            invoice.customer.Add(new KeyValuePair<string, string>("Customer Name", txbName.Text));
            invoice.customer.Add(new KeyValuePair<string, string>("Phone Number", txbPhone.Text));

            invoice.paymentMethod = "Gift";
            invoice.dateTime = DateTime.Now;

            invoice.total = 0;

            List<KeyValuePair<string, double>> productList = new List<KeyValuePair<string, double>>();

            //Tính discount và final Total
            var customer = customerList.FirstOrDefault(c => c.Phone == phone);
           
            double discountRate = 0;
            double discountAmount = invoice.total * discountRate;
            invoice.discount = discountAmount;
            invoice.finalTotal = invoice.total - discountAmount;

            invoice.customer.Add(new KeyValuePair<string, string>("Total Amount", invoice.total.ToString("N0", new CultureInfo("vi-VN"))));
            invoice.customer.Add(new KeyValuePair<string, string>("Discount", discountAmount.ToString("N0", new CultureInfo("vi-VN"))));
            invoice.customer.Add(new KeyValuePair<string, string>("Total", invoice.finalTotal.ToString("N0", new CultureInfo("vi-VN"))));

            int totalGiftCount = 0;
            foreach (DataGridViewRow row in dgvGift.Rows)
            {
                if (!row.IsNewRow)
                {
                    string productName = row.Cells[0].Value?.ToString();
                    int count = Convert.ToInt32(row.Cells[1].Value);
                    double price = Convert.ToDouble(row.Cells[2].Value);
                    double amount = Convert.ToDouble(row.Cells[3].Value);

                    totalGiftCount += count;
                    productList.Add(new KeyValuePair<string, double>(productName, amount));
                }
            }
            //Trừ điểm và cập nhật
            int totalPointSub = totalGiftCount * 20;
            customer.Point -= totalPointSub;
            invoice.customer.Add(new KeyValuePair<string, string>("Current Points", customer.Point.ToString()));
            Customers.SavePointCustomer(@"D:\BAKERY\Bakery\bin\Debug\DATA\Customers.txt", customerList);

            invoice.product = productList;

            invoice.Print(dgvGift, customerList);
            invoice.Save(dgvGift, customerList);
            invoice.SavetoFile();
        }

        private void dgvGift_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvGift_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                var row = dgvGift.Rows[e.RowIndex];
                var countCell = row.Cells[1];
                var priceCell = row.Cells[2];
                var amountCell = row.Cells[3];
                var productName = row.Cells[0].Value?.ToString();
                if (countCell.Value != null && int.TryParse(countCell.Value.ToString(), out int count))
                {
                    if (count == 0)
                    {
                        MessageBox.Show("Quantity is 0! Product will be removed.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvGift.Rows.RemoveAt(e.RowIndex);
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
    }
}

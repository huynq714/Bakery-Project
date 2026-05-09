using Bakery.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;

namespace Bakery
{
    public partial class frmCheckout : Form
    {
        private string role;
        
        private List<Customers> customerList;
        public frmCheckout() { }
        public frmCheckout(DataTable orderData, DataGridView dgvOrder, int total, string Role)
        {
            InitializeComponent();

            this.role = Role;
            this.customerList = Customers.LoadCustomerList(@"D:\BAKERY\Bakery\bin\Debug\DATA\Customers.txt");
            dgvCheckout.AutoGenerateColumns = true;
            dgvCheckout.DataSource = orderData;
            for (int i = 0; i < dgvOrder.Columns.Count; i++)
            {
                dgvCheckout.Columns[i].HeaderText = dgvOrder.Columns[i].HeaderText;
            }
            lbltotal.Text = "TOTAL: " + total.ToString("N0");

        }

        private void frmCheckout_Load(object sender, EventArgs e)
        {

        }

        private void btnreturn_Click(object sender, EventArgs e) //Return
        {
            frmOrders frmOrders = new frmOrders(role);
            this.Hide();
            frmOrders.ShowDialog();
            this.Close();
        }
        private void btnPrint_Click(object sender, EventArgs e) ///Xây dựng hóa đơn và print
        {
            Invoices invoice = new Invoices();
            string name = txbCusname.Text.Trim();
            string phone = txbPhonenumber.Text.Trim();

            invoice.customer.Add(new KeyValuePair<string, string>("Customer Name", txbCusname.Text));
            invoice.customer.Add(new KeyValuePair<string, string>("Phone Number", txbPhonenumber.Text));

            invoice.paymentMethod = cbPay.Text;
            invoice.dateTime = DateTime.Now;

            invoice.total = int.Parse(lbltotal.Text.Replace("TOTAL: ", "").Replace(" $", "").Replace(",", ""));
           
            List<KeyValuePair<string, double>> productList = new List<KeyValuePair<string, double>>();

            //Tính discount và final Total
            var customer = customerList.FirstOrDefault(c => c.Phone == phone);
            if (customer == null)
            {
                string newID = Customers.newID();
                customer = new Customers(newID, name, phone, 0);
                customerList.Add(customer);
            }
            int oldPoints = customer.Point;
            double discountRate = 0;
            if (oldPoints >= 300)
                discountRate = 0.10;
            else if (oldPoints >= 150)
                discountRate = 0.05;
            double discountAmount = invoice.total * discountRate;
            invoice.discount = discountAmount;
            invoice.finalTotal = invoice.total - discountAmount;

            invoice.customer.Add(new KeyValuePair<string, string>("Total Amount", invoice.total.ToString("N0", new CultureInfo("vi-VN"))));
            invoice.customer.Add(new KeyValuePair<string, string>("Discount", discountAmount.ToString("N0", new CultureInfo("vi-VN"))));
            invoice.customer.Add(new KeyValuePair<string, string>("Total", invoice.finalTotal.ToString("N0", new CultureInfo("vi-VN"))));
           
            //Tính điểm tích lũy và cộng dồn cho khách hàng
            int pointsToAdd = (int)(invoice.finalTotal / 10000);
            if (customer != null)
            {
                int currentPoints = customer.Point;
                customer.Point += pointsToAdd;
                invoice.customer.Add(new KeyValuePair<string, string>("Current Points", customer.Point.ToString()));
            }
            else
            {
                string newID = Customers.newID();
                customer = new Customers(newID, name, phone, pointsToAdd);
                customerList.Add(customer);
                invoice.customer.Add(new KeyValuePair<string, string>("Current Points", pointsToAdd.ToString()));
            }
            Customers.SavePointCustomer(@"D:\BAKERY\Bakery\bin\Debug\DATA\Customers.txt", customerList);

            foreach (DataGridViewRow row in dgvCheckout.Rows)
            {
                if (!row.IsNewRow)
                {
                    string productName = row.Cells[0].Value?.ToString();
                    int count = Convert.ToInt32(row.Cells[1].Value);
                    double price = Convert.ToDouble(row.Cells[2].Value);
                    double amount = Convert.ToDouble(row.Cells[3].Value);

                    productList.Add(new KeyValuePair<string, double>(productName, amount));
                }
            }

            invoice.product = productList;

            invoice.Print(dgvCheckout, customerList);
            invoice.Save(dgvCheckout, customerList);
            invoice.SavetoFile();

            frmOrders frmOrders = new frmOrders(role);
            this.Hide();
            frmOrders.ShowDialog();
        }

        private void lblCus_Click(object sender, EventArgs e)
        {

        }

        private void txbPhonenumber_TextChanged(object sender, EventArgs e)
        {
            string filePath = @"D:\BAKERY\Bakery\bin\Debug\DATA\Customers.txt";
            string searchPhone = txbPhonenumber.Text.Trim();

            if (string.IsNullOrEmpty(searchPhone))
            {
                txbCusname.Clear();
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

                    if (phone == searchPhone)
                    {
                        txbCusname.Text = name;
                        return;
                    }
                }
            }
            txbCusname.Clear();
        }
    }

}


using Bunifu.UI.WinForms.Helpers.Transitions;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Xml.Linq;


namespace Bakery.DTO
{
      class Invoices
    {
        public string idInvoice { get;  set; }
        public List<KeyValuePair<string, string>> customer { get; set; }
        public string paymentMethod { get; set; }
        public DateTime dateTime { get; set; }
        public List<KeyValuePair<string, double>> product  { get; set; }
        public double total { get; set; }
        public double discount {  get; set; }
        public double finalTotal { get; set; }

        private static string filepath = @"C:\Users\ACER\Desktop\BAKERY\Bakery\bin\Debug\DATA\Invoices.txt";
        public static string newID() // Tạo ID dạng "HD" + số
        {
            int maxID = 0;
            foreach (var line in File.ReadLines(filepath))
            {
                var data = line.Split(',');
                if (data[0].StartsWith("HD") && int.TryParse(data[0].Substring(2), out int currentID))
                {
                    if (currentID > maxID)
                    {
                        maxID = currentID;
                    }
                }
            }

            return "HD" + (maxID + 1).ToString();
        }
        public Invoices() 
        {
            idInvoice = newID();
            product= new List<KeyValuePair<string, double>>();
            customer = new List<KeyValuePair<string, string>>();
        } 
        public static List<Invoices> LoadInvoices(string filepath) //Load hóa đơn 
        {
            List<Invoices> invoicesList = new List<Invoices>();

            if (File.Exists(filepath))
            {
                var lines = File.ReadAllLines(filepath);
                foreach (var line in lines)
                {
                    var values = line.Split(',');
                    if (values.Length == 6)
                    {
                        Invoices invoice = new Invoices
                        {
                            idInvoice = values[0],
                            dateTime = DateTime.Parse(values[3]),
                            finalTotal = double.Parse(values[4]),
                        };
                        invoicesList.Add(invoice);
                    }
                }
            }
            return invoicesList;
        }
        public static Dictionary<string, double> LoadProductPrices() //Load giá sản phẩm
        {
            var productPrices = new Dictionary<string, double>();
            string filepath = @"D:\BAKERY\Bakery\bin\Debug\DATA\Products.txt";

            if (File.Exists(filepath))
            {
                try
                {
                    foreach (var line in File.ReadLines(filepath))
                    {
                        var data = line.Split(',');

                        if (data.Length >= 4 && double.TryParse(data[2], out double price) && int.TryParse(data[4], out int soldQuantity))
                        {
                            string productName = data[1];
                            double revenue = price * soldQuantity;

                            if (!productPrices.ContainsKey(productName))
                            {
                                productPrices[productName] = price * soldQuantity;
                            }
                            else
                            {
                                productPrices[productName] += price * soldQuantity;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"File not found: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("File not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return productPrices;
        }
        public static Dictionary<string, double> Revenue() //Doanh thu theo tháng
        {
            var invoices = LoadInvoices(filepath);
            var revenueByMonth = new Dictionary<string, double>();

            foreach (var invoice in invoices)
            {
                string monthYear = invoice.dateTime.ToString("yyyy-MM");

                if (revenueByMonth.ContainsKey(monthYear))
                {
                    revenueByMonth[monthYear] += invoice.finalTotal;  
                }
                else
                {
                    revenueByMonth[monthYear] = invoice.finalTotal;  
                }
            }

            return revenueByMonth;
        }
        public static Dictionary<string, double> Revenue(DateTime startDate, DateTime endDate) //Doanh thu theo tuần
        {
            var invoices = LoadInvoices(filepath);
            var revenueByDay = new Dictionary<string, double>();

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                string key = date.ToString("dd/MM");
                revenueByDay[key] = 0;
            }

            foreach (var invoice in invoices)
            {
                if (invoice.dateTime.Date >= startDate.Date && invoice.dateTime.Date <= endDate.Date)
                {
                    string key = invoice.dateTime.ToString("dd/MM");
                    if (revenueByDay.ContainsKey(key))
                    {
                        revenueByDay[key] += invoice.finalTotal;
                    }
                }
            }

            return revenueByDay;
        }
        public void LoadData(DataGridView dgv) //Xem hóa đơn
        {
            try
            {
                string filepath = @"D:\BAKERY\Bakery\bin\Debug\DATA\Invoices.txt";
                if (File.Exists(filepath))
                {
                    var line = File.ReadAllLines(filepath);
                    dgv.Rows.Clear();
                    foreach ( var lines in line)
                    {
                        var values = lines.Split(',');
                        if (values.Length == 6)
                        {
                            dgv.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5]);
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void SavetoFile() //Lưu thống kê hóa đơn
        {
            try 
            {
                string folderpath = @"D:\BAKERY\Bakery\bin\Debug\DATA\Invoice History";
                string invoicefileName = Path.Combine(folderpath, $"Invoice_{idInvoice}.txt");
                string filepath = @"D:\BAKERY\Bakery\bin\Debug\DATA\Invoices.txt";


                using (StreamWriter writer = new StreamWriter(filepath, append: true))
                {
                    writer.WriteLine($"{idInvoice},{customer.FirstOrDefault(c => c.Key == "Customer Name").Value}," 
                        + $"{customer.FirstOrDefault(c => c.Key == "Phone Number").Value}," 
                        + $"{dateTime.ToString("yyyy/MM/dd HH:mm:ss")},{finalTotal},{invoicefileName}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving invoice summary: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void Print(DataGridView dgv, List<Customers> customerList) //In hóa đơn
        {
            string filepath = @"D:\BAKERY\Bakery\bin\Debug\DATA\Products.txt";
            Products.LoadProducts(filepath);

            StringBuilder invoice = new StringBuilder();
            invoice.AppendLine("=========== A+ BAKERY ===========");
            invoice.AppendLine($"Invoice ID: {idInvoice}");

            foreach (var item in customer)
            {
                if (item.Key != "Total Amount" && item.Key != "Discount" && item.Key != "Total")
                {
                    invoice.AppendLine($"{item.Key}: {item.Value}");
                }
            }

            invoice.AppendLine($"Payment method: {paymentMethod}");
            invoice.AppendLine($"Order time: {dateTime}");
            invoice.AppendLine("-------------------------------------");
            invoice.AppendLine("Product Name        Count       Price           Amount");
            invoice.AppendLine("------------");

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                {
                    string productName = row.Cells[0].Value?.ToString();
                    int count = Convert.ToInt32(row.Cells[1].Value);
                    double price = Convert.ToDouble(row.Cells[2].Value);
                    double amount = Convert.ToDouble(row.Cells[3].Value);

                    var product = Products.productList.FirstOrDefault(p => p.productName == productName);
                    if (product != null)
                    {
                        product.soldProduct += count;
                        product.totalCountInShop -= count;
                    }

                    invoice.AppendLine(string.Format("{0,-20} {1,-10} {2,-10} {3,10}",
                        productName,
                        count,
                       price.ToString("N0", new CultureInfo("vi-VN")),
                       amount.ToString("N0", new CultureInfo("vi-VN"))
                    ));
                }
            }
            invoice.AppendLine("-----------------------------------------");
            string totalAmount = customer.FirstOrDefault(c => c.Key == "Total Amount").Value;
            string discount = customer.FirstOrDefault(c => c.Key == "Discount").Value;
            string finalTotal = customer.FirstOrDefault(c => c.Key == "Total").Value;
            invoice.AppendLine($"TOTAL AMOUNT:    {totalAmount}");
            invoice.AppendLine($"DISCOUNT:        {discount}");
            invoice.AppendLine($"TOTAL:           {finalTotal}");
            invoice.AppendLine("--------------------------------------------");

            MessageBox.Show(invoice.ToString(), "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Products.SaveSold(filepath);
        
        }
        public void Save(DataGridView dgv, List<Customers> customerList) //Lưu từng hóa đơn về folder quản lý
        {
            try
            {
                string filepath = @"D:\BAKERY\Bakery\bin\Debug\DATA\Invoice History";
                string fileName = Path.Combine(filepath, $"Invoice_{idInvoice}.txt") ; 
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    writer.WriteLine("=========== A+ BAKERY ===========");
                    writer.WriteLine($"Invoice ID: {idInvoice}");
                    foreach (var item in customer)
                    {
                        if (item.Key != "Total Amount" && item.Key != "Discount" && item.Key != "Total")
                        {
                           writer.WriteLine($"{item.Key}: {item.Value}");
                        }
                    }

                    writer.WriteLine($"Payment Method: {paymentMethod}");
                    writer.WriteLine($"Order Time: {dateTime}");
                    writer.WriteLine("--------------------------------");

                    writer.WriteLine("Product Name        Count        Price           Amount");
                    writer.WriteLine("------------");
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string productName = row.Cells[0].Value?.ToString();
                            int count = Convert.ToInt32(row.Cells[1].Value);
                            double price = Convert.ToDouble(row.Cells[2].Value);
                            double amount = Convert.ToDouble(row.Cells[3].Value);

                            writer.WriteLine(string.Format("{0,-20} {1,-10} {2,-10} {3,10}",
                                productName,
                                count,
                                price,
                                amount
                            ));
                        }
                    }
                    writer.WriteLine("----------------------------------------------");

                    string totalAmount = customer.FirstOrDefault(c => c.Key == "Total Amount").Value;
                    string discount = customer.FirstOrDefault(c => c.Key == "Discount").Value;
                    string finalTotal = customer.FirstOrDefault(c => c.Key == "Total").Value;

                    writer.WriteLine($"TOTAL AMOUNT:      {totalAmount}");
                    writer.WriteLine($"DISCOUNT:          {discount}");
                    writer.WriteLine($"TOTAL:             {finalTotal}");

                    writer.WriteLine("==============================================");
                }

                MessageBox.Show("Invoice saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving invoice: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public  static void Search(DataGridView dgv, string searchTerm) //Search
        {
            try
            {
                string filepath = @"D:\BAKERY\Bakery\bin\Debug\DATA\Invoices.txt";
                if (File.Exists(filepath))
                {
                    var lines = File.ReadAllLines(filepath);
                    dgv.Rows.Clear();  

                    foreach (var line in lines)
                    {
                        var values = line.Split(',');
                        if (values.Length >= 6 && values.Any(value => value.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0))
                        {
                            dgv.Rows.Add(values[0], 
                                         values[1], 
                                         values[2], 
                                         values[3], 
                                         values[4], 
                                         values[5]); 
                        }
                    }

                    if (dgv.Rows.Count == 0)
                    {
                        MessageBox.Show("Invoice not found!", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching invoices: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

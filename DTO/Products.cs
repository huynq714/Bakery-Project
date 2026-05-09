
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bakery.DTO
{
    class Products : IManage
    {
        public static List<Products> productList = new List<Products>();
        public string idProduct {  get; set; }
        public string productName { get; set; }
        public double pricePerOneProduct { get; set; }
        public int totalCountInShop { get; set; }
        public int soldProduct { get; set; }
        public string photo {  get; set; }

        private static string filepath= @"C:\Users\ACER\Desktop\BAKERY\Bakery\bin\Debug\DATA\Products.txt";
        public Products() { }
        public Products(string idProduct, string productName, double pricePerOneProductint, int totalCountInShop, int soldProduct, string photo)
        {
            this.idProduct = idProduct;
            this.productName = productName;
            this.pricePerOneProduct = pricePerOneProduct;
            this.totalCountInShop = totalCountInShop;
            this.soldProduct = soldProduct;
            this.photo = photo;
        }
        public static void LoadProducts(string filepath)
        {
            productList.Clear(); 

            if (File.Exists(filepath))
            {
                foreach (var line in File.ReadAllLines(filepath))
                {
                    var data = line.Split(',');
                    if (data.Length == 6)
                    {
                        var product = new Products(
                            data[0],
                            data[1],
                            Convert.ToDouble(data[2]),
                            Convert.ToInt32(data[3]),
                            Convert.ToInt32(data[4]),
                            data[5]
                        );
                        productList.Add(product);
                    }
                }
            }
        }
        public static void SaveSold(string filepath)
        {
            if (!File.Exists(filepath))
            {
                MessageBox.Show("Không tìm thấy file sản phẩm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var updatedLines = new List<string>();

            foreach (var line in File.ReadAllLines(filepath))
            {
                var data = line.Split(',');
                if (data.Length == 6)
                {
                    string idProduct = data[0];
                    string productName = data[1];
                    double pricePerOneProduct = Convert.ToDouble(data[2]);
                    string photo = data[5];

                    var updatedProduct = productList.FirstOrDefault(p => p.productName == productName);
                    if (updatedProduct != null)
                    {
                        
                        data[3] = updatedProduct.totalCountInShop.ToString();
                        data[4] = updatedProduct.soldProduct.ToString();
                    }

                    updatedLines.Add(string.Join(",", data));
                }
            }

            File.WriteAllLines(filepath, updatedLines);
        }
        public  void LoadData(string filepath, DataGridView dgv) //LoadData
        {
            try
            {
                using (StreamReader sr = new StreamReader(filepath, System.Text.Encoding.UTF8))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] values = line.Split(',');

                        if (double.TryParse(values[2].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double pricePerOneProduct) &&
                        int.TryParse(values[3].Trim(), out int totalCountInShop) && int.TryParse(values[4].Trim(), out int soldProduct) ) 
                        {
                            string idProduct = values[0].Trim();
                            string productName = values[1].Trim();
                            string photo = values[5].Trim();


                            Products newProduct = new Products(idProduct, productName, pricePerOneProduct, totalCountInShop, soldProduct, photo);
                            productList.Add(newProduct);
                            dgv.Rows.Add(newProduct.idProduct, newProduct.productName, pricePerOneProduct.ToString(), newProduct.totalCountInShop, newProduct.soldProduct, newProduct.photo);
                        }
                    }
                }
                MessageBox.Show("Data loaded successfully!", "Success", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void Create( string ID,string productName, double pricePerOneProduct, int totalCountInShop,int soldProduct, string photo, DataGridView dgv) //Create
        {

            Products newProduct = new Products
            {
                idProduct = ID,
                productName = productName,
                pricePerOneProduct = pricePerOneProduct,
                totalCountInShop = totalCountInShop,
                soldProduct = 0,
                photo= photo
            };
            productList.Add(newProduct);

            dgv.Rows.Add(newProduct.idProduct, newProduct.productName, newProduct.pricePerOneProduct, newProduct.totalCountInShop, newProduct.soldProduct, newProduct.photo);
        }
        public static void Delete(string idProduct, DataGridView dgv) //Delete
        {

            Products productDeleted = productList.FirstOrDefault(c => c.idProduct == idProduct);
            if (productDeleted == null)
            {
                MessageBox.Show("Product not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var confirm = MessageBox.Show("Are you sure to delete this product?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                productList.Remove(productDeleted);
                dgv.Rows.Clear();
                foreach (var product in productList)
                {
                    dgv.Rows.Add(product.idProduct, product.productName, product.pricePerOneProduct, product.totalCountInShop, product.soldProduct, product.photo);
                }

                UpdateProductIDs();
                MessageBox.Show("Deleted succesfully", "Notification!", MessageBoxButtons.OK);

            }
        }
        private static void UpdateProductIDs() //Update lại ID nếu 1 sản phẩm bị xóa
        {
            for (int i = 0; i < productList.Count; i++)
            {
                productList[i].idProduct=(i+ 1).ToString(); 
            }
        }
        public static void Edit(string idProduct, string newname, double newPrice, int newQuantity,string newphoto,  DataGridView dgv) //Edit
        {
            Products product = productList.FirstOrDefault(p=>p.idProduct== idProduct);
            if (product != null)
            {
                product.productName = newname;
                product.pricePerOneProduct = newPrice;
                product.totalCountInShop = newQuantity;
                product.photo = newphoto;

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == idProduct)
                    {
                        row.Cells[1].Value = newname;
                        row.Cells[2].Value = newPrice;
                        row.Cells[3].Value = newQuantity;
                        row.Cells[5].Value = newphoto;
                        break;
                    }

                }
            }
            else
            {
                MessageBox.Show("Product not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void ArrangePro()  // Sắp xếp theo tên với culture tiếng Việt
        {
            var vietnameseCulture = new System.Globalization.CultureInfo("vi-VN");
           productList =productList.OrderBy(p => p.productName, StringComparer.Create(vietnameseCulture, false)).ToList();
        }
        public static void SearchProduct(string searchTxb, DataGridView dgv) //Search
        {
            dgv.Rows.Clear();

            var searchResult = productList.Where(p =>
                p.idProduct.Contains(searchTxb) ||
                (!string.IsNullOrEmpty(p.productName) && p.productName.Contains(searchTxb)) ||
                (!string.IsNullOrEmpty(p.idProduct) && p.idProduct.Contains(searchTxb))
            ).ToList();

            if (searchResult.Count > 0)
            {
                foreach (var product in searchResult)
                {
                    dgv.Rows.Add(product.idProduct, product.productName, product.pricePerOneProduct, product.totalCountInShop, product.soldProduct, product.photo);
                }
            }
            else
            {
                MessageBox.Show("Product not found!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void SaveToFile(string filepath, DataGridView dgv) //Save
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filepath, false))
                {
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string[] rowData = new string[dgv.ColumnCount];
                            for (int i = 0; i < dgv.ColumnCount; i++)
                            {
                                rowData[i] = row.Cells[i].Value?.ToString() ?? "";
                            }
                            string line = string.Join(",", rowData);

                            sw.WriteLine(line);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void Reset(DataGridView dgv) //Reset
        {
            dgv.Rows.Clear();
            foreach (var product in productList.OrderBy(c => int.Parse(c.idProduct)))
            {
                dgv.Rows.Add(product.idProduct, product.productName,product.pricePerOneProduct, product.totalCountInShop);
            }
        }
        public void LoadData()
        {
            throw new NotImplementedException();
        }
        public void Create()
        {
            throw new NotImplementedException();
        }
        public void Delete()
        {
            throw new NotImplementedException();
        }
        public void Edit()
        {
            throw new NotImplementedException();
        }
        public void Search()
        {
            throw new NotImplementedException();
        }
        public void SaveToFile()
        {
            throw new NotImplementedException();
        }
      

    }
}

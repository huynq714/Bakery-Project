using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using System.Security.Policy;
using Bakery.DTO;
using System.Web.Security;

namespace Bakery
{
    public partial class frmManage: Form
    {
        private string role;
        public frmManage(string Role) //Phân quyền
        {
            InitializeComponent();
            this.role = Role;
            if (role == "Employee")
            {
                for (int i = tabMain.TabPages.Count - 1; i>=0; i--)
                {
                    if (tabMain.TabPages[i].Name != "tabCustomer")
                    {
                        tabMain.TabPages.RemoveAt(i);
                    }
                }
            }
        }
      
        private void frmManage_Load(object sender, EventArgs e) //Form Load
        {
            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellClick);
            dataGridView2.CellClick += new DataGridViewCellEventHandler(dataGridView2_CellClick);
            dataGridView3.CellClick += new DataGridViewCellEventHandler(dataGridView3_CellClick);
            dataGridView4.CellContentClick += new DataGridViewCellEventHandler(dataGridView4_CellContentClick);
        }

        //CUSTOMER
        private void button1_Click_1(object sender, EventArgs e) //view customer
        {
            string filepath = @"D:\BAKERY\Bakery\bin\Debug\DATA\Customers.txt";
           Customers.LoadData(filepath, dataGridView1);
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) //Sự kiện CLick
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txbIDC.Text = row.Cells[0].Value.ToString();
                txbNameC.Text = row.Cells[1].Value.ToString();
                txbPhoneC.Text = row.Cells[2].Value.ToString();
                txbPoint.Text = Convert.ToString(row.Cells[3].Value);
            }
        }
        private void btnSearch_Click(object sender, EventArgs e) //Search Customer
        {
            string idCustomer = txbSearch.Text.Trim();
            Customers cus = new Customers();
            Customers.ArrangeCus();
            Customers customer =Customers.Search(idCustomer);
            if (customer != null)
            {
                UpdateDataGrid(customer);
            }
            else
                MessageBox.Show("Customer not found!");
        }
        private void UpdateDataGrid(Customers customer) //Update
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add(customer.Id, customer.Name, customer.Phone, customer.Point);
        }
        private void btnDelete_Click(object sender, EventArgs e) //Edit Customer
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string idCustomer = dataGridView1.SelectedRows[0].Cells[0].Value?.ToString();

                if (!string.IsNullOrEmpty(idCustomer))
                {
                    string Name= txbNameC.Text.Trim();
                    string phone= txbPhoneC.Text.Trim();
                    int point= int.Parse(txbPoint.Text.Trim()); 
                   
                    Customers.Edit(idCustomer,Name, phone, point, dataGridView1);
                }
            }
            else
            {
                MessageBox.Show("Please choose a row", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void button1_Click_2(object sender, EventArgs e) //Save Customer
        {
            string filepath = @"D:\BAKERY\Bakery\bin\Debug\DATA\Customers.txt";
            var customerManager= new Customers();
            customerManager.SaveToFile(filepath, dataGridView1);
        }
        private void btnReset_Click(object sender, EventArgs e) //Reset Customer
        {
            txbSearch.Clear();
            txbIDC.Clear();
            txbNameC.Clear();
            txbPoint.Clear();

            Customers.Reset(dataGridView1);
        }

        //PRODUCTS
        private void UpdateDataGrid(Products products)
        {
            dataGridView2.Rows.Clear();
            dataGridView2.Rows.Add(products.idProduct, products.productName, products.productName, products.totalCountInShop);
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e) //Sự kiện Click
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                txbIDPro.Text= row.Cells[0].Value.ToString();
                txbNameP.Text = row.Cells[1].Value.ToString();  
                txbPrice.Text = row.Cells[2].Value.ToString(); 
                txbQuantity.Text = row.Cells[3].Value.ToString();
                txbSold.Text= row.Cells[4].Value.ToString();
                txbPhoto.Text = row.Cells[5].Value.ToString();

                string imagePath = row.Cells[5].Value?.ToString();
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    picture.Image = Image.FromFile(imagePath);
                }
                else
                {
                    picture.Image = null; 
                    MessageBox.Show("Photo not found: " + imagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void btnViewP_Click(object sender, EventArgs e)//view product
        {
            string filepath = @"D:\BAKERY\Bakery\bin\Debug\DATA\Products.txt";
            var product = new Products();
            product.LoadData(filepath, dataGridView2);
        }
        private void btnAddP_Click(object sender, EventArgs e) //Add product
        {
            try
            {
                string ID = txbIDP.Text.Trim();
                string Name = txbNameP.Text.Trim();
                string photo= txbPhoto.Text.Trim();

                if (string.IsNullOrEmpty(ID))
                {
                    MessageBox.Show("Product ID cannot be empty.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(Name))
                {
                    MessageBox.Show("Product name cannot be empty.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!double.TryParse(txbPrice.Text, out double Price) || Price <= 0)
                {
                    MessageBox.Show("Please enter a valid price greater than 0.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txbQuantity.Text, out int Quantity) || Quantity < 0)
                {
                    MessageBox.Show("Please enter a valid quantity (positive integer).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!int.TryParse(txbSold.Text, out int soldProduct) || soldProduct < 0)
                {
                    MessageBox.Show("Please enter a valid soldProduct (positive integer).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Products.Create(ID, Name, Price, Quantity, soldProduct, photo,  dataGridView2);
                txbNameP.Clear();
                txbPrice.Clear();
                txbQuantity.Clear();
                txbSold.Clear();
                txbPhoto.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSearchP_Click(object sender, EventArgs e) //Search Product
        {
            string keyword = txbSearchP.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Please enter a product name or ID to search!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Products.SearchProduct(keyword, dataGridView2);
        }
        private void btnDeleteP_Click(object sender, EventArgs e) //delete product
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                string idProduct = dataGridView2.SelectedRows[0].Cells[0].Value?.ToString();

                if (!string.IsNullOrEmpty(idProduct))
                {
                    Products.Delete(idProduct, dataGridView2);

                }
                txbNameP.Clear();
                txbPrice.Clear();
                txbQuantity.Clear();
                txbSold.Clear();
                txbPhoto.Clear();
            }
            else
            {
                MessageBox.Show("Please choose a row", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnSaveP_Click(object sender, EventArgs e) //save datagrid
        {
            string filepath = @"D:\BAKERY\Bakery\bin\Debug\DATA\Products.txt";
            var product = new Products();
            product.SaveToFile(filepath, dataGridView2);
        }
        private void btnEdit_Click(object sender, EventArgs e) // Edit product
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                string id = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                string newName = txbNameP.Text.Trim();
                double newPrice = double.Parse(txbPrice.Text);
                int newTotalCount = int.Parse(txbQuantity.Text);
                string newphoto= txbPhoto.Text.Trim();

                Products.Edit(id, newName, newPrice, newTotalCount,newphoto, dataGridView2);
            }
        }
        private void btnResetP_Click(object sender, EventArgs e) //Reset datagrid
        {
            txbSearchP.Clear();
            txbNameP.Clear();
            txbPrice.Clear();
            txbQuantity.Clear();

            Products.Reset(dataGridView2);
        }
        private string selectedImagePath = "";
        private void btnImport_Click(object sender, EventArgs e) //Import photo
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                open.Title = "Choose an image";

                if (open.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = open.FileName;
                    txbPhoto.Text = selectedImagePath;

                    picture.Image = Image.FromFile(selectedImagePath);
                    picture.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }
        private void txbPhoto_TextChanged(object sender, EventArgs e) //Ảnh sản phảm.
        {
            string path = txbPhoto.Text.Trim();

            if (File.Exists(path))
            {
                try
                {
                    picture.Image = Image.FromFile(path);
                    picture.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                catch
                {
                    picture.Image = null;
                    MessageBox.Show("Photo Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                picture.Image = null;
            }
        }
       
        //EMPLOYEES
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e) //Sự kiện Click
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView3.Rows.Count)
            {
                DataGridViewRow row = dataGridView3.Rows[e.RowIndex];

                txbIDE.Text = row.Cells[0].Value.ToString();
                txbNameE.Text = row.Cells[1].Value.ToString();
                txbPhoneE.Text= row.Cells[2].Value.ToString();
                txbAd.Text = row.Cells[3].Value.ToString();
                txbBD.Text = row.Cells[4].Value.ToString() ;
                cbRole.Text = row.Cells[5].Value.ToString();
            }
        }
        private void btnAddE_Click(object sender, EventArgs e) //Add employee
        {
            try
            {
                string name = txbNameE.Text.Trim();
                string phone = txbPhoneE.Text.Trim();
                string address = txbAd.Text.Trim();
               
                string role = cbRole.SelectedItem?.ToString() ?? "Employee";
                if (!DateTime.TryParse(txbBD.Text.Trim(), out DateTime dateofBirth))
                {
                    MessageBox.Show("Invalid date format! Please enter a valid birth date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(address))
                {
                    MessageBox.Show("Please fill in all fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!phone.All(char.IsDigit))
                {
                    MessageBox.Show("Phone number must be numeric!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Employees employee = new Employees();
                employee.Create(name,  dateofBirth, role, phone, address, dataGridView3);

                MessageBox.Show("Added susccesfully!", "Nontification", MessageBoxButtons.OK);

                txbNameE.Clear();
                txbPhoneE.Clear();
                txbAd.Clear();
                txbBD.Clear();
                cbRole.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEditE_Click(object sender, EventArgs e) //Edit employee
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                string id = dataGridView3.SelectedRows[0].Cells[0].Value.ToString();
                string newName = txbNameE.Text.Trim();
                string newPhone = txbPhoneE.Text.Trim();
                string newAddress = txbAd.Text.Trim();
                if (!DateTime.TryParse(txbBD.Text.Trim(), out DateTime dateofBirth))
                {
                    MessageBox.Show("Invalid date format! Please enter a valid birth date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string newRole= cbRole.Text;
                Employees.Edit(id, newName, dateofBirth, newRole, newPhone, newAddress,  dataGridView3);
            }
        }
        private void btnDeleteE_Click(object sender, EventArgs e) //Delete employee
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                string idEmployee = dataGridView3.SelectedRows[0].Cells[0].Value?.ToString();
                if (!string.IsNullOrEmpty(idEmployee)) {
                    Employees.Delete(idEmployee, dataGridView3);

                    txbNameE.Clear();
                    txbPhoneE.Clear();
                    txbAd.Clear();
                }
             }
        }
        private void btnSearchE_Click(object sender, EventArgs e) //Search Employee
        {
            string idEm = txbSearchE.Text.Trim();
            Employees employees = new Employees();
            Employees.ArrangeEm();
            Employees employee =Employees.Search(idEm);
            if (employee != null)
            {
                UpdateDataGrid(employee);
            }
            else
                MessageBox.Show("Employee not found!");
        }
        private void UpdateDataGrid(Employees employee)
        {
            dataGridView3.Rows.Clear();
            dataGridView3.Rows.Add(employee.Id, employee.Name, employee.Address, employee.Phone, employee.dateofBirth, employee.role);
        }
        private void BtnViewE_Click(object sender, EventArgs e) //View Employee
        {
            string filepath = @"D:\BAKERY\Bakery\bin\Debug\DATA\Employees.txt";
            var employeeManager = new Employees();
            employeeManager.LoadData(filepath, dataGridView3);
        }
        private void btnSaveE_Click(object sender, EventArgs e) //Save employee
        {
                string filepath = @"D:\BAKERY\Bakery\bin\Debug\DATA\Employees.txt";
                var employeeManager = new Employees();
               employeeManager.SaveToFile(filepath, dataGridView3);
        }
        private void btnResetE_Click(object sender, EventArgs e)//Reset employee
        {
            txbSearchE.Clear();
            txbNameE.Clear();
            txbPhoneE.Clear();
            txbAd.Clear();
            txbBD.Clear();

            Employees.Reset(dataGridView3);
        }

        //INVOICES
        private void btnViewI_Click_1(object sender, EventArgs e) //View invoices
        {
            string filepath = @"D:\BAKERY\Bakery\bin\Debug\DATA\Invoices.txt";
            Invoices invoices = new Invoices();
            invoices.LoadData(dataGridView4);
        }
        private void btnResetI_Click(object sender, EventArgs e) //Reset sau khi tìm kiếm
        {
            dataGridView4.Rows.Clear();
            string filepath = @"D:\BAKERY\Bakery\bin\Debug\DATA\Invoices.txt";
            Invoices invoices = new Invoices();
            invoices.LoadData(dataGridView4);
        }
        public void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e) //Hiển thị chi tiết từng hóa đơn
        {
            if (e.ColumnIndex == 5)
            {
                try
                {
                    string invoiceFilePath = dataGridView4.Rows[e.RowIndex].Cells[5].Value?.ToString();
                    if (!string.IsNullOrEmpty(invoiceFilePath) && File.Exists(invoiceFilePath))
                    {
                        string invoiceContent = File.ReadAllText(invoiceFilePath);

                        frmInvoiceInfo invoiceDetailForm = new frmInvoiceInfo(invoiceContent);
                        invoiceDetailForm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Invoice file not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening invoice: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnSearchI_Click(object sender, EventArgs e) //Search Invoice
        {
            string searchInfo = txbSearchI.Text;
            Invoices.Search(dataGridView4, searchInfo);
        }

        //BUTTON, TEXTBOX, LABEL, PANEL
        private void button13_Click(object sender, EventArgs e) 
        {
            frmManager manager = new frmManager(role);
            this.Hide();
            manager.ShowDialog();
        }
        private void button14_Click(object sender, EventArgs e)
        {
            frmManager manager1 = new frmManager(role);
            this.Hide();
            manager1.ShowDialog();
        }
        private void button16_Click(object sender, EventArgs e)
        {
            frmManager manage2 = new frmManager(role);
            this.Hide();
            manage2.ShowDialog();
        }
        private void button18_Click(object sender, EventArgs e)
        {
            frmManager manage3 = new frmManager(role);
            this.Hide();
            manage3.ShowDialog();
        }
        private void btnreturn_Click(object sender, EventArgs e)
        {
            frmManager frmManager = new frmManager(role);
            this.Hide();
            frmManager.ShowDialog();
        }
        private void button13_Click_1(object sender, EventArgs e)
        {
            frmManager frmManager = new frmManager(role);
            this.Hide();
            frmManager.ShowDialog();
            this.Show();
        }
        private void btnViewI_Click(object sender, EventArgs e)
        {

        }
        private void guna2Panel8_Paint(object sender, PaintEventArgs e)
        {

        }
        private void txbName_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void tabPage1_Click(object sender, EventArgs e)
        {


        }
        private void txbSearch_TextChanged(object sender, EventArgs e)
        {

        }
        private void txbQuantity_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }
        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void label7_Click(object sender, EventArgs e)
        {

        }
        

        private void guna2Panel5_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void txbAd_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Panel9_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    
}

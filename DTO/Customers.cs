
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Bakery.DTO
{

    class Customers : People  , IManage
    {
        public static List<Customers> customerList = new List<Customers>();
        public int Point {get; set;}

        private static string filepath = @"C:\Users\ACER\Desktop\BAKERY\Bakery\bin\Debug\DATA\Customers.txt";
        public static string newID() // Tạo ID tự động dạng "KH" + số
        {

            int maxID = 0;
            foreach (var line in File.ReadLines(filepath))
            {
                var data = line.Split(',');
                if (data[0].StartsWith("KH") && int.TryParse(data[0].Substring(2), out int currentID))
                {
                    if (currentID > maxID)
                    {
                        maxID = currentID;
                    }
                }
            }

            return "KH" + (maxID + 1).ToString();
        }
        public Customers(string id, string name, string phone,  int point) : base()
        {
            this.Id = id;
            this.Name = name;
            this.Phone = phone;
            this.Point = point;
        }
        public Customers() { } 
        public static void ArrangeCus()  // Sắp xếp theo tên với culture tiếng Việt
        {
            var vietnameseCulture = new System.Globalization.CultureInfo("vi-VN");
            customerList = customerList.OrderBy(p => p.Name, StringComparer.Create(vietnameseCulture, false)).ToList();
        }
        public static Customers Search(string name) //Search
        {
            var vietnameseCulture = new System.Globalization.CultureInfo("vi-VN");
            var comparer = StringComparer.Create(vietnameseCulture, false);

            int l = 0;
            int r = customerList.Count - 1;

            while (l <= r)
            {
                int mid = (l + r) / 2;
                Customers midCustomer = customerList[mid];
                int comparison = comparer.Compare(name, midCustomer.Name);

                if (comparison == 0)
                {
                    return midCustomer;
                }
                else if (comparison < 0)
                {
                    r = mid - 1;
                }
                else
                {
                    l = mid + 1;
                }
            }
            return null;
        }
        public static void LoadData(string filepath, DataGridView dgv) //LoadData
        {
            try
            {
                using (StreamReader sr = new StreamReader(filepath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] values = line.Split(',');

                        if (values.Length == 4)
                        {
                            string id = values[0];
                            string nameCustomer = values[1];
                            string phoneNumber = values[2];
                            int Point = int.Parse(values[3]);
                            Customers newCustomer = new Customers(id, nameCustomer, phoneNumber, Point);
                            customerList.Add(newCustomer);
                            dgv.Rows.Add(id, nameCustomer, phoneNumber, Point);
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
        public static void Edit(string id, string name, string phone, int point, DataGridView dgv)//Edit
        {
            Customers customer = customerList.FirstOrDefault(p => p.Id == id);
            if (customer != null)
            {
                customer.Name = name;
                customer.Phone = phone;
                customer.Point = point;

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == id)
                    {
                        row.Cells[1].Value = name;
                        row.Cells[2].Value = phone;
                        row.Cells[3].Value = point;
                        break;
                    }

                }
            }
            else
            {
                MessageBox.Show("Customer not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void UpdatecustomerID() //Cập nhật lại ID khi 1 khách hàng trong danh sách bị xóa
        {
            for (int i = 0; i < customerList.Count; i++) {
                    customerList[i].Id = (i + 1).ToString(); 
            }
        }
        public void SaveToFile(string filepath, DataGridView dgv) //Save
        { 
            try
            {
                    using (StreamWriter sw = new StreamWriter(filepath, false))
                    {
                        foreach (var customer in customerList)
                        {
                            string line = $"{customer.Id},{customer.Name},{customer.Phone},{customer.Point}";
                            sw.WriteLine(line);
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
            foreach (var customer in customerList.OrderBy(c=>int.Parse(c.Id)))
            {
                dgv.Rows.Add(customer.Id, customer.Name,  customer.Phone, customer.Point);
            }

        }
        public static void SavePointCustomer(string filepath, List<Customers> customerList) // Lưu cập nhật điểm
        {
            try
            {

                List<string> lines = File.ReadAllLines(filepath).ToList();
                bool customerFound;

                foreach (var customer in customerList)
                {
                    customerFound = false;

                    for (int i = 0; i < lines.Count; i++)
                    {
                        string[] parts = lines[i].Split(',');

                        if (parts.Length >= 4 && parts[2].Trim() == customer.Phone)
                        {
                            parts[3] = customer.Point.ToString(); 
                            lines[i] = string.Join(",", parts);  
                            customerFound = true;
                            break;
                        }
                    }
                    if (!customerFound)
                    {
                        string newLine = $"{customer.Id},{customer.Name},{customer.Phone},{customer.Point}";
                        lines.Add(newLine);  
                    }
                }

                File.WriteAllLines(filepath, lines);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving customer points: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static List<Customers> LoadCustomerList(string filepath)
        {
            List<Customers> list = new List<Customers>();

            if (!File.Exists(filepath)) return list;

            foreach (var line in File.ReadAllLines(filepath))
            {
                var data = line.Split(',');
                if (data.Length >= 4)
                {
                    string id = data[0];
                    string name = data[1];
                    string phone = data[2];
                    int point = int.Parse(data[3]);


                    list.Add(new Customers(id, name, phone, point));
                }
            }

            return list;
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







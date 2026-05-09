using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bakery.DTO
{
    class Employees : People, IManage
    {
        public static List<Employees> employeeList = new List<Employees>();
        public DateTime dateofBirth { get; set; }
        public string role { get; set; }
        private static string filepath = @"C:\Users\ACER\Desktop\BAKERY\Bakery\bin\Debug\DATA\Employees.txt";
        public static string newID() // Tạo ID dạng "NV" + số
        {
            int maxID = 0;
            foreach (var line in File.ReadLines(filepath))
            {
                var data = line.Split(',');
                if (data[0].StartsWith("NV") && int.TryParse(data[0].Substring(2), out int currentID))
                {
                    if (currentID > maxID)
                    {
                        maxID = currentID;
                    }
                }
            }

            return "NV" + (maxID + 1).ToString();
        }
        public Employees() { }
        public Employees(string id, string name, string phone, string address, DateTime dateofBirth, string role)
            : base()
        {
            this.Id = id;
            this.Name = name;
            this.Phone = phone;
            this.Address = address;
            this.dateofBirth = dateofBirth;
            this.role = role;
        }
        public void LoadData(string filepath, DataGridView dgv) //LoadData
        {
            try
            {
                using (StreamReader sr = new StreamReader(filepath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] values = line.Split(',');

                        if (values.Length == 6 &&
                            DateTime.TryParse(values[4], out DateTime dateOfBirth))
                        {
                            string idEmployee= values[0];
                            string nameEmployee = values[1];
                            string roleOfEmployee = values[5];
                            string addressEmployee = values[3];
                            string phoneNumber = values[2];

                            Employees newEmployee = new Employees(idEmployee, nameEmployee, phoneNumber, addressEmployee, dateOfBirth, roleOfEmployee);
                            employeeList.Add(newEmployee);
                            dgv.Rows.Add(newEmployee.Id, newEmployee.Name, newEmployee.Phone, newEmployee.Address, newEmployee.dateofBirth, newEmployee.role);
                        }
                    }
                }
               
                MessageBox.Show("Data loaded susccessfully!", "Nontification", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
         public void Create( string name,  DateTime dateofbirth, string role, string phone, string address, DataGridView dgv) //Create
        {

            string ID = newID();

            Employees newEmployee = new Employees
            {
                Id = ID,
                Name = name,
                Phone = phone,
                Address = address,
                dateofBirth= dateofbirth,
                role= role,
            };
            employeeList.Add(newEmployee);

            dgv.Rows.Add(newEmployee.Id,newEmployee.Name, newEmployee.Phone, newEmployee.Address, newEmployee.dateofBirth, newEmployee.role);

        }
         public static void Delete(string id, DataGridView dgv) //Delete
        {

            Employees employeeDeleted = employeeList.FirstOrDefault(c => c.Id == id);
            if (employeeDeleted == null)
            {
                MessageBox.Show("Employee not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var confirm = MessageBox.Show("Are you sure to delete this employee?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                employeeList.Remove(employeeDeleted);

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Cells[0].Value != null && (string)row.Cells[0].Value == id)
                    {
                        dgv.Rows.Remove(row);
                        break; 
                    }
                }
                UpdateIdEmployee();
                MessageBox.Show("Deleted succesfully", "Notification!", MessageBoxButtons.OK);

            }
        }
        private static void UpdateIdEmployee() //Update ID khi có 1 nhân viên trong danh sách bị xóa
        {
            for (int i = 0; i < employeeList.Count; i++)
            {
                employeeList[i].Id = (i + 1).ToString();
            }
        }
        public static void Edit(string id, string name, DateTime dateofbirth, string role, string phone, string address, DataGridView dgv )//Edit
        {
           Employees employee = employeeList.FirstOrDefault(p => p.Id == id);
            if (employee != null)
            {
                employee.Name = name;
                employee.Address = address;
                employee.dateofBirth = dateofbirth;
                employee.role = role;
                employee.Phone = phone;

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == id)
                    {
                        row.Cells[1].Value = name;
                        row.Cells[2].Value = phone;
                        row.Cells[3].Value = address;
                        row.Cells[4].Value =dateofbirth;
                        row.Cells[5].Value=role;
                        
                        break;
                    }

                }
            }
            else
            {
                MessageBox.Show("Employee not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void Reset(DataGridView dgv) //Reset
        {
            dgv.Rows.Clear();
            foreach (var employee in employeeList.OrderBy(c => int.Parse(c.Id)))
            {
                dgv.Rows.Add(employee.Id, employee.Name, employee.Address, employee.Phone, employee.dateofBirth, employee.role);
            }

        }
        public static void ArrangeEm()  // Sắp xếp theo tên với culture tiếng Việt
        {
            var vietnameseCulture = new System.Globalization.CultureInfo("vi-VN");
            employeeList = employeeList.OrderBy(p => p.Name, StringComparer.Create(vietnameseCulture, false)).ToList();
        }
        public static Employees Search(string name) //Search
        {
            var vietnameseCulture = new System.Globalization.CultureInfo("vi-VN");
            var comparer = StringComparer.Create(vietnameseCulture, false);

            int l = 0;
            int r = employeeList.Count - 1;

            while (l <= r)
            {
                int mid = (l + r) / 2;
                Employees midemployee = employeeList[mid];
                int comparison = comparer.Compare(name, midemployee.Name);

                if (comparison == 0)
                {
                    return midemployee;
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
        public void SaveToFile(string filepath, DataGridView dgv) //Save
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filepath, false))
                {
                    foreach (var employee in employeeList)
                    {
                        string line = $"{employee.Id},{employee.Name},{employee.Phone},{employee.Address},{employee.dateofBirth:yyyy-MM-dd},{employee.role}";
                        sw.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

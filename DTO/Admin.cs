using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bakery.DTO
{
     public class Admin
    {
        public static List<Admin> accountList = new List<Admin>();
        public string username { get; set; }
        public string password { get; set; }
        public string Role {  get; set; }
      

        public Admin(string username, string password, string role)
        {
            this.username = username;
            this.password = password;
            Role = role;
        }
        public static void loadDataAccount(string filepath, DataGridView dgv) //Load
        {

            {
                try
                {
                    using (StreamReader sr = new StreamReader(filepath))
                    {
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            string[] values = line.Split(',');

                            if (values.Length ==3)
                            {
                                
                                string username= values[0];
                                string password = values[1];
                                string Role = values[2];
                                
                                Admin newAcoount = new Admin( username, password, Role);
                                accountList.Add(newAcoount);
                                dgv.Rows.Add(username, password, Role);
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
        }
        public static void saveDataAccount(string filepath, DataGridView dgv) //Save
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
        public static void CreateAccount (string username, string password, string Role, DataGridView dgv)
        {
            Admin newAccount = new Admin(username, password, Role);
            accountList.Add(newAccount);
            dgv.Rows.Add(newAccount.username, newAccount.password, newAccount.Role);
        }
        public static void changeAccount(string username, string newpassword, string Role, DataGridView dgv) {
            var accountEdited = accountList.FirstOrDefault(p => p.username == username);
            if (accountEdited != null)
            {
                accountEdited.password = newpassword;
                accountEdited.Role = Role;
               
                MessageBox.Show("Changed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Account not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
             
    }
}

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
    public partial class frmInvoiceInfo: Form
    {
        public frmInvoiceInfo(string invoiceContent)
        {
            InitializeComponent();
            richTextBox1.Text = invoiceContent;
            guna2Button1.Click += (sender, e) => this.Close();
        }

        private void frmInvoiceInfo_Load(object sender, EventArgs e)
        {
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e) //Close
        {
            this.Close();
        }
    }
}

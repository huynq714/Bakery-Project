using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bakery.Components
{
    public partial class Tiramisu: UserControl
    {
        public Tiramisu()
        {
            InitializeComponent();
        }
        public EventHandler select = null;

        private void panelTira_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void panelTira_Click(object sender, EventArgs e)
        {
            select?.Invoke(this, e);
        }
        public string Cost { get => lblPTira.Text; set => lblPTira.Text = value; }
        public string Title { get => lblTira.Text; set => lblTira.Text = value; }
        public Image Icon { get => picTira.Image; set=> picTira.Image=value; }
    }
}

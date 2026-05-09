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
    public partial class Gift: UserControl
    {
        public Gift()
        {
            InitializeComponent();
        }
       
        public EventHandler select = null;
        private void Gift_Load(object sender, EventArgs e)
        {

        }

        private void Gift_Click(object sender, EventArgs e)
        {
            select?.Invoke(this, e);
        }
        public string Cost { get; set; }
        public string Title { get => lblTitle.Text; set => lblTitle.Text = value; }
        public Image Icon { get => pic.Image; set => pic.Image = value; }
    }
}

using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bakery.DTO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Globalization;
namespace Bakery.DashBoard
{
    public partial class General: UserControl
    {
        public General()
        {
            InitializeComponent();
            LoadRevenueByMonthChart();
        }
        public void LoadRevenueByMonthChart()
        {
            var revenueByMonth = Invoices.Revenue();

            chartReve.Series.Clear();
            chartReve.ChartAreas.Clear();
            chartReve.Titles.Clear();

            Title chartTitle = new Title
            {
                Text = "Revenue by Month",
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.DarkRed
            };
            chartReve.Titles.Add(chartTitle);

            ChartArea area = new ChartArea("MainArea");
            chartReve.ChartAreas.Add(area);

            Series series = new Series("Revenue")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true,
            };
            series.Color = Color.DarkRed;

            var months = new[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
            foreach (var month in months)
            {
                string monthYearKey = DateTime.Now.Year.ToString() + "-" + month;  // Định dạng "yyyy-MM"

                double revenue = 0;
                if (revenueByMonth.ContainsKey(monthYearKey))
                {
                    revenue = revenueByMonth[monthYearKey];
                }

                series.Points.AddXY(month, revenue);
            }
            area.AxisY.LineColor = Color.Teal;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = false;

            chartReve.Series.Add(series);
            chartReve.Visible = true;
        }

        private void General_Load(object sender, EventArgs e)
        {

        }

        private void chartReve_Click(object sender, EventArgs e)
        {

        }
    }
}

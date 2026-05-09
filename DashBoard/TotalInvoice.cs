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
using System.IO;

namespace Bakery.DashBoard
{
    public partial class TotalInvoice: UserControl
    {
        public TotalInvoice()
        {
            InitializeComponent();
        }
        private void TotalInvoice_Load(object sender, EventArgs e)
        {
            DateTime month = DateTime.Now;
            chartWeek.Legends.Clear();
            chartWeek.Series.Clear();
        }
        private void chart1_Click(object sender, EventArgs e)
        {

        }
        public void LoadRevenueByWeekChart(DateTime selectedDate)
        {
            DateTime startOfWeek = selectedDate.AddDays(-(int)selectedDate.DayOfWeek + (selectedDate.DayOfWeek == DayOfWeek.Sunday ? -6 : 1));
            DateTime endOfWeek = startOfWeek.AddDays(6);

            var revenueData = Invoices.Revenue(startOfWeek, endOfWeek);

            bool hasData = revenueData.Values.Any(value => value > 0); //Kiểm tra xem có dữ liệu cho tuần đó không
            if (!hasData)
            {
                MessageBox.Show("No data for the selected week.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                chartWeek.Series.Clear();
                chartWeek.Titles.Clear();
                chartWeek.ChartAreas.Clear();
                return;
            }

            chartWeek.Series.Clear();
            chartWeek.ChartAreas.Clear();
            chartWeek.Titles.Clear();

            Title chartTitle = new Title
            {
                Text = $"Revenue from {startOfWeek:dd/MM} to {endOfWeek:dd/MM}",
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.DarkBlue
            };
            chartWeek.Titles.Add(chartTitle);

            ChartArea area = new ChartArea("WeeklyArea");
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = false;
            chartWeek.ChartAreas.Add(area);

            Series series = new Series("Revenue")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true,
                Color = Color.Teal
            };

            foreach (var item in revenueData)
            {
                series.Points.AddXY(item.Key, item.Value);  // item.Key là "dd/MM"
            }

            chartWeek.Series.Add(series);
        }


        private void View_Click_1(object sender, EventArgs e) //btn View
        {
            DateTime selectedDate = guna2DateTimePicker1.Value;
            LoadRevenueByWeekChart(selectedDate);
        }

        private void TotalInvoices_Click(object sender, EventArgs e)
        {

        }
    }
}

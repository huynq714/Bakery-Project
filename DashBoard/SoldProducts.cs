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
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace Bakery.DashBoard
{
    public partial class SoldProducts: UserControl
    {
       
      
        public SoldProducts()
        {
            InitializeComponent();
           
            LoadChart(@"D:\BAKERY\Bakery\bin\Debug\DATA\Products.txt");
        }
        private void dbProduct_Load(object sender, EventArgs e)
        {
           
        }
         public void LoadChart(string filePath)//Tạo biểu đồ
        {
            string filepath = @"D:\BAKERY\Bakery\bin\Debug\DATA\Products.txt";
            if (!File.Exists(filepath))
            {
                MessageBox.Show("File not found!");
                return;
            }
            chartSold.Series.Clear();
            chartSold.ChartAreas.Clear();
            chartSold.Titles.Clear();

            chartSold.Titles.Add("Product Sales and Quantity");
            chartSold.Titles[0].Font = new Font("Arial", 16, FontStyle.Bold);
            chartSold.Titles[0].ForeColor = Color.DarkRed;

            // Tạo một ChartArea với 2 trục 
            ChartArea area = new ChartArea("MainArea");

            // Trục X: Tên sản phẩm
            area.AxisX.LabelStyle.Angle = -45;
            area.AxisX.Interval = 1;
            area.AxisX.IsLabelAutoFit = true;
            area.AxisX.LabelStyle.IsEndLabelVisible = true;
           

            // Trục Y cho số lượng đã bán
            area.AxisY.Minimum = 0;
            area.AxisY.Title = "Sold Quantity";
            area.AxisY.LineColor= Color.DarkGreen;
            area.AxisY.TitleFont = new Font("Arial", 14, FontStyle.Bold); 
            area.AxisY.TitleForeColor = Color.DarkGreen;

            // Thêm trục Y thứ hai cho số lượng trong cửa hàng
            area.AxisY2.Minimum = 0;
            area.AxisY2.Title = "Total Quantity";
            area.AxisY2.LineColor = Color.RosyBrown;
            area.AxisY2.TitleFont = new Font("Arial", 14, FontStyle.Bold);
            area.AxisY.TitleForeColor = Color.DarkRed;

            // Thêm ChartArea vào chart
            chartSold.ChartAreas.Add(area);

            // Series cho số lượng bán
            Series seriesSold = new Series("Sold Products");
            seriesSold.ChartType = SeriesChartType.Column;
            seriesSold.IsValueShownAsLabel = true;
            seriesSold.YAxisType = AxisType.Primary; 
            seriesSold.Color = Color.DarkRed;

            // Series cho tổng số lượng
            Series seriesInStore = new Series("Total Products");
            seriesInStore.ChartType = SeriesChartType.Column;
            seriesInStore.IsValueShownAsLabel = true;
            seriesInStore.YAxisType = AxisType.Secondary;  
            seriesInStore.Color = Color.Teal;

            area.AxisX.MajorGrid.Enabled = false;  
            area.AxisY.MajorGrid.Enabled = false;  
            area.AxisY2.MajorGrid.Enabled = false;

            // Đọc dữ liệu từ file
            foreach (var line in File.ReadLines(filePath))
            {
                var data = line.Split(',');

                if (data.Length >= 6)
                {
                    string productName = data[1];  

                  
                    if (int.TryParse(data[4], out int sold))
                    {
                        seriesSold.Points.AddXY(productName, sold);
                    }

                   
                    if (int.TryParse(data[3], out int quantity))
                    {
                        seriesInStore.Points.AddXY(productName, quantity);
                    }
                }
            }
            chartSold.Series.Add(seriesSold);
            chartSold.Series.Add(seriesInStore);

        }

        private void chartSold_Click(object sender, EventArgs e)
        {

        }
    }
}

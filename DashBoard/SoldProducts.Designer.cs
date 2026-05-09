namespace Bakery.DashBoard
{
    partial class SoldProducts
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.chartSold = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartSold)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // chartSold
            // 
            chartArea1.Name = "ChartArea1";
            this.chartSold.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartSold.Legends.Add(legend1);
            this.chartSold.Location = new System.Drawing.Point(56, 23);
            this.chartSold.Name = "chartSold";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartSold.Series.Add(series1);
            this.chartSold.Size = new System.Drawing.Size(1439, 587);
            this.chartSold.TabIndex = 6;
            this.chartSold.Text = "chart1";
            this.chartSold.Click += new System.EventHandler(this.chartSold_Click);
            // 
            // SoldProducts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.chartSold);
            this.Name = "SoldProducts";
            this.Size = new System.Drawing.Size(1464, 610);
            this.Load += new System.EventHandler(this.dbProduct_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartSold)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSold;
    }
}

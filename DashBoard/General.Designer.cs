namespace Bakery.DashBoard
{
    partial class General
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
            this.chartReve = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.chartReve)).BeginInit();
            this.SuspendLayout();
            // 
            // chartReve
            // 
            chartArea1.Name = "ChartArea1";
            this.chartReve.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartReve.Legends.Add(legend1);
            this.chartReve.Location = new System.Drawing.Point(250, 65);
            this.chartReve.Name = "chartReve";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartReve.Series.Add(series1);
            this.chartReve.Size = new System.Drawing.Size(994, 560);
            this.chartReve.TabIndex = 2;
            this.chartReve.Text = "chart2";
            this.chartReve.Click += new System.EventHandler(this.chartReve_Click);
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // General
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.chartReve);
            this.Name = "General";
            this.Size = new System.Drawing.Size(1460, 610);
            this.Load += new System.EventHandler(this.General_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartReve)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartReve;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
    }
}

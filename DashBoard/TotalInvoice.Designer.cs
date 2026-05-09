namespace Bakery.DashBoard
{
    partial class TotalInvoice
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartWeek = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.guna2DateTimePicker1 = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.btnView = new System.Windows.Forms.Button();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.chartWeek)).BeginInit();
            this.SuspendLayout();
            // 
            // chartWeek
            // 
            chartArea2.Name = "ChartArea1";
            this.chartWeek.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartWeek.Legends.Add(legend2);
            this.chartWeek.Location = new System.Drawing.Point(64, 93);
            this.chartWeek.Name = "chartWeek";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartWeek.Series.Add(series2);
            this.chartWeek.Size = new System.Drawing.Size(1379, 514);
            this.chartWeek.TabIndex = 0;
            this.chartWeek.Text = "chart1";
            this.chartWeek.Click += new System.EventHandler(this.TotalInvoices_Click);
            // 
            // guna2DateTimePicker1
            // 
            this.guna2DateTimePicker1.Checked = true;
            this.guna2DateTimePicker1.FillColor = System.Drawing.Color.MidnightBlue;
            this.guna2DateTimePicker1.Font = new System.Drawing.Font("Microsoft Tai Le", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2DateTimePicker1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.guna2DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.guna2DateTimePicker1.Location = new System.Drawing.Point(581, 41);
            this.guna2DateTimePicker1.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.guna2DateTimePicker1.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.guna2DateTimePicker1.Name = "guna2DateTimePicker1";
            this.guna2DateTimePicker1.Size = new System.Drawing.Size(281, 36);
            this.guna2DateTimePicker1.TabIndex = 1;
            this.guna2DateTimePicker1.Value = new System.DateTime(2025, 4, 17, 15, 14, 8, 140);
            // 
            // btnView
            // 
            this.btnView.BackColor = System.Drawing.Color.SteelBlue;
            this.btnView.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnView.Location = new System.Drawing.Point(907, 41);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(84, 36);
            this.btnView.TabIndex = 12;
            this.btnView.Text = "View";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.View_Click_1);
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // TotalInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.guna2DateTimePicker1);
            this.Controls.Add(this.chartWeek);
            this.Name = "TotalInvoice";
            this.Size = new System.Drawing.Size(1460, 610);
            this.Load += new System.EventHandler(this.TotalInvoice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartWeek)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2DateTimePicker guna2DateTimePicker1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartWeek;
        private System.Windows.Forms.Button btnView;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
    }
}

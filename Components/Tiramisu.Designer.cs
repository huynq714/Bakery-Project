namespace Bakery.Components
{
    partial class Tiramisu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tiramisu));
            this.panelTira = new System.Windows.Forms.Panel();
            this.lblPTira = new System.Windows.Forms.Label();
            this.lblTira = new System.Windows.Forms.Label();
            this.picTira = new System.Windows.Forms.PictureBox();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.panelTira.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTira)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTira
            // 
            this.panelTira.BackColor = System.Drawing.Color.White;
            this.panelTira.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTira.Controls.Add(this.lblPTira);
            this.panelTira.Controls.Add(this.lblTira);
            this.panelTira.Controls.Add(this.picTira);
            this.panelTira.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelTira.Location = new System.Drawing.Point(3, 3);
            this.panelTira.Name = "panelTira";
            this.panelTira.Size = new System.Drawing.Size(290, 159);
            this.panelTira.TabIndex = 5;
            this.panelTira.Click += new System.EventHandler(this.panelTira_Click);
            this.panelTira.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTira_Paint);
            // 
            // lblPTira
            // 
            this.lblPTira.AutoSize = true;
            this.lblPTira.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPTira.ForeColor = System.Drawing.Color.Red;
            this.lblPTira.Location = new System.Drawing.Point(13, 72);
            this.lblPTira.Name = "lblPTira";
            this.lblPTira.Size = new System.Drawing.Size(89, 26);
            this.lblPTira.TabIndex = 2;
            this.lblPTira.Text = "$35.000";
            // 
            // lblTira
            // 
            this.lblTira.AutoSize = true;
            this.lblTira.Font = new System.Drawing.Font("Microsoft Tai Le", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTira.Location = new System.Drawing.Point(15, 46);
            this.lblTira.Name = "lblTira";
            this.lblTira.Size = new System.Drawing.Size(61, 16);
            this.lblTira.TabIndex = 1;
            this.lblTira.Text = "Tiramisu";
            // 
            // picTira
            // 
            this.picTira.Image = ((System.Drawing.Image)(resources.GetObject("picTira.Image")));
            this.picTira.Location = new System.Drawing.Point(137, 4);
            this.picTira.Name = "picTira";
            this.picTira.Size = new System.Drawing.Size(151, 151);
            this.picTira.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picTira.TabIndex = 0;
            this.picTira.TabStop = false;
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // Tiramisu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelTira);
            this.Name = "Tiramisu";
            this.Size = new System.Drawing.Size(299, 168);
            this.panelTira.ResumeLayout(false);
            this.panelTira.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTira)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTira;
        private System.Windows.Forms.PictureBox picTira;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        public System.Windows.Forms.Label lblPTira;
        public System.Windows.Forms.Label lblTira;
    }
}

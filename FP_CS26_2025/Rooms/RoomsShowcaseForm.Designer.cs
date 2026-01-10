namespace FP_CS26_2025.Rooms
{
    partial class RoomsShowcaseForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.mainBackgroundPanel = new FP_CS26_2025.ModernDesign.GradientPanel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblLogo = new System.Windows.Forms.Label();
            this.modernNavbar1 = new FP_CS26_2025.ModernDesign.ModernNavbar();
            this.roomGalleryView1 = new FP_CS26_2025.Rooms.RoomGalleryView();
            this.footerControl = new FP_CS26_2025.ModernDesign.FooterControl();
            this.mainBackgroundPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainBackgroundPanel
            // 
            this.mainBackgroundPanel.Angle = 60F;
            this.mainBackgroundPanel.ColorBottom = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(30)))), ((int)(((byte)(50)))));
            this.mainBackgroundPanel.ColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(88)))), ((int)(((byte)(118)))));
            this.mainBackgroundPanel.Controls.Add(this.lblSubtitle);
            this.mainBackgroundPanel.Controls.Add(this.lblLogo);
            this.mainBackgroundPanel.Controls.Add(this.modernNavbar1);
            this.mainBackgroundPanel.Controls.Add(this.roomGalleryView1);
            this.mainBackgroundPanel.Controls.Add(this.footerControl);
            
            // Date Pickers
            this.mainBackgroundPanel.Controls.Add(this.lblArrival);
            this.mainBackgroundPanel.Controls.Add(this.dtpArrival);
            this.mainBackgroundPanel.Controls.Add(this.lblDeparture);
            this.mainBackgroundPanel.Controls.Add(this.dtpDeparture);
            
            this.mainBackgroundPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainBackgroundPanel.Location = new System.Drawing.Point(0, 0);
            this.mainBackgroundPanel.Name = "mainBackgroundPanel";
            this.mainBackgroundPanel.Size = new System.Drawing.Size(1600, 862);
            this.mainBackgroundPanel.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI Semilight", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.lblSubtitle.Location = new System.Drawing.Point(52, 74);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(378, 32);
            this.lblSubtitle.TabIndex = 3;
            this.lblSubtitle.Text = "EXPERIENCE LUXURY AT ITS FINEST";
            // 
            // lblArrival
            // 
            this.lblArrival = new System.Windows.Forms.Label();
            this.lblArrival.AutoSize = true;
            this.lblArrival.BackColor = System.Drawing.Color.Transparent;
            this.lblArrival.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArrival.ForeColor = System.Drawing.Color.White;
            this.lblArrival.Location = new System.Drawing.Point(866, 110);
            this.lblArrival.Name = "lblArrival";
            this.lblArrival.Size = new System.Drawing.Size(98, 28);
            this.lblArrival.Text = "ARRIVAL:";
            // 
            // dtpArrival
            // 
            this.dtpArrival = new System.Windows.Forms.DateTimePicker();
            this.dtpArrival.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpArrival.Location = new System.Drawing.Point(940, 108);
            this.dtpArrival.Name = "dtpArrival";
            this.dtpArrival.Size = new System.Drawing.Size(120, 25);
            // 
            // lblDeparture
            // 
            this.lblDeparture = new System.Windows.Forms.Label();
            this.lblDeparture.AutoSize = true;
            this.lblDeparture.BackColor = System.Drawing.Color.Transparent;
            this.lblDeparture.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeparture.ForeColor = System.Drawing.Color.White;
            this.lblDeparture.Location = new System.Drawing.Point(1080, 110);
            this.lblDeparture.Name = "lblDeparture";
            this.lblDeparture.Size = new System.Drawing.Size(125, 28);
            this.lblDeparture.Text = "DEPARTURE:";
            // 
            // dtpDeparture
            // 
            this.dtpDeparture = new System.Windows.Forms.DateTimePicker();
            this.dtpDeparture.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDeparture.Location = new System.Drawing.Point(1180, 108);
            this.dtpDeparture.Name = "dtpDeparture";
            this.dtpDeparture.Size = new System.Drawing.Size(120, 25);
            // 
            // footerControl
            // 
            this.footerControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.footerControl.BackColor = System.Drawing.Color.Transparent;
            this.footerControl.Location = new System.Drawing.Point(50, 750); // Kept similar, but BringToFront should help
            this.footerControl.Name = "footerControl";
            this.footerControl.Size = new System.Drawing.Size(500, 100); // Increased height just in case
            this.footerControl.TabIndex = 8;
            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.BackColor = System.Drawing.Color.Transparent;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Location = new System.Drawing.Point(50, 30);
            this.lblLogo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(395, 54);
            this.lblLogo.TabIndex = 2;
            this.lblLogo.Text = "THE NEXUS GRAND";


            // 
            // modernNavbar1
            // 
            this.modernNavbar1.ActivePage = "Rooms";
            this.modernNavbar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.modernNavbar1.BackColor = System.Drawing.Color.Transparent;
            this.modernNavbar1.Location = new System.Drawing.Point(700, 55);
            this.modernNavbar1.Name = "modernNavbar1";
            this.modernNavbar1.Size = new System.Drawing.Size(450, 50);
            this.modernNavbar1.TabIndex = 1;
            // 
            // roomGalleryView1
            // 
            this.roomGalleryView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.roomGalleryView1.BackColor = System.Drawing.Color.Transparent;
            this.roomGalleryView1.Location = new System.Drawing.Point(50, 150);
            this.roomGalleryView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.roomGalleryView1.Name = "roomGalleryView1";
            this.roomGalleryView1.Size = new System.Drawing.Size(1500, 650);
            this.roomGalleryView1.TabIndex = 0;
            this.roomGalleryView1.Load += new System.EventHandler(this.roomGalleryView1_Load);
            // 
            // RoomsShowcaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 862);
            this.Controls.Add(this.mainBackgroundPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RoomsShowcaseForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.mainBackgroundPanel.ResumeLayout(false);
            this.mainBackgroundPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        private FP_CS26_2025.ModernDesign.GradientPanel mainBackgroundPanel;
        private FP_CS26_2025.ModernDesign.ModernNavbar modernNavbar1;
        private System.Windows.Forms.Label lblLogo;
        private FP_CS26_2025.ModernDesign.FooterControl footerControl;
        private FP_CS26_2025.Rooms.RoomGalleryView roomGalleryView1;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblArrival;
        private System.Windows.Forms.Label lblDeparture;
        private System.Windows.Forms.DateTimePicker dtpArrival;
        private System.Windows.Forms.DateTimePicker dtpDeparture;
    }
}

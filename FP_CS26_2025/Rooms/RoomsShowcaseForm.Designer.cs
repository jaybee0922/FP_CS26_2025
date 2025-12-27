namespace FP_CS26_2025.Rooms
{
    partial class RoomsShowcaseForm
    {
        private System.ComponentModel.IContainer components = null;
        private RoomGalleryView roomGalleryView1;

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
            this.modernNavbar1 = new FP_CS26_2025.ModernDesign.ModernNavbar();
            this.roomGalleryView1 = new FP_CS26_2025.Rooms.RoomGalleryView();
            this.lblLogo = new System.Windows.Forms.Label();
            this.mainBackgroundPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainBackgroundPanel
            // 
            this.mainBackgroundPanel.Angle = 60F;
            this.mainBackgroundPanel.ColorBottom = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(30)))), ((int)(((byte)(50)))));
            this.mainBackgroundPanel.ColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(88)))), ((int)(((byte)(118)))));
            this.mainBackgroundPanel.Controls.Add(this.lblLogo);
            this.mainBackgroundPanel.Controls.Add(this.modernNavbar1);
            this.mainBackgroundPanel.Controls.Add(this.roomGalleryView1);
            this.mainBackgroundPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainBackgroundPanel.Location = new System.Drawing.Point(0, 0);
            this.mainBackgroundPanel.Name = "mainBackgroundPanel";
            this.mainBackgroundPanel.Size = new System.Drawing.Size(1600, 862);
            this.mainBackgroundPanel.TabIndex = 0;
            // 
            // modernNavbar1
            // 
            this.modernNavbar1.ActivePage = "Rooms";
            this.modernNavbar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.modernNavbar1.BackColor = System.Drawing.Color.Transparent;
            this.modernNavbar1.Location = new System.Drawing.Point(866, 49);
            this.modernNavbar1.Name = "modernNavbar1";
            this.modernNavbar1.Size = new System.Drawing.Size(600, 50);
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
            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.BackColor = System.Drawing.Color.Transparent;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Location = new System.Drawing.Point(144, 45);
            this.lblLogo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(395, 54);
            this.lblLogo.TabIndex = 2;
            this.lblLogo.Text = "THE NEXUS GRAND";
            // 
            // RoomsShowcaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 862);
            this.Controls.Add(this.mainBackgroundPanel);
            this.Name = "RoomsShowcaseForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.mainBackgroundPanel.ResumeLayout(false);
            this.mainBackgroundPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        private FP_CS26_2025.ModernDesign.GradientPanel mainBackgroundPanel;
        private FP_CS26_2025.ModernDesign.ModernNavbar modernNavbar1;
        private System.Windows.Forms.Label lblLogo;
    }
}

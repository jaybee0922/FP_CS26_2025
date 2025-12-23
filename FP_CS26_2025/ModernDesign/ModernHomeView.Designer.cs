namespace FP_CS26_2025.ModernDesign
{
    partial class ModernHomeView
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            
            // Main Background
            this.mainBackgroundPanel = new FP_CS26_2025.ModernDesign.GradientPanel();
            
            // Controls (Directly on Background for Transparency)
            this.lblLogo = new System.Windows.Forms.Label();
            this.btnPages = new System.Windows.Forms.Button();
            this.btnShop = new System.Windows.Forms.Button();
            this.btnBookNow = new System.Windows.Forms.Button();
            
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblMainTitle = new System.Windows.Forms.Label();
            
            // Booking Panel (Opaque)
            this.panelBooking = new System.Windows.Forms.Panel();
            this.btnCheck = new FP_CS26_2025.ModernDesign.ModernButton();
            this.txtPromo = new System.Windows.Forms.TextBox();
            this.lblPromo = new System.Windows.Forms.Label();
            this.dtpDeparture = new System.Windows.Forms.DateTimePicker();
            this.lblDeparture = new System.Windows.Forms.Label();
            this.dtpArrival = new System.Windows.Forms.DateTimePicker();
            this.lblArrival = new System.Windows.Forms.Label();

            this.mainBackgroundPanel.SuspendLayout();
            this.panelBooking.SuspendLayout();
            this.SuspendLayout();

            // 
            // mainBackgroundPanel
            // 
            this.mainBackgroundPanel.Angle = 60F;
            this.mainBackgroundPanel.ColorBottom = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(30)))), ((int)(((byte)(50))))); // Deep Midnight
            this.mainBackgroundPanel.ColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(88)))), ((int)(((byte)(118))))); // Ocean Blue
            this.mainBackgroundPanel.Controls.Add(this.lblLogo);
            this.mainBackgroundPanel.Controls.Add(this.btnPages);
            this.mainBackgroundPanel.Controls.Add(this.btnShop);
            this.mainBackgroundPanel.Controls.Add(this.btnBookNow);
            this.mainBackgroundPanel.Controls.Add(this.lblWelcome);
            this.mainBackgroundPanel.Controls.Add(this.lblMainTitle);
            this.mainBackgroundPanel.Controls.Add(this.panelBooking);
            this.mainBackgroundPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainBackgroundPanel.Location = new System.Drawing.Point(0, 0);
            this.mainBackgroundPanel.Name = "mainBackgroundPanel";
            this.mainBackgroundPanel.Size = new System.Drawing.Size(1200, 700);
            this.mainBackgroundPanel.TabIndex = 0;

            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.BackColor = System.Drawing.Color.Transparent;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Location = new System.Drawing.Point(50, 30);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(142, 45);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "THE NEXUS GRAND";

            // 
            // btnPages
            // 
            this.btnPages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPages.BackColor = System.Drawing.Color.Transparent;
            this.btnPages.FlatAppearance.BorderSize = 0;
            this.btnPages.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnPages.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnPages.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPages.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);
            this.btnPages.ForeColor = System.Drawing.Color.White;
            this.btnPages.Location = new System.Drawing.Point(850, 40);
            this.btnPages.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPages.Name = "btnPages";
            this.btnPages.Size = new System.Drawing.Size(100, 35);
            this.btnPages.TabIndex = 1;
            this.btnPages.Text = "PAGES";
            this.btnPages.UseVisualStyleBackColor = false;

            // 
            // btnShop
            // 
            this.btnShop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShop.BackColor = System.Drawing.Color.Transparent;
            this.btnShop.FlatAppearance.BorderSize = 0;
            this.btnShop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnShop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnShop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShop.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);
            this.btnShop.ForeColor = System.Drawing.Color.White;
            this.btnShop.Location = new System.Drawing.Point(950, 40);
            this.btnShop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShop.Name = "btnShop";
            this.btnShop.Size = new System.Drawing.Size(100, 35);
            this.btnShop.TabIndex = 2;
            this.btnShop.Text = "SHOP";
            this.btnShop.UseVisualStyleBackColor = false;

            // 
            // btnBookNow
            // 
            this.btnBookNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBookNow.BackColor = System.Drawing.Color.Transparent;
            this.btnBookNow.FlatAppearance.BorderSize = 0;
            this.btnBookNow.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnBookNow.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnBookNow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBookNow.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnBookNow.ForeColor = System.Drawing.Color.White;
            this.btnBookNow.Location = new System.Drawing.Point(1050, 40);
            this.btnBookNow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBookNow.Name = "btnBookNow";
            this.btnBookNow.Size = new System.Drawing.Size(120, 35);
            this.btnBookNow.TabIndex = 3;
            this.btnBookNow.Text = "BOOK NOW";
            this.btnBookNow.UseVisualStyleBackColor = false;

            // 
            // lblWelcome
            // 
            this.lblWelcome.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblWelcome.BackColor = System.Drawing.Color.Transparent;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            this.lblWelcome.Location = new System.Drawing.Point(200, 220);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(800, 40);
            this.lblWelcome.TabIndex = 4;
            this.lblWelcome.Text = "WELCOME TO THE NEXUS GRAND";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // lblMainTitle
            // 
            this.lblMainTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMainTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblMainTitle.Font = new System.Drawing.Font("Segoe UI Light", 56F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainTitle.ForeColor = System.Drawing.Color.White;
            this.lblMainTitle.Location = new System.Drawing.Point(100, 260);
            this.lblMainTitle.Name = "lblMainTitle";
            this.lblMainTitle.Size = new System.Drawing.Size(1000, 150);
            this.lblMainTitle.TabIndex = 5;
            this.lblMainTitle.Text = "SIMPLY HEARTFELT";
            this.lblMainTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // panelBooking
            // 
            this.panelBooking.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panelBooking.BackColor = System.Drawing.Color.White;
            this.panelBooking.Controls.Add(this.btnCheck);
            this.panelBooking.Controls.Add(this.txtPromo);
            this.panelBooking.Controls.Add(this.lblPromo);
            this.panelBooking.Controls.Add(this.dtpDeparture);
            this.panelBooking.Controls.Add(this.lblDeparture);
            this.panelBooking.Controls.Add(this.dtpArrival);
            this.panelBooking.Controls.Add(this.lblArrival);
            this.panelBooking.Location = new System.Drawing.Point(150, 500);
            this.panelBooking.Name = "panelBooking";
            this.panelBooking.Size = new System.Drawing.Size(900, 100);
            this.panelBooking.TabIndex = 6;

            // 
            // lblArrival (Rest unchanged)
            // 
            this.lblArrival.AutoSize = true;
            this.lblArrival.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblArrival.Location = new System.Drawing.Point(20, 20);
            this.lblArrival.Name = "lblArrival";
            this.lblArrival.Size = new System.Drawing.Size(68, 15);
            this.lblArrival.TabIndex = 0;
            this.lblArrival.Text = "Arrival Date";

            // 
            // dtpArrival
            // 
            this.dtpArrival.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpArrival.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpArrival.Location = new System.Drawing.Point(23, 40);
            this.dtpArrival.Name = "dtpArrival";
            this.dtpArrival.Size = new System.Drawing.Size(150, 25);
            this.dtpArrival.TabIndex = 1;

            // 
            // lblDeparture
            // 
            this.lblDeparture.AutoSize = true;
            this.lblDeparture.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDeparture.Location = new System.Drawing.Point(220, 20);
            this.lblDeparture.Name = "lblDeparture";
            this.lblDeparture.Size = new System.Drawing.Size(86, 15);
            this.lblDeparture.TabIndex = 2;
            this.lblDeparture.Text = "Departure Date";

            // 
            // dtpDeparture
            // 
            this.dtpDeparture.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpDeparture.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDeparture.Location = new System.Drawing.Point(223, 40);
            this.dtpDeparture.Name = "dtpDeparture";
            this.dtpDeparture.Size = new System.Drawing.Size(150, 25);
            this.dtpDeparture.TabIndex = 3;

            // 
            // lblPromo
            // 
            this.lblPromo.AutoSize = true;
            this.lblPromo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPromo.Location = new System.Drawing.Point(420, 20);
            this.lblPromo.Name = "lblPromo";
            this.lblPromo.Size = new System.Drawing.Size(73, 15);
            this.lblPromo.TabIndex = 4;
            this.lblPromo.Text = "Promo Code";

            // 
            // txtPromo
            // 
            this.txtPromo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPromo.Location = new System.Drawing.Point(423, 40);
            this.txtPromo.Name = "txtPromo";
            this.txtPromo.Size = new System.Drawing.Size(150, 25);
            this.txtPromo.TabIndex = 5;

            // 
            // btnCheck
            // 
            this.btnCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(176)))), ((int)(((byte)(76)))));
            this.btnCheck.BorderColor = System.Drawing.Color.Empty;
            this.btnCheck.BorderRadius = 4;
            this.btnCheck.FlatAppearance.BorderSize = 0;
            this.btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheck.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCheck.ForeColor = System.Drawing.Color.White;
            this.btnCheck.Location = new System.Drawing.Point(650, 25);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(200, 50);
            this.btnCheck.TabIndex = 6;
            this.btnCheck.Text = "CHECK AVAILABILITY";
            this.btnCheck.UseVisualStyleBackColor = false;

            // 
            // ModernHomeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.mainBackgroundPanel);
            this.Name = "ModernHomeView";
            this.Text = "ModernHomeView";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ModernHomeView_Load);
            this.mainBackgroundPanel.ResumeLayout(false);
            this.mainBackgroundPanel.PerformLayout();
            this.panelBooking.ResumeLayout(false);
            this.panelBooking.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FP_CS26_2025.ModernDesign.GradientPanel mainBackgroundPanel;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Button btnBookNow;
        private System.Windows.Forms.Button btnShop;
        private System.Windows.Forms.Button btnPages;
        private System.Windows.Forms.Label lblMainTitle;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Panel panelBooking;
        private System.Windows.Forms.Label lblArrival;
        private System.Windows.Forms.DateTimePicker dtpArrival;
        private System.Windows.Forms.Label lblDeparture;
        private System.Windows.Forms.DateTimePicker dtpDeparture;
        private System.Windows.Forms.Label lblPromo;
        private System.Windows.Forms.TextBox txtPromo;
        private FP_CS26_2025.ModernDesign.ModernButton btnCheck;
    }
}

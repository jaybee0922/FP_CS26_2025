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
            this.mainBackgroundPanel = new FP_CS26_2025.ModernDesign.GradientPanel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.modernNavbar = new FP_CS26_2025.ModernDesign.ModernNavbar();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblMainTitle = new System.Windows.Forms.Label();
            this.panelBooking = new System.Windows.Forms.Panel();
            this.btnCheck = new FP_CS26_2025.ModernDesign.ModernButton();
            this.txtPromo = new System.Windows.Forms.TextBox();
            this.lblPromo = new System.Windows.Forms.Label();
            this.dtpDeparture = new System.Windows.Forms.DateTimePicker();
            this.lblDeparture = new System.Windows.Forms.Label();
            this.dtpArrival = new System.Windows.Forms.DateTimePicker();
            this.lblArrival = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.mainBackgroundPanel.SuspendLayout();
            this.panelBooking.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainBackgroundPanel
            // 
            this.mainBackgroundPanel.Angle = 60F;
            this.mainBackgroundPanel.ColorBottom = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(30)))), ((int)(((byte)(50)))));
            this.mainBackgroundPanel.ColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(88)))), ((int)(((byte)(118)))));
            this.mainBackgroundPanel.Controls.Add(this.lblSubtitle);
            this.mainBackgroundPanel.Controls.Add(this.lblLogo);
            this.mainBackgroundPanel.Controls.Add(this.modernNavbar);
            this.mainBackgroundPanel.Controls.Add(this.lblWelcome);
            this.mainBackgroundPanel.Controls.Add(this.lblMainTitle);
            this.mainBackgroundPanel.Controls.Add(this.panelBooking);
            this.mainBackgroundPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainBackgroundPanel.Location = new System.Drawing.Point(0, 0);
            this.mainBackgroundPanel.Margin = new System.Windows.Forms.Padding(4);
            this.mainBackgroundPanel.Name = "mainBackgroundPanel";
            this.mainBackgroundPanel.Size = new System.Drawing.Size(1600, 862);
            this.mainBackgroundPanel.TabIndex = 0;
            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.BackColor = System.Drawing.Color.Transparent;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Location = new System.Drawing.Point(67, 37);
            this.lblLogo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(395, 54);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "THE NEXUS GRAND";
            this.lblLogo.Click += new System.EventHandler(this.lblLogo_Click);
            // 
            // modernNavbar
            // 
            this.modernNavbar.ActivePage = "Home";
            this.modernNavbar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.modernNavbar.BackColor = System.Drawing.Color.Transparent;
            this.modernNavbar.Location = new System.Drawing.Point(1066, 49);
            this.modernNavbar.Name = "modernNavbar";
            this.modernNavbar.Size = new System.Drawing.Size(400, 50);
            this.modernNavbar.TabIndex = 1;
            // 
            // lblWelcome
            // 
            this.lblWelcome.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblWelcome.BackColor = System.Drawing.Color.Transparent;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblWelcome.Location = new System.Drawing.Point(267, 271);
            this.lblWelcome.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(1067, 49);
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
            this.lblMainTitle.Location = new System.Drawing.Point(133, 320);
            this.lblMainTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMainTitle.Name = "lblMainTitle";
            this.lblMainTitle.Size = new System.Drawing.Size(1333, 185);
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
            this.panelBooking.Location = new System.Drawing.Point(200, 615);
            this.panelBooking.Margin = new System.Windows.Forms.Padding(4);
            this.panelBooking.Name = "panelBooking";
            this.panelBooking.Size = new System.Drawing.Size(1200, 123);
            this.panelBooking.TabIndex = 6;
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
            this.btnCheck.Location = new System.Drawing.Point(867, 31);
            this.btnCheck.Margin = new System.Windows.Forms.Padding(4);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(267, 62);
            this.btnCheck.TabIndex = 6;
            this.btnCheck.Text = "BOOK NOW";
            this.btnCheck.UseVisualStyleBackColor = false;
            // 
            // txtPromo
            // 
            this.txtPromo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPromo.Location = new System.Drawing.Point(564, 49);
            this.txtPromo.Margin = new System.Windows.Forms.Padding(4);
            this.txtPromo.Name = "txtPromo";
            this.txtPromo.Size = new System.Drawing.Size(199, 30);
            this.txtPromo.TabIndex = 5;
            // 
            // lblPromo
            // 
            this.lblPromo.AutoSize = true;
            this.lblPromo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPromo.Location = new System.Drawing.Point(560, 25);
            this.lblPromo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPromo.Name = "lblPromo";
            this.lblPromo.Size = new System.Drawing.Size(92, 20);
            this.lblPromo.TabIndex = 4;
            this.lblPromo.Text = "Promo Code";
            // 
            // dtpDeparture
            // 
            this.dtpDeparture.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpDeparture.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDeparture.Location = new System.Drawing.Point(297, 49);
            this.dtpDeparture.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDeparture.Name = "dtpDeparture";
            this.dtpDeparture.Size = new System.Drawing.Size(199, 30);
            this.dtpDeparture.TabIndex = 3;
            // 
            // lblDeparture
            // 
            this.lblDeparture.AutoSize = true;
            this.lblDeparture.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDeparture.Location = new System.Drawing.Point(293, 25);
            this.lblDeparture.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDeparture.Name = "lblDeparture";
            this.lblDeparture.Size = new System.Drawing.Size(112, 20);
            this.lblDeparture.TabIndex = 2;
            this.lblDeparture.Text = "Departure Date";
            // 
            // dtpArrival
            // 
            this.dtpArrival.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpArrival.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpArrival.Location = new System.Drawing.Point(31, 49);
            this.dtpArrival.Margin = new System.Windows.Forms.Padding(4);
            this.dtpArrival.Name = "dtpArrival";
            this.dtpArrival.Size = new System.Drawing.Size(199, 30);
            this.dtpArrival.TabIndex = 1;
            // 
            // lblArrival
            // 
            this.lblArrival.AutoSize = true;
            this.lblArrival.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblArrival.Location = new System.Drawing.Point(27, 25);
            this.lblArrival.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblArrival.Name = "lblArrival";
            this.lblArrival.Size = new System.Drawing.Size(88, 20);
            this.lblArrival.TabIndex = 0;
            this.lblArrival.Text = "Arrival Date";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI Semilight", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.lblSubtitle.Location = new System.Drawing.Point(70, 91);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(378, 32);
            this.lblSubtitle.TabIndex = 7;
            this.lblSubtitle.Text = "EXPERIENCE LUXURY AT ITS FINEST";
            // 
            // ModernHomeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 862);
            this.Controls.Add(this.mainBackgroundPanel);
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private FP_CS26_2025.ModernDesign.ModernNavbar modernNavbar;
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
        private System.Windows.Forms.Label lblSubtitle;
    }
}

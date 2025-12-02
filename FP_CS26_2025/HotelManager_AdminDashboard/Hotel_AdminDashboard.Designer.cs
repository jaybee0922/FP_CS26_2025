namespace FP_CS26_2025.HotelManager_AdminDashboard
{
    partial class Hotel_AdminDashboard
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bookingManager1 = new FP_CS26_2025.HotelManager_AdminDashboard.BookingManager();
            this.statsPanelManager1 = new FP_CS26_2025.HotelManager_AdminDashboard.StatsPanelManager();
            this.quickAccessManager1 = new FP_CS26_2025.HotelManager_AdminDashboard.QuickAccessManager();
            this.sidebarManager1 = new FP_CS26_2025.HotelManager_AdminDashboard.SidebarManager();
            this.SuspendLayout();
            // 
            // bookingManager1
            // 
            this.bookingManager1.BackColor = System.Drawing.Color.White;
            this.bookingManager1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bookingManager1.Location = new System.Drawing.Point(197, 12);
            this.bookingManager1.Name = "bookingManager1";
            this.bookingManager1.Size = new System.Drawing.Size(641, 417);
            this.bookingManager1.TabIndex = 5;
            // 
            // statsPanelManager1
            // 
            this.statsPanelManager1.AvailableRooms = 20;
            this.statsPanelManager1.BackColor = System.Drawing.Color.White;
            this.statsPanelManager1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statsPanelManager1.CurrentBookings = 124;
            this.statsPanelManager1.Location = new System.Drawing.Point(844, 12);
            this.statsPanelManager1.Name = "statsPanelManager1";
            this.statsPanelManager1.OccupancyRate = 86;
            this.statsPanelManager1.Padding = new System.Windows.Forms.Padding(15);
            this.statsPanelManager1.Revenue = new decimal(new int[] {
            3450,
            0,
            0,
            0});
            this.statsPanelManager1.Size = new System.Drawing.Size(568, 222);
            this.statsPanelManager1.StatPanelBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.statsPanelManager1.StatValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.statsPanelManager1.StatValueFontSize = 16;
            this.statsPanelManager1.TabIndex = 4;
            this.statsPanelManager1.Target = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.statsPanelManager1.WelcomeText = "Welcome, Hotel Manager";
            this.statsPanelManager1.WelcomeTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            // 
            // quickAccessManager1
            // 
            this.quickAccessManager1.BackColor = System.Drawing.Color.White;
            this.quickAccessManager1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.quickAccessManager1.ButtonBackColor = System.Drawing.Color.White;
            this.quickAccessManager1.ButtonBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.quickAccessManager1.ButtonHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.quickAccessManager1.ButtonHoverTextColor = System.Drawing.Color.White;
            this.quickAccessManager1.ButtonsEnabled = true;
            this.quickAccessManager1.ButtonTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.quickAccessManager1.Location = new System.Drawing.Point(197, 435);
            this.quickAccessManager1.Name = "quickAccessManager1";
            this.quickAccessManager1.PanelTitle = "Quick Access Functions";
            this.quickAccessManager1.Size = new System.Drawing.Size(641, 244);
            this.quickAccessManager1.TabIndex = 2;
            this.quickAccessManager1.TitleBottomMargin = 25;
            // 
            // sidebarManager1
            // 
            this.sidebarManager1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.sidebarManager1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sidebarManager1.ButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.sidebarManager1.ButtonHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(61)))), ((int)(((byte)(86)))));
            this.sidebarManager1.ButtonsEnabled = true;
            this.sidebarManager1.ButtonTextColor = System.Drawing.Color.White;
            this.sidebarManager1.Location = new System.Drawing.Point(0, 0);
            this.sidebarManager1.Margin = new System.Windows.Forms.Padding(2);
            this.sidebarManager1.Name = "sidebarManager1";
            this.sidebarManager1.SelectedButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.sidebarManager1.SidebarTitle = "Hotel Manager";
            this.sidebarManager1.Size = new System.Drawing.Size(192, 694);
            this.sidebarManager1.TabIndex = 1;
            this.sidebarManager1.Paint += new System.Windows.Forms.PaintEventHandler(this.sidebarManager1_Paint);
            // 
            // Hotel_AdminDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1424, 691);
            this.Controls.Add(this.bookingManager1);
            this.Controls.Add(this.statsPanelManager1);
            this.Controls.Add(this.quickAccessManager1);
            this.Controls.Add(this.sidebarManager1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Hotel_AdminDashboard";
            this.Text = "Hotel_AdminDashboard";
            this.Load += new System.EventHandler(this.Hotel_AdminDashboard_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private SidebarManager sidebarManager1;
        private QuickAccessManager quickAccessManager1;
        private StatsPanelManager statsPanelManager1;
        private BookingManager bookingManager1;
    }
}
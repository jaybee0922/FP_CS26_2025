namespace FP_CS26_2025.FrontDesk_ReceptionistAccount
{
    partial class Hotel_FrontDeskDashboard
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
            this.sidebarManager1 = new FP_CS26_2025.FrontDesk_ReceptionistAccount.FrontDesk_SidebarManager();
            this.mainContentPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.mainRootLayout = new System.Windows.Forms.TableLayoutPanel();
            this.dashboardContainer = new System.Windows.Forms.Panel();
            this.mainContentPanel.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.mainRootLayout.SuspendLayout();
            this.dashboardContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // bookingManager1
            // 
            this.bookingManager1.BackColor = System.Drawing.Color.White;
            this.bookingManager1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bookingManager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bookingManager1.Location = new System.Drawing.Point(20, 20);
            this.bookingManager1.Margin = new System.Windows.Forms.Padding(20);
            this.bookingManager1.Name = "bookingManager1";
            this.bookingManager1.Size = new System.Drawing.Size(810, 455);
            this.bookingManager1.TabIndex = 5;
            this.bookingManager1.TabIndex = 5;
            // 
            // statsPanelManager1
            // 
            this.statsPanelManager1.AvailableRooms = 15;
            this.statsPanelManager1.BackColor = System.Drawing.Color.White;
            this.statsPanelManager1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statsPanelManager1.CurrentBookings = 42;
            this.statsPanelManager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statsPanelManager1.Location = new System.Drawing.Point(870, 20);
            this.statsPanelManager1.Margin = new System.Windows.Forms.Padding(20);
            this.statsPanelManager1.Name = "statsPanelManager1";
            this.statsPanelManager1.OccupancyRate = 75;
            this.statsPanelManager1.Padding = new System.Windows.Forms.Padding(20, 18, 20, 18);
            this.statsPanelManager1.Revenue = new decimal(new int[] {
            1200,
            0,
            0,
            0});
            this.statsPanelManager1.Size = new System.Drawing.Size(657, 455);
            this.statsPanelManager1.StatPanelBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(255)))), ((int)(((byte)(250)))));
            this.statsPanelManager1.StatValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.statsPanelManager1.StatValueFontSize = 16;
            this.statsPanelManager1.TabIndex = 4;
            this.statsPanelManager1.Target = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.statsPanelManager1.WelcomeText = "Welcome, Front Desk";
            this.statsPanelManager1.WelcomeTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            // 
            // quickAccessManager1
            // 
            this.quickAccessManager1.BackColor = System.Drawing.Color.White;
            this.quickAccessManager1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.quickAccessManager1.ButtonBackColor = System.Drawing.Color.White;
            this.quickAccessManager1.ButtonBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.quickAccessManager1.ButtonHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.quickAccessManager1.ButtonHoverTextColor = System.Drawing.Color.White;
            this.quickAccessManager1.ButtonsEnabled = true;
            this.quickAccessManager1.ButtonTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.tableLayoutPanelMain.SetColumnSpan(this.quickAccessManager1, 2);
            this.quickAccessManager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.quickAccessManager1.Location = new System.Drawing.Point(20, 515);
            this.quickAccessManager1.Margin = new System.Windows.Forms.Padding(20);
            this.quickAccessManager1.Name = "quickAccessManager1";
            this.quickAccessManager1.PanelTitle = "Receptionist Quick Actions";
            this.quickAccessManager1.Size = new System.Drawing.Size(1507, 227);
            this.quickAccessManager1.TabIndex = 2;
            this.quickAccessManager1.TitleBottomMargin = 25;
            // 
            // sidebarManager1
            // 
            this.sidebarManager1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.sidebarManager1.ButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.sidebarManager1.ButtonHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.sidebarManager1.ButtonsEnabled = true;
            this.sidebarManager1.ButtonTextColor = System.Drawing.Color.White;
            this.sidebarManager1.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarManager1.Location = new System.Drawing.Point(0, 0);
            this.sidebarManager1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sidebarManager1.Name = "sidebarManager1";
            this.sidebarManager1.SelectedButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.sidebarManager1.SidebarTitle = "Front Desk";
            this.sidebarManager1.Size = new System.Drawing.Size(255, 792);
            this.sidebarManager1.TabIndex = 1;
            // 
            // mainContentPanel
            // 
            this.mainContentPanel.Controls.Add(this.tableLayoutPanelMain);
            this.mainContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContentPanel.Location = new System.Drawing.Point(255, 0);
            this.mainContentPanel.Name = "mainContentPanel";
            this.mainContentPanel.Padding = new System.Windows.Forms.Padding(15);
            this.mainContentPanel.Size = new System.Drawing.Size(1577, 792);
            this.mainContentPanel.TabIndex = 6;
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanelMain.Controls.Add(this.bookingManager1, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.statsPanelManager1, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.quickAccessManager1, 0, 1);
            this.tableLayoutPanelMain.SetColumnSpan(this.quickAccessManager1, 2);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(15, 15);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 2;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1547, 762);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // mainRootLayout
            // 
            this.mainRootLayout.BackColor = System.Drawing.Color.Transparent;
            this.mainRootLayout.ColumnCount = 1;
            this.mainRootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainRootLayout.Controls.Add(this.dashboardContainer, 0, 0);
            this.mainRootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainRootLayout.Location = new System.Drawing.Point(0, 0);
            this.mainRootLayout.Name = "mainRootLayout";
            this.mainRootLayout.Padding = new System.Windows.Forms.Padding(40);
            this.mainRootLayout.RowCount = 1;
            this.mainRootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainRootLayout.Size = new System.Drawing.Size(1920, 880);
            this.mainRootLayout.TabIndex = 7;
            // 
            // dashboardContainer
            // 
            this.dashboardContainer.BackColor = System.Drawing.Color.White;
            this.dashboardContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dashboardContainer.Controls.Add(this.mainContentPanel);
            this.dashboardContainer.Controls.Add(this.sidebarManager1);
            this.dashboardContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dashboardContainer.Location = new System.Drawing.Point(43, 43);
            this.dashboardContainer.Name = "dashboardContainer";
            this.dashboardContainer.Size = new System.Drawing.Size(1834, 794);
            this.dashboardContainer.TabIndex = 8;
            // 
            // Hotel_FrontDeskDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 880);
            this.Controls.Add(this.mainRootLayout);
            this.Name = "Hotel_FrontDeskDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Front Desk Dashboard";
            this.mainContentPanel.ResumeLayout(false);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.mainRootLayout.ResumeLayout(false);
            this.dashboardContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private FP_CS26_2025.FrontDesk_ReceptionistAccount.FrontDesk_SidebarManager sidebarManager1;
        private FP_CS26_2025.HotelManager_AdminDashboard.QuickAccessManager quickAccessManager1;
        private FP_CS26_2025.HotelManager_AdminDashboard.StatsPanelManager statsPanelManager1;
        private FP_CS26_2025.HotelManager_AdminDashboard.BookingManager bookingManager1;
        private System.Windows.Forms.Panel mainContentPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.TableLayoutPanel mainRootLayout;
        private System.Windows.Forms.Panel dashboardContainer;
    }
}

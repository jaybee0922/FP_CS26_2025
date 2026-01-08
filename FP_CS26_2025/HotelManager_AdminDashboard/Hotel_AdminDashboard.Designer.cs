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
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.dashboardContainer = new System.Windows.Forms.Panel();
            this.mainContentPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanelContent = new System.Windows.Forms.TableLayoutPanel();
            this.bookingManager1 = new FP_CS26_2025.HotelManager_AdminDashboard.BookingManager();
            this.sidebarManager1 = new FP_CS26_2025.HotelManager_AdminDashboard.SidebarManager();
            this.userManagementControl1 = new FP_CS26_2025.HotelManager_AdminDashboard.UserManagementControl();
            this.systemConfigurationControl1 = new FP_CS26_2025.HotelManager_AdminDashboard.SystemConfigurationControl();
            
            this.mainLayout.SuspendLayout();
            this.dashboardContainer.SuspendLayout();
            this.mainContentPanel.SuspendLayout();
            this.tableLayoutPanelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainLayout
            // 
            this.mainLayout.BackColor = System.Drawing.Color.Transparent;
            this.mainLayout.ColumnCount = 1;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Controls.Add(this.dashboardContainer, 0, 0);
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 0);
            this.mainLayout.Margin = new System.Windows.Forms.Padding(2);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.Padding = new System.Windows.Forms.Padding(30, 32, 30, 32);
            this.mainLayout.RowCount = 1;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Size = new System.Drawing.Size(1432, 715);
            this.mainLayout.TabIndex = 6;
            // 
            // dashboardContainer
            // 
            this.dashboardContainer.BackColor = System.Drawing.Color.White;
            this.dashboardContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dashboardContainer.Controls.Add(this.mainContentPanel);
            this.dashboardContainer.Controls.Add(this.sidebarManager1);
            this.dashboardContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dashboardContainer.Location = new System.Drawing.Point(32, 34);
            this.dashboardContainer.Margin = new System.Windows.Forms.Padding(2);
            this.dashboardContainer.Name = "dashboardContainer";
            this.dashboardContainer.Size = new System.Drawing.Size(1368, 647);
            this.dashboardContainer.TabIndex = 7;
            // 
            // mainContentPanel
            // 
            this.mainContentPanel.Controls.Add(this.tableLayoutPanelContent);
            this.mainContentPanel.Controls.Add(this.userManagementControl1);
            this.mainContentPanel.Controls.Add(this.systemConfigurationControl1);
            this.mainContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContentPanel.Location = new System.Drawing.Point(191, 0);
            this.mainContentPanel.Margin = new System.Windows.Forms.Padding(2);
            this.mainContentPanel.Name = "mainContentPanel";
            this.mainContentPanel.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.mainContentPanel.Size = new System.Drawing.Size(1175, 645);
            this.mainContentPanel.TabIndex = 8;
            // 
            // userManagementControl1
            // 
            this.userManagementControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userManagementControl1.Location = new System.Drawing.Point(15, 15);
            this.userManagementControl1.Name = "userManagementControl1";
            this.userManagementControl1.Size = new System.Drawing.Size(1537, 762);
            this.userManagementControl1.TabIndex = 1;
            this.userManagementControl1.Visible = false;
            // 
            // tableLayoutPanelContent
            // 
            this.tableLayoutPanelContent.ColumnCount = 1;
            this.tableLayoutPanelContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelContent.Controls.Add(this.bookingManager1, 0, 0);
            this.tableLayoutPanelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelContent.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelContent.Name = "tableLayoutPanelContent";
            this.tableLayoutPanelContent.RowCount = 1;
            this.tableLayoutPanelContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelContent.Size = new System.Drawing.Size(1153, 621);
            this.tableLayoutPanelContent.TabIndex = 0;
            // 
            // bookingManager1
            // 
            this.bookingManager1.BackColor = System.Drawing.Color.White;
            this.bookingManager1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bookingManager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bookingManager1.Location = new System.Drawing.Point(8, 8);
            this.bookingManager1.Margin = new System.Windows.Forms.Padding(8);
            this.bookingManager1.Name = "bookingManager1";
            this.bookingManager1.Size = new System.Drawing.Size(1137, 605);
            this.bookingManager1.TabIndex = 6;
            // 
            // userManagementControl1
            // 
            this.userManagementControl1.BackColor = System.Drawing.Color.White;
            this.userManagementControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userManagementControl1.Location = new System.Drawing.Point(11, 12);
            this.userManagementControl1.Margin = new System.Windows.Forms.Padding(2);
            this.userManagementControl1.Name = "userManagementControl1";
            this.userManagementControl1.Size = new System.Drawing.Size(1153, 621);
            this.userManagementControl1.TabIndex = 1;
            this.userManagementControl1.Visible = false;
            // 
            // systemConfigurationControl1
            // 
            this.systemConfigurationControl1.BackColor = System.Drawing.Color.White;
            this.systemConfigurationControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.systemConfigurationControl1.Location = new System.Drawing.Point(11, 12);
            this.systemConfigurationControl1.Margin = new System.Windows.Forms.Padding(2);
            this.systemConfigurationControl1.Name = "systemConfigurationControl1";
            this.systemConfigurationControl1.Size = new System.Drawing.Size(1153, 621);
            this.systemConfigurationControl1.TabIndex = 2;
            this.systemConfigurationControl1.Visible = false;
            // 
            // 
            // 
            // 
            // sidebarManager1
            // 
            this.sidebarManager1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.sidebarManager1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sidebarManager1.ButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.sidebarManager1.ButtonHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(61)))), ((int)(((byte)(86)))));
            this.sidebarManager1.ButtonsEnabled = true;
            this.sidebarManager1.ButtonTextColor = System.Drawing.Color.White;
            this.sidebarManager1.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarManager1.Location = new System.Drawing.Point(0, 0);
            this.sidebarManager1.Margin = new System.Windows.Forms.Padding(2);
            this.sidebarManager1.Name = "sidebarManager1";
            this.sidebarManager1.SelectedButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.sidebarManager1.SidebarTitle = "Hotel Manager";
            this.sidebarManager1.Size = new System.Drawing.Size(191, 645);
            this.sidebarManager1.TabIndex = 5;
            // 
            // Hotel_AdminDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1432, 715);
            this.Controls.Add(this.mainLayout);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Hotel_AdminDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hotel Manager Dashboard";
            this.mainLayout.ResumeLayout(false);
            this.dashboardContainer.ResumeLayout(false);
            this.mainContentPanel.ResumeLayout(false);
            this.tableLayoutPanelContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private SidebarManager sidebarManager1;
        
        // private StatsPanelManager statsPanelManager1;
        private BookingManager bookingManager1;
        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.Panel dashboardContainer;
        private System.Windows.Forms.Panel mainContentPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelContent;
        private UserManagementControl userManagementControl1;
        private SystemConfigurationControl systemConfigurationControl1;
    }
}
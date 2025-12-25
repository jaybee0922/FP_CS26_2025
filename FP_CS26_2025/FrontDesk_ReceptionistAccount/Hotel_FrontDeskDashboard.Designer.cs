namespace FP_CS26_2025
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
            this.sidebarManager1 = new FP_CS26_2025.FrontDesk_SidebarManager();
            this.mainContentPanel = new System.Windows.Forms.Panel();
            this.dashboardContainer = new System.Windows.Forms.Panel();
            this.mainRootLayout = new System.Windows.Forms.TableLayoutPanel();
            this.dashboardContainer.SuspendLayout();
            this.mainRootLayout.SuspendLayout();
            this.SuspendLayout();
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
            this.mainContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContentPanel.Location = new System.Drawing.Point(255, 0);
            this.mainContentPanel.Name = "mainContentPanel";
            this.mainContentPanel.Padding = new System.Windows.Forms.Padding(15);
            this.mainContentPanel.Size = new System.Drawing.Size(1577, 792);
            this.mainContentPanel.TabIndex = 6;
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
            // Hotel_FrontDeskDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 880);
            this.Controls.Add(this.mainRootLayout);
            this.Name = "Hotel_FrontDeskDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Front Desk Dashboard";
            this.dashboardContainer.ResumeLayout(false);
            this.mainRootLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private FP_CS26_2025.FrontDesk_SidebarManager sidebarManager1;
        private System.Windows.Forms.Panel mainContentPanel;
        private System.Windows.Forms.Panel dashboardContainer;
        private System.Windows.Forms.TableLayoutPanel mainRootLayout;
    }
}

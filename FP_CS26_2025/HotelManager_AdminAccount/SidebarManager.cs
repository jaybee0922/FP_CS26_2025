using System;
using System.Drawing;
using System.Windows.Forms;

namespace HotelManagementSystem
{
    public class SidebarManager : IDisposable
    {
        private Panel sidebarPanel;
        private Button btnDashboard, btnUserManagement, btnRoomRates, btnReports, btnSystemConfig;
        private Action<Button> navigationHandler;

        public SidebarManager(Panel sidebarPanel)
        {
            this.sidebarPanel = sidebarPanel;
            CreateSidebar();
        }

        private void CreateSidebar()
        {
            var lblHotelManager = new Label
            {
                Text = "Hotel Manager",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(180, 30)
            };

            btnDashboard = CreateNavButton("Dashboard", 80);
            btnUserManagement = CreateNavButton("User Management", 130);
            btnRoomRates = CreateNavButton("Room Rates & Policies", 180);
            btnReports = CreateNavButton("Reports", 230);
            btnSystemConfig = CreateNavButton("System Configuration", 280);

            sidebarPanel.Controls.AddRange(new Control[] {
                lblHotelManager, btnDashboard, btnUserManagement,
                btnRoomRates, btnReports, btnSystemConfig
            });
        }

        private Button CreateNavButton(string text, int top)
        {
            var button = new Button
            {
                Text = text,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(51, 51, 76),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft,
                Size = new Size(200, 40),
                Location = new Point(10, top),
                Font = new Font("Segoe UI", 10),
                Cursor = Cursors.Hand
            };

            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(61, 61, 86);

            button.Click += (s, e) => navigationHandler?.Invoke(button);
            return button;
        }

        public void SetNavigationHandler(Action<Button> handler)
        {
            navigationHandler = handler;
        }

        public Button GetDashboardButton() => btnDashboard;

        public void Dispose()
        {
            // Clean up resources if needed
            btnDashboard?.Dispose();
            btnUserManagement?.Dispose();
            btnRoomRates?.Dispose();
            btnReports?.Dispose();
            btnSystemConfig?.Dispose();
        }
    }
}
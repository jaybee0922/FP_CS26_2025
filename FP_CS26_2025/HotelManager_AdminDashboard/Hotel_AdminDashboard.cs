using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FP_CS26_2025.Services;

namespace FP_CS26_2025.HotelManager_AdminDashboard
{
    public partial class Hotel_AdminDashboard : Form
    {
        private readonly ILogoutService _logoutService;

        public Hotel_AdminDashboard()
        {
            _logoutService = new LogoutService();
            InitializeComponent();
            this.DoubleBuffered = true;
            
            // Initial data load
            // Initial data load
            bookingManager1.LoadSampleData();
           
            // Data Manager Setup
            var dataManager = new DataManager(); // Central data manager
            userManagementControl1.SetDataManager(dataManager);

            // Sidebar Events
            sidebarManager1.SelectButtonByText("Dashboard");
            sidebarManager1.LogoutClicked += (s, e) => _logoutService.HandleLogout(this);
            
            sidebarManager1.DashboardClicked += (s, e) => 
            {
                sidebarManager1.SelectButtonByText("Dashboard");
                tableLayoutPanelContent.Visible = true;
                userManagementControl1.Visible = false;
            };

            sidebarManager1.UserManagementClicked += (s, e) =>
            {
                sidebarManager1.SelectButtonByText("User Management");
                tableLayoutPanelContent.Visible = false;
                userManagementControl1.Visible = true;
                userManagementControl1.BringToFront();
            };
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(45, 52, 71),   // Dark Blue/Gray
                Color.FromArgb(20, 23, 30),   // Deeper Blue/Black
                45f))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }
    }
}

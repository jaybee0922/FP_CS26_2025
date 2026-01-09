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
using FP_CS26_2025.Room_Rates___Policies;

namespace FP_CS26_2025.HotelManager_AdminDashboard
{
    public partial class Hotel_AdminDashboard : Form
    {
        private readonly ILogoutService _logoutService;
        private RoomRatesControl _roomRatesControl;
        private FP_CS26_2025.HotelManager_AdminDashboard.Reports.ReportsControl _reportsControl;
        private FP_CS26_2025.HotelManager_AdminDashboard.SalesProfitsControl _salesProfitsControl;
        private PromoManagementControl _promoControl;

        public Hotel_AdminDashboard()
        {
            _logoutService = new LogoutService();
            InitializeComponent();
            
            // Initialize RoomRatesControl
            _roomRatesControl = new RoomRatesControl();
            _roomRatesControl.Dock = DockStyle.Fill;
            _roomRatesControl.Visible = false;
            mainContentPanel.Controls.Add(_roomRatesControl);

            // Initialize ReportsControl
            _reportsControl = new FP_CS26_2025.HotelManager_AdminDashboard.Reports.ReportsControl();
            _reportsControl.Dock = DockStyle.Fill;
            _reportsControl.Visible = false;
            mainContentPanel.Controls.Add(_reportsControl);

            // Initialize SalesProfitsControl
            _salesProfitsControl = new FP_CS26_2025.HotelManager_AdminDashboard.SalesProfitsControl();
            _salesProfitsControl.Dock = DockStyle.Fill;
            _salesProfitsControl.Visible = false;
            mainContentPanel.Controls.Add(_salesProfitsControl);

            // Initialize PromoManagementControl
            _promoControl = new PromoManagementControl();
            _promoControl.Dock = DockStyle.Fill;
            _promoControl.Visible = false;
            mainContentPanel.Controls.Add(_promoControl);

            this.DoubleBuffered = true;
            
            // Initial data load
            // Data Manager Setup
            var dataManager = new DataManager(); // Central data manager
            userManagementControl1.SetDataManager(dataManager);

            // statsPanelManager1 usage removed

            // Load Recent Bookings from Database
            try
            {
                var dtBookings = dataManager.GetRecentBookingsFromDb();
                bookingManager1.LoadData(dtBookings);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading recent bookings: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Booking Removal Event
            bookingManager1.BookingRemovalRequested += (s, bookingId) =>
            {
                try
                {
                    bool success = dataManager.DeleteReservationFromDb(bookingId);
                    if (success)
                    {
                        MessageBox.Show("Booking deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Refresh the list
                        var dt = dataManager.GetRecentBookingsFromDb();
                        bookingManager1.LoadData(dt);
                    }
                    else
                    {
                        MessageBox.Show("Could not find booking to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting booking: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            // Sidebar Events
            sidebarManager1.SelectButtonByText("Dashboard");
            sidebarManager1.LogoutClicked += (s, e) => _logoutService.HandleLogout(this);
            
            sidebarManager1.DashboardClicked += (s, e) => 
            {
                sidebarManager1.SelectButtonByText("Dashboard");
                tableLayoutPanelContent.Visible = true;
                userManagementControl1.Visible = false;
                _roomRatesControl.Visible = false;
                _reportsControl.Visible = false;
                _salesProfitsControl.Visible = false;
                _promoControl.Visible = false;
                systemConfigurationControl1.Visible = false;
            };

            sidebarManager1.UserManagementClicked += (s, e) =>
            {
                sidebarManager1.SelectButtonByText("User Management");
                tableLayoutPanelContent.Visible = false;
                userManagementControl1.Visible = true;
                _roomRatesControl.Visible = false;
                _reportsControl.Visible = false;
                _salesProfitsControl.Visible = false;
                _promoControl.Visible = false;
                systemConfigurationControl1.Visible = false;
                userManagementControl1.BringToFront();
            };

            sidebarManager1.RoomRatesClicked += (s, e) =>
            {
                sidebarManager1.SelectButtonByText("Room Rates and Policies");
                tableLayoutPanelContent.Visible = false;
                userManagementControl1.Visible = false;
                systemConfigurationControl1.Visible = false;
                _reportsControl.Visible = false;
                _salesProfitsControl.Visible = false;
                _promoControl.Visible = false;
                _roomRatesControl.Visible = true;
                _roomRatesControl.BringToFront();
                _roomRatesControl.SetDataManager(dataManager);
                _roomRatesControl.SetConfigService(new FP_CS26_2025.HotelManager_AdminDashboard.Configuration.XmlConfigService());
            };

            sidebarManager1.ReportsClicked += (s, e) =>
            {
                sidebarManager1.SelectButtonByText("Reports");
                tableLayoutPanelContent.Visible = false;
                userManagementControl1.Visible = false;
                _roomRatesControl.Visible = false;
                systemConfigurationControl1.Visible = false;
                _salesProfitsControl.Visible = false;
                _promoControl.Visible = false;
                _reportsControl.Visible = true;
                _reportsControl.BringToFront();
            };

            sidebarManager1.SalesProfitsClicked += (s, e) =>
            {
                sidebarManager1.SelectButtonByText("Sales and Profits");
                tableLayoutPanelContent.Visible = false;
                userManagementControl1.Visible = false;
                _roomRatesControl.Visible = false;
                systemConfigurationControl1.Visible = false;
                _reportsControl.Visible = false;
                _promoControl.Visible = false;
                _salesProfitsControl.Visible = true;
                _salesProfitsControl.BringToFront();
                _salesProfitsControl.LoadData();
            };

            sidebarManager1.SystemConfigClicked += (s, e) =>
            {
                sidebarManager1.SelectButtonByText("System Configuration");
                tableLayoutPanelContent.Visible = false;
                userManagementControl1.Visible = false;
                _roomRatesControl.Visible = false;
                _reportsControl.Visible = false;
                _salesProfitsControl.Visible = false;
                _promoControl.Visible = false;
                systemConfigurationControl1.Visible = true;
                systemConfigurationControl1.BringToFront();
                systemConfigurationControl1.SetDataManager(dataManager);
            };

            sidebarManager1.PromoCodesClicked += (s, e) =>
            {
                sidebarManager1.SelectButtonByText("Promo Codes");
                tableLayoutPanelContent.Visible = false;
                userManagementControl1.Visible = false;
                _roomRatesControl.Visible = false;
                _reportsControl.Visible = false;
                _salesProfitsControl.Visible = false;
                systemConfigurationControl1.Visible = false;
                _promoControl.Visible = true;
                _promoControl.BringToFront();
            };
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(45, 52, 71),  
                Color.FromArgb(20, 23, 30), 
                45f))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        
    }
}

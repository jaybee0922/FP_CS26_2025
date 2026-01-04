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
<<<<<<< HEAD
        private RoomRatesControl _roomRatesControl;
=======
>>>>>>> 2d839a00d5ea4e1d0bbbaf3500d0bf97d0c91e82

        public Hotel_AdminDashboard()
        {
            _logoutService = new LogoutService();
            InitializeComponent();
<<<<<<< HEAD
            
            // Initialize RoomRatesControl
            _roomRatesControl = new RoomRatesControl();
            _roomRatesControl.Dock = DockStyle.Fill;
            _roomRatesControl.Visible = false;
            mainContentPanel.Controls.Add(_roomRatesControl);
=======
>>>>>>> 2d839a00d5ea4e1d0bbbaf3500d0bf97d0c91e82
            this.DoubleBuffered = true;
            
            // Initial data load
            // Initial data load
            // Data Manager Setup
            var dataManager = new DataManager(); // Central data manager
            userManagementControl1.SetDataManager(dataManager);

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
<<<<<<< HEAD
                _roomRatesControl.Visible = false;
=======
>>>>>>> 2d839a00d5ea4e1d0bbbaf3500d0bf97d0c91e82
            };

            sidebarManager1.UserManagementClicked += (s, e) =>
            {
                sidebarManager1.SelectButtonByText("User Management");
                tableLayoutPanelContent.Visible = false;
                userManagementControl1.Visible = true;
<<<<<<< HEAD
                _roomRatesControl.Visible = false;
=======
>>>>>>> 2d839a00d5ea4e1d0bbbaf3500d0bf97d0c91e82
                userManagementControl1.BringToFront();
            };

            sidebarManager1.RoomRatesClicked += (s, e) =>
            {
                sidebarManager1.SelectButtonByText("Room Rates and Policies");
<<<<<<< HEAD
                tableLayoutPanelContent.Visible = false;
                userManagementControl1.Visible = false;
                _roomRatesControl.Visible = true;
                _roomRatesControl.BringToFront();
                _roomRatesControl.SetDataManager(dataManager);
=======
                var roomRatesForm = new RoomRates_and_Pricing_Form();
                roomRatesForm.Show(); 
>>>>>>> 2d839a00d5ea4e1d0bbbaf3500d0bf97d0c91e82
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

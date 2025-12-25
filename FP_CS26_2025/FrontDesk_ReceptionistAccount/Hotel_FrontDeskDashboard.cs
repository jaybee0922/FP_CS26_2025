using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using FP_CS26_2025.FrontDesk_MVC;
using FP_CS26_2025.HotelManager_AdminDashboard;

namespace FP_CS26_2025
{
    public partial class Hotel_FrontDeskDashboard : Form
    {
        private readonly FrontDeskController _controller;

        public Hotel_FrontDeskDashboard()
        {
            var dataService = new InMemoryHotelService();
            _controller = new FrontDeskController(dataService);

            InitializeComponent();
            SetupDashboard();
            SetupSidebarEvents();
            this.DoubleBuffered = true;
        }

        private void SetupDashboard()
        {
            sidebarManager1.SidebarTitle = "Front Desk";
            // Default to Reservations view
            sidebarManager1.SelectButtonByText("Reservations");
            SwitchView(new ReservationPanel(_controller));
        }

        private void SwitchView(Control newView)
        {
            mainContentPanel.Controls.Clear();
            
            if (newView != null)
            {
                newView.Dock = DockStyle.Fill;
                mainContentPanel.Controls.Add(newView);
                newView.BringToFront();
            }
        }

        private void SetupSidebarEvents()
        {
            sidebarManager1.ReservationsClicked += (s, e) => {
                sidebarManager1.SelectButtonByText("Reservations");
                SwitchView(new ReservationPanel(_controller));
            };

            sidebarManager1.CheckInClicked += (s, e) => {
                sidebarManager1.SelectButtonByText("Check-In");
                SwitchView(new CheckInPanel(_controller));
            };

            sidebarManager1.CheckOutClicked += (s, e) => {
                sidebarManager1.SelectButtonByText("Check-Out");
                SwitchView(new CheckOutPanel(_controller));
            };

            sidebarManager1.RoomAssignmentsClicked += (s, e) => {
                sidebarManager1.SelectButtonByText("Rooms");
                SwitchView(new RoomsCalendarPanel(_controller));
            };

            sidebarManager1.BillingClicked += (s, e) => {
                sidebarManager1.SelectButtonByText("Billing");
                SwitchView(new BillingPanel(_controller));
            };

            sidebarManager1.LogoutClicked += (s, e) => {
                var result = MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.Close();
                    // Assuming the parent form (Login) will handle showing itself again
                }
            };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(245, 247, 251), // Light modern background
                Color.FromArgb(245, 247, 251),
                45f))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }
    }
}

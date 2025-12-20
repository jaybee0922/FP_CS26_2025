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
        private Control dashboardView;
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
            statsPanelManager1.WelcomeText = "Welcome, Front Desk";
            UpdateStats();
            quickAccessManager1.PanelTitle = "Receptionist Quick Actions";
            bookingManager1.LoadSampleData();
            sidebarManager1.SelectButtonByText("Dashboard");
            dashboardView = mainContentPanel.Controls.Cast<Control>().FirstOrDefault(); 
        }

        private void UpdateStats()
        {
            int currentBookings = _controller.GetActiveReservations().Count(r => r.IsCheckedIn);
            int availableRooms = _controller.GetAllRooms().Count(r => r.Status == RoomStatus.Available);
            int totalRooms = _controller.GetAllRooms().Count();
            int occupancyRate = totalRooms > 0 ? (currentBookings * 100 / totalRooms) : 0;

            statsPanelManager1.SetCurrentBookings(currentBookings);
            statsPanelManager1.SetAvailableRooms(availableRooms);
            statsPanelManager1.SetOccupancyRate(occupancyRate);
        }

        private void SwitchView(Control newView)
        {
            foreach (Control ctrl in mainContentPanel.Controls.Cast<Control>().ToList())
            {
                if (ctrl != tableLayoutPanelMain)
                {
                    mainContentPanel.Controls.Remove(ctrl);
                    ctrl.Dispose();
                }
            }

            if (newView != null)
            {
                tableLayoutPanelMain.Visible = false;
                newView.Dock = DockStyle.Fill;
                mainContentPanel.Controls.Add(newView);
                newView.BringToFront();
            }
            else
            {
                UpdateStats();
                tableLayoutPanelMain.Visible = true;
            }
        }

        private void SetupSidebarEvents()
        {
            sidebarManager1.DashboardClicked += (s, e) => {
                sidebarManager1.SelectButtonByText("Dashboard");
                SwitchView(null);
            };

            sidebarManager1.CheckInClicked += (s, e) => {
                sidebarManager1.SelectButtonByText("Check-In");
                SwitchView(new CheckInPanel(_controller));
            };

            sidebarManager1.CheckOutClicked += (s, e) => {
                sidebarManager1.SelectButtonByText("Check-Out");
                SwitchView(new CheckOutPanel(_controller));
            };

            sidebarManager1.RoomsCalendarClicked += (s, e) => {
                sidebarManager1.SelectButtonByText("Rooms & Calendar");
                SwitchView(new RoomsCalendarPanel(_controller));
            };

            sidebarManager1.GuestListClicked += (s, e) => {
                sidebarManager1.SelectButtonByText("Guest List");
                SwitchView(new ReservationPanel(_controller));
            };

            sidebarManager1.BillingClicked += (s, e) => {
                sidebarManager1.SelectButtonByText("Billing");
                SwitchView(new BillingPanel(_controller));
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

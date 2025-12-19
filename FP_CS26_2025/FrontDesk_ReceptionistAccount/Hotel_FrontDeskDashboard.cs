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

namespace FP_CS26_2025.FrontDesk_ReceptionistAccount
{
    public partial class Hotel_FrontDeskDashboard : Form
    {
        private Control dashboardView;

        public Hotel_FrontDeskDashboard()
        {
            InitializeComponent();
            SetupDashboard();
            SetupSidebarEvents();
            this.DoubleBuffered = true;
        }

        private void SetupDashboard()
        {
            // Set up sidebar properties
            sidebarManager1.SidebarTitle = "Front Desk";
            
            // Set up stats panel for front desk
            statsPanelManager1.WelcomeText = "Welcome, Front Desk";
            statsPanelManager1.SetCurrentBookings(12); 
            statsPanelManager1.SetAvailableRooms(25);
            statsPanelManager1.SetOccupancyRate(60);
            
            // Set up quick access titles
            quickAccessManager1.PanelTitle = "Receptionist Quick Actions";
            
            // Initial data load
            bookingManager1.LoadSampleData();
            sidebarManager1.SelectButtonByText("Dashboard");

            // Store the initial dashboard view
            dashboardView = mainContentPanel.Controls.Cast<Control>().FirstOrDefault(); 
           
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
                // Restore Dashboard View
                tableLayoutPanelMain.Visible = true;
            }
        }

        private void SetupSidebarEvents()
        {
            sidebarManager1.DashboardClicked += (s, e) => {
                sidebarManager1.SelectButtonByText("Dashboard");
                SwitchView(null); // null means default dashboard
            };

            sidebarManager1.CheckInClicked += (s, e) => {
                sidebarManager1.SelectButtonByText("Check-In");
                SwitchView(new CheckInPanel());
            };

            sidebarManager1.CheckOutClicked += (s, e) => {
                sidebarManager1.SelectButtonByText("Check-Out");
                SwitchView(new CheckOutPanel());
            };

            sidebarManager1.RoomsCalendarClicked += (s, e) => {
                sidebarManager1.SelectButtonByText("Rooms & Calendar");
                SwitchView(new RoomsCalendarPanel());
            };

            sidebarManager1.GuestListClicked += (s, e) => {
                sidebarManager1.SelectButtonByText("Guest List");
                SwitchView(new GuestListPanel());
            };

            sidebarManager1.BillingClicked += (s, e) => {
                sidebarManager1.SelectButtonByText("Billing");
                SwitchView(new BillingPanel());
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

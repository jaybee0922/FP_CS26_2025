using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace FP_CS26_2025.HotelManager_AdminDashboard
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Panel))]
    public class MainContentManager : Panel, IDisposable
    {
        private BookingManager bookingsManager;
        private QuickAccessManager quickAccessManager;
        private SalesProfitsControl salesProfitsControl;
        private Label lblLastUpdated;

        // Events
        public event EventHandler ContentLoaded;
        public event EventHandler LastUpdatedTimeChanged;

        public MainContentManager()
        {
            InitializeComponent();
            InitializeMainContent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Set up the main content panel properties
            this.Size = new Size(1180, 800);
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.AutoScroll = true;
            this.BorderStyle = BorderStyle.FixedSingle;

            this.ResumeLayout(false);
        }

        private void InitializeMainContent()
        {
            CreateLastUpdatedLabel();

            // Initialize sub-managers with parameterless constructors
            bookingsManager = new BookingManager();
            quickAccessManager = new QuickAccessManager();
            salesProfitsControl = new SalesProfitsControl();

            // Add the sub-components to this panel
            this.Controls.Add(bookingsManager);
            this.Controls.Add(quickAccessManager);
            this.Controls.Add(salesProfitsControl);

            // Position the sub-components
            PositionComponents();
        }

        private void PositionComponents()
        {
            // Position bookings manager at top (took previous stats place, or just main place)
            if (bookingsManager != null)
            {
                bookingsManager.Location = new Point(25, 20); 
                bookingsManager.BringToFront();
            }

            // Position quick access manager on the right side
            if (quickAccessManager != null)
            {
                quickAccessManager.Location = new Point(650, 20);
                quickAccessManager.BringToFront();
            }

            if (salesProfitsControl != null)
            {
                salesProfitsControl.Dock = DockStyle.Fill;
                salesProfitsControl.Visible = false;
            }

            // Position last updated label at bottom
            UpdateLastUpdatedLabelPosition();
        }
        
        // ... (methods continuing)

        // Public methods for external control


        // ... existing code ...
        
        // Public methods for external control
        public void LoadSampleData()
        {
            bookingsManager?.LoadSampleData();
            quickAccessManager?.LoadSampleData();
            salesProfitsControl?.LoadData();

            ContentLoaded?.Invoke(this, EventArgs.Empty);
        }

        public void UpdateLastUpdatedTime()
        {
            string newTime = $"Updated {DateTime.Now:hh:mm tt}";
            if (lblLastUpdated != null)
            {
                lblLastUpdated.Text = newTime;
            }

            LastUpdatedTimeChanged?.Invoke(this, EventArgs.Empty);
        }

        public void RefreshAllContent()
        {
            LoadSampleData();
            UpdateLastUpdatedTime();
        }

        // Public methods to access sub-components
        public void AddBooking(string bookingId, string guestName, string room, string checkIn, string checkOut, string status)
        {
            bookingsManager?.AddBooking(bookingId, guestName, room, checkIn, checkOut, status);
        }

        public void ClearAllBookings()
        {
            bookingsManager?.ClearAllBookings();
        }

        // Method to get booking count (implementation needed)
        public int GetBookingCount()
        {
            // This would need to access the actual booking count from bookingsManager
            // For now, return a placeholder
            return bookingsManager != null ? 0 : 0;
        }

        // Properties for external access
        [Browsable(false)]
        public BookingManager BookingsManager => bookingsManager;

        [Browsable(false)]
        public QuickAccessManager QuickAccessManager => quickAccessManager;

        // ... existing properties ...
        
        // Hide inherited properties that don't make sense for this control
        // ...
        
        // Method to show/hide specific sections
        public void ShowStatsSection(bool show)
        {
            // Stats section removed, so this is a no-op
        }

        public void ShowBookingsSection(bool show)
        {
            if (bookingsManager != null)
            {
                bookingsManager.Visible = show;
            }
        }

        public void ShowQuickAccessSection(bool show)
        {
            if (quickAccessManager != null)
            {
                quickAccessManager.Visible = show;
            }
        }

        public void ShowSalesProfitsSection(bool show)
        {
            if (salesProfitsControl != null)
            {
                salesProfitsControl.Visible = show;
                if(show) salesProfitsControl.BringToFront();
            }
        }

        public void RefreshSalesData()
        {
            salesProfitsControl?.LoadData();
        }

        // Method to handle quick access button clicks
        public void SetQuickAccessButtonHandler(EventHandler<string> handler)
        {
            if (quickAccessManager != null)
            {
                quickAccessManager.QuickAccessButtonClicked += handler;
            }
        }

        // Method to set specific quick access button handlers
        public void SetQuickAccessButtonHandlers(
            EventHandler manageRoomsHandler = null,
            EventHandler manageGuestsHandler = null,
            EventHandler processPaymentsHandler = null,
            EventHandler roomCalendarHandler = null,
            EventHandler adjustPricingHandler = null,
            EventHandler manageStaffHandler = null)
        {
            if (quickAccessManager != null)
            {
                if (manageRoomsHandler != null)
                    quickAccessManager.ManageRoomsClicked += manageRoomsHandler;

                if (manageGuestsHandler != null)
                    quickAccessManager.ManageGuestsClicked += manageGuestsHandler;

                if (processPaymentsHandler != null)
                    quickAccessManager.ProcessPaymentsClicked += processPaymentsHandler;

                if (roomCalendarHandler != null)
                    quickAccessManager.RoomCalendarClicked += roomCalendarHandler;

                if (adjustPricingHandler != null)
                    quickAccessManager.AdjustPricingClicked += adjustPricingHandler;

                if (manageStaffHandler != null)
                    quickAccessManager.ManageStaffClicked += manageStaffHandler;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                bookingsManager?.Dispose();
                quickAccessManager?.Dispose();
                salesProfitsControl?.Dispose();
                lblLastUpdated?.Dispose();
            }
            base.Dispose(disposing);
        }
        private void CreateLastUpdatedLabel()
        {
            lblLastUpdated = new Label
            {
                Text = "Updated 5 minutes ago.",
                Size = new Size(200, 20),
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 8),
                Visible = false 
            };
            UpdateLastUpdatedLabelPosition();
            this.Controls.Add(lblLastUpdated);
        }

        private void UpdateLastUpdatedLabelPosition()
        {
            if (lblLastUpdated != null)
            {
                lblLastUpdated.Location = new Point(25, this.Height - 40);
            }
        }
    }
}
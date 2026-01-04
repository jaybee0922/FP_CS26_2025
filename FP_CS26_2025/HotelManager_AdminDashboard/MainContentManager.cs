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
        private StatsPanelManager statsManager;
        private BookingManager bookingsManager;
        private QuickAccessManager quickAccessManager;
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
            statsManager = new StatsPanelManager();
            bookingsManager = new BookingManager();
            quickAccessManager = new QuickAccessManager();

            // Add the sub-components to this panel
            this.Controls.Add(statsManager);
            this.Controls.Add(bookingsManager);
            this.Controls.Add(quickAccessManager);

            // Position the sub-components
            PositionComponents();
        }

        private void PositionComponents()
        {
            // Position stats manager at top
            if (statsManager != null)
            {
                statsManager.Location = new Point(25, 20);
                statsManager.BringToFront();
            }

            // Position bookings manager below stats
            if (bookingsManager != null)
            {
                bookingsManager.Location = new Point(25, 220); // Increased spacing
                bookingsManager.BringToFront();
            }

            // Position quick access manager on the right side
            if (quickAccessManager != null)
            {
                quickAccessManager.Location = new Point(650, 20);
                quickAccessManager.BringToFront();
            }

            // Position last updated label at bottom
            UpdateLastUpdatedLabelPosition();
        }

        private void CreateLastUpdatedLabel()
        {
            lblLastUpdated = new Label
            {
                Text = "Updated 5 minutes ago.",
                Size = new Size(200, 20),
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 8)
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

        // Public methods for external control
        public void LoadSampleData()
        {
            statsManager?.LoadSampleData();
            bookingsManager?.LoadSampleData();
            quickAccessManager?.LoadSampleData();

            ContentLoaded?.Invoke(this, EventArgs.Empty);
        }

        public void UpdateLastUpdatedTime()
        {
            string newTime = $"Updated {DateTime.Now:hh:mm tt}";
            if (lblLastUpdated != null)
            {
                lblLastUpdated.Text = newTime;
            }
            statsManager?.UpdateStats();

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
        public StatsPanelManager StatsManager => statsManager;

        [Browsable(false)]
        public BookingManager BookingsManager => bookingsManager;

        [Browsable(false)]
        public QuickAccessManager QuickAccessManager => quickAccessManager;

        [Category("Appearance")]
        [Description("Text of the last updated label")]
        public string LastUpdatedText
        {
            get => lblLastUpdated?.Text ?? string.Empty;
            set
            {
                if (lblLastUpdated != null)
                    lblLastUpdated.Text = value;
            }
        }

        [Category("Appearance")]
        [Description("Color of the last updated label")]
        public Color LastUpdatedColor
        {
            get => lblLastUpdated?.ForeColor ?? Color.Gray;
            set
            {
                if (lblLastUpdated != null)
                    lblLastUpdated.ForeColor = value;
            }
        }

        [Category("Behavior")]
        [Description("Gets the number of bookings currently displayed")]
        [Browsable(false)]
        public int BookingCount => GetBookingCount();

        // Hide inherited properties that don't make sense for this control
        [Browsable(false)]
        public new bool AutoSize => base.AutoSize;

        [Browsable(false)]
        public new AutoSizeMode AutoSizeMode => base.AutoSizeMode;

        [Browsable(false)]
        public override string Text => base.Text;

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateLastUpdatedLabelPosition();
            PositionComponents();
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            PositionComponents();
        }

        // Method to show/hide specific sections
        public void ShowStatsSection(bool show)
        {
            if (statsManager != null)
            {
                statsManager.Visible = show;
            }
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

        // Method to update stats with custom data
        public void UpdateStatistics(int totalGuests, int availableRooms, int occupancyRate, int revenue)
        {
            statsManager?.UpdateStatistics(totalGuests, availableRooms, occupancyRate, revenue);
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
                statsManager?.Dispose();
                bookingsManager?.Dispose();
                quickAccessManager?.Dispose();
                lblLastUpdated?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
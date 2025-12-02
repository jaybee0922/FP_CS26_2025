using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace FP_CS26_2025.HotelManager_AdminDashboard
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Panel))]
    public class HotelDashboardManager : Panel, IDisposable
    {
        private Panel panelSidebar, panelMain;
        private Button selectedNavButton;
        private Timer timerUpdate;

        private SidebarManager sidebarManager;
        private MainContentManager mainContentManager;
        private DataManager dataManager;

        // Events for external handling
        public event EventHandler DashboardLoaded;
        public event EventHandler NavigationChanged;

        public HotelDashboardManager()
        {
            InitializeComponent();
            InitializeManagers();

            // Wait a moment for components to initialize before loading data
            var initTimer = new Timer { Interval = 100 };
            initTimer.Tick += (s, e) =>
            {
                initTimer.Stop();
                initTimer.Dispose();
                LoadDashboardData();
                if (sidebarManager != null)
                {
                    SelectNavButton(sidebarManager.GetDashboardButton());
                }
            };
            initTimer.Start();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Set up the main container properties
            this.Size = new Size(1384, 761);
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9F);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Dock = DockStyle.Fill; // Make it fill its container

            this.ResumeLayout(false);
        }

        private void InitializeManagers()
        {
            // Create main panels first with proper docking
            panelSidebar = new Panel
            {
                BackColor = Color.FromArgb(51, 51, 76),
                Size = new Size(220, 0), // Height will be set by dock
                Dock = DockStyle.Left, // Dock to left side
                MinimumSize = new Size(200, 0) // Prevent getting too small
            };

            panelMain = new Panel
            {
                BackColor = Color.FromArgb(240, 240, 240),
                Dock = DockStyle.Fill, // Fill remaining space
                AutoScroll = true // Allow scrolling if content is too large
            };

            // Initialize managers with parameterless constructors
            sidebarManager = new SidebarManager();
            mainContentManager = new MainContentManager();
            dataManager = new DataManager();

            // Set proper sizes for the managers
            sidebarManager.Dock = DockStyle.Fill;
            mainContentManager.Dock = DockStyle.Fill;

            // Add the manager components to their respective panels
            panelSidebar.Controls.Add(sidebarManager);
            panelMain.Controls.Add(mainContentManager);

            // Add panels to this control
            this.Controls.Add(panelMain);
            this.Controls.Add(panelSidebar); // Add sidebar last so it appears on top

            // Setup navigation
            SetupNavigation();
            CreateUpdateTimer();
        }

        private void SetupNavigation()
        {
            if (sidebarManager != null)
            {
                sidebarManager.SetNavigationHandler(SelectNavButton);

                // Subscribe to specific navigation events
                sidebarManager.DashboardClicked += (s, e) => HandleDashboardNavigation();
                sidebarManager.UserManagementClicked += (s, e) => HandleUserManagementNavigation();
                sidebarManager.RoomRatesClicked += (s, e) => HandleRoomRatesNavigation();
                sidebarManager.ReportsClicked += (s, e) => HandleReportsNavigation();
                sidebarManager.SystemConfigClicked += (s, e) => HandleSystemConfigNavigation();
            }
        }

        private void SelectNavButton(Button button)
        {
            if (button == null) return;

            if (selectedNavButton != null)
            {
                selectedNavButton.BackColor = Color.FromArgb(51, 51, 76);
            }

            selectedNavButton = button;
            sidebarManager.SelectButton(button);

            var timer = new Timer { Interval = 50 };
            int count = 0;
            timer.Tick += (s, e) =>
            {
                count++;
                if (count <= 3)
                {
                    button.BackColor = count % 2 == 0 ? Color.FromArgb(70, 130, 180) : Color.FromArgb(90, 150, 200);
                }
                else
                {
                    timer.Stop();
                    timer.Dispose();
                    button.BackColor = Color.FromArgb(70, 130, 180);
                }
            };
            timer.Start();

            // Raise navigation changed event
            NavigationChanged?.Invoke(this, EventArgs.Empty);
        }

        // Navigation handlers
        private void HandleDashboardNavigation()
        {
            // Show dashboard content
            if (mainContentManager != null)
            {
                mainContentManager.ShowStatsSection(true);
                mainContentManager.ShowBookingsSection(true);
                mainContentManager.ShowQuickAccessSection(true);
            }
        }

        private void HandleUserManagementNavigation()
        {
            // Show user management content
            if (mainContentManager != null)
            {
                mainContentManager.ShowStatsSection(false);
                mainContentManager.ShowBookingsSection(false);
                mainContentManager.ShowQuickAccessSection(false);
                // You can add user management specific content here
            }
        }

        private void HandleRoomRatesNavigation()
        {
            // Show room rates content
            if (mainContentManager != null)
            {
                mainContentManager.ShowStatsSection(false);
                mainContentManager.ShowBookingsSection(false);
                mainContentManager.ShowQuickAccessSection(false);
                // You can add room rates specific content here
            }
        }

        private void HandleReportsNavigation()
        {
            // Show reports content
            if (mainContentManager != null)
            {
                mainContentManager.ShowStatsSection(false);
                mainContentManager.ShowBookingsSection(false);
                mainContentManager.ShowQuickAccessSection(false);
                // You can add reports specific content here
            }
        }

        private void HandleSystemConfigNavigation()
        {
            // Show system configuration content
            if (mainContentManager != null)
            {
                mainContentManager.ShowStatsSection(false);
                mainContentManager.ShowBookingsSection(false);
                mainContentManager.ShowQuickAccessSection(false);
                // You can add system config specific content here
            }
        }

        // Public methods for external control
        public void LoadDashboardData()
        {
            mainContentManager?.LoadSampleData();
            dataManager?.LoadSampleData();
            DashboardLoaded?.Invoke(this, EventArgs.Empty);
        }

        public void RefreshDashboard()
        {
            mainContentManager?.UpdateLastUpdatedTime();
            mainContentManager?.LoadSampleData();
        }

        public void NavigateToDashboard()
        {
            if (sidebarManager != null)
            {
                var dashboardButton = sidebarManager.GetDashboardButton();
                if (dashboardButton != null)
                {
                    SelectNavButton(dashboardButton);
                    dashboardButton.PerformClick();
                }
            }
        }

        public void NavigateToBookings()
        {
            // Since we don't have a specific bookings button, navigate to dashboard
            // You can add a bookings button to SidebarManager if needed
            NavigateToDashboard();
        }

        public void AddBooking(string bookingId, string guestName, string room, string checkIn, string checkOut, string status)
        {
            mainContentManager?.AddBooking(bookingId, guestName, room, checkIn, checkOut, status);
        }

        public void UpdateStatistics(int totalGuests, int availableRooms, int occupancyRate, int revenue)
        {
            mainContentManager?.UpdateStatistics(totalGuests, availableRooms, occupancyRate, revenue);
        }

        private void CreateUpdateTimer()
        {
            timerUpdate = new Timer { Interval = 300000 }; // 5 minutes
            timerUpdate.Tick += TimerUpdate_Tick;
            timerUpdate.Start();
        }

        private void TimerUpdate_Tick(object sender, EventArgs e)
        {
            mainContentManager?.UpdateLastUpdatedTime();
        }

        // Properties for external access
        [Browsable(false)]
        public SidebarManager Sidebar => sidebarManager;

        [Browsable(false)]
        public MainContentManager MainContent => mainContentManager;

        [Browsable(false)]
        public DataManager Data => dataManager;

        [Category("Appearance")]
        [Description("Background color of the sidebar")]
        public Color SidebarColor
        {
            get => panelSidebar?.BackColor ?? Color.FromArgb(51, 51, 76);
            set
            {
                if (panelSidebar != null)
                    panelSidebar.BackColor = value;
            }
        }

        [Category("Appearance")]
        [Description("Background color of the main content area")]
        public Color MainContentColor
        {
            get => panelMain?.BackColor ?? Color.FromArgb(240, 240, 240);
            set
            {
                if (panelMain != null)
                    panelMain.BackColor = value;
            }
        }

        [Category("Appearance")]
        [Description("Width of the sidebar")]
        public int SidebarWidth
        {
            get => panelSidebar?.Width ?? 220;
            set
            {
                if (panelSidebar != null && value >= 200) // Minimum width
                    panelSidebar.Width = value;
            }
        }

        [Category("Behavior")]
        [Description("Gets the currently selected navigation button")]
        [Browsable(false)]
        public Button SelectedNavigationButton => selectedNavButton;

        [Category("Behavior")]
        [Description("Auto refresh interval in milliseconds")]
        public int AutoRefreshInterval
        {
            get => timerUpdate?.Interval ?? 300000;
            set
            {
                if (timerUpdate != null)
                    timerUpdate.Interval = value;
            }
        }

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
            // The docking will automatically handle resizing
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            // Let the docking layout handle the arrangement
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                timerUpdate?.Stop();
                timerUpdate?.Dispose();
                sidebarManager?.Dispose();
                mainContentManager?.Dispose();
                dataManager?.Dispose();

                panelSidebar?.Dispose();
                panelMain?.Dispose();
                selectedNavButton?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HotelManagementSystem
{
    public partial class HotelDashboardForm : Form
    {
        private Panel panelSidebar, panelMain;
        private Button selectedNavButton;
        private Timer timerUpdate;

        private SidebarManager sidebarManager;
        private MainContentManager mainContentManager;
        private DataManager dataManager;

        public HotelDashboardForm()
        {
            InitializeComponent();
            InitializeManagers();
            LoadDashboardData();
            SelectNavButton(sidebarManager.GetDashboardButton());
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // HotelDashboardForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1384, 761);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "HotelDashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hotel Manager Dashboard";
            this.Load += new System.EventHandler(this.HotelDashboardForm_Load);
            this.ResumeLayout(false);

        }

        private void InitializeManagers()
        {
            // Create main panels first
            panelSidebar = new Panel
            {
                BackColor = Color.FromArgb(51, 51, 76),
                Location = new Point(0, 0),
                Size = new Size(220, 800)
            };

            panelMain = new Panel
            {
                BackColor = Color.FromArgb(240, 240, 240),
                Location = new Point(220, 0),
                Size = new Size(1180, 800)
            };

            // Initialize managers
            sidebarManager = new SidebarManager(panelSidebar);
            mainContentManager = new MainContentManager(panelMain);
            dataManager = new DataManager();

            // Add panels to form
            this.Controls.Add(panelSidebar);
            this.Controls.Add(panelMain);

            // Setup navigation
            SetupNavigation();
            CreateUpdateTimer();
        }

        private void SetupNavigation()
        {
            sidebarManager.SetNavigationHandler(SelectNavButton);
        }

        private void SelectNavButton(Button button)
        {
            if (selectedNavButton != null)
            {
                selectedNavButton.BackColor = Color.FromArgb(51, 51, 76);
            }

            button.BackColor = Color.FromArgb(70, 130, 180);
            selectedNavButton = button;

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
        }

        private void LoadDashboardData()
        {
            mainContentManager.LoadSampleData();
        }

        private void CreateUpdateTimer()
        {
            timerUpdate = new Timer { Interval = 300000 }; // 5 minutes
            timerUpdate.Tick += TimerUpdate_Tick;
            timerUpdate.Start();
        }

        private void TimerUpdate_Tick(object sender, EventArgs e)
        {
            mainContentManager.UpdateLastUpdatedTime();
        }

        private void HotelDashboardForm_Load(object sender, EventArgs e)
        {

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                timerUpdate?.Dispose();
                sidebarManager?.Dispose();
                mainContentManager?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
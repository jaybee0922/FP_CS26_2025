using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

namespace HotelManagementSystem
{
    public class MainContentManager : IDisposable
    {
        private Panel mainPanel;
        private StatsPanelManager statsManager;
        private BookingsManager bookingsManager;
        private QuickAccessManager quickAccessManager;
        private Label lblLastUpdated;

        public MainContentManager(Panel mainPanel)
        {
            this.mainPanel = mainPanel;
            InitializeMainContent();
        }

        private void InitializeMainContent()
        {
            CreateLastUpdatedLabel();
            statsManager = new StatsPanelManager(mainPanel);
            bookingsManager = new BookingsManager(mainPanel);
            quickAccessManager = new QuickAccessManager(mainPanel);
        }

        private void CreateLastUpdatedLabel()
        {
            lblLastUpdated = new Label
            {
                Text = "Updated 5 minutes ago.",
                Location = new Point(25, 720),
                Size = new Size(200, 20),
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 8)
            };
            mainPanel.Controls.Add(lblLastUpdated);
        }

        public void LoadSampleData()
        {
            bookingsManager.LoadSampleData();
        }

        public void UpdateLastUpdatedTime()
        {
            lblLastUpdated.Text = $"Updated {DateTime.Now:hh:mm tt}";
            statsManager.UpdateStats();
        }

        public void Dispose()
        {
            statsManager?.Dispose();
            bookingsManager?.Dispose();
            quickAccessManager?.Dispose();
        }
    }
}
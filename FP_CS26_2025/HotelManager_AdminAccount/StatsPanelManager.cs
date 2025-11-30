using System;
using System.Drawing;
using System.Windows.Forms;

namespace HotelManagementSystem
{
    public class StatsPanelManager : IDisposable
    {
        private Panel panelStats;
        private Label lblCurrentBookings, lblAvailableRooms, lblRevenue, lblOccupancy, lblTarget;
        private Random random;

        public StatsPanelManager(Panel mainPanel)
        {
            random = new Random();
            CreateWelcomeStatsPanel(mainPanel);
        }

        private void CreateWelcomeStatsPanel(Panel mainPanel)
        {
            var lblWelcome = new Label
            {
                Text = "Welcome, Hotel Manager",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(650, 20),
                Size = new Size(300, 30),
                ForeColor = Color.FromArgb(51, 51, 76)
            };
            mainPanel.Controls.Add(lblWelcome);

            panelStats = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(650, 60),
                Size = new Size(500, 180)
            };

            CreateStatsPanels();
            mainPanel.Controls.Add(panelStats);
        }

        private void CreateStatsPanels()
        {
            var currentBookingsPanel = CreateStatPanel("Current Bookings", "124", 20, 20);
            var availableRoomsPanel = CreateStatPanel("Available Rooms", "20", 180, 20);
            var revenuePanel = CreateStatPanel("Revenue (Today)", "Php 3,450", 340, 20);

            var infoPanel = new Panel
            {
                Location = new Point(20, 100),
                Size = new Size(460, 50),
                BackColor = Color.FromArgb(250, 250, 250)
            };

            lblOccupancy = new Label
            {
                Text = "Occupancy rate: 86%",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                Location = new Point(10, 5),
                Size = new Size(150, 20),
                ForeColor = Color.FromArgb(51, 51, 76)
            };

            var lblLastUpdatedInfo = new Label
            {
                Text = "Last updated 2 mins ago",
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                Location = new Point(10, 25),
                Size = new Size(150, 20),
                ForeColor = Color.Gray
            };

            lblTarget = new Label
            {
                Text = "Target: P4,000",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                Location = new Point(300, 15),
                Size = new Size(150, 20),
                ForeColor = Color.FromArgb(51, 51, 76)
            };

            infoPanel.Controls.AddRange(new Control[] { lblOccupancy, lblLastUpdatedInfo, lblTarget });
            panelStats.Controls.AddRange(new Control[] { currentBookingsPanel, availableRoomsPanel, revenuePanel, infoPanel });
        }

        private Panel CreateStatPanel(string title, string value, int x, int y)
        {
            var panel = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(140, 60),
                BackColor = Color.FromArgb(240, 245, 255)
            };

            var titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(5, 5),
                Size = new Size(130, 15),
                ForeColor = Color.FromArgb(51, 51, 76)
            };

            var valueLabel = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", title.Contains("Revenue") ? 12 : 16, FontStyle.Bold),
                Location = new Point(5, 25),
                Size = new Size(130, 25),
                ForeColor = Color.FromArgb(70, 130, 180)
            };

            // Store references to updateable labels
            if (title.Contains("Current Bookings")) lblCurrentBookings = valueLabel;
            else if (title.Contains("Available Rooms")) lblAvailableRooms = valueLabel;
            else if (title.Contains("Revenue")) lblRevenue = valueLabel;

            panel.Controls.Add(titleLabel);
            panel.Controls.Add(valueLabel);
            return panel;
        }

        public void UpdateStats()
        {
            if (lblAvailableRooms != null)
                lblAvailableRooms.Text = random.Next(15, 25).ToString();
            if (lblRevenue != null)
                lblRevenue.Text = $"Php {random.Next(3000, 4000):N0}";
        }

        public void Dispose()
        {
            // Clean up resources
        }
    }
}
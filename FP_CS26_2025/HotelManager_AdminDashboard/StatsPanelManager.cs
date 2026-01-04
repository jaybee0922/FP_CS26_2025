using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace FP_CS26_2025.HotelManager_AdminDashboard
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Panel))]
    public class StatsPanelManager : Panel, IDisposable
    {
        private Label lblWelcome;
        private Label lblCurrentBookings, lblAvailableRooms, lblRevenue, lblOccupancy, lblTarget, lblLastUpdatedInfo;
        private Panel panelCurrentBookings, panelAvailableRooms, panelRevenue, panelInfo;
        private Random random;

        // Events
        public event EventHandler StatsUpdated;
        public event EventHandler WelcomeLabelClicked;
        public event EventHandler<string> StatPanelClicked;

        public StatsPanelManager()
        {
            InitializeComponent();
            CreateWelcomeStatsPanel();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Set up the main stats panel properties
            this.Size = new Size(580, 240); // Increased width and height for better spacing
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Padding = new Padding(15); // Increased padding around the entire panel

            this.ResumeLayout(false);
        }

        private void CreateWelcomeStatsPanel()
        {
            random = new Random();

            lblWelcome = new Label
            {
                Text = "Welcome, Hotel Manager",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(0, 15),
                Size = new Size(this.Width, 35),
                ForeColor = Color.FromArgb(51, 51, 76),
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Add click event to welcome label
            lblWelcome.Click += (s, e) => WelcomeLabelClicked?.Invoke(this, EventArgs.Empty);
            lblWelcome.MouseEnter += (s, e) =>
            {
                lblWelcome.ForeColor = Color.FromArgb(70, 130, 180);
                lblWelcome.Font = new Font(lblWelcome.Font, FontStyle.Underline | FontStyle.Bold);
            };
            lblWelcome.MouseLeave += (s, e) =>
            {
                lblWelcome.ForeColor = Color.FromArgb(51, 51, 76);
                lblWelcome.Font = new Font(lblWelcome.Font, FontStyle.Bold);
            };

            this.Controls.Add(lblWelcome);
            CreateStatsPanels();
        }

        private TableLayoutPanel tableLayoutPanelStats;

        private void CreateStatsPanels()
        {
            tableLayoutPanelStats = new TableLayoutPanel
            {
                ColumnCount = 3,
                RowCount = 1,
                Dock = DockStyle.Fill,
                Padding = new Padding(15, 60, 15, 45), // Room for welcome title and info panel
                BackColor = Color.Transparent
            };

            tableLayoutPanelStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanelStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanelStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));

            // Position stats panels using docking inside table cells
            panelCurrentBookings = CreateStatPanel("Current Bookings", "124", 0, 0, 0, 0);
            panelAvailableRooms = CreateStatPanel("Available Rooms", "20", 0, 0, 0, 0);
            panelRevenue = CreateStatPanel("Revenue (Today)", "₱3,450", 0, 0, 0, 0);

            panelCurrentBookings.Dock = DockStyle.Fill;
            panelAvailableRooms.Dock = DockStyle.Fill;
            panelRevenue.Dock = DockStyle.Fill;

            tableLayoutPanelStats.Controls.Add(panelCurrentBookings, 0, 0);
            tableLayoutPanelStats.Controls.Add(panelAvailableRooms, 1, 0);
            tableLayoutPanelStats.Controls.Add(panelRevenue, 2, 0);

            // Info panel at the bottom
            panelInfo = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 35,
                BackColor = Color.FromArgb(245, 247, 250),
                BorderStyle = BorderStyle.None
            };

            lblOccupancy = new Label
            {
                Text = "📊 Occupancy: 86%",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                Location = new Point(15, 8),
                Size = new Size(130, 20),
                ForeColor = Color.FromArgb(51, 51, 76)
            };

            lblLastUpdatedInfo = new Label
            {
                Text = "🕒 Updated 2 mins ago",
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                Location = new Point(160, 9),
                Size = new Size(160, 20),
                ForeColor = Color.Gray
            };

            lblTarget = new Label
            {
                Text = "🎯 Target: ₱4,000",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                Location = new Point(330, 8),
                Size = new Size(130, 20),
                ForeColor = Color.FromArgb(51, 51, 76)
            };

            panelInfo.Controls.AddRange(new Control[] { lblOccupancy, lblLastUpdatedInfo, lblTarget });
            
            this.Controls.Add(tableLayoutPanelStats);
            this.Controls.Add(panelInfo);
        }

        private Panel CreateStatPanel(string title, string value, int x, int y, int width, int height)
        {
            var panel = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                BackColor = Color.FromArgb(240, 245, 255),
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand,
                Padding = new Padding(10) // Increased internal padding
            };

            var titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(8, 10),
                Size = new Size(width - 16, 18), // More width for text
                ForeColor = Color.FromArgb(51, 51, 76),
                TextAlign = ContentAlignment.MiddleLeft
            };

            var valueLabel = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", title.Contains("Revenue") ? 14 : 18, FontStyle.Bold),
                Location = new Point(8, 35),
                Size = new Size(width - 16, 35), // More height for value
                ForeColor = Color.FromArgb(70, 130, 180),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Add icons based on panel type
            string icon = "";
            if (title.Contains("Current Bookings")) icon = "📋 ";
            else if (title.Contains("Available Rooms")) icon = "🏨 ";
            else if (title.Contains("Revenue")) icon = "💰 ";

            titleLabel.Text = icon + title;

            // Enhanced hover effects
            panel.MouseEnter += (s, e) =>
            {
                panel.BackColor = Color.FromArgb(220, 235, 255);
                panel.BorderStyle = BorderStyle.Fixed3D;
                valueLabel.ForeColor = Color.FromArgb(30, 80, 120);

                // Add a subtle shadow effect using padding
                panel.Padding = new Padding(8, 8, 12, 12);
            };

            panel.MouseLeave += (s, e) =>
            {
                panel.BackColor = Color.FromArgb(240, 245, 255);
                panel.BorderStyle = BorderStyle.FixedSingle;
                valueLabel.ForeColor = Color.FromArgb(70, 130, 180);

                // Reset padding
                panel.Padding = new Padding(10);
            };

            panel.Click += (s, e) =>
            {
                StatPanelClicked?.Invoke(this, title);

                // Add click animation
                var originalColor = panel.BackColor;
                panel.BackColor = Color.FromArgb(200, 225, 255);

                var timer = new Timer { Interval = 100 };
                timer.Tick += (timerSender, timerE) =>
                {
                    panel.BackColor = originalColor;
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
            };

            // Store references to updateable labels
            if (title.Contains("Current Bookings")) lblCurrentBookings = valueLabel;
            else if (title.Contains("Available Rooms")) lblAvailableRooms = valueLabel;
            else if (title.Contains("Revenue")) lblRevenue = valueLabel;

            panel.Controls.Add(titleLabel);
            panel.Controls.Add(valueLabel);
            return panel;
        }

        // Public methods
        public void UpdateStats()
        {
            if (lblAvailableRooms != null)
                lblAvailableRooms.Text = random.Next(15, 25).ToString();
            if (lblRevenue != null)
                lblRevenue.Text = $"₱{random.Next(3000, 4000):N0}";
            if (lblLastUpdatedInfo != null)
                lblLastUpdatedInfo.Text = $"🕒 Updated {DateTime.Now:hh:mm tt}";

            StatsUpdated?.Invoke(this, EventArgs.Empty);
        }

        public void LoadSampleData()
        {
            // Set initial sample data
            SetCurrentBookings(124);
            SetAvailableRooms(20);
            SetRevenue(3450);
            SetOccupancyRate(86);
            SetTarget(4000);
        }

        public void SetCurrentBookings(int count)
        {
            if (lblCurrentBookings != null)
                lblCurrentBookings.Text = count.ToString();
        }

        public void SetAvailableRooms(int count)
        {
            if (lblAvailableRooms != null)
                lblAvailableRooms.Text = count.ToString();
        }

        public void SetRevenue(decimal amount)
        {
            if (lblRevenue != null)
                lblRevenue.Text = $"₱{amount:N0}";
        }

        public void SetOccupancyRate(int rate)
        {
            if (lblOccupancy != null)
                lblOccupancy.Text = $"📊 Occupancy: {rate}%";
        }

        public void SetTarget(decimal amount)
        {
            if (lblTarget != null)
                lblTarget.Text = $"🎯 Target: ₱{amount:N0}";
        }

        public void UpdateStatistics(int totalGuests, int availableRooms, int occupancyRate, int revenue)
        {
            SetCurrentBookings(totalGuests);
            SetAvailableRooms(availableRooms);
            SetOccupancyRate(occupancyRate);
            SetRevenue(revenue);
            UpdateStats();
        }

        // Properties for external access
        [Category("Appearance")]
        [Description("Welcome text displayed at the top")]
        public string WelcomeText
        {
            get => lblWelcome?.Text ?? "Welcome, Hotel Manager";
            set
            {
                if (lblWelcome != null)
                    lblWelcome.Text = value;
            }
        }

        [Category("Appearance")]
        [Description("Color of the welcome text")]
        public Color WelcomeTextColor
        {
            get => lblWelcome?.ForeColor ?? Color.FromArgb(51, 51, 76);
            set
            {
                if (lblWelcome != null)
                    lblWelcome.ForeColor = value;
            }
        }

        [Category("Appearance")]
        [Description("Font size of the stat values")]
        public int StatValueFontSize
        {
            get => 16; // Default size
            set
            {
                var labels = new[] { lblCurrentBookings, lblAvailableRooms, lblRevenue };
                foreach (var label in labels)
                {
                    if (label != null)
                    {
                        bool isRevenue = label == lblRevenue;
                        label.Font = new Font("Segoe UI", isRevenue ? value - 2 : value, FontStyle.Bold);
                    }
                }
            }
        }

        [Category("Data")]
        [Description("Current number of bookings")]
        public int CurrentBookings
        {
            get
            {
                if (lblCurrentBookings != null && int.TryParse(lblCurrentBookings.Text, out int result))
                    return result;
                return 0;
            }
            set => SetCurrentBookings(value);
        }

        [Category("Data")]
        [Description("Number of available rooms")]
        public int AvailableRooms
        {
            get
            {
                if (lblAvailableRooms != null && int.TryParse(lblAvailableRooms.Text, out int result))
                    return result;
                return 0;
            }
            set => SetAvailableRooms(value);
        }

        [Category("Data")]
        [Description("Today's revenue")]
        public decimal Revenue
        {
            get
            {
                if (lblRevenue != null)
                {
                    string revenueText = lblRevenue.Text.Replace("₱", "").Replace(",", "").Trim();
                    if (decimal.TryParse(revenueText, out decimal result))
                        return result;
                }
                return 0;
            }
            set => SetRevenue(value);
        }

        [Category("Data")]
        [Description("Current occupancy rate")]
        public int OccupancyRate
        {
            get
            {
                if (lblOccupancy != null)
                {
                    string rateText = lblOccupancy.Text.Replace("📊 Occupancy:", "").Replace("%", "").Trim();
                    if (int.TryParse(rateText, out int result))
                        return result;
                }
                return 0;
            }
            set => SetOccupancyRate(value);
        }

        [Category("Data")]
        [Description("Revenue target")]
        public decimal Target
        {
            get
            {
                if (lblTarget != null)
                {
                    string targetText = lblTarget.Text.Replace("🎯 Target: ₱", "").Replace(",", "").Trim();
                    if (decimal.TryParse(targetText, out decimal result))
                        return result;
                }
                return 0;
            }
            set => SetTarget(value);
        }

        [Category("Appearance")]
        [Description("Background color of stat panels")]
        public Color StatPanelBackColor
        {
            get => panelCurrentBookings?.BackColor ?? Color.FromArgb(240, 245, 255);
            set
            {
                var panels = new[] { panelCurrentBookings, panelAvailableRooms, panelRevenue };
                foreach (var panel in panels)
                {
                    if (panel != null)
                        panel.BackColor = value;
                }
            }
        }

        [Category("Appearance")]
        [Description("Text color of stat values")]
        public Color StatValueColor
        {
            get => lblCurrentBookings?.ForeColor ?? Color.FromArgb(70, 130, 180);
            set
            {
                var labels = new[] { lblCurrentBookings, lblAvailableRooms, lblRevenue };
                foreach (var label in labels)
                {
                    if (label != null)
                        label.ForeColor = value;
                }
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
            UpdateLayout();
        }

        private void UpdateLayout()
        {
            if (lblWelcome != null)
            {
                // Center the welcome text
                lblWelcome.Location = new Point(0, 15);
                lblWelcome.Size = new Size(this.Width, 35);

                // Reposition stat panels
                int panelWidth = 160;
                int horizontalSpacing = 25;
                int startX = (this.Width - (panelWidth * 3 + horizontalSpacing * 2)) / 2;
                int panelsTop = lblWelcome.Bottom + 25;

                if (panelCurrentBookings != null)
                    panelCurrentBookings.Location = new Point(startX, panelsTop);
                if (panelAvailableRooms != null)
                    panelAvailableRooms.Location = new Point(startX + panelWidth + horizontalSpacing, panelsTop);
                if (panelRevenue != null)
                    panelRevenue.Location = new Point(startX + (panelWidth + horizontalSpacing) * 2, panelsTop);

                // Reposition info panel
                if (panelInfo != null)
                {
                    panelInfo.Location = new Point(25, this.Height - 45);
                    panelInfo.Size = new Size(this.Width - 50, 35);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                lblWelcome?.Dispose();
                lblCurrentBookings?.Dispose();
                lblAvailableRooms?.Dispose();
                lblRevenue?.Dispose();
                lblOccupancy?.Dispose();
                lblTarget?.Dispose();
                lblLastUpdatedInfo?.Dispose();
                panelCurrentBookings?.Dispose();
                panelAvailableRooms?.Dispose();
                panelRevenue?.Dispose();
                panelInfo?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FP_CS26_2025.FrontDesk_MVC;
using FP_CS26_2025.ModernDesign;

namespace FP_CS26_2025
{
    public class CheckInPanel : BaseFrontDeskPanel
    {
        private ListBox lbReservations;
        private Button btnCheckIn;
        private TableLayoutPanel mainLayout;
        private ModernTextBox txtSearch;
        private ModernShadowPanel shadowPanel;

        public CheckInPanel() : base() { InitializeComponents(); }

        public CheckInPanel(FrontDeskController controller) : base(controller, "Check-In Processing")
        {
            InitializeComponents();
            RefreshData();
        }

        private void InitializeComponents()
        {
            this.SuspendLayout();

            mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4, 
                Padding = new Padding(20)
            };
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F)); 
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); 
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));

            // Search
            txtSearch = new ModernTextBox
            {
                PlaceholderText = "Search Reservation ID or Guest Name...",
                Size = new Size(300, 35),
                BorderColor = Color.LightGray,
                BorderFocusColor = Color.FromArgb(52, 152, 219),
                UnderlinedStyle = true,
                 Anchor = AnchorStyles.Left | AnchorStyles.Right
            };
            txtSearch.TextChange += (s, e) => PerformSearch(txtSearch.Text);

             // Wrapper for search
            Panel searchWrapper = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, 10) };
            searchWrapper.Controls.Add(txtSearch);
            txtSearch.Dock = DockStyle.Top;

            // Shadow List
            shadowPanel = new ModernShadowPanel
            {
                Dock = DockStyle.Fill,
                ShadowDepth = 4,
                ShadowColor = Color.FromArgb(200, 200, 200),
                BorderRadius = 10,
                Padding = new Padding(15)
            };

            lbReservations = new ListBox 
            { 
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 11f),
                ItemHeight = 30,
                BorderStyle = BorderStyle.None,
                BackColor = Color.White
            };
            shadowPanel.Controls.Add(lbReservations);

            btnCheckIn = new Button 
            { 
                Text = "Process Check-In", 
                Size = new Size(180, 40),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Right
            };
            btnCheckIn.FlatAppearance.BorderSize = 0;

            btnCheckIn.Click += (s, e) => {
                if (_controller == null) return;
                if (lbReservations.SelectedItem is Reservation res) {
                    _controller.CheckIn(res.ReservationId);
                    MessageBox.Show($"Checked in: {res.Guest.FullName} to Room {res.AssignedRoom.RoomNumber}");
                    RefreshData();
                }
            };

            // Button Panel
            var buttonPanel = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0) };
            buttonPanel.Controls.Add(btnCheckIn);
            btnCheckIn.Location = new Point(buttonPanel.Width - btnCheckIn.Width, 10);

            mainLayout.Padding = new Padding(20, 60, 20, 20); 
            mainLayout.Controls.Add(searchWrapper, 0, 1);
            mainLayout.Controls.Add(shadowPanel, 0, 2);
            mainLayout.Controls.Add(buttonPanel, 0, 3);

            this.Controls.Add(mainLayout);
            mainLayout.BringToFront();

            this.ResumeLayout(false);
        }

        public override void RefreshData() {
            lbReservations.DataSource = null;
             if (_controller == null) return;
            // Filter to show ONLY Approved reservations that are not checked in
            lbReservations.DataSource = _controller.GetActiveReservations()
                .Where(r => r.Status == "Approved" && !r.IsCheckedIn)
                .ToList();
            lbReservations.DisplayMember = "ReservationId"; 
        }

        public override void PerformSearch(string query)
        {
            if (_controller == null) return;
            if (string.IsNullOrWhiteSpace(query))
            {
                RefreshData();
                return;
            }

            // Filter to show ONLY Approved reservations that are not checked in
            var active = _controller.GetActiveReservations()
                .Where(r => r.Status == "Approved" && !r.IsCheckedIn);
            var filtered = active.Where(r => 
                r.Guest.FullName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 ||
                r.ReservationId.ToString().Contains(query)
            ).ToList();

            lbReservations.DataSource = null;
            lbReservations.DataSource = filtered;
            lbReservations.DisplayMember = "ReservationId";
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CheckInPanel
            // 
            this.Name = "CheckInPanel";
            this.Size = new System.Drawing.Size(1033, 666);
            this.ResumeLayout(false);

        }
    }
}

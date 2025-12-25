using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FP_CS26_2025.FrontDesk_MVC;
using FP_CS26_2025.ModernDesign;

namespace FP_CS26_2025
{
    public class ReservationPanel : BaseFrontDeskPanel
    {
        private DataGridView dgvReservations;
        private Button btnNewReservation;
        private TableLayoutPanel mainLayout;
        private ModernTextBox txtSearch;
        private ModernShadowPanel shadowPanel;

        // Constructor for Designer
        public ReservationPanel() : base() 
        {
            InitializeComponents(); 
        }

        public ReservationPanel(FrontDeskController controller) : base(controller, "Reservations")
        {
            InitializeComponents();
            if (_controller != null) RefreshData();
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

            // Modern Search Box
            txtSearch = new ModernTextBox
            {
                PlaceholderText = "Search by Guest Name...",
                Size = new Size(300, 35),
                BorderColor = Color.LightGray,
                BorderFocusColor = Color.FromArgb(41, 128, 185),
                UnderlinedStyle = true,
                Anchor = AnchorStyles.Left | AnchorStyles.Right
            };
            txtSearch.TextChange += (s, e) => PerformSearch(txtSearch.Text);

            // Wrapper for search to manage width
            Panel searchWrapper = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, 10) };
            searchWrapper.Controls.Add(txtSearch);
            txtSearch.Dock = DockStyle.Top;

            // Modern Shadow Panel Container for Grid
            shadowPanel = new ModernShadowPanel
            {
                Dock = DockStyle.Fill,
                ShadowDepth = 4,
                ShadowColor = Color.FromArgb(200, 200, 200),
                BorderRadius = 10,
                Padding = new Padding(15) 
            };

            // Grid
            dgvReservations = new DataGridView
            {
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false,
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                DefaultCellStyle = new DataGridViewCellStyle 
                { 
                    SelectionBackColor = Color.FromArgb(41, 128, 185),
                    SelectionForeColor = Color.White,
                    Font = new Font("Segoe UI", 9.5f),
                    Padding = new Padding(5)
                },
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(240, 240, 240),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                    SelectionBackColor = Color.FromArgb(240, 240, 240),
                    Padding = new Padding(5)
                },
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing,
                ColumnHeadersHeight = 40,
                EnableHeadersVisualStyles = false,
                RowTemplate = { Height = 35 }
            };
            shadowPanel.Controls.Add(dgvReservations);

            // Button
            btnNewReservation = new Button
            {
                Text = "New Reservation",
                Size = new Size(200, 40),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Right
            };
            btnNewReservation.FlatAppearance.BorderSize = 0;
            btnNewReservation.Click += (s, e) => CreateNewReservationFlow();

            // Layout Assembly
            var buttonPanel = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0) };
            buttonPanel.Controls.Add(btnNewReservation);
            btnNewReservation.Location = new Point(buttonPanel.Width - btnNewReservation.Width, 10);
            
            mainLayout.Controls.Add(searchWrapper, 0, 1);
            mainLayout.Controls.Add(shadowPanel, 0, 2);
            mainLayout.Controls.Add(buttonPanel, 0, 3);

            // Adjust base Title padding
            mainLayout.Padding = new Padding(20, 60, 20, 20); 

            this.Controls.Add(mainLayout);
            mainLayout.BringToFront();

            this.ResumeLayout(false);
        }

        private void CreateNewReservationFlow()
        {
             if (_controller == null) return;
            // Simplified "Wizard" logic for now - prompting via simple inputs or form
            // ideally this opens a Modern Dialog
            var guest = new Guest { FullName = "Walk-in Guest", Email = "walkin@hotel.com" };
            var availableRoom = _controller.GetAvailableRooms().FirstOrDefault();
            
            if (availableRoom != null) {
                DialogResult result = MessageBox.Show(
                    $"Create reservation for Standard Walk-In?\nRoom: {availableRoom.RoomNumber}\nPrice: {availableRoom.BasePrice:C}", 
                    "Quick Reservation", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _controller.CreateReservation(guest, availableRoom.RoomNumber, DateTime.Now, DateTime.Now.AddDays(1));
                    MessageBox.Show("Reservation Created Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshData();
                }
            } else {
                MessageBox.Show("No rooms available for immediate check-in.", "Occupancy Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public override void RefreshData()
        {
            if (_controller == null) return;
            var data = _controller.GetActiveReservations().Select(r => new {
                ID = r.ReservationId,
                Guest = r.Guest.FullName,
                Room = r.AssignedRoom.RoomNumber,
                Type = r.AssignedRoom.RoomType,
                Dates = $"{r.CheckInDate:MM/dd} - {r.CheckOutDate:MM/dd}",
                Status = r.IsCheckedIn ? "Checked In" : "Reserved"
            }).ToList();
            
            dgvReservations.DataSource = data;
        }

        public override void PerformSearch(string query)
        {
            if (_controller == null) return;
            if (string.IsNullOrWhiteSpace(query))
            {
                RefreshData();
                return;
            }

            var allData = _controller.GetActiveReservations();
            var filtered = allData.Where(r => 
                r.Guest.FullName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 ||
                r.ReservationId.ToString().Contains(query) ||
                r.AssignedRoom.RoomNumber.ToString().Contains(query)
            ).Select(r => new {
                ID = r.ReservationId,
                Guest = r.Guest.FullName,
                Room = r.AssignedRoom.RoomNumber,
                Type = r.AssignedRoom.RoomType,
                Dates = $"{r.CheckInDate:MM/dd} - {r.CheckOutDate:MM/dd}",
                Status = r.IsCheckedIn ? "Checked In" : "Reserved"
            }).ToList();

            dgvReservations.DataSource = filtered;
        }
    }
}

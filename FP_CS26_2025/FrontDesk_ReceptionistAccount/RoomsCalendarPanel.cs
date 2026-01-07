using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FP_CS26_2025.FrontDesk_MVC;
using FP_CS26_2025.ModernDesign;

namespace FP_CS26_2025
{
    public class RoomsCalendarPanel : BaseFrontDeskPanel
    {
        private DataGridView dgvRooms;
        private TableLayoutPanel mainLayout;
        private ModernTextBox txtSearch;
        private ModernShadowPanel shadowPanel;
        private Button btnSetAvailable;

        public RoomsCalendarPanel() : base() { InitializeComponents(); }

        public RoomsCalendarPanel(FrontDeskController controller) : base(controller, "Room Assignments & Status")
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

            // Search
            txtSearch = new ModernTextBox
            {
                PlaceholderText = "Search Room Number or Type...",
                Size = new Size(300, 35),
                BorderColor = Color.LightGray,
                BorderFocusColor = Color.FromArgb(41, 128, 185),
                UnderlinedStyle = true,
                Anchor = AnchorStyles.Left | AnchorStyles.Right
            };
            txtSearch.TextChange += (s, e) => PerformSearch(txtSearch.Text);

            // Wrapper for search
            Panel searchWrapper = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, 10) };
            searchWrapper.Controls.Add(txtSearch);
            txtSearch.Dock = DockStyle.Top;

            // Shadow Panel
            shadowPanel = new ModernShadowPanel
            {
                Dock = DockStyle.Fill,
                ShadowDepth = 4,
                ShadowColor = Color.FromArgb(200, 200, 200),
                BorderRadius = 10,
                Padding = new Padding(15)
            };

             dgvRooms = new DataGridView
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
                    SelectionBackColor = Color.FromArgb(0, 120, 215),
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
            shadowPanel.Controls.Add(dgvRooms);

            mainLayout.Padding = new Padding(20, 60, 20, 20); 
            mainLayout.Controls.Add(searchWrapper, 0, 1);
            mainLayout.Controls.Add(shadowPanel, 0, 2);

            // Button Panel
            FlowLayoutPanel buttonFlow = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(0, 10, 0, 0) };
            btnSetAvailable = new Button 
            { 
                Text = "Mark Selected as Available", 
                Size = new Size(200, 35), 
                BackColor = Color.FromArgb(46, 204, 113), 
                ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Enabled = false
            };
            btnSetAvailable.Click += BtnSetAvailable_Click;
            buttonFlow.Controls.Add(btnSetAvailable);
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            mainLayout.Controls.Add(buttonFlow, 0, 3);

            dgvRooms.SelectionChanged += (s, e) => {
                btnSetAvailable.Enabled = dgvRooms.SelectedRows.Count > 0;
            };

            this.Controls.Add(mainLayout);
            mainLayout.BringToFront();
            
            this.ResumeLayout(false);
        }

        public override void RefreshData()
        {
            if (_controller == null) return;
            var allRooms = _controller.GetAllRooms().ToList();
            
            // Calculate available rooms per type
            var availabilityCounts = allRooms
                .GroupBy(r => r.RoomType)
                .ToDictionary(g => g.Key ?? "Unknown", g => g.Count(r => r.Status == RoomStatus.Available));

            dgvRooms.DataSource = allRooms.Select(r => new {
                Number = r.RoomNumber,
                Type = $"{r.RoomType} (Available: {(availabilityCounts.ContainsKey(r.RoomType ?? "") ? availabilityCounts[r.RoomType ?? ""] : 0)})",
                Floor = GetFloorOrdinal(r.Floor),
                Price = r.BasePrice,
                Status = r.Status
            }).ToList();
        }

        public override void PerformSearch(string query)
        {
             if (_controller == null) return;
            if (string.IsNullOrWhiteSpace(query))
            {
                RefreshData();
                return;
            }

            var allRooms = _controller.GetAllRooms().ToList();
            
            // Calculate availability counts for the formatted display
            var availabilityCounts = allRooms
                .GroupBy(r => r.RoomType)
                .ToDictionary(g => g.Key ?? "Unknown", g => g.Count(r => r.Status == RoomStatus.Available));

            var filtered = allRooms.Where(r => 
                r.RoomNumber.ToString().Contains(query) ||
                (r.RoomType != null && r.RoomType.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0) ||
                r.Status.ToString().IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0
            ).Select(r => new {
                Number = r.RoomNumber,
                Type = $"{r.RoomType} (Available: {(availabilityCounts.ContainsKey(r.RoomType ?? "") ? availabilityCounts[r.RoomType ?? ""] : 0)})",
                Floor = GetFloorOrdinal(r.Floor),
                Price = r.BasePrice,
                Status = r.Status
            }).ToList();

            dgvRooms.DataSource = filtered;
        }

        private void BtnSetAvailable_Click(object sender, EventArgs e)
        {
            if (dgvRooms.SelectedRows.Count == 0) return;

            var row = dgvRooms.SelectedRows[0];
            int roomNum = (int)row.Cells["Number"].Value;
            string currentStatus = row.Cells["Status"].Value.ToString();

            if (currentStatus == "Available")
            {
                MessageBox.Show("Room is already available.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                _controller.UpdateRoomStatus(roomNum, RoomStatus.Available);
                MessageBox.Show($"Room {roomNum} is now marked as Available!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating room: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string GetFloorOrdinal(int floor)
        {
            if (floor <= 0) return floor.ToString();
            int lastDigit = floor % 10;
            int lastTwoDigits = floor % 100;
            string suffix = "th";
            if (lastTwoDigits < 11 || lastTwoDigits > 13)
            {
                switch (lastDigit)
                {
                    case 1: suffix = "st"; break;
                    case 2: suffix = "nd"; break;
                    case 3: suffix = "rd"; break;
                }
            }
            return $"{floor}{suffix} Floor";
        }
    }
}

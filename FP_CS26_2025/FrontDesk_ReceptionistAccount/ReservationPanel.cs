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
        private Button btnApproveReservation;
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
                ReadOnly = false,
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

            // Add Checkbox Column
            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn
            {
                Name = "colSelect",
                HeaderText = "Select",
                Width = 60,
                ReadOnly = false,
                DisplayIndex = 0,
                FlatStyle = FlatStyle.Flat
            };
            checkColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvReservations.Columns.Add(checkColumn);
            dgvReservations.CellContentClick += dgvReservations_CellContentClick;
            dgvReservations.DataBindingComplete += (s, e) => {
                // Ensure checkbox column stays at the beginning
                if (dgvReservations.Columns["colSelect"] != null)
                {
                    dgvReservations.Columns["colSelect"].DisplayIndex = 0;
                    dgvReservations.Columns["colSelect"].ReadOnly = false;
                }

                // Set all other columns as ReadOnly
                foreach (DataGridViewColumn col in dgvReservations.Columns)
                {
                    if (col.Name != "colSelect")
                        col.ReadOnly = true;
                }
            };

            shadowPanel.Controls.Add(dgvReservations);

            // Buttons
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

            btnApproveReservation = new Button
            {
                Text = "Approve Reservation",
                Size = new Size(200, 40),
                BackColor = Color.FromArgb(41, 128, 185),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Right
            };
            btnApproveReservation.FlatAppearance.BorderSize = 0;
            btnApproveReservation.Click += (s, e) => ApproveSelectedReservation();

            // Layout Assembly
            var buttonPanel = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0) };
            buttonPanel.Controls.Add(btnNewReservation);
            buttonPanel.Controls.Add(btnApproveReservation);
            btnNewReservation.Location = new Point(buttonPanel.Width - btnNewReservation.Width, 10);
            btnApproveReservation.Location = new Point(buttonPanel.Width - btnNewReservation.Width - btnApproveReservation.Width - 10, 10);
            
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
            try
            {
                // Filter to show ONLY Pending reservations
                var data = _controller.GetActiveReservations()
                    .Where(r => r.Status == "Pending")
                    .Select(r => new {
                        ID = r.ReservationId,
                        Guest = r.Guest.FullName,
                        Room = r.AssignedRoom.RoomNumber > 0 ? r.AssignedRoom.RoomNumber.ToString() : "N/A",
                        Type = r.RoomType ?? "N/A",
                        Dates = $"{r.CheckInDate:MM/dd} - {r.CheckOutDate:MM/dd}",
                        Status = r.Status,
                        Price = $"P{r.TotalPrice:N2}",
                        Adults = r.NumAdults,
                        Children = r.NumChildren,
                        Rooms = r.NumRooms
                    }).ToList();
                
                dgvReservations.DataSource = data;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("UI Error in RefreshData: " + ex.Message);
            }
        }

        public override void PerformSearch(string query)    
        {
            if (_controller == null) return;
            if (string.IsNullOrWhiteSpace(query))
            {
                RefreshData();
                return;
            }

            try
            {
                // Filter to show ONLY Pending reservations
                var allData = _controller.GetActiveReservations().Where(r => r.Status == "Pending");
                var filtered = allData.Where(r => 
                    r.Guest.FullName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    r.ReservationId.ToString().Contains(query) ||
                    (r.AssignedRoom != null && r.AssignedRoom.RoomNumber.ToString().Contains(query))
                ).Select(r => new {
                    ID = r.ReservationId,
                    Guest = r.Guest.FullName,
                    Room = r.AssignedRoom != null && r.AssignedRoom.RoomNumber > 0 ? r.AssignedRoom.RoomNumber.ToString() : "N/A",
                    Type = r.RoomType ?? "N/A",
                    Dates = $"{r.CheckInDate:MM/dd} - {r.CheckOutDate:MM/dd}",
                    Status = r.Status,
                    Price = $"P{r.TotalPrice:N2}",
                    Adults = r.NumAdults,
                    Children = r.NumChildren,
                    Rooms = r.NumRooms
                }).ToList();

                dgvReservations.DataSource = filtered;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("UI Error in PerformSearch: " + ex.Message);
            }
        }

        private void dgvReservations_CellContentClick(object sender, DataGridViewCellEventArgs e)   
        {
            if (e.RowIndex >= 0 && dgvReservations.Columns[e.ColumnIndex].Name == "colSelect")
            {
                // Toggle the checkbox
                bool isChecked = (dgvReservations.Rows[e.RowIndex].Cells["colSelect"].Value as bool?) ?? false;
                
                // Clear all other checkboxes (Radio button behavior for clarity)
                foreach (DataGridViewRow row in dgvReservations.Rows)
                {
                    row.Cells["colSelect"].Value = false;
                }
                
                dgvReservations.Rows[e.RowIndex].Cells["colSelect"].Value = !isChecked;
                
                // Force row selection to highlight it
                dgvReservations.ClearSelection();
                if (!isChecked) // If we just checked it
                {
                    dgvReservations.Rows[e.RowIndex].Selected = true;
                }
                
                dgvReservations.EndEdit();
            }
        }

        private void ApproveSelectedReservation()
        {
            if (_controller == null) return;
            
            DataGridViewRow selectedRow = null;
            
            // Find the row that is CHECKED
            foreach (DataGridViewRow row in dgvReservations.Rows)
            {
                if ((row.Cells["colSelect"].Value as bool?) == true)
                {
                    selectedRow = row;
                    break;
                }
            }

            if (selectedRow == null)
            {
                MessageBox.Show("Please check the box for the reservation you want to approve.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var reservationId = selectedRow.Cells["ID"].Value.ToString();
                var guestName = selectedRow.Cells["Guest"].Value.ToString();

                DialogResult result = MessageBox.Show(
                    $"Approve reservation for {guestName}?\nReservation ID: {reservationId}\n\nThis will move the reservation to Check-In Guests.",
                    "Approve Reservation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _controller.ApproveReservation(reservationId);
                    MessageBox.Show("Reservation approved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error approving reservation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

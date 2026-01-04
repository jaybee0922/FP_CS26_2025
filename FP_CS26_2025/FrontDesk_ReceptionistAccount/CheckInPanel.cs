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
        private DataGridView dgvReservations;
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
                    SelectionBackColor = Color.FromArgb(52, 152, 219),
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
                if (dgvReservations.Columns["colSelect"] != null)
                {
                    dgvReservations.Columns["colSelect"].DisplayIndex = 0;
                    dgvReservations.Columns["colSelect"].ReadOnly = false;
                }
                foreach (DataGridViewColumn col in dgvReservations.Columns)
                {
                    if (col.Name != "colSelect") col.ReadOnly = true;
                }
            };

            shadowPanel.Controls.Add(dgvReservations);

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
                
                DataGridViewRow selectedRow = null;
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
                    MessageBox.Show("Please check the box for the reservation you want to check in.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string reservationId = selectedRow.Cells["ReservationID"].Value.ToString();
                string guestName = selectedRow.Cells["GuestName"].Value.ToString();
                int roomNum = int.Parse(selectedRow.Cells["Room"].Value.ToString());

                try {
                    _controller.CheckIn(reservationId);
                    MessageBox.Show($"Checked in: {guestName} to Room {roomNum}", "Check-In Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshData();
                } catch (Exception ex) {
                    MessageBox.Show($"Error processing check-in: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (_controller == null) return;
            try {
                var data = _controller.GetActiveReservations()
                    .Where(r => r.Status == "Approved" && !r.IsCheckedIn)
                    .Select(r => new {
                        ReservationID = r.ReservationId,
                        GuestName = r.Guest.FullName,
                        Room = r.AssignedRoom.RoomNumber,
                        CheckIn = r.CheckInDate.ToString("MM/dd/yyyy"),
                        CheckOut = r.CheckOutDate.ToString("MM/dd/yyyy")
                    }).ToList();
                
                dgvReservations.DataSource = data;
            } catch (Exception ex) {
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

            try {
                var filtered = _controller.GetActiveReservations()
                    .Where(r => r.Status == "Approved" && !r.IsCheckedIn)
                    .Where(r => r.Guest.FullName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                r.ReservationId.Contains(query))
                    .Select(r => new {
                        ReservationID = r.ReservationId,
                        GuestName = r.Guest.FullName,
                        Room = r.AssignedRoom.RoomNumber,
                        CheckIn = r.CheckInDate.ToString("MM/dd/yyyy"),
                        CheckOut = r.CheckOutDate.ToString("MM/dd/yyyy")
                    }).ToList();

                dgvReservations.DataSource = filtered;
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine("UI Error in PerformSearch: " + ex.Message);
            }
        }

        private void dgvReservations_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvReservations.Columns[e.ColumnIndex].Name == "colSelect")
            {
                bool isChecked = (dgvReservations.Rows[e.RowIndex].Cells["colSelect"].Value as bool?) ?? false;
                
                foreach (DataGridViewRow row in dgvReservations.Rows)
                {
                    row.Cells["colSelect"].Value = false;
                }
                
                dgvReservations.Rows[e.RowIndex].Cells["colSelect"].Value = !isChecked;
                dgvReservations.ClearSelection();
                if (!isChecked) dgvReservations.Rows[e.RowIndex].Selected = true;
                
                dgvReservations.EndEdit();
            }
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

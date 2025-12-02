using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace FP_CS26_2025.HotelManager_AdminDashboard
{
    [ToolboxItem(true)]
    public class BookingManager : Panel, IDisposable
    {
        private DataGridView dataGridViewRecentActivities;
        private Button btnRemoveBooking;
        private Label lblRecentBookings;

        public BookingManager()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Set up the main panel properties
            this.Size = new Size(650, 420);
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;

            CreateRecentBookingsSection();

            this.ResumeLayout(false);
        }

        private void CreateRecentBookingsSection()
        {
            CreateRecentBookingsLabel();
            CreateRecentBookingsTable();
            CreateRemoveBookingButton();
        }

        private void CreateRecentBookingsLabel()
        {
            lblRecentBookings = new Label
            {
                Text = "Recent Booking and Activities",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(25, 20),
                Size = new Size(300, 25),
                ForeColor = Color.FromArgb(51, 51, 76)
            };
            this.Controls.Add(lblRecentBookings);
        }

        private void CreateRecentBookingsTable()
        {
            dataGridViewRecentActivities = new DataGridView
            {
                Location = new Point(25, 50),
                Size = new Size(600, 300),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                RowHeadersVisible = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowTemplate = { Height = 30 },
                ColumnHeadersHeight = 35,
                ScrollBars = ScrollBars.Vertical,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            dataGridViewRecentActivities.DefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 9),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(3),
                SelectionBackColor = Color.FromArgb(240, 245, 255),
                SelectionForeColor = Color.Black
            };

            SetupDataGridColumns();
            StyleDataGridHeaders();
            dataGridViewRecentActivities.CellFormatting += DataGridViewRecentActivities_CellFormatting;

            this.Controls.Add(dataGridViewRecentActivities);
        }

        private void SetupDataGridColumns()
        {
            dataGridViewRecentActivities.Columns.Add("BookingId", "BOOKING ID");
            dataGridViewRecentActivities.Columns.Add("GuestName", "GUEST NAME");
            dataGridViewRecentActivities.Columns.Add("Room", "ROOM");
            dataGridViewRecentActivities.Columns.Add("CheckIn", "CHECK-IN");
            dataGridViewRecentActivities.Columns.Add("CheckOut", "CHECK-OUT");
            dataGridViewRecentActivities.Columns.Add("Status", "STATUS");

            dataGridViewRecentActivities.Columns["BookingId"].Width = 90;
            dataGridViewRecentActivities.Columns["GuestName"].Width = 120;
            dataGridViewRecentActivities.Columns["Room"].Width = 60;
            dataGridViewRecentActivities.Columns["CheckIn"].Width = 90;
            dataGridViewRecentActivities.Columns["CheckOut"].Width = 90;
            dataGridViewRecentActivities.Columns["Status"].Width = 80;
        }

        private void StyleDataGridHeaders()
        {
            dataGridViewRecentActivities.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(37, 157, 244),
                ForeColor = Color.FromArgb(74, 85, 104),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(5)
            };
            dataGridViewRecentActivities.EnableHeadersVisualStyles = false;
        }

        private void DataGridViewRecentActivities_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridViewRecentActivities.Rows.Count) return;

            if (e.ColumnIndex == 5 && e.Value != null) // Status column
            {
                string status = e.Value.ToString().ToLower();
                e.CellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.CellStyle.Padding = new Padding(2);

                if (status.Contains("checked in"))
                {
                    e.CellStyle.BackColor = Color.FromArgb(34, 197, 94);
                    e.CellStyle.ForeColor = Color.Black;
                }
                else if (status.Contains("checked out"))
                {
                    e.CellStyle.BackColor = Color.FromArgb(156, 163, 175);
                    e.CellStyle.ForeColor = Color.Black;
                }
                else if (status.Contains("pending"))
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 244, 79);
                    e.CellStyle.ForeColor = Color.Black;
                }
                else
                {
                    e.CellStyle.BackColor = Color.LightGray;
                    e.CellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void CreateRemoveBookingButton()
        {
            btnRemoveBooking = new Button
            {
                Text = "Remove Selected Booking",
                Location = new Point(25, 360),
                Size = new Size(180, 35),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat
            };

            btnRemoveBooking.FlatAppearance.BorderSize = 0;
            btnRemoveBooking.Click += RemoveSelectedBooking_Click;
            this.Controls.Add(btnRemoveBooking);
        }

        private void RemoveSelectedBooking_Click(object sender, EventArgs e)
        {
            if (dataGridViewRecentActivities.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewRecentActivities.SelectedRows[0];
                string guestName = selectedRow.Cells["GuestName"].Value?.ToString() ?? "Unknown Guest";
                string bookingId = selectedRow.Cells["BookingId"].Value?.ToString() ?? "Unknown ID";

                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to remove booking {bookingId} for {guestName}?",
                    "Confirm Removal",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    dataGridViewRecentActivities.Rows.Remove(selectedRow);
                    MessageBox.Show($"Booking {bookingId} removed successfully!", "Success",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a booking to remove.", "No Selection",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        [Browsable(false)]
        public new bool AutoSize => base.AutoSize;

        [Browsable(false)]
        public new AutoSizeMode AutoSizeMode => base.AutoSizeMode;

        // Public method to load sample data
        public void LoadSampleData()
        {
            // Clear existing data
            dataGridViewRecentActivities.Rows.Clear();

            var sampleData = new[]
            {
                new { BookingId = "BKG001", GuestName = "John Doe", Room = "101", CheckIn = "2023-10-26", CheckOut = "2023-10-29", Status = "Checked In" },
                new { BookingId = "BKG002", GuestName = "Jane Smith", Room = "205", CheckIn = "2023-10-27", CheckOut = "2023-10-30", Status = "Checked In" },
                new { BookingId = "BKG003", GuestName = "Alice Johnson", Room = "312", CheckIn = "2023-10-27", CheckOut = "2023-10-28", Status = "Pending" },
                new { BookingId = "BKG004", GuestName = "Bob Brown", Room = "403", CheckIn = "2023-10-26", CheckOut = "2023-10-27", Status = "Checked Out" },
                new { BookingId = "BKG005", GuestName = "Charlie Davis", Room = "108", CheckIn = "2023-10-28", CheckOut = "2023-11-01", Status = "Checked In" },
                new { BookingId = "BKG006", GuestName = "Diana Wilson", Room = "209", CheckIn = "2023-10-29", CheckOut = "2023-11-02", Status = "Checked In" },
                new { BookingId = "BKG007", GuestName = "Edward Miller", Room = "315", CheckIn = "2023-10-28", CheckOut = "2023-10-30", Status = "Checked In" },
                new { BookingId = "BKG008", GuestName = "Fiona Garcia", Room = "107", CheckIn = "2023-10-30", CheckOut = "2023-11-03", Status = "Pending" },
                new { BookingId = "BKG009", GuestName = "George Martinez", Room = "211", CheckIn = "2023-10-27", CheckOut = "2023-10-31", Status = "Checked In" }
            };

            foreach (var data in sampleData)
            {
                dataGridViewRecentActivities.Rows.Add(
                    data.BookingId,
                    data.GuestName,
                    data.Room,
                    data.CheckIn,
                    data.CheckOut,
                    data.Status
                );
            }
        }

        // Public method to add a new booking
        public void AddBooking(string bookingId, string guestName, string room, string checkIn, string checkOut, string status)
        {
            dataGridViewRecentActivities.Rows.Add(bookingId, guestName, room, checkIn, checkOut, status);
        }

        // Public method to clear all bookings
        public void ClearAllBookings()
        {
            dataGridViewRecentActivities.Rows.Clear();
        }

        // Property to get selected booking info
        [Browsable(false)]
        public string SelectedBookingId
        {
            get
            {
                if (dataGridViewRecentActivities.SelectedRows.Count > 0)
                    return dataGridViewRecentActivities.SelectedRows[0].Cells["BookingId"].Value?.ToString();
                return null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dataGridViewRecentActivities?.Dispose();
                btnRemoveBooking?.Dispose();
                lblRecentBookings?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
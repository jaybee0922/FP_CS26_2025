using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

namespace HotelManagementSystem
{
    public class BookingsManager : IDisposable
    {
        private DataGridView dataGridViewRecentActivities;
        private Button btnRemoveBooking;

        public BookingsManager(Panel mainPanel)
        {
            CreateRecentBookingsSection(mainPanel);
        }

        private void CreateRecentBookingsSection(Panel mainPanel)
        {
            CreateRecentBookingsLabel(mainPanel);
            CreateRecentBookingsTable(mainPanel);
            CreateRemoveBookingButton(mainPanel);
        }

        private void CreateRecentBookingsLabel(Panel mainPanel)
        {
            var lblRecentBookings = new Label
            {
                Text = "Recent Bookings & Activities",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(25, 20),
                Size = new Size(300, 25),
                ForeColor = Color.FromArgb(51, 51, 76)
            };
            mainPanel.Controls.Add(lblRecentBookings);
        }

        private void CreateRecentBookingsTable(Panel mainPanel)
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

            mainPanel.Controls.Add(dataGridViewRecentActivities);
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

        private void CreateRemoveBookingButton(Panel mainPanel)
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
            mainPanel.Controls.Add(btnRemoveBooking);
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

        public void LoadSampleData()
        {
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

        public void Dispose()
        {
            dataGridViewRecentActivities?.Dispose();
            btnRemoveBooking?.Dispose();
        }
    }
}
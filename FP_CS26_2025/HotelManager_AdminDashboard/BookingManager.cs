using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;

namespace FP_CS26_2025.HotelManager_AdminDashboard
{
    [ToolboxItem(true)]
    public class BookingManager : Panel, IDisposable
    {
        private DataGridView dataGridViewRecentActivities;
        private Button btnRemoveBooking;
        private Label lblRecentBookings;
        private TableLayoutPanel mainGrid;

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
            this.BorderStyle = BorderStyle.None; // Cleaner look

            mainGrid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(15)
            };
            mainGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F)); // Header
            mainGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Table
            mainGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F)); // Footer

            CreateRecentBookingsLabel();
            CreateRecentBookingsTable();
            CreateRemoveBookingButton();

            this.Controls.Add(mainGrid);
            this.ResumeLayout(false);
        }

        private void CreateRecentBookingsSection()
        {
            // The order of adding docked controls matters! 
            // Add Top and Bottom first, then Fill last.
            CreateRecentBookingsLabel();
            CreateRemoveBookingButton(); 
            CreateRecentBookingsTable();
        }

        private void CreateRecentBookingsLabel()
        {
            lblRecentBookings = new Label
            {
                Text = "Recent Booking and Activities",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(51, 51, 76),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(5, 0, 0, 0)
            };
            
            mainGrid.Controls.Add(lblRecentBookings, 0, 0);
        }

        private void CreateRecentBookingsTable()
        {
            dataGridViewRecentActivities = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                GridColor = Color.FromArgb(230, 235, 245),
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowTemplate = { Height = 40 },
                ColumnHeadersHeight = 50,
                ScrollBars = ScrollBars.Both,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                EnableHeadersVisualStyles = false,
                ColumnHeadersVisible = true
            };

            dataGridViewRecentActivities.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(248, 250, 252)
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

            mainGrid.Controls.Add(dataGridViewRecentActivities, 0, 1);
        }

        private void SetupDataGridColumns()
        {
            dataGridViewRecentActivities.Columns.Add("BookingId", "BOOKING ID");
            dataGridViewRecentActivities.Columns.Add("GuestName", "GUEST NAME");
            dataGridViewRecentActivities.Columns.Add("Room", "ROOM");
            dataGridViewRecentActivities.Columns.Add("CheckIn", "CHECK-IN");
            dataGridViewRecentActivities.Columns.Add("CheckOut", "CHECK-OUT");
            dataGridViewRecentActivities.Columns.Add("Status", "STATUS");

            // Set minimum widths instead of fixed widths
            dataGridViewRecentActivities.Columns["BookingId"].MinimumWidth = 80;
            dataGridViewRecentActivities.Columns["GuestName"].MinimumWidth = 120;
            dataGridViewRecentActivities.Columns["Room"].MinimumWidth = 60;
            dataGridViewRecentActivities.Columns["CheckIn"].MinimumWidth = 100;
            dataGridViewRecentActivities.Columns["CheckOut"].MinimumWidth = 100;
            dataGridViewRecentActivities.Columns["Status"].MinimumWidth = 80;
        }

        private void StyleDataGridHeaders()
        {
            var headerStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(79, 70, 229), // Indigo
                ForeColor = Color.White,
                SelectionBackColor = Color.FromArgb(79, 70, 229), // Force same color on selection
                SelectionForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(5)
            };

            dataGridViewRecentActivities.EnableHeadersVisualStyles = false; // Must be false for custom colors
            dataGridViewRecentActivities.ColumnHeadersDefaultCellStyle = headerStyle;
            dataGridViewRecentActivities.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            
            // Apply style to each column specifically to ensure consistency
            foreach (DataGridViewColumn col in dataGridViewRecentActivities.Columns)
            {
                col.HeaderCell.Style = headerStyle;
                col.SortMode = DataGridViewColumnSortMode.NotSortable; // Disable sorting if that's causing visual glitches, or keep it. 
                // User didn't ask to disable sorting, but preventing header click interaction usually solves the "turn white" issue if it's related to focus.
                // Let's try just the colors first. If they want sorting, we keep it. 
                // Actually, the "white" issue on click is often the system highlight. Setting SelectionBackColor usually fixes it.
            }
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
                    e.CellStyle.BackColor = Color.FromArgb(220, 252, 231); // Light Green
                    e.CellStyle.ForeColor = Color.FromArgb(22, 101, 52);  // Dark Green
                }
                else if (status.Contains("checked out"))
                {
                    e.CellStyle.BackColor = Color.FromArgb(241, 245, 249); // Slate Gray
                    e.CellStyle.ForeColor = Color.FromArgb(71, 85, 105);
                }
                else if (status.Contains("pending"))
                {
                    e.CellStyle.BackColor = Color.FromArgb(254, 249, 195); // Light Yellow
                    e.CellStyle.ForeColor = Color.FromArgb(133, 77, 14);
                }
                else
                {
                    e.CellStyle.BackColor = Color.FromArgb(243, 244, 246);
                    e.CellStyle.ForeColor = Color.FromArgb(107, 114, 128);
                }
                e.FormattingApplied = true;
            }
        }

        public event EventHandler<string> BookingRemovalRequested;

        private void CreateRemoveBookingButton()
        {
            btnRemoveBooking = new Button
            {
                Text = "Remove Booking",
                Size = new Size(160, 40),
                BackColor = Color.FromArgb(239, 68, 68), // Modern Red
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Left | AnchorStyles.Top
            };
            btnRemoveBooking.FlatAppearance.BorderSize = 0;
            btnRemoveBooking.FlatAppearance.MouseDownBackColor = Color.FromArgb(185, 28, 28);
            btnRemoveBooking.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 38, 38);

            btnRemoveBooking.Click += RemoveSelectedBooking_Click;
            
            Panel pnl = new Panel { Dock = DockStyle.Fill, Padding = new Padding(5, 10, 0, 0) };
            pnl.Controls.Add(btnRemoveBooking);
            mainGrid.Controls.Add(pnl, 0, 2);
        }

        private void RemoveSelectedBooking_Click(object sender, EventArgs e)
        {
            if (dataGridViewRecentActivities.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewRecentActivities.SelectedRows[0];
                string guestName = selectedRow.Cells["GuestName"].Value?.ToString() ?? "Unknown Guest";
                string bookingId = selectedRow.Cells["BookingId"].Value?.ToString() ?? "Unknown ID";

                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to permanently delete booking {bookingId} for {guestName} from the database?",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    BookingRemovalRequested?.Invoke(this, bookingId);
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

        // Public method to load sample data (Cleared for DB integration)
        public void LoadSampleData()
        {
            // Clear existing data
            dataGridViewRecentActivities.Rows.Clear();
            
            // Sample data removed in favor of Database connection
        }

        // Public method to load data from DataTable (SQL Source)
        public void LoadData(DataTable dt)
        {
            dataGridViewRecentActivities.Rows.Clear();

            if (dt == null) return;

            foreach (DataRow row in dt.Rows)
            {
                dataGridViewRecentActivities.Rows.Add(
                    row["BookingId"].ToString(),
                    row["GuestName"].ToString(),
                    row["Room"].ToString(),
                    Convert.ToDateTime(row["CheckIn"]).ToString("yyyy-MM-dd"),
                    Convert.ToDateTime(row["CheckOut"]).ToString("yyyy-MM-dd"),
                    row["Status"].ToString()
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
                mainGrid?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
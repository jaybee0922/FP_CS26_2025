using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FP_CS26_2025.FrontDesk_MVC;

namespace FP_CS26_2025
{
    public class BaseFrontDeskPanel : Panel
    {
        protected readonly FrontDeskController _controller;
        private Label lblTitle;

        public BaseFrontDeskPanel(FrontDeskController controller, string title)
        {
            _controller = controller;
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(245, 247, 251);

            lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185),
                Location = new Point(30, 20),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);
        }
    }

    public class ReservationPanel : BaseFrontDeskPanel
    {
        private DataGridView dgvReservations;
        private Button btnNewReservation;

        public ReservationPanel(FrontDeskController controller) : base(controller, "Reservations")
        {
            InitializeComponents();
            RefreshData();
        }

        private void InitializeComponents()
        {
            dgvReservations = new DataGridView
            {
                Location = new Point(30, 80),
                Size = new Size(700, 400),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true
            };
            this.Controls.Add(dgvReservations);

            btnNewReservation = new Button
            {
                Text = "Create Reservation",
                Location = new Point(30, 500),
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnNewReservation.Click += (s, e) => {
                var guest = new Guest { FullName = "Sample Guest", Email = "guest@example.com" };
                var availableRoom = _controller.GetAvailableRooms().FirstOrDefault();
                if (availableRoom != null) {
                    _controller.CreateReservation(guest, availableRoom.RoomNumber, DateTime.Now, DateTime.Now.AddDays(2));
                    MessageBox.Show($"Reservation created for {guest.FullName} in Room {availableRoom.RoomNumber}");
                    RefreshData();
                } else {
                    MessageBox.Show("No rooms available.");
                }
            };
            this.Controls.Add(btnNewReservation);
        }

        private void RefreshData()
        {
            dgvReservations.DataSource = _controller.GetActiveReservations().Select(r => new {
                ID = r.ReservationId,
                Guest = r.Guest.FullName,
                Room = r.AssignedRoom.RoomNumber,
                Type = r.AssignedRoom.RoomType,
                CheckIn = r.CheckInDate.ToShortDateString(),
                CheckOut = r.CheckOutDate.ToShortDateString(),
                CheckedIn = r.IsCheckedIn ? "Yes" : "No"
            }).ToList();
        }
    }

    public class CheckInPanel : BaseFrontDeskPanel
    {
        private ListBox lbReservations;
        private Button btnCheckIn;

        public CheckInPanel(FrontDeskController controller) : base(controller, "Check-In Processing")
        {
            lbReservations = new ListBox { Location = new Point(30, 80), Size = new Size(300, 300) };
            btnCheckIn = new Button { 
                Text = "Check In Selected", 
                Location = new Point(30, 400), 
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            
            btnCheckIn.Click += (s, e) => {
                if (lbReservations.SelectedItem is Reservation res) {
                    _controller.CheckIn(res.ReservationId);
                    MessageBox.Show($"Checked in: {res.Guest.FullName}");
                    RefreshList();
                }
            };

            this.Controls.Add(lbReservations);
            this.Controls.Add(btnCheckIn);
            RefreshList();
        }

        private void RefreshList()
        {
            lbReservations.DataSource = null;
            lbReservations.DataSource = _controller.GetActiveReservations().Where(r => !r.IsCheckedIn).ToList();
            lbReservations.DisplayMember = "ReservationId";
        }
    }

    public class CheckOutPanel : BaseFrontDeskPanel
    {
        public CheckOutPanel(FrontDeskController controller) : base(controller, "Check-Out Processing")
        {
            var lbCheckedIn = new ListBox { Location = new Point(30, 80), Size = new Size(300, 300) };
            var btnCheckOut = new Button { 
                Text = "Process Check-Out", 
                Location = new Point(30, 400), 
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            void Refresh() {
                lbCheckedIn.DataSource = null;
                lbCheckedIn.DataSource = _controller.GetActiveReservations().Where(r => r.IsCheckedIn).ToList();
                lbCheckedIn.DisplayMember = "ReservationId";
            }

            btnCheckOut.Click += (s, e) => {
                if (lbCheckedIn.SelectedItem is Reservation res) {
                    var bill = _controller.CheckOut(res.ReservationId);
                    MessageBox.Show($"Check-out complete.\nTotal Bill: ${bill.TotalAmount}\nRoom {res.AssignedRoom.RoomNumber} is now Available.");
                    Refresh();
                }
            };

            this.Controls.Add(lbCheckedIn);
            this.Controls.Add(btnCheckOut);
            Refresh();
        }
    }

    public class RoomsCalendarPanel : BaseFrontDeskPanel
    {
        public RoomsCalendarPanel(FrontDeskController controller) : base(controller, "Room Status")
        {
            var dgvRooms = new DataGridView
            {
                Location = new Point(30, 80),
                Size = new Size(600, 400),
                BackgroundColor = Color.White,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                DataSource = _controller.GetAllRooms().Select(r => new {
                    Room = r.RoomNumber,
                    Type = r.RoomType,
                    Price = r.BasePrice,
                    Status = r.Status.ToString()
                }).ToList()
            };
            this.Controls.Add(dgvRooms);
        }
    }

    public class GuestListPanel : BaseFrontDeskPanel
    {
        public GuestListPanel(FrontDeskController controller) : base(controller, "Guest List")
        {
            var lblPlaceholder = new Label 
            { 
                Text = "Manage guest information and history.", 
                Location = new Point(30, 80), 
                AutoSize = true,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 12)
            };
            this.Controls.Add(lblPlaceholder);
        }
    }

    public class BillingPanel : BaseFrontDeskPanel
    {
        public BillingPanel(FrontDeskController controller) : base(controller, "Billing History") { }
    }
}

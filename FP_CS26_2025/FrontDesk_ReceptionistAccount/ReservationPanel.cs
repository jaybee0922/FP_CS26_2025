using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FP_CS26_2025.FrontDesk_MVC;

namespace FP_CS26_2025
{
    public class ReservationPanel : BaseFrontDeskPanel
    {
        private DataGridView dgvReservations;
        private Button btnNewReservation;

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
                if (_controller == null) return;
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
            if (_controller == null) return;
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
}

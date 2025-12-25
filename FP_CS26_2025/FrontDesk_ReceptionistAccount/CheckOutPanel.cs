using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FP_CS26_2025.FrontDesk_MVC;

namespace FP_CS26_2025
{
    public class CheckOutPanel : BaseFrontDeskPanel
    {
        private ListBox lbCheckedIn;
        private Button btnCheckOut;

        public CheckOutPanel() : base() { InitializeComponents(); }

        public CheckOutPanel(FrontDeskController controller) : base(controller, "Check-Out Processing")
        {
            InitializeComponents();
            Refresh();
        }

        private void InitializeComponents()
        {
            lbCheckedIn = new ListBox { Location = new Point(30, 80), Size = new Size(300, 300) };
            btnCheckOut = new Button { 
                Text = "Process Check-Out", 
                Location = new Point(30, 400), 
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            btnCheckOut.Click += (s, e) => {
                if (_controller == null) return;
                if (lbCheckedIn.SelectedItem is Reservation res) {
                    var bill = _controller.CheckOut(res.ReservationId);
                    MessageBox.Show($"Check-out complete.\nTotal Bill: ${bill.TotalAmount}\nRoom {res.AssignedRoom.RoomNumber} is now Available.");
                    Refresh();
                }
            };

            this.Controls.Add(lbCheckedIn);
            this.Controls.Add(btnCheckOut);
        }

        private void Refresh() {
            if (_controller == null) return;
            lbCheckedIn.DataSource = null;
            lbCheckedIn.DataSource = _controller.GetActiveReservations().Where(r => r.IsCheckedIn).ToList();
            lbCheckedIn.DisplayMember = "ReservationId";
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CheckOutPanel
            // 
            this.Name = "CheckOutPanel";
            this.Size = new System.Drawing.Size(1033, 666);
            this.ResumeLayout(false);

        }
    }
}

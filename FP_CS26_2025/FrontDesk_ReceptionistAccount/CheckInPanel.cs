using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FP_CS26_2025.FrontDesk_MVC;

namespace FP_CS26_2025
{
    public class CheckInPanel : BaseFrontDeskPanel
    {
        private ListBox lbReservations;
        private Button btnCheckIn;

        public CheckInPanel() : base() { InitializeComponents(); }

        public CheckInPanel(FrontDeskController controller) : base(controller, "Check-In Processing")
        {
            InitializeComponents();
            RefreshList();
        }

        private void InitializeComponents()
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
                if (_controller == null) return;
                if (lbReservations.SelectedItem is Reservation res) {
                    _controller.CheckIn(res.ReservationId);
                    MessageBox.Show($"Checked in: {res.Guest.FullName}");
                    RefreshList();
                }
            };

            this.Controls.Add(lbReservations);
            this.Controls.Add(btnCheckIn);
        }

        private void RefreshList()
        {
            if (_controller == null) return;
            lbReservations.DataSource = null;
            lbReservations.DataSource = _controller.GetActiveReservations().Where(r => !r.IsCheckedIn).ToList();
            lbReservations.DisplayMember = "ReservationId";
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

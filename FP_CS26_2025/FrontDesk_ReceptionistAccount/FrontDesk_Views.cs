using System;
using System.Drawing;
using System.Windows.Forms;

namespace FP_CS26_2025.FrontDesk_ReceptionistAccount
{
    public class PlaceholderPanel : Panel
    {
        private Label lblTitle;
        private Label lblDescription;

        public PlaceholderPanel(string title, string description)
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 128, 128),
                Location = new Point(50, 50),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            lblDescription = new Label
            {
                Text = description,
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.Gray,
                Location = new Point(50, 110),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblDescription);
        }
    }

    public class CheckInPanel : PlaceholderPanel
    {
        public CheckInPanel() : base("Check-In Module", "Process guest arrivals and assign rooms.") { }
    }

    public class CheckOutPanel : PlaceholderPanel
    {
        public CheckOutPanel() : base("Check-Out Module", "Process guest departures and finalize bills.") { }
    }

    public class RoomsCalendarPanel : PlaceholderPanel
    {
        public RoomsCalendarPanel() : base("Rooms & Calendar", "View room status and availability over time.") { }
    }

    public class GuestListPanel : PlaceholderPanel
    {
        public GuestListPanel() : base("Guest List", "Manage guest information and history.") { }
    }

    public class BillingPanel : PlaceholderPanel
    {
        public BillingPanel() : base("Billing & Payments", "Handle invoices, payments, and financial records.") { }
    }
}

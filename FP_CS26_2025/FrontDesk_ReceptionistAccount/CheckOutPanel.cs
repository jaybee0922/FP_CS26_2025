using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FP_CS26_2025.FrontDesk_MVC;
using FP_CS26_2025.ModernDesign;

namespace FP_CS26_2025
{
    public class CheckOutPanel : BaseFrontDeskPanel
    {
        private ListBox lbCheckedIn;
        private Button btnCheckOut;
        private TableLayoutPanel mainLayout;
        private ModernTextBox txtSearch;
        private ModernShadowPanel shadowPanel;

        public CheckOutPanel() : base() { InitializeComponents(); }

        public CheckOutPanel(FrontDeskController controller) : base(controller, "Check-Out Processing")
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
                PlaceholderText = "Search Room Number or Guest Name...",
                Size = new Size(300, 35),
                BorderColor = Color.LightGray,
                BorderFocusColor = Color.FromArgb(231, 76, 60),
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

            lbCheckedIn = new ListBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 11f),
                ItemHeight = 30,
                BorderStyle = BorderStyle.None,
                BackColor = Color.White
            };
            shadowPanel.Controls.Add(lbCheckedIn);

            btnCheckOut = new Button
            {
                Text = "Process Check-Out",
                Size = new Size(180, 40),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Right
            };
            btnCheckOut.FlatAppearance.BorderSize = 0;

            btnCheckOut.Click += (s, e) => {
                if (_controller == null) return;
                if (lbCheckedIn.SelectedItem is Reservation res) {
                    var bill = _controller.CheckOut(res.ReservationId);
                    MessageBox.Show($"Check-out complete.\nTotal Bill: ${bill.TotalAmount}\nRoom {res.AssignedRoom.RoomNumber} is now Available.");
                    RefreshData();
                }
            };

            // Button Panel
            var buttonPanel = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0) };
            buttonPanel.Controls.Add(btnCheckOut);
            btnCheckOut.Location = new Point(buttonPanel.Width - btnCheckOut.Width, 10);

            mainLayout.Padding = new Padding(20, 60, 20, 20); 
            mainLayout.Controls.Add(searchWrapper, 0, 1);
            mainLayout.Controls.Add(shadowPanel, 0, 2);
            mainLayout.Controls.Add(buttonPanel, 0, 3);

            this.Controls.Add(mainLayout);
            mainLayout.BringToFront();

            this.ResumeLayout(false);
        }

        public override void RefreshData() {
            lbCheckedIn.DataSource = null;
            if (_controller == null) return;
            lbCheckedIn.DataSource = _controller.GetActiveReservations().Where(r => r.IsCheckedIn).ToList();
            lbCheckedIn.DisplayMember = "ReservationId";
        }


    }
}

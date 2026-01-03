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
        private DataGridView dgvBillItems;
        private Button btnCheckOut;
        private Button btnPrintReceipt;
        private TableLayoutPanel mainLayout;
        private ModernTextBox txtSearch;
        private ModernShadowPanel listShadow;
        private ModernShadowPanel billShadow;
        
        // Billing Labels
        private Label lblGuestName;
        private Label lblRoomNum;
        private Label lblDates;
        private Label lblTotalAmount;
        private Label lblChange;
        private ModernTextBox txtAmountPaid;
        private ComboBox cbPaymentMethod;
        
        private Bill currentBill;

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
                ColumnCount = 2,
                RowCount = 2, 
                Padding = new Padding(20, 60, 20, 20)
            };
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F)); 
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // --- LEFT SIDE: LIST ---
            Panel leftPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0, 0, 10, 0) };
            
            txtSearch = new ModernTextBox
            {
                PlaceholderText = "Search Guest/Room...",
                Dock = DockStyle.Top,
                BorderColor = Color.LightGray,
                BorderFocusColor = Color.FromArgb(231, 76, 60),
                UnderlinedStyle = true
            };
            txtSearch.TextChange += (s, e) => PerformSearch(txtSearch.Text);

            listShadow = new ModernShadowPanel
            {
                Dock = DockStyle.Fill,
                ShadowDepth = 4,
                ShadowColor = Color.FromArgb(200, 200, 200),
                BorderRadius = 10,
                Padding = new Padding(5),
                Margin = new Padding(0, 10, 0, 0)
            };

            lbCheckedIn = new ListBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10f),
                BorderStyle = BorderStyle.None,
                BackColor = Color.White
            };
            lbCheckedIn.SelectedIndexChanged += lbCheckedIn_SelectedIndexChanged;
            
            listShadow.Controls.Add(lbCheckedIn);
            leftPanel.Controls.Add(listShadow);
            leftPanel.Controls.Add(txtSearch);

            // --- RIGHT SIDE: BILLING ---
            billShadow = new ModernShadowPanel
            {
                Dock = DockStyle.Fill,
                ShadowDepth = 4,
                ShadowColor = Color.FromArgb(200, 200, 200),
                BorderRadius = 10,
                Padding = new Padding(20)
            };

            TableLayoutPanel billLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 5
            };
            billLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F)); // Info
            billLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Grid
            billLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F)); // Totals
            billLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F)); // Payment
            billLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F)); // Buttons

            // 1. Info Area
            FlowLayoutPanel infoFlow = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown };
            lblGuestName = new Label { Text = "Guest: ---", Font = new Font("Segoe UI", 11f, FontStyle.Bold), AutoSize = true };
            lblRoomNum = new Label { Text = "Room: ---", Font = new Font("Segoe UI", 10f), AutoSize = true };
            lblDates = new Label { Text = "Dates: ---", Font = new Font("Segoe UI", 10f), AutoSize = true };
            infoFlow.Controls.AddRange(new Control[] { lblGuestName, lblRoomNum, lblDates });

            // 2. Grid Area
            dgvBillItems = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            // 3. Totals Area
            lblTotalAmount = new Label { Text = "TOTAL: P0.00", Font = new Font("Segoe UI", 14f, FontStyle.Bold), ForeColor = Color.FromArgb(231, 76, 60), Dock = DockStyle.Right, TextAlign = ContentAlignment.MiddleRight, AutoSize = true };

            // 4. Payment Area
            TableLayoutPanel paymentLayout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 2 };
            paymentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            paymentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            
            paymentLayout.Controls.Add(new Label { Text = "Method:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill }, 0, 0);
            cbPaymentMethod = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cbPaymentMethod.Items.AddRange(new object[] { "Cash", "Credit Card", "Debit Card", "Online Transfer" });
            cbPaymentMethod.SelectedIndex = 0;
            paymentLayout.Controls.Add(cbPaymentMethod, 1, 0);

            paymentLayout.Controls.Add(new Label { Text = "Paid Amnt:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill }, 0, 1);
            txtAmountPaid = new ModernTextBox { Dock = DockStyle.Fill, PlaceholderText = "0.00" };
            txtAmountPaid.TextChange += (s, e) => CalculateChange();
            paymentLayout.Controls.Add(txtAmountPaid, 1, 1);

            paymentLayout.Controls.Add(new Label { Text = "Change:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill }, 2, 1);
            lblChange = new Label { Text = "P0.00", Font = new Font("Segoe UI", 12f, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill };
            paymentLayout.Controls.Add(lblChange, 3, 1);

            // 5. Buttons
            FlowLayoutPanel buttonFlow = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft };
            btnCheckOut = new Button { Text = "Process Payment", BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(150, 40), Font = new Font("Segoe UI", 9f, FontStyle.Bold), Enabled = false };
            btnPrintReceipt = new Button { Text = "Print Receipt", BackColor = Color.FromArgb(52, 152, 219), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(150, 40), Font = new Font("Segoe UI", 9f, FontStyle.Bold), Enabled = false };
            
            btnCheckOut.Click += (s, e) => ProcessPayment();
            btnPrintReceipt.Click += (s, e) => PrintReceipt();
            
            buttonFlow.Controls.Add(btnCheckOut);
            buttonFlow.Controls.Add(btnPrintReceipt);

            billLayout.Controls.Add(infoFlow, 0, 0);
            billLayout.Controls.Add(dgvBillItems, 0, 1);
            billLayout.Controls.Add(lblTotalAmount, 0, 2);
            billLayout.Controls.Add(paymentLayout, 0, 3);
            billLayout.Controls.Add(buttonFlow, 0, 4);
            billShadow.Controls.Add(billLayout);

            mainLayout.Controls.Add(leftPanel, 0, 1);
            mainLayout.Controls.Add(billShadow, 1, 1);

            this.Controls.Add(mainLayout);
            mainLayout.BringToFront();

            this.ResumeLayout(false);
        }

        public override void RefreshData()
        {
            // Reset state before loading
            currentBill = null;
            ResetBillDisplay();
            
            lbCheckedIn.SelectedIndexChanged -= lbCheckedIn_SelectedIndexChanged; // Temporarily detach
            lbCheckedIn.DataSource = null;
            
            if (_controller == null) return;
            
            var checkedIn = _controller.GetActiveReservations()
                .Where(r => r.IsCheckedIn || r.Status == "CheckedIn")
                .Where(r => r.Status != "CheckedOut")
                .ToList();
                
            lbCheckedIn.DataSource = checkedIn;
            lbCheckedIn.DisplayMember = "ReservationId";
            lbCheckedIn.SelectedIndexChanged += lbCheckedIn_SelectedIndexChanged; // Re-attach
            
            // Manually trigger first line load if items exist
            if (checkedIn.Count > 0)
            {
                lbCheckedIn.SelectedIndex = 0;
                LoadReservationBilling();
            }
        }

        private void lbCheckedIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadReservationBilling();
        }

        private void LoadReservationBilling()
        {
            if (lbCheckedIn.SelectedItem is Reservation res)
            {
                currentBill = _controller.CheckOut(res.ReservationId);
                DisplayBill(currentBill);
            }
            else
            {
                ResetBillDisplay();
            }
        }

        private void DisplayBill(Bill bill)
        {
            lblGuestName.Text = $"Guest: {bill.Reservation.Guest.FullName}";
            lblRoomNum.Text = $"Room: {bill.Reservation.AssignedRoom.RoomNumber} ({bill.Reservation.RoomType})";
            lblDates.Text = $"Stay: {bill.Reservation.CheckInDate:MM/dd} to {bill.Reservation.CheckOutDate:MM/dd} ({bill.Reservation.Duration} nights)";
            
            dgvBillItems.DataSource = bill.LineItems;
            lblTotalAmount.Text = $"TOTAL: P{bill.TotalAmount:N2}";
            
            btnCheckOut.Enabled = !bill.IsPaid;
            btnPrintReceipt.Enabled = bill.IsPaid;
            txtAmountPaid.Enabled = !bill.IsPaid;
            cbPaymentMethod.Enabled = !bill.IsPaid;
            
            CalculateChange();
        }

        private void ResetBillDisplay()
        {
            lblGuestName.Text = "Guest: ---";
            lblRoomNum.Text = "Room: ---";
            lblDates.Text = "Dates: ---";
            dgvBillItems.DataSource = null;
            lblTotalAmount.Text = "TOTAL: P0.00";
            lblChange.Text = "P0.00";
            txtAmountPaid.Text = "";
            btnCheckOut.Enabled = false;
            btnPrintReceipt.Enabled = false;
        }

        private void CalculateChange()
        {
            if (currentBill == null || string.IsNullOrWhiteSpace(txtAmountPaid.Text))
            {
                lblChange.Text = "P0.00";
                return;
            }

            if (decimal.TryParse(txtAmountPaid.Text, out decimal paid))
            {
                decimal change = paid - currentBill.TotalAmount;
                lblChange.Text = $"P{Math.Max(0, change):N2}";
                
                if (change < 0) lblChange.ForeColor = Color.Red;
                else lblChange.ForeColor = Color.Green;
                
                btnCheckOut.Enabled = !currentBill.IsPaid && paid >= currentBill.TotalAmount;
            }
        }

        private void ProcessPayment()
        {
            if (currentBill == null) return;
            
            if (decimal.TryParse(txtAmountPaid.Text, out decimal paid))
            {
                try
                {
                    _controller.CompleteCheckOut(currentBill, cbPaymentMethod.Text, paid);
                    MessageBox.Show($"Payment processed successfully!\nChange: P{currentBill.Change:N2}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    DisplayBill(currentBill); // Refresh display to show as paid
                    btnCheckOut.Enabled = false;
                    btnPrintReceipt.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Check-out error: {ex.Message}", "Transaction Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PrintReceipt()
        {
            if (currentBill == null || !currentBill.IsPaid) return;

            string receipt = $@"
═══════════════════════════════════════
        GRAND NEXUS HOTEL
═══════════════════════════════════════
Date: {currentBill.BillingDate:MM/dd/yyyy hh:mm tt}
Invoice #: {currentBill.BillId}

Guest: {currentBill.Reservation.Guest.FullName}
Room: {currentBill.Reservation.AssignedRoom.RoomNumber} - {currentBill.Reservation.RoomType}
Check-In: {currentBill.Reservation.CheckInDate:MM/dd/yyyy}
Check-Out: {currentBill.Reservation.CheckOutDate:MM/dd/yyyy}
Nights: {currentBill.Reservation.Duration}

───────────────────────────────────────
CHARGES
───────────────────────────────────────
{currentBill.LineItems[0].Description,-25} P {currentBill.LineItems[0].Total,10:N2}
{currentBill.Reservation.Duration} nights @ P{currentBill.LineItems[0].UnitPrice:N2}/night

───────────────────────────────────────
Subtotal:               P {currentBill.Subtotal,10:N2}
Tax (0%):               P {currentBill.Tax,10:N2}
───────────────────────────────────────
TOTAL:                  P {currentBill.TotalAmount,10:N2}

Payment Method: {currentBill.PaymentMethod}
Amount Paid:            P {currentBill.AmountPaid,10:N2}
Change:                 P {currentBill.Change,10:N2}

═══════════════════════════════════════
    Thank you for staying with us!
═══════════════════════════════════════";

            MessageBox.Show(receipt, "Final Receipt - Grand Nexus Hotel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshData(); // Remove from list
        }

        public override void PerformSearch(string query)
        {
            if (_controller == null) return;
            if (string.IsNullOrWhiteSpace(query)) { RefreshData(); return; }

            var results = _controller.GetActiveReservations()
                .Where(r => r.IsCheckedIn && r.Status != "CheckedOut" && 
                    (r.Guest.FullName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 ||
                     r.AssignedRoom.RoomNumber.ToString().Contains(query) ||
                     r.ReservationId.Contains(query)))
                .ToList();

            lbCheckedIn.DataSource = results;
        }
    }
}

using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FP_CS26_2025.FrontDesk_MVC;
using FP_CS26_2025.ModernDesign;

namespace FP_CS26_2025
{
    public class BillingPanel : BaseFrontDeskPanel
    {
        private DataGridView dgvBilling;
        private TableLayoutPanel mainLayout;
        private Button btnGenerateReport;
        private DateTimePicker dtStart, dtEnd;
        private ComboBox cmbStatus;
        private ModernShadowPanel shadowPanel;
        
        // Pagination
        private Button btnPrevious, btnNext;
        private Label lblPageInfo;
        private int _currentPage = 1;
        private int _pageSize = 20;
        private int _totalRecords = 0;

        public BillingPanel() : base() { InitializeComponents(); }

        public BillingPanel(FrontDeskController controller) : base(controller, "Billing & Transactions")
        {
            InitializeComponents();
            dtStart.Value = DateTime.Today.AddDays(-30); // Default to last 30 days
            dtEnd.Value = DateTime.Today;
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
            // Filters Panel
            Panel filterPanel = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, 10) };
            
            Label lblFrom = new Label { Text = "From:", Location = new Point(0, 8), AutoSize = true, Font = new Font("Segoe UI", 10) };
            dtStart = new DateTimePicker { Location = new Point(50, 5), Format = DateTimePickerFormat.Short, Width = 100, Font = new Font("Segoe UI", 10) };
            
            Label lblTo = new Label { Text = "To:", Location = new Point(170, 8), AutoSize = true, Font = new Font("Segoe UI", 10) };
            dtEnd = new DateTimePicker { Location = new Point(200, 5), Format = DateTimePickerFormat.Short, Width = 100, Font = new Font("Segoe UI", 10) };
            
            Label lblStatus = new Label { Text = "Status:", Location = new Point(320, 8), AutoSize = true, Font = new Font("Segoe UI", 10) };
            cmbStatus = new ComboBox { Location = new Point(370, 5), Width = 120, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };
            cmbStatus.Items.AddRange(new object[] { "All", "Paid", "Pending" });
            cmbStatus.SelectedIndex = 0;

            Button btnFilter = new Button { Text = "Apply Filter", Location = new Point(510, 3), Size = new Size(100, 30), BackColor = Color.FromArgb(41, 128, 185), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnFilter.Click += (s, e) => { _currentPage = 1; RefreshData(); };

            filterPanel.Controls.AddRange(new Control[] { lblFrom, dtStart, lblTo, dtEnd, lblStatus, cmbStatus, btnFilter });

            // Shadow Grid
            shadowPanel = new ModernShadowPanel
            {
                Dock = DockStyle.Fill,
                ShadowDepth = 4,
                ShadowColor = Color.FromArgb(200, 200, 200),
                BorderRadius = 10,
                Padding = new Padding(15)
            };

            dgvBilling = new DataGridView
            {
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false,
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                DefaultCellStyle = new DataGridViewCellStyle 
                { 
                    SelectionBackColor = Color.FromArgb(0, 120, 215),
                    SelectionForeColor = Color.White,
                    Font = new Font("Segoe UI", 9.5f),
                    Padding = new Padding(5)
                },
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(240, 240, 240),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                    SelectionBackColor = Color.FromArgb(240, 240, 240),
                    Padding = new Padding(5)
                },
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing,
                ColumnHeadersHeight = 40,
                EnableHeadersVisualStyles = false,
                RowTemplate = { Height = 35 }
            };

            shadowPanel.Controls.Add(dgvBilling);

            // Footer Panel (Pagination + Report)
            var footerPanel = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0) };

            // Create a container for pagination to keep them together and centered/spaced
            var paginationCont = new FlowLayoutPanel
            {
                Location = new Point(0, 5),
                Size = new Size(450, 45),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(0, 5, 0, 0)
            };

            btnPrevious = new Button { Text = "< Previous", Size = new Size(100, 35), FlatStyle = FlatStyle.Flat, Margin = new Padding(0, 0, 10, 0) };
            btnPrevious.Click += (s, e) => { if (_currentPage > 1) { _currentPage--; RefreshData(); } };

            lblPageInfo = new Label { Text = "Page 1 of 1", AutoSize = true, Font = new Font("Segoe UI", 10), Margin = new Padding(0, 8, 10, 0) };

            btnNext = new Button { Text = "Next >", Size = new Size(100, 35), FlatStyle = FlatStyle.Flat, Margin = new Padding(0) };
            btnNext.Click += (s, e) => { if (_currentPage * _pageSize < _totalRecords) { _currentPage++; RefreshData(); } };

            paginationCont.Controls.AddRange(new Control[] { btnPrevious, lblPageInfo, btnNext });

            btnGenerateReport = new Button
            {
                Text = "Generate Monthly Report",
                Size = new Size(220, 40),
                BackColor = Color.FromArgb(142, 68, 173),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnGenerateReport.FlatAppearance.BorderSize = 0;
            btnGenerateReport.Location = new Point(footerPanel.Width - btnGenerateReport.Width, 5);
            btnGenerateReport.Click += BtnGenerateReport_Click;

            // Handle Resize for Report Button
            footerPanel.Resize += (s, e) => {
                btnGenerateReport.Location = new Point(footerPanel.Width - btnGenerateReport.Width, 5);
            };

            footerPanel.Controls.AddRange(new Control[] { paginationCont, btnGenerateReport });

            mainLayout.Padding = new Padding(20, 60, 20, 20); 
            mainLayout.Controls.Add(filterPanel, 0, 1);
            mainLayout.Controls.Add(shadowPanel, 0, 2);
            mainLayout.Controls.Add(footerPanel, 0, 3);

            this.Controls.Add(mainLayout);
            mainLayout.BringToFront();

            this.ResumeLayout(false);
        }

        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            // Set Filters to Current Month
            var now = DateTime.Now;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            dtStart.Value = startOfMonth;
            dtEnd.Value = endOfMonth;
            cmbStatus.SelectedItem = "All"; // Report usually covers all valid payments

            // Refresh to show data
            _currentPage = 1;
            RefreshData();

            // Calculate Summary of ALL matching records
            int totalRecs;
            var reportData = _controller.GetFilteredPayments(startOfMonth, endOfMonth, "All", "", 1, 100000, out totalRecs);
            
            decimal totalRevenue = reportData.Sum(p => p.Amount);
            int count = reportData.Count();

            string message = $"Monthly Report ({startOfMonth:MMM yyyy})\n\n" +
                             $"Total Transactions: {count}\n" +
                             $"Total Revenue: P {totalRevenue:N2}\n\n" +
                             $"Do you want to ARCHIVE this report to the database?";

            var result = MessageBox.Show(message, "Monthly Report", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    _controller.SaveMonthlyReport(now.Month, now.Year, totalRevenue, count);
                    MessageBox.Show("Report successfully archived!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                   MessageBox.Show("Failed to archive report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public override void RefreshData()
        {
            if (_controller == null || dgvBilling == null) return;
            
            // Capture Filters
            DateTime start = dtStart.Value;
            DateTime end = dtEnd.Value;
            string status = cmbStatus.SelectedItem?.ToString() ?? "All";
            string query = ""; // Add SearchBox if needed, currently using Base functionality?
            
            // Base class text search if available (BaseFrontDeskPanel often has a search box, check usage)
            // But we don't have access to the base search box text directly here unless exposed.
            // We will just assume empty query for refresh, or if PerformSearch calls this.
            
            var payments = _controller.GetFilteredPayments(start, end, status, query, _currentPage, _pageSize, out _totalRecords);
            
            var displayList = payments.Select(p => new {
                ID = p.PaymentId,
                Date = p.PaymentDate.ToString("MM/dd HH:mm"),
                Guest = p.GuestName,
                Reservation = p.ReservationId,
                Amount = $"P {p.Amount:N2}",
                Method = p.PaymentMethod
            }).ToList();
            
            dgvBilling.DataSource = displayList;

            // Update Pagination Controls
            int totalPages = (int)Math.Ceiling((double)_totalRecords / _pageSize);
            if (totalPages < 1) totalPages = 1;
            
            lblPageInfo.Text = $"Page {_currentPage} of {totalPages} (Total: {_totalRecords})";
            btnPrevious.Enabled = _currentPage > 1;
            btnNext.Enabled = _currentPage < totalPages;
        }

        public override void PerformSearch(string query)
        {
            if (_controller == null) return;
            _currentPage = 1; // Reset to page 1 on search
            
            DateTime start = dtStart.Value;
            DateTime end = dtEnd.Value;
            string status = cmbStatus.SelectedItem?.ToString() ?? "All";
            
            var payments = _controller.GetFilteredPayments(start, end, status, query, _currentPage, _pageSize, out _totalRecords);

            var displayList = payments.Select(p => new {
                ID = p.PaymentId,
                Date = p.PaymentDate.ToString("MM/dd HH:mm"),
                Guest = p.GuestName,
                Reservation = p.ReservationId,
                Amount = $"P {p.Amount:N2}",
                Method = p.PaymentMethod
            }).ToList();
            
            dgvBilling.DataSource = displayList;
            
            // Update Pagination Controls
            int totalPages = (int)Math.Ceiling((double)_totalRecords / _pageSize);
            if (totalPages < 1) totalPages = 1;
            
            lblPageInfo.Text = $"Page {_currentPage} of {totalPages} (Total: {_totalRecords})";
            btnPrevious.Enabled = _currentPage > 1;
            btnNext.Enabled = _currentPage < totalPages;
        }
    }
}

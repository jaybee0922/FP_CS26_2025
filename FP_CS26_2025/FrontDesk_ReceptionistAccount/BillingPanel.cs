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

        public BillingPanel() : base() { InitializeComponents(); }

        public BillingPanel(FrontDeskController controller) : base(controller, "Billing & Transactions")
        {
            InitializeComponents();
            // LoadDummyData(); // Placeholder
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
            cmbStatus.Items.AddRange(new object[] { "All", "Paid", "Pending", "Refused" });
            cmbStatus.SelectedIndex = 0;

            Button btnFilter = new Button { Text = "Apply Filter", Location = new Point(510, 3), Size = new Size(100, 30), BackColor = Color.FromArgb(41, 128, 185), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnFilter.Click += (s, e) => MessageBox.Show("Filter logic pending API integration");

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

            // Add columns manually for placeholder
            dgvBilling.Columns.Add("Date", "Date");
            dgvBilling.Columns.Add("Guest", "Guest");
            dgvBilling.Columns.Add("Amount", "Amount");
            dgvBilling.Columns.Add("Status", "Status");

            shadowPanel.Controls.Add(dgvBilling);

            btnGenerateReport = new Button
            {
                Text = "Generate Monthly Report",
                Size = new Size(220, 40),
                BackColor = Color.FromArgb(142, 68, 173),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Right
            };
            btnGenerateReport.FlatAppearance.BorderSize = 0;
            btnGenerateReport.Click += (s, e) => MessageBox.Show("Report generation feature coming soon.");

            
            // Footer Panel
            var footerPanel = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0) };
            footerPanel.Controls.Add(btnGenerateReport);
            btnGenerateReport.Location = new Point(footerPanel.Width - btnGenerateReport.Width, 10);

            mainLayout.Padding = new Padding(20, 60, 20, 20); 
            mainLayout.Controls.Add(filterPanel, 0, 1);
            mainLayout.Controls.Add(shadowPanel, 0, 2);
            mainLayout.Controls.Add(footerPanel, 0, 3);

            this.Controls.Add(mainLayout);
            mainLayout.BringToFront();

            this.ResumeLayout(false);
        }

        public override void RefreshData()
        {
            // Placeholder: Connect to controller billing data when available
        }
    }
}

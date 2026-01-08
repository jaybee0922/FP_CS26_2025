using System.Drawing;
using System.Windows.Forms;
using FP_CS26_2025.ModernDesign;

namespace FP_CS26_2025.HotelManager_AdminDashboard.Reports
{
    partial class ReportsControl
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel mainLayout;
        private Panel filterPanel;
        private ComboBox cmbReportType;
        private DateTimePicker dtPicker;
        private Button btnGenerate;
        private ModernShadowPanel shadowPanel;
        private DataGridView dgvReports;
        private Label lblTotalRevenue;
        private Label lblTotalTransactions;
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle rowStyle = new System.Windows.Forms.DataGridViewCellStyle();

            this.SuspendLayout();
            this.Size = new Size(1100, 600);
            this.BackColor = Color.White;

            mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(20)
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F)); // Filter Height
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Grid
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F)); // Footer

            // --- Filter Panel ---
            filterPanel = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, 10) };
            
            Label lblType = new Label { Text = "Report Type:", Location = new Point(0, 15), AutoSize = true, Font = new Font("Segoe UI", 10) };
            cmbReportType = new ComboBox 
            { 
                Location = new Point(90, 12), 
                Width = 120, 
                DropDownStyle = ComboBoxStyle.DropDownList, 
                Font = new Font("Segoe UI", 10) 
            };
            cmbReportType.Items.AddRange(new object[] { "Weekly", "Monthly", "Yearly" });
            cmbReportType.SelectedIndex = 1; // Default Month

            Label lblDate = new Label { Text = "Select Date:", Location = new Point(230, 15), AutoSize = true, Font = new Font("Segoe UI", 10) };
            dtPicker = new DateTimePicker 
            { 
                Location = new Point(310, 12), 
                Width = 200, 
                Font = new Font("Segoe UI", 10) 
            };

            btnGenerate = new Button 
            { 
                Text = "Generate Report", 
                Location = new Point(530, 10), 
                Size = new Size(140, 30), 
                BackColor = Color.FromArgb(41, 128, 185), 
                ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnGenerate.FlatAppearance.BorderSize = 0;

            filterPanel.Controls.AddRange(new Control[] { lblType, cmbReportType, lblDate, dtPicker, btnGenerate });

            // --- Grid (Shadow Panel) ---
            shadowPanel = new ModernShadowPanel
            {
                Dock = DockStyle.Fill,
                ShadowDepth = 4,
                ShadowColor = Color.FromArgb(200, 200, 200),
                BorderRadius = 10,
                Padding = new Padding(15)
            };

            dgvReports = new DataGridView
            {
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false,
                Dock = DockStyle.Fill,
                Margin = new Padding(0)
            };

            headerStyle.BackColor = Color.FromArgb(240, 240, 240);
            headerStyle.ForeColor = Color.FromArgb(64, 64, 64);
            headerStyle.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            headerStyle.SelectionBackColor = Color.FromArgb(240, 240, 240);
            headerStyle.Padding = new Padding(5);

            rowStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            rowStyle.SelectionForeColor = Color.White;
            rowStyle.Font = new Font("Segoe UI", 9.5f);
            rowStyle.Padding = new Padding(5);

            dgvReports.ColumnHeadersDefaultCellStyle = headerStyle;
            dgvReports.DefaultCellStyle = rowStyle;
            dgvReports.EnableHeadersVisualStyles = false;
            dgvReports.ColumnHeadersHeight = 40;
            dgvReports.RowTemplate.Height = 35;

            shadowPanel.Controls.Add(dgvReports);

            // --- Footer Panel ---
            Panel footerPanel = new Panel { Dock = DockStyle.Fill };
            
            lblTotalTransactions = new Label 
            { 
                Text = "Total Transactions: 0", 
                Location = new Point(0, 15), 
                AutoSize = true, 
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.Gray
            };

            lblTotalRevenue = new Label 
            { 
                Text = "Total Revenue: P 0.00", 
                Location = new Point(300, 15), 
                AutoSize = true, 
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(39, 174, 96) // Green for money
            };

            footerPanel.Controls.Add(lblTotalTransactions);
            footerPanel.Controls.Add(lblTotalRevenue);

            // Add to Main Layout
            mainLayout.Controls.Add(filterPanel, 0, 0);
            mainLayout.Controls.Add(shadowPanel, 0, 1);
            mainLayout.Controls.Add(footerPanel, 0, 2);

            this.Controls.Add(mainLayout);
            this.ResumeLayout(false);
        }
    }
}

using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using FP_CS26_2025.FrontDesk_MVC;
using FP_CS26_2025.ModernDesign;

namespace FP_CS26_2025
{
    public class ArchiveReportsPanel : BaseFrontDeskPanel
    {
        private DataGridView dgvReports;
        private TableLayoutPanel mainLayout;
        private ModernShadowPanel shadowPanel;

        public ArchiveReportsPanel(FrontDeskController controller) : base(controller, "Archived Monthly Reports")
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
                RowCount = 2,
                Padding = new Padding(20, 60, 20, 20)
            };
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F)); // Header space
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

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

            shadowPanel.Controls.Add(dgvReports);
            mainLayout.Controls.Add(shadowPanel, 0, 1);

            this.Controls.Add(mainLayout);
            mainLayout.BringToFront();

            this.ResumeLayout(false);
        }

        public override void RefreshData()
        {
            if (_controller == null || dgvReports == null) return;

            try
            {
                System.Data.DataTable dt = _controller.GetArchivedReports();
                
                // Format for display
                dgvReports.DataSource = dt;
                
                if (dgvReports.Columns.Contains("TotalRevenue"))
                {
                    dgvReports.Columns["TotalRevenue"].DefaultCellStyle.Format = "P #,##0.00";
                    dgvReports.Columns["TotalRevenue"].HeaderText = "Total Revenue";
                }
                if (dgvReports.Columns.Contains("TransactionCount"))
                {
                    dgvReports.Columns["TransactionCount"].HeaderText = "Transactions";
                }
                if (dgvReports.Columns.Contains("GeneratedDate"))
                {
                    dgvReports.Columns["GeneratedDate"].HeaderText = "Archived On";
                    dgvReports.Columns["GeneratedDate"].DefaultCellStyle.Format = "MM/dd/yyyy HH:mm";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load archived reports: {ex.Message}", "Error", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

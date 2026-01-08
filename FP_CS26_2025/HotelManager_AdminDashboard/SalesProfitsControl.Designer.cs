using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FP_CS26_2025.HotelManager_AdminDashboard
{
    partial class SalesProfitsControl
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel mainLayout;
        private Panel topPanel;
        private Label lblTitle;
        private Label lblTrendFilter;
        private ComboBox cmbTrendPeriod;
        private Label lblChartType;
        private ComboBox cmbChartType;
        private Label lblDistFilter;
        private ComboBox cmbDistCategory;
        private ComboBox cmbDistType;
        private ComboBox cmbDistPeriod;
        private Chart chartTrend;
        private Chart chartDistribution;
        private Label lblNoData;

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
            ChartArea chartArea1 = new ChartArea();
            Legend legend1 = new Legend();
            ChartArea chartArea2 = new ChartArea();
            Legend legend2 = new Legend();

            this.mainLayout = new TableLayoutPanel();
            this.topPanel = new Panel();
            this.lblTitle = new Label();
            this.lblNoData = new Label(); // New label
            
            // Filters
            this.lblTrendFilter = new Label();
            this.cmbTrendPeriod = new ComboBox();
            this.lblChartType = new Label();
            this.cmbChartType = new ComboBox();
            
            this.lblDistFilter = new Label();
            this.cmbDistCategory = new ComboBox();
            this.cmbDistPeriod = new ComboBox();
            this.cmbDistType = new ComboBox();

            this.chartTrend = new Chart();
            this.chartDistribution = new Chart();

            ((System.ComponentModel.ISupportInitialize)(this.chartTrend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDistribution)).BeginInit();
            this.SuspendLayout();

            // 
            // SalesProfitsControl
            // 
            this.Size = new Size(1100, 700);
            this.BackColor = Color.FromArgb(240, 240, 240); // Light gray background

            // 
            // mainLayout
            // 
            this.mainLayout.ColumnCount = 2;
            this.mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F)); // 60% for Trend
            this.mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F)); // 40% for Distribution
            this.mainLayout.RowCount = 2;
            this.mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F)); // Header height
            this.mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Charts height
            this.mainLayout.Controls.Add(this.topPanel, 0, 0);
            this.mainLayout.Dock = DockStyle.Fill;
            this.mainLayout.Padding = new Padding(10);

            // 
            // topPanel
            // 
            this.mainLayout.SetColumnSpan(this.topPanel, 2);
            this.topPanel.Dock = DockStyle.Fill;
            this.topPanel.BackColor = Color.White;
            this.topPanel.Controls.Add(this.lblTitle);
            this.topPanel.Controls.Add(this.lblTrendFilter);
            this.topPanel.Controls.Add(this.cmbTrendPeriod);
            this.topPanel.Controls.Add(this.lblChartType);
            this.topPanel.Controls.Add(this.cmbChartType);
            this.topPanel.Controls.Add(this.lblDistFilter);
            this.topPanel.Controls.Add(this.cmbDistCategory);
            this.topPanel.Controls.Add(this.cmbDistPeriod);
            this.topPanel.Controls.Add(this.cmbDistType);

            // 
            // lblTitle
            // 
            this.lblTitle.Text = "Sales and Profits Overview";
            this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitle.Location = new Point(20, 10);
            this.lblTitle.AutoSize = true;
            this.lblTitle.ForeColor = Color.FromArgb(51, 51, 76);

            // 
            // Trend Controls (Left side of filter panel)
            // 
            this.lblTrendFilter.Text = "Trend Period:";
            this.lblTrendFilter.Location = new Point(20, 50);
            this.lblTrendFilter.AutoSize = true;
            this.lblTrendFilter.Font = new Font("Segoe UI", 9F);

            this.cmbTrendPeriod.Location = new Point(110, 48);
            this.cmbTrendPeriod.Width = 100;
            this.cmbTrendPeriod.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTrendPeriod.Items.AddRange(new object[] { "Weekly", "Monthly", "Yearly" });
            this.cmbTrendPeriod.SelectedIndex = 0; // Weekly default

            this.lblChartType.Text = "Chart Type:";
            this.lblChartType.Location = new Point(230, 50);
            this.lblChartType.AutoSize = true;
            this.lblChartType.Font = new Font("Segoe UI", 9F);

            this.cmbChartType.Location = new Point(310, 48);
            this.cmbChartType.Width = 100;
            this.cmbChartType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbChartType.Items.AddRange(new object[] { "Bar", "Line" });
            this.cmbChartType.SelectedIndex = 0; // Bar default

            // 
            // Distribution Controls (Right side of filter panel)
            // 
            this.lblDistFilter.Text = "Distribution:";
            this.lblDistFilter.Location = new Point(550, 50); // Offset to right
            this.lblDistFilter.AutoSize = true;
            this.lblDistFilter.Font = new Font("Segoe UI", 9F);

            this.cmbDistCategory.Location = new Point(630, 48);
            this.cmbDistCategory.Width = 120;
            this.cmbDistCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbDistCategory.Items.AddRange(new object[] { "Room Type", "Payment Method" });
            this.cmbDistCategory.SelectedIndex = 0;

            this.cmbDistPeriod.Location = new Point(760, 48);
            this.cmbDistPeriod.Width = 120;
            this.cmbDistPeriod.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbDistPeriod.Items.AddRange(new object[] { "This Week", "Current Month", "Year-To-Date" });
            this.cmbDistPeriod.SelectedIndex = 1; // Current Month

            this.cmbDistType.Location = new Point(890, 48);
            this.cmbDistType.Width = 100;
            this.cmbDistType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbDistType.Items.AddRange(new object[] { "Revenue", "Profit", "Sales" });
            this.cmbDistType.SelectedIndex = 0; // Revenue default

            // 
            // lblNoData
            // 
            this.lblNoData.Text = "No sales data available for the selected period.\n" + 
                                  "Please verify database connections or generate transactions.";
            this.lblNoData.Font = new Font("Segoe UI", 14F, FontStyle.Italic);
            this.lblNoData.ForeColor = Color.Gray;
            this.lblNoData.AutoSize = false;
            this.lblNoData.TextAlign = ContentAlignment.MiddleCenter;
            this.lblNoData.Dock = DockStyle.Fill;
            this.lblNoData.Visible = false;

            // 
            // chartTrend
            // 
            chartArea1.Name = "ChartArea1";
            chartArea1.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea1.AxisY.MajorGrid.LineColor = Color.LightGray;
            this.chartTrend.ChartAreas.Add(chartArea1);
            
            legend1.Name = "Legend1";
            legend1.Docking = Docking.Top;
            legend1.Alignment = StringAlignment.Center;
            this.chartTrend.Legends.Add(legend1);
            
            this.chartTrend.Dock = DockStyle.Fill;
            this.chartTrend.Name = "chartTrend";
            this.chartTrend.Text = "Sales Trend";
            this.chartTrend.Titles.Add("Sales, Revenue & Profits Trend");
            this.mainLayout.Controls.Add(this.chartTrend, 0, 1);

            // 
            // chartDistribution
            // 
            chartArea2.Name = "ChartArea2";
            this.chartDistribution.ChartAreas.Add(chartArea2);
            
            legend2.Name = "Legend2";
            legend2.Docking = Docking.Bottom;
            legend2.Alignment = StringAlignment.Center;
            this.chartDistribution.Legends.Add(legend2);
            
            this.chartDistribution.Dock = DockStyle.Fill;
            this.chartDistribution.Name = "chartDistribution";
            this.chartDistribution.Titles.Add("Distribution Breakdown");
            this.mainLayout.Controls.Add(this.chartDistribution, 1, 1);

            // 
            // Finalize
            // 
            this.Controls.Add(this.lblNoData);
            this.Controls.Add(this.mainLayout);
            ((System.ComponentModel.ISupportInitialize)(this.chartTrend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDistribution)).EndInit();
            this.ResumeLayout(false);
        }
    }
}

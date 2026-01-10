using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FP_CS26_2025.HotelManager_AdminDashboard
{
    /// <summary>
    /// Dashboard chart component for visualizing occupancy and revenue trends.
    /// Implements requirement for "Visual charts (occupancy trend, revenue)".
    /// </summary>
    public class DashboardChartPanel : Panel
    {
        private Chart chartOccupancy;
        private Chart chartRevenue;
        private TableLayoutPanel chartLayout;

        public DashboardChartPanel()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.SuspendLayout();

            chartLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(10)
            };
            chartLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            chartLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            chartLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // Occupancy Chart
            chartOccupancy = CreateChart("Occupancy Trend", "Date", "Occupancy %");
            chartLayout.Controls.Add(chartOccupancy, 0, 0);

            // Revenue Chart
            chartRevenue = CreateChart("Revenue Trend", "Date", "Revenue (₱)");
            chartLayout.Controls.Add(chartRevenue, 1, 0);

            this.Controls.Add(chartLayout);
            this.ResumeLayout(false);
        }

        private Chart CreateChart(string title, string xAxisTitle, string yAxisTitle)
        {
            var chart = new Chart
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderlineColor = Color.LightGray,
                BorderlineDashStyle = ChartDashStyle.Solid,
                BorderlineWidth = 1
            };

            // Chart Area
            var chartArea = new ChartArea("MainArea")
            {
                BackColor = Color.White,
                BorderColor = Color.LightGray,
                BorderDashStyle = ChartDashStyle.Solid,
                BorderWidth = 1
            };
            chartArea.AxisX.Title = xAxisTitle;
            chartArea.AxisX.TitleFont = new Font("Segoe UI", 10, FontStyle.Bold);
            chartArea.AxisX.LabelStyle.Font = new Font("Segoe UI", 8);
            chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
            
            chartArea.AxisY.Title = yAxisTitle;
            chartArea.AxisY.TitleFont = new Font("Segoe UI", 10, FontStyle.Bold);
            chartArea.AxisY.LabelStyle.Font = new Font("Segoe UI", 8);
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;

            chart.ChartAreas.Add(chartArea);

            // Title
            var chartTitle = new Title(title)
            {
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 76),
                Docking = Docking.Top,
                Alignment = ContentAlignment.MiddleCenter
            };
            chart.Titles.Add(chartTitle);

            // Series
            var series = new Series("Data")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.FromArgb(41, 128, 185),
                BorderWidth = 3,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 8,
                MarkerColor = Color.FromArgb(41, 128, 185)
            };
            chart.Series.Add(series);

            // Legend
            var legend = new Legend("Legend")
            {
                Docking = Docking.Bottom,
                Alignment = StringAlignment.Center,
                Font = new Font("Segoe UI", 9)
            };
            chart.Legends.Add(legend);

            return chart;
        }

        /// <summary>
        /// Loads sample occupancy data for demonstration.
        /// In production, this would fetch real data from the database.
        /// </summary>
        public void LoadOccupancyData()
        {
            chartOccupancy.Series["Data"].Points.Clear();
            
            // Sample data for last 7 days
            var today = DateTime.Today;
            var random = new Random();
            
            for (int i = 6; i >= 0; i--)
            {
                var date = today.AddDays(-i);
                var occupancy = 60 + random.Next(0, 30); // 60-90% occupancy
                chartOccupancy.Series["Data"].Points.AddXY(date.ToString("MM/dd"), occupancy);
            }
        }

        /// <summary>
        /// Loads sample revenue data for demonstration.
        /// In production, this would fetch real data from the database.
        /// </summary>
        public void LoadRevenueData()
        {
            chartRevenue.Series["Data"].Points.Clear();
            
            // Sample data for last 7 days
            var today = DateTime.Today;
            var random = new Random();
            
            for (int i = 6; i >= 0; i--)
            {
                var date = today.AddDays(-i);
                var revenue = 15000 + random.Next(-3000, 8000); // ₱12k-23k per day
                chartRevenue.Series["Data"].Points.AddXY(date.ToString("MM/dd"), revenue);
            }
        }

        /// <summary>
        /// Refreshes both charts with current data.
        /// </summary>
        public void RefreshCharts()
        {
            LoadOccupancyData();
            LoadRevenueData();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                chartOccupancy?.Dispose();
                chartRevenue?.Dispose();
                chartLayout?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

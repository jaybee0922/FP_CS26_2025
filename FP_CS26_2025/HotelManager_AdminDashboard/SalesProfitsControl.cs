using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FP_CS26_2025.Services;
using System.Linq;

namespace FP_CS26_2025.HotelManager_AdminDashboard
{
    public partial class SalesProfitsControl : UserControl
    {
        private SalesService _salesService;

        public SalesProfitsControl()
        {
            InitializeComponent();
            _salesService = new SalesService();
            
            // Wire up events
            cmbTrendPeriod.SelectedIndexChanged += (s, e) => UpdateTrendChart();
            cmbChartType.SelectedIndexChanged += (s, e) => UpdateChartType();
            cmbDistCategory.SelectedIndexChanged += (s, e) => UpdateDistributionChart();
            cmbDistType.SelectedIndexChanged += (s, e) => UpdateDistributionChart();
        }

        public void LoadData()
        {
            UpdateTrendChart();
            UpdateDistributionChart();
        }

        private void UpdateTrendChart()
        {
            try
            {
                string period = cmbTrendPeriod.SelectedItem?.ToString() ?? "Weekly";
                var data = _salesService.GetSalesTrend(period);

                if (data == null || data.Count == 0)
                {
                    chartTrend.Visible = false;
                    lblNoData.Visible = true;
                    lblNoData.BringToFront();
                    return;
                }

                chartTrend.Visible = true;
                lblNoData.Visible = false;
                chartTrend.Series.Clear();

                // Revenue Series
                var revenueSeries = new Series("Revenue")
                {
                    ChartType = GetSelectedChartType(),
                    Color = Color.FromArgb(41, 128, 185), // Blue
                    BorderWidth = 3
                };

                // Profit Series
                var profitSeries = new Series("Profit")
                {
                    ChartType = GetSelectedChartType(),
                    Color = Color.FromArgb(39, 174, 96), // Green
                    BorderWidth = 3
                };

                // Sales Count Series (Always Line on secondary axis)
                var salesSeries = new Series("Sales Count")
                {
                    ChartType = SeriesChartType.Line, 
                    Color = Color.FromArgb(243, 156, 18), // Orange
                    BorderWidth = 2,
                    YAxisType = AxisType.Secondary
                };

                foreach (var item in data)
                {
                    revenueSeries.Points.AddXY(item.Label, item.Revenue);
                    profitSeries.Points.AddXY(item.Label, item.Profit);
                    salesSeries.Points.AddXY(item.Label, item.SalesCount);
                }

                chartTrend.Series.Add(revenueSeries);
                chartTrend.Series.Add(profitSeries);
                chartTrend.Series.Add(salesSeries);

                // Styling
                chartTrend.ChartAreas[0].AxisX.Interval = 1;
                chartTrend.ChartAreas[0].AxisY.LabelStyle.Format = "C0"; // Currency
                chartTrend.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True; // Force enable to show if 0
                chartTrend.ChartAreas[0].AxisY2.LabelStyle.Format = "N0"; // Number
                chartTrend.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading trend chart: {ex.Message}");
            }
        }

        public void UpdateDistributionChart()
        {
            try
            {
                string category = cmbDistCategory.SelectedItem?.ToString() ?? "Room Type";
                string period = cmbDistPeriod.SelectedItem?.ToString() ?? "Current Month";
                string type = cmbDistType.SelectedItem?.ToString() ?? "Revenue";

                var data = _salesService.GetSalesDistribution(category, period, type);

                chartDistribution.Titles[0].Text = $"{type} by {category} ({period})";

                if (data == null || data.Count == 0)
                {
                    // Don't hide the whole chart, just clear series so titles remain or show empty state
                    // But here we can just clear series
                    chartDistribution.Series.Clear();
                    return;
                }

                chartDistribution.Visible = true;
                chartDistribution.Series.Clear();

                var series = new Series("Distribution")
                {
                    ChartType = SeriesChartType.Doughnut,
                    IsValueShownAsLabel = true,
                };

                // Format based on type
                if (type == "Sales") 
                    series.LabelFormat = "N0"; // Number for count
                else 
                    series.LabelFormat = "C0"; // Currency for others

                foreach (var item in data)
                {
                    int p = series.Points.AddXY(item.Label, item.Value);
                    // Add Tooltip
                    string valStr = type == "Sales" ? item.Value.ToString("N0") : item.Value.ToString("C2");
                    series.Points[p].ToolTip = $"{item.Label}: {valStr}";
                    
                    // Show Percentage and Label
                    series.Points[p].Label = "#PERCENT"; 
                    series.Points[p].LegendText = item.Label;
                }

                chartDistribution.Series.Add(series);
                
                // Clear Doughnut hole size for better look
                chartDistribution.Series[0]["DoughnutRadius"] = "60"; 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading distribution chart: {ex.Message}");
            }
        }

        private void UpdateChartType()
        {
            var type = GetSelectedChartType();
            if (chartTrend.Series.Count > 0)
            {
                chartTrend.Series["Revenue"].ChartType = type;
                chartTrend.Series["Profit"].ChartType = type;
                // Keep Sales Count as Line usually, or change it too?
                // Let's keep Sales Count as Line for contrast if Bar is selected.
                // If Line is selected, all Line.
            }
        }

        private SeriesChartType GetSelectedChartType()
        {
            string selected = cmbChartType.SelectedItem?.ToString() ?? "Bar";
            return selected == "Line" ? SeriesChartType.Line : SeriesChartType.Column;
        }
    }
}

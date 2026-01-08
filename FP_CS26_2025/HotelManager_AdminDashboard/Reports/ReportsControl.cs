using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FP_CS26_2025.Services;

namespace FP_CS26_2025.HotelManager_AdminDashboard.Reports
{
    public partial class ReportsControl : UserControl
    {
        private readonly IReportService _reportService;

        public ReportsControl()
        {
            InitializeComponent();
            _reportService = new ReportService();
            
            this.Load += (s, e) => {
                SetupEvents();
                // Initial Load (Current Month)
                btnGenerate.PerformClick();
            };
        }

        private void SetupEvents()
        {
            btnGenerate.Click += BtnGenerate_Click;
            cmbReportType.SelectedIndexChanged += (s, e) => UpdateDatePickerFormat();
        }

        private void UpdateDatePickerFormat()
        {
            string type = cmbReportType.SelectedItem.ToString();
            if (type == "Yearly")
            {
                dtPicker.Format = DateTimePickerFormat.Custom;
                dtPicker.CustomFormat = "yyyy";
                dtPicker.ShowUpDown = true; 
            }
            else if (type == "Monthly")
            {
                dtPicker.Format = DateTimePickerFormat.Custom;
                dtPicker.CustomFormat = "MMMM yyyy";
                dtPicker.ShowUpDown = true; // WinForms DatePicker limitation: picking months usually requires custom handling, but this is simple enough
            }
            else // Weekly
            {
                dtPicker.Format = DateTimePickerFormat.Short;
                dtPicker.ShowUpDown = false;
            }
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            DateTime start, end;
            CalculateDateRange(out start, out end);

            LoadReport(start, end);
        }

        private void CalculateDateRange(out DateTime start, out DateTime end)
        {
            string type = cmbReportType.SelectedItem.ToString();
            DateTime selected = dtPicker.Value;

            if (type == "Weekly")
            {
                // Assuming start of week is Monday? Or Sunday? Let's use Monday as usually business standard
                int diff = (7 + (selected.DayOfWeek - DayOfWeek.Monday)) % 7;
                start = selected.AddDays(-1 * diff).Date;
                end = start.AddDays(6).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            else if (type == "Monthly")
            {
                start = new DateTime(selected.Year, selected.Month, 1);
                end = start.AddMonths(1).AddDays(-1).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            else // Yearly
            {
                start = new DateTime(selected.Year, 1, 1);
                end = new DateTime(selected.Year, 12, 31).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
        }

        private void LoadReport(DateTime start, DateTime end)
        {
            try
            {
                var data = _reportService.GetSalesReport(start, end);
                
                // Bind Grid
                var displayList = data.Select(x => new 
                {
                    ID = x.PaymentId,
                    Date = x.PaymentDate.ToString("yyyy-MM-dd HH:mm"),
                    Guest = x.GuestName,
                    Room = x.RoomNumber,
                    Method = x.PaymentMethod,
                    Amount = $"P {x.Amount:N2}"
                }).ToList();

                dgvReports.DataSource = displayList;

                // Update Stats
                decimal totalRev = _reportService.GetTotalRevenue(start, end);
                int count = _reportService.GetTransactionCount(start, end);

                lblTotalRevenue.Text = $"Total Revenue: P {totalRev:N2}";
                lblTotalTransactions.Text = $"Total Transactions: {count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

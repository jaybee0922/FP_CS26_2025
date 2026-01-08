using System;
using System.Collections.Generic;

namespace FP_CS26_2025.Services
{
    public interface IReportService
    {
        IEnumerable<SalesReportItem> GetSalesReport(DateTime startDate, DateTime endDate);
        decimal GetTotalRevenue(DateTime startDate, DateTime endDate);
        int GetTransactionCount(DateTime startDate, DateTime endDate);
        Dictionary<string, decimal> GetRevenueData(string period);
        List<GraphDataPoint> GetGraphData(string period);
    }

    public class GraphDataPoint
    {
        public string Label { get; set; }
        public decimal Revenue { get; set; }
        public int Count { get; set; }
    }

    public class SalesReportItem
    {
        public int PaymentId { get; set; }
        public string ReservationId { get; set; }
        public string GuestName { get; set; }
        public string RoomNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
    }
}

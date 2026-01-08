using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FP_CS26_2025.Data;

namespace FP_CS26_2025.Services
{
    public class ReportService : IReportService
    {
        public IEnumerable<SalesReportItem> GetSalesReport(DateTime startDate, DateTime endDate)
        {
            var list = new List<SalesReportItem>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT 
                        p.PaymentID, p.ReservationID, p.Amount, p.PaymentDate, p.PaymentMethod,
                        g.FirstName + ' ' + g.LastName as GuestName,
                        r.RoomNumber
                    FROM Payments p
                    JOIN Reservations res ON p.ReservationID = res.ReservationID
                    JOIN Guests g ON res.GuestID = g.GuestID
                    JOIN Rooms r ON res.RoomNumber = r.RoomNumber
                    WHERE p.PaymentDate >= @Start AND p.PaymentDate <= @End
                    ORDER BY p.PaymentDate DESC";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Start", startDate);
                    cmd.Parameters.AddWithValue("@End", endDate);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new SalesReportItem
                            {
                                PaymentId = reader.GetInt32(0),
                                ReservationId = reader.GetString(1),
                                Amount = reader.GetDecimal(2),
                                PaymentDate = reader.GetDateTime(3),
                                PaymentMethod = reader.IsDBNull(4) ? "Cash" : reader.GetString(4),
                                GuestName = reader.GetString(5),
                                RoomNumber = reader.GetString(6)
                            });
                        }
                    }
                }
            }
            return list;
        }

        public decimal GetTotalRevenue(DateTime startDate, DateTime endDate)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT ISNULL(SUM(Amount), 0) FROM Payments WHERE PaymentDate >= @Start AND PaymentDate <= @End";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Start", startDate);
                    cmd.Parameters.AddWithValue("@End", endDate);
                    return (decimal)cmd.ExecuteScalar();
                }
            }
        }

        public int GetTransactionCount(DateTime startDate, DateTime endDate)
        {
             using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Payments WHERE PaymentDate >= @Start AND PaymentDate <= @End";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Start", startDate);
                    cmd.Parameters.AddWithValue("@End", endDate);
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public Dictionary<string, decimal> GetRevenueData(string period)
        {
            // Keeping for backward compatibility if needed, logic is similar to GetGraphData but simpler
            var data = new Dictionary<string, decimal>();
            var graphData = GetGraphData(period);
            foreach(var item in graphData)
            {
                data[item.Label] = item.Revenue;
            }
            return data;
        }

        public List<GraphDataPoint> GetGraphData(string period)
        {
            var data = new List<GraphDataPoint>();
            string query = "";

            switch (period.ToLower())
            {
                case "yearly":
                    // Last 5 years
                    query = @"
                        SELECT DATENAME(year, PaymentDate) as Label, SUM(Amount) as Revenue, COUNT(*) as Count
                        FROM Payments
                        WHERE PaymentDate >= DATEADD(year, -5, GETDATE())
                        GROUP BY DATENAME(year, PaymentDate)
                        ORDER BY Label"; 
                    break;
                case "monthly":
                    // Last 12 months
                    query = @"
                        SELECT FORMAT(PaymentDate, 'MMM yyyy') as Label, SUM(Amount) as Revenue, COUNT(*) as Count,
                               YEAR(PaymentDate) as Y, MONTH(PaymentDate) as M
                        FROM Payments
                        WHERE PaymentDate >= DATEADD(month, -11, GETDATE())
                        GROUP BY FORMAT(PaymentDate, 'MMM yyyy'), YEAR(PaymentDate), MONTH(PaymentDate)
                        ORDER BY Y, M";
                    break;
                case "weekly":
                default:
                    // Last 8 weeks
                    query = @"
                        SELECT 'W' + CAST(DATEPART(week, PaymentDate) as VARCHAR) + '-' + CAST(YEAR(PaymentDate) as VARCHAR) as Label, 
                               SUM(Amount) as Revenue, COUNT(*) as Count,
                               YEAR(PaymentDate) as Y, DATEPART(week, PaymentDate) as W
                        FROM Payments
                        WHERE PaymentDate >= DATEADD(week, -8, GETDATE())
                        GROUP BY 'W' + CAST(DATEPART(week, PaymentDate) as VARCHAR) + '-' + CAST(YEAR(PaymentDate) as VARCHAR), YEAR(PaymentDate), DATEPART(week, PaymentDate)
                        ORDER BY Y, W";
                    break;
            }

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            data.Add(new GraphDataPoint
                            {
                                Label = reader["Label"].ToString(),
                                Revenue = reader["Revenue"] != DBNull.Value ? (decimal)reader["Revenue"] : 0,
                                Count = reader["Count"] != DBNull.Value ? (int)reader["Count"] : 0
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching graph data: {ex.Message}");
            }
            return data;
        }
    }
}

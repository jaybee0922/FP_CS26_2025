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
    }
}

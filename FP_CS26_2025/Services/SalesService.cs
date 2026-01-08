using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FP_CS26_2025.Data;

namespace FP_CS26_2025.Services
{
    public class SalesService
    {
        public List<SalesTrendData> GetSalesTrend(string period)
        {
            var data = new List<SalesTrendData>();
            string query = "";

            switch (period.ToLower())
            {
                case "yearly":
                    query = @"
                        SELECT DATENAME(year, PaymentDate) as Label, 
                               SUM(Amount) as Revenue, 
                               SUM(Amount) * 0.3 as Profit, -- Estimated 30% profit margin
                               COUNT(*) as SalesCount
                        FROM Payments
                        WHERE PaymentDate >= DATEADD(year, -5, GETDATE())
                        GROUP BY DATENAME(year, PaymentDate)
                        ORDER BY Label";
                    break;
                case "monthly":
                    query = @"
                        SELECT FORMAT(PaymentDate, 'MMM yyyy') as Label, 
                               SUM(Amount) as Revenue, 
                               SUM(Amount) * 0.3 as Profit,
                               COUNT(*) as SalesCount,
                               YEAR(PaymentDate) as Y, MONTH(PaymentDate) as M
                        FROM Payments
                        WHERE PaymentDate >= DATEADD(month, -11, GETDATE())
                        GROUP BY FORMAT(PaymentDate, 'MMM yyyy'), YEAR(PaymentDate), MONTH(PaymentDate)
                        ORDER BY Y, M";
                    break;
                case "weekly":
                default:
                    query = @"
                        SELECT 'W' + CAST(DATEPART(week, PaymentDate) as VARCHAR) + '-' + CAST(YEAR(PaymentDate) as VARCHAR) as Label, 
                               SUM(Amount) as Revenue, 
                               SUM(Amount) * 0.3 as Profit,
                               COUNT(*) as SalesCount,
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
                            data.Add(new SalesTrendData
                            {
                                Label = reader["Label"].ToString(),
                                Revenue = reader["Revenue"] != DBNull.Value ? (decimal)reader["Revenue"] : 0,
                                Profit = reader["Profit"] != DBNull.Value ? (decimal)reader["Profit"] : 0,
                                SalesCount = reader["SalesCount"] != DBNull.Value ? (int)reader["SalesCount"] : 0
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching sales trend: {ex.Message}");
            }
            return data;
        }

        public List<SalesDistributionData> GetSalesDistribution(string category, string period, string type)
        {
            var data = new List<SalesDistributionData>();
            string groupBy = "";
            string join = "";
            string selectLabel = "";

            // Determine Grouping and Joins
            switch (category.ToLower())
            {
                case "room type":
                case "roomtype":
                    selectLabel = "rt.TypeName";
                    join = @"JOIN Reservations res ON p.ReservationID = res.ReservationID
                             JOIN Rooms r ON res.RoomNumber = r.RoomNumber
                             JOIN RoomTypes rt ON r.RoomTypeID = rt.RoomTypeID";
                    groupBy = "rt.TypeName";
                    break;
                case "payment method":
                case "paymentmethod":
                    selectLabel = "p.PaymentMethod";
                    groupBy = "p.PaymentMethod";
                    break;
                default:
                    // Default fallback
                    selectLabel = "'Unknown'";
                    groupBy = "";
                    break;
            }

            // Determine Time Filter
            string dateFilter = "";
            switch (period.ToLower())
            {
                case "this week":
                    dateFilter = "p.PaymentDate >= DATEADD(week, DATEDIFF(week, 0, GETDATE()), 0)";
                    break;
                case "current month":
                    dateFilter = "p.PaymentDate >= DATEADD(month, DATEDIFF(month, 0, GETDATE()), 0)";
                    break;
                case "year-to-date":
                    dateFilter = "p.PaymentDate >= DATEADD(year, DATEDIFF(year, 0, GETDATE()), 0)";
                    break;
                default: // Last 30 days default
                     dateFilter = "p.PaymentDate >= DATEADD(day, -30, GETDATE())";
                     break;
            }

            // Determine Value Selection (Metric)
            string valueExpression = "SUM(p.Amount)"; // Default Revenue
            switch(type.ToLower())
            {
                case "sales": // Sales Count
                    valueExpression = "COUNT(*)";
                    break;
                case "profit": // Estimated 30%
                    valueExpression = "SUM(p.Amount) * 0.3";
                    break;
                case "revenue":
                default:
                    valueExpression = "SUM(p.Amount)";
                    break;
            }

            if (string.IsNullOrEmpty(groupBy))
            {
                return data; 
            }

            string query = $@"
                SELECT {selectLabel} as Label, {valueExpression} as Value
                FROM Payments p
                {join}
                WHERE {dateFilter}
                GROUP BY {groupBy}
                ORDER BY Value DESC";

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
                            data.Add(new SalesDistributionData
                            {
                                Label = reader["Label"].ToString(),
                                Value = reader["Value"] != DBNull.Value ? (decimal)reader["Value"] : 0
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching sales distribution: {ex.Message}");
            }

            return data;
        }
    }

    public class SalesTrendData
    {
        public string Label { get; set; }
        public decimal Revenue { get; set; }
        public decimal Profit { get; set; }
        public int SalesCount { get; set; }
    }

    public class SalesDistributionData
    {
        public string Label { get; set; }
        public decimal Value { get; set; }
    }
}

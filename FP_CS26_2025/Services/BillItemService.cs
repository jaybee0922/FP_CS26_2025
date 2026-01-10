using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FP_CS26_2025.Data;

namespace FP_CS26_2025.Services
{
    /// <summary>
    /// Service for managing itemized bill charges.
    /// Supports adding extra charges beyond room fees (e.g., Mini-bar, Late Checkout).
    /// </summary>
    public class BillItemService
    {
        private readonly string _connectionString;

        public BillItemService()
        {
            _connectionString = DatabaseHelper.ConnectionString;
        }

        /// <summary>
        /// Adds an itemized charge to a reservation's bill.
        /// </summary>
        public void AddBillItem(string reservationId, string description, int quantity, decimal unitPrice)
        {
            if (string.IsNullOrWhiteSpace(reservationId))
                throw new ArgumentException("Reservation ID is required.");

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description is required.");

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            if (unitPrice < 0)
                throw new ArgumentException("Unit price cannot be negative.");

            decimal totalPrice = quantity * unitPrice;

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    const string query = @"
                        INSERT INTO BillItems (ReservationID, Description, Quantity, UnitPrice, TotalPrice)
                        VALUES (@ResID, @Desc, @Qty, @UnitPrice, @Total)";

                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ResID", reservationId);
                        cmd.Parameters.AddWithValue("@Desc", description);
                        cmd.Parameters.AddWithValue("@Qty", quantity);
                        cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                        cmd.Parameters.AddWithValue("@Total", totalPrice);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to add bill item: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Retrieves all itemized charges for a specific reservation.
        /// </summary>
        public List<BillItemData> GetBillItems(string reservationId)
        {
            var items = new List<BillItemData>();

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    const string query = @"
                        SELECT ItemID, Description, Quantity, UnitPrice, TotalPrice, CreatedAt
                        FROM BillItems
                        WHERE ReservationID = @ResID
                        ORDER BY CreatedAt";

                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ResID", reservationId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                items.Add(new BillItemData
                                {
                                    ItemId = reader.GetInt32(0),
                                    Description = reader.GetString(1),
                                    Quantity = reader.GetInt32(2),
                                    UnitPrice = reader.GetDecimal(3),
                                    TotalPrice = reader.GetDecimal(4),
                                    CreatedAt = reader.GetDateTime(5)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve bill items: {ex.Message}", ex);
            }

            return items;
        }

        /// <summary>
        /// Calculates the total of all extra charges for a reservation.
        /// </summary>
        public decimal GetTotalExtraCharges(string reservationId)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    const string query = @"
                        SELECT ISNULL(SUM(TotalPrice), 0)
                        FROM BillItems
                        WHERE ReservationID = @ResID";

                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ResID", reservationId);
                        return Convert.ToDecimal(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to calculate extra charges: {ex.Message}", ex);
            }
        }
    }

    /// <summary>
    /// Data transfer object for bill items.
    /// </summary>
    public class BillItemData
    {
        public int ItemId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

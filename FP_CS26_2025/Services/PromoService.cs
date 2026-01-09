using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FP_CS26_2025.Data;
using FP_CS26_2025.Services.Models;

namespace FP_CS26_2025.Services
{
    /// <summary>
    /// SRP: Handles only database operations and logic related to Promo Codes.
    /// Robustness: Implements try-catch blocks to prevent crashes.
    /// </summary>
    public class PromoService : IPromoService
    {
        private readonly string _connectionString;

        public PromoService()
        {
            _connectionString = DatabaseHelper.ConnectionString;
        }

        public PromoCode GetPromoCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) return null;

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    const string query = "SELECT PromoID, Code, DiscountType, DiscountValue, ExpiryDate, IsActive FROM PromoCodes WHERE TRIM(Code) = @Code";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Code", code.Trim());
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new PromoCode
                                {
                                    PromoID = reader.GetInt32(0),
                                    Code = reader.GetString(1),
                                    DiscountType = reader.GetString(2),
                                    DiscountValue = reader.GetDecimal(3),
                                    ExpiryDate = reader.GetDateTime(4),
                                    IsActive = reader.GetBoolean(5)
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"PromoService Error (Get): {ex.Message}");
            }
            return null;
        }

        public IEnumerable<PromoCode> GetAllPromos()
        {
            var promos = new List<PromoCode>();
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    const string query = "SELECT PromoID, Code, DiscountType, DiscountValue, ExpiryDate, IsActive FROM PromoCodes ORDER BY CreatedAt DESC";
                    using (var cmd = new SqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            promos.Add(new PromoCode
                            {
                                PromoID = reader.GetInt32(0),
                                Code = reader.GetString(1),
                                DiscountType = reader.GetString(2),
                                DiscountValue = reader.GetDecimal(3),
                                ExpiryDate = reader.GetDateTime(4),
                                IsActive = reader.GetBoolean(5)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"PromoService Error (GetAll): {ex.Message}");
            }
            return promos;
        }

        public bool AddPromoCode(PromoCode promo)
        {
            if (promo == null) return false;

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    const string query = @"
                        INSERT INTO PromoCodes (Code, DiscountType, DiscountValue, ExpiryDate, IsActive)
                        VALUES (@Code, @Type, @Value, @Expiry, @Active)";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Code", promo.Code.Trim());
                        cmd.Parameters.AddWithValue("@Type", promo.DiscountType);
                        cmd.Parameters.AddWithValue("@Value", promo.DiscountValue);
                        cmd.Parameters.AddWithValue("@Expiry", promo.ExpiryDate);
                        cmd.Parameters.AddWithValue("@Active", promo.IsActive);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"PromoService Error (Add): {ex.Message}");
                return false;
            }
        }

        public bool DeletePromoCode(int promoId)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    const string query = "DELETE FROM PromoCodes WHERE PromoID = @ID";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", promoId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"PromoService Error (Delete): {ex.Message}");
                return false;
            }
        }

        public bool TogglePromoStatus(int promoId, bool isActive)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    const string query = "UPDATE PromoCodes SET IsActive = @Active WHERE PromoID = @ID";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Active", isActive);
                        cmd.Parameters.AddWithValue("@ID", promoId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"PromoService Error (Toggle): {ex.Message}");
                return false;
            }
        }
    }
}

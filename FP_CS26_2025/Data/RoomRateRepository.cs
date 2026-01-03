using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FP_CS26_2025.Data
{
    public class RoomRateRepository
    {
        // Strictly using the existing DatabaseHelper for connections
        public List<RoomRate> GetAllRoomRates()
        {
            return SearchRoomRates(null);
        }

        public List<RoomRate> SearchRoomRates(string searchText)
        {
            var list = new List<RoomRate>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT RoomType, PricePerNight, Status, CapacityInfo FROM RoomRates";
                
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query += " WHERE RoomType LIKE @Search";
                }

                using (var cmd = new SqlCommand(query, conn))
                {
                    if (!string.IsNullOrWhiteSpace(searchText))
                    {
                        cmd.Parameters.AddWithValue("@Search", "%" + searchText.Trim() + "%");
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new RoomRate
                            {
                                RoomType = reader["RoomType"].ToString(),
                                // Safely handle potential parsing errors or different DB types
                                PricePerNight = Convert.ToDecimal(reader["PricePerNight"]),
                                Status = reader["Status"].ToString(),
                                CapacityInfo = reader["CapacityInfo"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }

        public bool UpdateRoomPrice(string roomType, decimal newPrice)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "UPDATE RoomRates SET PricePerNight = @Price WHERE RoomType = @RoomType";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Price", newPrice);
                    cmd.Parameters.AddWithValue("@RoomType", roomType);

                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
        }
    }
}

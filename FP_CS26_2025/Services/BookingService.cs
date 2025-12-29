using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using FP_CS26_2025.Data;

namespace FP_CS26_2025.Services
{
    public class BookingService : IBookingService
    {
        public bool CheckAvailability(DateTime arrival, DateTime departure, string promoCode)
        {
            if (arrival >= departure)
                return false;

            // Simple check: Is there at least one available room?
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(1) FROM Rooms WHERE Status = 'Available'";
                using (var cmd = new SqlCommand(query, conn))
                {
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public decimal CalculateTotal(string roomType, int numRooms, int nights)
        {
            decimal basePrice = 1200m; // Default fallback

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT BasePrice FROM RoomTypes WHERE TypeName = @RoomType";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RoomType", roomType);
                    var result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        basePrice = Convert.ToDecimal(result);
                    }
                }
            }

            return basePrice * numRooms * nights;
        }

        public string ProcessBookingRequest(BookingRequestData request)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Create or Find Guest
                        int guestId;
                        string checkGuest = "SELECT GuestID FROM Guests WHERE Email = @Email OR PhoneNumber = @Phone";
                        using (var cmd = new SqlCommand(checkGuest, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Email", request.Email);
                            cmd.Parameters.AddWithValue("@Phone", request.Phone);
                            var result = cmd.ExecuteScalar();
                            if (result != null)
                            {
                                guestId = Convert.ToInt32(result);
                            }
                            else
                            {
                                string insertGuest = "INSERT INTO Guests (FirstName, LastName, Email, PhoneNumber) OUTPUT INSERTED.GuestID VALUES (@FN, @LN, @Email, @Phone)";
                                using (var insCmd = new SqlCommand(insertGuest, conn, transaction))
                                {
                                    insCmd.Parameters.AddWithValue("@FN", request.FirstName);
                                    insCmd.Parameters.AddWithValue("@LN", request.LastName);
                                    insCmd.Parameters.AddWithValue("@Email", request.Email);
                                    insCmd.Parameters.AddWithValue("@Phone", request.Phone);
                                    guestId = (int)insCmd.ExecuteScalar();
                                }
                            }
                        }

                        // 2. Find an available room of the requested type
                        string roomNumber = "";
                        string findRoom = @"
                            SELECT TOP 1 r.RoomNumber 
                            FROM Rooms r 
                            JOIN RoomTypes rt ON r.RoomTypeID = rt.RoomTypeID 
                            WHERE rt.TypeName = @RoomType AND r.Status = 'Available'";
                        
                        using (var cmd = new SqlCommand(findRoom, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@RoomType", request.RoomType);
                            var result = cmd.ExecuteScalar();
                            if (result != null)
                            {
                                roomNumber = result.ToString();
                            }
                            else
                            {
                                // Fallback: try to find ANY available room if requested type is full? 
                                // Actually, for a service request, we might just store the "RequestedRoomType" 
                                // but the schema requires a RoomNumber. Let's use '101' as fallback or throw error.
                                throw new Exception("No rooms of type " + request.RoomType + " are available.");
                            }
                        }

                        // 3. Generate Reservation ID (e.g., RES-XXXXX)
                        string resId = "RES-" + DateTime.Now.ToString("yyyyMMdd") + new Random().Next(1000, 9999).ToString();

                        // 4. Create Reservation
                        string insertRes = @"
                            INSERT INTO Reservations (ReservationID, GuestID, RoomNumber, CheckInDate, CheckOutDate, Status, TotalAmount)
                            VALUES (@ResID, @GuestID, @RoomNum, @CheckIn, @CheckOut, 'Pending', @Total)";
                        
                        using (var cmd = new SqlCommand(insertRes, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@ResID", resId);
                            cmd.Parameters.AddWithValue("@GuestID", guestId);
                            cmd.Parameters.AddWithValue("@RoomNum", roomNumber);
                            cmd.Parameters.AddWithValue("@CheckIn", request.CheckInDate);
                            cmd.Parameters.AddWithValue("@CheckOut", request.CheckOutDate);
                            cmd.Parameters.AddWithValue("@Total", request.TotalPrice);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return resId;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}

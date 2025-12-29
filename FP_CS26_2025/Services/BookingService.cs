using System;
using System.Data.SqlClient;
using FP_CS26_2025.Data;
using FP_CS26_2025.Services.Models;

namespace FP_CS26_2025.Services
{
    /// <summary>
    /// Service for handling hotel bookings with robustness and security.
    /// Following SRP: Only handles booking-related database operations.
    /// </summary>
    public class BookingService : IBookingService
    {
        private readonly string _connectionString;

        public BookingService()
        {
            _connectionString = DatabaseHelper.ConnectionString;
        }

        /// <summary>
        /// Precision Availability Check: Verifies if a room of the requested type is free for the entire duration.
        /// Accounts for overlapping reservations to prevent double-booking (Security & Robustness).
        /// </summary>
        public bool CheckAvailability(DateTime arrival, DateTime departure, string roomType)
        {
            if (arrival.Date >= departure.Date) return false;

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    // Accurate Overlap Logic: (RequestedStart < ReservedEnd) AND (RequestedEnd > ReservedStart)
                    const string query = @"
                        SELECT COUNT(*) 
                        FROM Rooms r
                        JOIN RoomTypes rt ON r.RoomTypeID = rt.RoomTypeID
                        WHERE rt.TypeName = @RoomType 
                        AND r.Status <> 'Maintenance'
                        AND r.RoomNumber NOT IN (
                            SELECT RoomNumber 
                            FROM Reservations 
                            WHERE Status IN ('Pending', 'CheckedIn')
                            AND (@Arrival < CheckOutDate AND @Departure > CheckInDate)
                        )";

                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@RoomType", roomType);
                        cmd.Parameters.AddWithValue("@Arrival", arrival.Date);
                        cmd.Parameters.AddWithValue("@Departure", departure.Date);

                        var availableCount = Convert.ToInt32(cmd.ExecuteScalar());
                        return availableCount > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Robustness: Log or handle exception
                throw new Exception("Security Error: Failed to verify room availability securely.", ex);
            }
        }

        public decimal CalculateTotal(string roomType, int numRooms, int nights)
        {
            if (nights <= 0) nights = 1;
            if (numRooms <= 0) numRooms = 1;

            decimal basePrice = 0;

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    const string query = "SELECT BasePrice FROM RoomTypes WHERE TypeName = @RoomType";
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
            }
            catch (Exception ex)
            {
                throw new Exception("Financial Error: Could not retrieve precise rate for calculation.", ex);
            }

            // Financial Accuracy: Decimals used throughout
            return basePrice * numRooms * nights;
        }

        public string ProcessBookingRequest(BookingRequestData request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1. Get or Create Guest securely 
                            var guestId = GetOrCreateGuest(request, conn, transaction);

                            // 2. Find an available room specifically for these dates (Security: Double check availability)
                            var roomNumber = FindActualAvailableRoom(request, conn, transaction);

                            // 3. Generate Reservation ID
                            var resId = GenerateReservationId();

                            // 4. Create Reservation Record (Financial Precision & Security)
                            CreateReservationRecord(resId, guestId, roomNumber, request, conn, transaction);

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
            catch (Exception ex)
            {
                throw new Exception("Booking Failure: System could not process your request safely. Please try again.", ex);
            }
        }

        private int GetOrCreateGuest(BookingRequestData request, SqlConnection conn, SqlTransaction trans)
        {
            // Security: Parameterized SQL prevents injection
            const string checkGuest = "SELECT GuestID FROM Guests WHERE Email = @Email";
            using (var cmd = new SqlCommand(checkGuest, conn, trans))
            {
                cmd.Parameters.AddWithValue("@Email", request.Email.Trim().ToLower());
                var result = cmd.ExecuteScalar();
                if (result != null) return Convert.ToInt32(result);

                const string insertGuest = @"
                    INSERT INTO Guests (FirstName, LastName, Email, PhoneNumber) 
                    OUTPUT INSERTED.GuestID 
                    VALUES (@FN, @LN, @Email, @Phone)";
                
                using (var insCmd = new SqlCommand(insertGuest, conn, trans))
                {
                    insCmd.Parameters.AddWithValue("@FN", request.FirstName.Trim());
                    insCmd.Parameters.AddWithValue("@LN", request.LastName.Trim());
                    insCmd.Parameters.AddWithValue("@Email", request.Email.Trim().ToLower());
                    insCmd.Parameters.AddWithValue("@Phone", request.Phone.Trim());
                    return (int)insCmd.ExecuteScalar();
                }
            }
        }

        private string FindActualAvailableRoom(BookingRequestData request, SqlConnection conn, SqlTransaction trans)
        {
            // Robustness: Ensuring the room is truly available for the duration
            const string query = @"
                SELECT TOP 1 r.RoomNumber 
                FROM Rooms r
                JOIN RoomTypes rt ON r.RoomTypeID = rt.RoomTypeID
                WHERE rt.TypeName = @RoomType 
                AND r.Status <> 'Maintenance'
                AND r.RoomNumber NOT IN (
                    SELECT RoomNumber 
                    FROM Reservations 
                    WHERE Status IN ('Pending', 'CheckedIn')
                    AND (@Arrival < CheckOutDate AND @Departure > CheckInDate)
                )";

            using (var cmd = new SqlCommand(query, conn, trans))
            {
                cmd.Parameters.AddWithValue("@RoomType", request.RoomType);
                cmd.Parameters.AddWithValue("@Arrival", request.CheckInDate.Date);
                cmd.Parameters.AddWithValue("@Departure", request.CheckOutDate.Date);

                var result = cmd.ExecuteScalar();
                if (result != null) return result.ToString();

                throw new InvalidOperationException($"Reliability Error: Room of type '{request.RoomType}' is no longer available for your selected dates.");
            }
        }

        private string GenerateReservationId()
        {
            return "RES-" + DateTime.Now.ToString("yyyyMMdd") + new Random().Next(1000, 9999);
        }

        private void CreateReservationRecord(string resId, int guestId, string roomNumber, BookingRequestData request, SqlConnection conn, SqlTransaction trans)
        {
            const string insertRes = @"
                INSERT INTO Reservations (ReservationID, GuestID, RoomNumber, CheckInDate, CheckOutDate, Status, TotalAmount, RoomType, NumAdults, NumChildren, NumRooms)
                VALUES (@ResID, @GuestID, @RoomNum, @CheckIn, @CheckOut, 'Pending', @Total, @RoomType, @Adults, @Children, @Rooms)";

            using (var cmd = new SqlCommand(insertRes, conn, trans))
            {
                cmd.Parameters.AddWithValue("@ResID", resId);
                cmd.Parameters.AddWithValue("@GuestID", guestId);
                cmd.Parameters.AddWithValue("@RoomNum", roomNumber);
                cmd.Parameters.AddWithValue("@CheckIn", request.CheckInDate.Date);
                cmd.Parameters.AddWithValue("@CheckOut", request.CheckOutDate.Date);
                cmd.Parameters.AddWithValue("@Total", request.TotalPrice);
                cmd.Parameters.AddWithValue("@RoomType", request.RoomType);
                cmd.Parameters.AddWithValue("@Adults", request.NumAdults);
                cmd.Parameters.AddWithValue("@Children", request.NumChildren);
                cmd.Parameters.AddWithValue("@Rooms", request.NumRooms);
                cmd.ExecuteNonQuery();
            }
        }
    }
}

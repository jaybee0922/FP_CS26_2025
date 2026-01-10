using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FP_CS26_2025.Data;

namespace FP_CS26_2025.FrontDesk_MVC
{
    /// <summary>
    /// Database-backed implementation of IHotelDataService.
    /// Following SRP: Handles retrieval of hotel state from SQL.
    /// </summary>
    public class SqlHotelDataService : IHotelDataService
    {
        private readonly string _connectionString;

        public SqlHotelDataService()
        {
            _connectionString = DatabaseHelper.ConnectionString;
        }

        public IEnumerable<IRoom> GetAllRooms()
        {
            var rooms = new List<IRoom>();
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    const string query = @"
                        SELECT TRIM(r.RoomNumber), TRIM(rt.TypeName), rt.BasePrice, rt.Capacity, TRIM(r.Status), r.Floor 
                        FROM Rooms r
                        JOIN RoomTypes rt ON r.RoomTypeID = rt.RoomTypeID";
                    
                    using (var cmd = new SqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Safe parsing: RoomNumber is NVARCHAR in SQL but int in model
                            int num = 0;
                            if (reader[0] != DBNull.Value)
                                int.TryParse(reader[0].ToString(), out num);

                            string type = reader.GetString(1);
                            decimal price = reader.GetDecimal(2);
                            int cap = reader.GetInt32(3);
                            int floor = (reader[5] != DBNull.Value) ? reader.GetInt32(5) : 1;
                            string statusStr = reader.GetString(4);

                            RoomStatus status = MapStatus(statusStr);
                            rooms.Add(new GenericRoom(num, type, price, cap, floor) { Status = status });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error fetching rooms: " + ex.Message);
            }
            return rooms;
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            var reservations = new List<Reservation>();
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    // Robust Join: Fetching Guest info and Reservation details in one query
                    const string query = @"
                        SELECT res.ReservationID, g.FirstName + ' ' + g.LastName as GuestName, g.Email, g.PhoneNumber,
                               res.RoomNumber, res.CheckInDate, res.CheckOutDate, res.Status, res.TotalAmount,
                               res.RoomType, res.NumAdults, res.NumChildren, res.NumRooms
                        FROM Reservations res
                        JOIN Guests g ON res.GuestID = g.GuestID
                        ORDER BY res.CheckInDate DESC";
                    
                    using (var cmd = new SqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var res = new Reservation
                            {
                                ReservationId = reader["ReservationID"].ToString(),
                                Guest = new Guest { 
                                    FullName = reader["GuestName"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    PhoneNumber = reader["PhoneNumber"].ToString()
                                },
                                CheckInDate = (DateTime)reader["CheckInDate"],
                                CheckOutDate = (DateTime)reader["CheckOutDate"],
                                Status = reader["Status"].ToString(),
                                TotalPrice = reader["TotalAmount"] != DBNull.Value ? (decimal)reader["TotalAmount"] : 0,
                                RoomType = reader["RoomType"]?.ToString(),
                                NumAdults = reader["NumAdults"] != DBNull.Value ? (int)reader["NumAdults"] : 1,
                                NumChildren = reader["NumChildren"] != DBNull.Value ? (int)reader["NumChildren"] : 0,
                                NumRooms = reader["NumRooms"] != DBNull.Value ? (int)reader["NumRooms"] : 1,
                                IsCheckedIn = reader["Status"].ToString() == "CheckedIn"
                            };

                            // Abstraction: Mocking a room object for the model's interface requirement
                            res.AssignedRoom = new GenericRoom(
                                string.IsNullOrEmpty(reader["RoomNumber"].ToString()) ? 0 : int.Parse(reader["RoomNumber"].ToString()), 
                                res.RoomType, 0, 0);

                            reservations.Add(res);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error fetching reservations: " + ex.Message);
            }
            return reservations;
        }

        public IEnumerable<Bill> GetAllBills()
        {
            // Placeholder for billing expansion
            return new List<Bill>();
        }

        public void AddReservation(Reservation reservation)
        {
            if (reservation == null) return;

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1. Insert Guest
                            string firstName = reservation.Guest.FullName;
                            string lastName = "";
                            if (reservation.Guest.FullName.Contains(" "))
                            {
                                int lastSpace = reservation.Guest.FullName.LastIndexOf(" ");
                                firstName = reservation.Guest.FullName.Substring(0, lastSpace);
                                lastName = reservation.Guest.FullName.Substring(lastSpace + 1);
                            }

                            const string insertGuestQuery = @"
                                INSERT INTO Guests (FirstName, LastName, Email, PhoneNumber) 
                                VALUES (@FirstName, @LastName, @Email, @Phone);
                                SELECT SCOPE_IDENTITY();";
                            
                            int guestId;
                            using (var cmd = new SqlCommand(insertGuestQuery, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@FirstName", firstName);
                                cmd.Parameters.AddWithValue("@LastName", lastName);
                                cmd.Parameters.AddWithValue("@Email", reservation.Guest.Email ?? "");
                                cmd.Parameters.AddWithValue("@Phone", reservation.Guest.PhoneNumber ?? "");
                                guestId = Convert.ToInt32(cmd.ExecuteScalar());
                            }

                            // 2. Insert Reservation
                            const string insertResQuery = @"
                                INSERT INTO Reservations (ReservationID, GuestID, RoomNumber, CheckInDate, CheckOutDate, Status, TotalAmount, RoomType, NumAdults, NumChildren, NumRooms)
                                VALUES (@ResId, @GuestId, @RoomNum, @CheckIn, @CheckOut, @Status, @Amount, @Type, @Adults, @Children, @Rooms)";
                            
                            using (var cmd = new SqlCommand(insertResQuery, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@ResId", reservation.ReservationId);
                                cmd.Parameters.AddWithValue("@GuestId", guestId);
                                cmd.Parameters.AddWithValue("@RoomNum", reservation.AssignedRoom.RoomNumber.ToString());
                                cmd.Parameters.AddWithValue("@CheckIn", reservation.CheckInDate);
                                cmd.Parameters.AddWithValue("@CheckOut", reservation.CheckOutDate);
                                cmd.Parameters.AddWithValue("@Status", reservation.Status);
                                cmd.Parameters.AddWithValue("@Amount", reservation.TotalPrice);
                                cmd.Parameters.AddWithValue("@Type", reservation.RoomType ?? "");
                                cmd.Parameters.AddWithValue("@Adults", reservation.NumAdults);
                                cmd.Parameters.AddWithValue("@Children", reservation.NumChildren);
                                cmd.Parameters.AddWithValue("@Rooms", reservation.NumRooms);
                                cmd.ExecuteNonQuery();
                            }

                            // 3. Update Room Status ONLY if not Pending (Strict Workflow)
                            if (reservation.Status != "Pending")
                            {
                                const string updateRoomQuery = "UPDATE Rooms SET Status = 'Reserved' WHERE TRIM(RoomNumber) = TRIM(@RoomNum)";
                                using (var cmd = new SqlCommand(updateRoomQuery, conn, trans))
                                {
                                    cmd.Parameters.AddWithValue("@RoomNum", reservation.AssignedRoom.RoomNumber.ToString());
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error adding reservation: " + ex.Message);
                throw new Exception("Failed to save reservation to database.", ex);
            }
        }

        public void UpdateRoomStatus(int roomNumber, RoomStatus status)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    const string query = "UPDATE Rooms SET Status = @Status WHERE TRIM(RoomNumber) = TRIM(@Num)";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", StatusToString(status));
                        cmd.Parameters.AddWithValue("@Num", roomNumber.ToString());
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                 System.Diagnostics.Debug.WriteLine("Error updating room status: " + ex.Message);
            }
        }

        public void AddBill(Bill bill) { }

        public IRoom GetRoomByNumber(int roomNumber)
        {
            return GetAllRooms().FirstOrDefault(r => r.RoomNumber == roomNumber);
        }

        public void ApproveReservation(string reservationId)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1. Get the RoomNumber for this reservation first
                            string roomNumber = "";
                            const string getRoomQuery = "SELECT RoomNumber FROM Reservations WHERE ReservationID = @ResId";
                            using (var cmd = new SqlCommand(getRoomQuery, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@ResId", reservationId);
                                var result = cmd.ExecuteScalar();
                                if (result == null) throw new Exception("Reservation not found.");
                                roomNumber = result.ToString();
                            }

                            // 2. Update Reservation Status to Approved
                            const string updateResQuery = "UPDATE Reservations SET Status = 'Approved' WHERE ReservationID = @ResId";
                            using (var cmd = new SqlCommand(updateResQuery, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@ResId", reservationId);
                                cmd.ExecuteNonQuery();
                            }

                            // 3. Update Room Status to Reserved (Actual Lock)
                            const string updateRoomQuery = "UPDATE Rooms SET Status = 'Reserved' WHERE TRIM(RoomNumber) = TRIM(@RoomNum)";
                            using (var cmd = new SqlCommand(updateRoomQuery, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@RoomNum", roomNumber);
                                cmd.ExecuteNonQuery();
                            }

                            trans.Commit();
                        }
                        catch
                        {
                            trans.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error approving reservation: " + ex.Message);
                throw new Exception("Failed to approve reservation. Please try again.", ex);
            }
        }

        public void SavePayment(string reservationId, decimal amount, string paymentMethod)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    const string query = @"
                        INSERT INTO Payments (ReservationID, Amount, PaymentMethod, PaymentDate)
                        VALUES (@ResId, @Amount, @Method, @Date)";
                    
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ResId", reservationId);
                        cmd.Parameters.AddWithValue("@Amount", amount);
                        cmd.Parameters.AddWithValue("@Method", paymentMethod);
                        cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error saving payment: " + ex.Message);
                throw new Exception("Failed to save payment. Please try again.", ex);
            }
        }

        public bool IsRoomAvailable(int roomNumber, DateTime start, DateTime end)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    // Robust overlap check for a specific room
                    const string query = @"
                        SELECT COUNT(*) 
                        FROM Reservations 
                        WHERE TRIM(RoomNumber) = TRIM(@RoomNum)
                        AND Status IN ('Pending', 'Approved', 'CheckedIn')
                        AND (@Start < CheckOutDate AND @End > CheckInDate)";
                    
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@RoomNum", roomNumber.ToString());
                        cmd.Parameters.AddWithValue("@Start", start.Date);
                        cmd.Parameters.AddWithValue("@End", end.Date);
                        
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count == 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error checking room availability: " + ex.Message);
                return false;
            }
        }

        public void UpdateReservationStatus(string reservationId, string status)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    const string query = "UPDATE Reservations SET Status = @Status WHERE ReservationID = @ResId";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@ResId", reservationId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        
                        if (rowsAffected == 0)
                        {
                            throw new InvalidOperationException($"Reservation {reservationId} not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error updating reservation status: " + ex.Message);
                throw new Exception("Failed to update reservation status. Please try again.", ex);
            }
        }

        public IEnumerable<PaymentRecord> GetAllPayments()
        {
            var payments = new List<PaymentRecord>();
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    const string query = @"
                        SELECT p.PaymentID, p.ReservationID, g.FirstName + ' ' + g.LastName as GuestName, 
                               p.Amount, p.PaymentMethod, p.PaymentDate
                        FROM Payments p
                        JOIN Reservations r ON p.ReservationID = r.ReservationID
                        JOIN Guests g ON r.GuestID = g.GuestID
                        ORDER BY p.PaymentDate DESC";
                    
                    using (var cmd = new SqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            payments.Add(new PaymentRecord
                            {
                                PaymentId = (int)reader["PaymentID"],
                                ReservationId = reader["ReservationID"].ToString(),
                                GuestName = reader["GuestName"].ToString(),
                                Amount = (decimal)reader["Amount"],
                                PaymentMethod = reader["PaymentMethod"].ToString(),
                                PaymentDate = (DateTime)reader["PaymentDate"]
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error fetching payments: " + ex.Message);
            }
            return payments;
        }

        private RoomStatus MapStatus(string status)
        {
            if (status == "Under Maintenance") return RoomStatus.UnderMaintenance;
            if (status == "Out of Service") return RoomStatus.OutOfService;
            if (Enum.TryParse(status.Replace(" ", ""), out RoomStatus result)) return result;
            return RoomStatus.Available;
        }

        private string StatusToString(RoomStatus status)
        {
            switch (status)
            {
                case RoomStatus.UnderMaintenance:
                    return "UnderMaintenance";
                case RoomStatus.OutOfService:
                    return "OutOfService";
                default:
                    return status.ToString();
            }
        }

        public void SaveMonthlyReport(int month, int year, decimal revenue, int count)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    // Upsert Logic: Update if exists, Insert if new
                    const string query = @"
                        MERGE MonthlyReports AS target
                        USING (SELECT @Month AS Month, @Year AS Year) AS source
                        ON (target.Month = source.Month AND target.Year = source.Year)
                        WHEN MATCHED THEN
                            UPDATE SET TotalRevenue = @Revenue, TransactionCount = @Count, GeneratedDate = GETDATE()
                        WHEN NOT MATCHED THEN
                            INSERT ([Month], [Year], TotalRevenue, TransactionCount, GeneratedDate)
                            VALUES (@Month, @Year, @Revenue, @Count, GETDATE());";

                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Month", month);
                        cmd.Parameters.AddWithValue("@Year", year);
                        cmd.Parameters.AddWithValue("@Revenue", revenue);
                        cmd.Parameters.AddWithValue("@Count", count);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error saving monthly report: " + ex.Message);
                throw new Exception("Failed to save annual report to database.", ex);
            }
        }

        public System.Data.DataTable GetArchivedReports()
        {
            var dt = new System.Data.DataTable();
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT [Month], [Year], TotalRevenue, TransactionCount, GeneratedDate FROM MonthlyReports ORDER BY [Year] DESC, [Month] DESC";
                    using (var adapter = new SqlDataAdapter(query, conn))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error fetching archived reports: " + ex.Message);
                throw new Exception("Data Access Error: Could not retrieve archived reports. Please check your connection.", ex);
            }
            return dt;
        }

        public DataTable GetAllPhysicalRooms()
        {
            var dt = new DataTable();
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    const string query = @"
                        SELECT r.RoomNumber, rt.TypeName as RoomType, r.Floor, r.Status, r.RoomTypeID, r.BedConfig, r.ViewType
                        FROM Rooms r
                        JOIN RoomTypes rt ON r.RoomTypeID = rt.RoomTypeID
                        ORDER BY r.Floor, r.RoomNumber";
                    using (var adapter = new SqlDataAdapter(query, conn))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching physical rooms: " + ex.Message, ex);
            }
            return dt;
        }

        public void SavePhysicalRoom(string roomNumber, int roomTypeId, int floor, string status, string bedConfig = "Standard", string viewType = "City View")
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    const string query = @"
                        IF EXISTS (SELECT 1 FROM Rooms WHERE RoomNumber = @Num)
                        BEGIN
                            UPDATE Rooms SET RoomTypeID = @TypeId, Floor = @Floor, Status = @Status, BedConfig = @BedConfig, ViewType = @ViewType WHERE RoomNumber = @Num
                        END
                        ELSE
                        BEGIN
                            INSERT INTO Rooms (RoomNumber, RoomTypeID, Floor, Status, BedConfig, ViewType) VALUES (@Num, @TypeId, @Floor, @Status, @BedConfig, @ViewType)
                        END";
                    
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Num", roomNumber);
                        cmd.Parameters.AddWithValue("@TypeId", roomTypeId);
                        cmd.Parameters.AddWithValue("@Floor", floor);
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@BedConfig", bedConfig);
                        cmd.Parameters.AddWithValue("@ViewType", viewType);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving physical room: " + ex.Message, ex);
            }
        }

        public void DeletePhysicalRoom(string roomNumber)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    const string query = "DELETE FROM Rooms WHERE RoomNumber = @Num";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Num", roomNumber);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting room: " + ex.Message, ex);
            }
        }

        public DataTable GetAllRoomTypes()
        {
            var dt = new DataTable();
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    const string query = "SELECT RoomTypeID, TypeName, BasePrice, Capacity FROM RoomTypes ORDER BY TypeName";
                    using (var adapter = new SqlDataAdapter(query, conn))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching room types: " + ex.Message, ex);
            }
            return dt;
        }

        public void DeleteReservation(string reservationId)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1. Get Room Number to free it up
                            string roomNumber = null;
                            const string getRoomQuery = "SELECT RoomNumber FROM Reservations WHERE ReservationID = @ResId";
                            using (var cmd = new SqlCommand(getRoomQuery, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@ResId", reservationId);
                                var result = cmd.ExecuteScalar();
                                if (result != null) roomNumber = result.ToString();
                            }

                            // 2. Delete Payments (Constraint)
                            const string deletePayQuery = "DELETE FROM Payments WHERE ReservationID = @ResId";
                            using (var cmd = new SqlCommand(deletePayQuery, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@ResId", reservationId);
                                cmd.ExecuteNonQuery();
                            }

                            // 3. Delete Reservation
                            const string deleteResQuery = "DELETE FROM Reservations WHERE ReservationID = @ResId";
                            using (var cmd = new SqlCommand(deleteResQuery, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@ResId", reservationId);
                                int rows = cmd.ExecuteNonQuery();
                                if (rows == 0) throw new Exception("Reservation not found.");
                            }

                            // 4. Update Room Status to Available
                            if (!string.IsNullOrEmpty(roomNumber))
                            {
                                const string updateRoomQuery = "UPDATE Rooms SET Status = 'Available' WHERE TRIM(RoomNumber) = TRIM(@Num)";
                                using (var cmd = new SqlCommand(updateRoomQuery, conn, trans))
                                {
                                    cmd.Parameters.AddWithValue("@Num", roomNumber);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            trans.Commit();
                        }
                        catch
                        {
                            trans.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error deleting reservation: " + ex.Message);
                throw new Exception("Failed to delete reservation.", ex);
            }
        }
    }

    /// <summary>
    /// Concrete implementation of Room for general database mapping.
    /// Following Polymorphism: acts as a standard room container.
    /// </summary>
    public class GenericRoom : Room
    {
        public GenericRoom(int roomNumber, string type, decimal price, int cap, int floor = 1) 
            : base(roomNumber, type, price, cap, floor) { }
    }
}

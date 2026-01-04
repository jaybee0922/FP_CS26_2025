using System;
using System.Collections.Generic;
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
                        SELECT r.RoomNumber, rt.TypeName, rt.BasePrice, rt.Capacity, r.Status 
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
                            string statusStr = reader.GetString(4);

                            RoomStatus status = MapStatus(statusStr);
                            rooms.Add(new GenericRoom(num, type, price, cap) { Status = status });
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
                                const string updateRoomQuery = "UPDATE Rooms SET Status = 'Reserved' WHERE RoomNumber = @RoomNum";
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
                    const string query = "UPDATE Rooms SET Status = @Status WHERE RoomNumber = @Num";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", status.ToString());
                        cmd.Parameters.AddWithValue("@Num", roomNumber);
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
                            const string updateRoomQuery = "UPDATE Rooms SET Status = 'Reserved' WHERE RoomNumber = @RoomNum";
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
            if (Enum.TryParse(status, out RoomStatus result)) return result;
            return RoomStatus.Available;
        }
    }

    /// <summary>
    /// Concrete implementation of Room for general database mapping.
    /// Following Polymorphism: acts as a standard room container.
    /// </summary>
    public class GenericRoom : Room
    {
        public GenericRoom(int roomNumber, string type, decimal price, int cap) 
            : base(roomNumber, type, price, cap) { }
    }
}

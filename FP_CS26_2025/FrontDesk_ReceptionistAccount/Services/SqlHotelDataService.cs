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
                            int num = reader.GetInt32(0);
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
            // Implementation for direct dashboard-created reservations if needed
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

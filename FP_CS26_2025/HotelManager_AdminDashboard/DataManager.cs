using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace FP_CS26_2025.HotelManager_AdminDashboard
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(DataGridView))]
    public class DataManager : Component, IDisposable
    {
        // Sample data collections
        private List<Booking> bookings;
        private List<Room> rooms;
        private List<Guest> guests;
        private List<Staff> staffMembers;
        private List<SystemUser> users;

        // Events
        public event EventHandler DataLoaded;
        public event EventHandler<string> DataOperationPerformed;
        public event EventHandler<Booking> BookingAdded;
        public event EventHandler<Booking> BookingRemoved;
        public event EventHandler DataChanged;

        public DataManager()
        {
            InitializeData();
        }

        private void InitializeData()
        {
            bookings = new List<Booking>();
            rooms = new List<Room>();
            guests = new List<Guest>();
            staffMembers = new List<Staff>();
            users = new List<SystemUser>();

            if (AutoLoadSampleData)
            {
                LoadSampleData();
            }
        }

        #region Sample Data Generation
        public void LoadSampleData()
        {
            // Sample rooms
            rooms.Clear();
            for (int i = 1; i <= 50; i++)
            {
                rooms.Add(new Room
                {
                    RoomNumber = i.ToString("D3"),
                    Type = i % 5 == 0 ? "Suite" : i % 3 == 0 ? "Deluxe" : "Standard",
                    Price = i % 5 == 0 ? 2500 : i % 3 == 0 ? 1800 : 1200,
                    IsAvailable = i % 7 != 0,
                    Floor = (i / 10) + 1
                });
            }

            // Sample guests
            guests.Clear();
            var guestNames = new[] { "John Doe", "Jane Smith", "Alice Johnson", "Bob Brown", "Charlie Davis", "Diana Wilson", "Edward Miller", "Fiona Garcia", "George Martinez", "Helen Anderson" };
            foreach (var name in guestNames)
            {
                guests.Add(new Guest
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Email = $"{name.Replace(" ", ".").ToLower()}@email.com",
                    Phone = "+1-555-" + new Random().Next(100, 999).ToString("D3") + "-" + new Random().Next(1000, 9999).ToString("D4")
                });
            }

            // Sample bookings
            bookings.Clear();
            var sampleBookings = new[]
            {
                new { BookingId = "BKG001", GuestName = "John Doe", Room = "101", CheckIn = "2023-10-26", CheckOut = "2023-10-29", Status = "Checked In" },
                new { BookingId = "BKG002", GuestName = "Jane Smith", Room = "205", CheckIn = "2023-10-27", CheckOut = "2023-10-30", Status = "Checked In" },
                new { BookingId = "BKG003", GuestName = "Alice Johnson", Room = "312", CheckIn = "2023-10-27", CheckOut = "2023-10-28", Status = "Pending" },
                new { BookingId = "BKG004", GuestName = "Bob Brown", Room = "403", CheckIn = "2023-10-26", CheckOut = "2023-10-27", Status = "Checked Out" },
                new { BookingId = "BKG005", GuestName = "Charlie Davis", Room = "108", CheckIn = "2023-10-28", CheckOut = "2023-11-01", Status = "Checked In" }
            };

            foreach (var data in sampleBookings)
            {
                bookings.Add(new Booking
                {
                    BookingId = data.BookingId,
                    GuestName = data.GuestName,
                    RoomNumber = data.Room,
                    CheckInDate = DateTime.Parse(data.CheckIn),
                    CheckOutDate = DateTime.Parse(data.CheckOut),
                    Status = data.Status,
                    TotalAmount = CalculateBookingAmount(data.Room, data.CheckIn, data.CheckOut)
                });
            }

            // Sample staff
            staffMembers.Clear();
            var staffData = new[]
            {
                new { Name = "Manager Admin", Role = "Manager", Department = "Administration" },
                new { Name = "Receptionist 1", Role = "Receptionist", Department = "Front Desk" },
                new { Name = "Housekeeping 1", Role = "Housekeeper", Department = "Housekeeping" }
            };

            foreach (var member in staffData)
            {
                staffMembers.Add(new Staff
                {
                    Id = Guid.NewGuid(),
                    Name = member.Name,
                    Role = member.Role,
                    Department = member.Department,
                    IsActive = true
                    });
            }

            // Sample Users
            users.Clear();
            users.Add(new AdminUser("Meljan Evarolo", "MeljanE@untitledui.com", "password"));
            users.Add(new AdminUser("Geoffrey Orpia", "GeoffreyO@untitledui.com", "password"));
            users.Add(new GeneralUser("Test User", "user@test.com", "password"));
            for (int i = 1; i <= 20; i++)
            {
                users.Add(new GeneralUser($"User {i}", $"user{i}@test.com", "password"));
                users.Add(new GeneralUser($"User {i}", $"user{i}@test.com", "password"));
            }

            DataLoaded?.Invoke(this, EventArgs.Empty);
            DataOperationPerformed?.Invoke(this, "Sample data loaded successfully");
        }

        private decimal CalculateBookingAmount(string roomNumber, string checkIn, string checkOut)
        {
            var room = rooms.Find(r => r.RoomNumber == roomNumber);
            if (room == null) return 0;

            var checkInDate = DateTime.Parse(checkIn);
            var checkOutDate = DateTime.Parse(checkOut);
            var days = (checkOutDate - checkInDate).Days;

            return room.Price * days;
        }
        #endregion

        #region Public Methods
        public DataTable GetBookingsDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("BookingId", typeof(string));
            dt.Columns.Add("GuestName", typeof(string));
            dt.Columns.Add("Room", typeof(string));
            dt.Columns.Add("CheckIn", typeof(string));
            dt.Columns.Add("CheckOut", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("Amount", typeof(decimal));

            foreach (var booking in bookings)
            {
                dt.Rows.Add(
                    booking.BookingId,
                    booking.GuestName,
                    booking.RoomNumber,
                    booking.CheckInDate.ToString("yyyy-MM-dd"),
                    booking.CheckOutDate.ToString("yyyy-MM-dd"),
                    booking.Status,
                    booking.TotalAmount
                );
            }

            return dt;
        }

        public DataTable GetRoomsDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("RoomNumber", typeof(string));
            dt.Columns.Add("Type", typeof(string));
            dt.Columns.Add("Price", typeof(decimal));
            dt.Columns.Add("Floor", typeof(int));
            dt.Columns.Add("Status", typeof(string));

            foreach (var room in rooms)
            {
                dt.Rows.Add(
                    room.RoomNumber,
                    room.Type,
                    room.Price,
                    room.Floor,
                    room.IsAvailable ? "Available" : "Occupied"
                );
            }

            return dt;
        }

        public DataTable GetStaffDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Role", typeof(string));
            dt.Columns.Add("Department", typeof(string));
            dt.Columns.Add("Status", typeof(string));

            foreach (var staff in staffMembers)
            {
                dt.Rows.Add(
                    staff.Name,
                    staff.Role,
                    staff.Department,
                    staff.IsActive ? "Active" : "Inactive"
                );
            }

            return dt;
        }

        public void AddBooking(string guestName, string roomNumber, DateTime checkIn, DateTime checkOut, string status = "Pending")
        {
            var booking = new Booking
            {
                BookingId = "BKG" + (bookings.Count + 1).ToString("D3"),
                GuestName = guestName,
                RoomNumber = roomNumber,
                CheckInDate = checkIn,
                CheckOutDate = checkOut,
                Status = status,
                TotalAmount = CalculateBookingAmount(roomNumber, checkIn.ToString("yyyy-MM-dd"), checkOut.ToString("yyyy-MM-dd"))
            };

            bookings.Add(booking);
            BookingAdded?.Invoke(this, booking);
            DataOperationPerformed?.Invoke(this, $"Booking {booking.BookingId} added for {guestName}");
            DataChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool RemoveBooking(string bookingId)
        {
            var booking = bookings.Find(b => b.BookingId == bookingId);
            if (booking != null)
            {
                bookings.Remove(booking);
                BookingRemoved?.Invoke(this, booking);
                DataOperationPerformed?.Invoke(this, $"Booking {bookingId} removed");
                DataChanged?.Invoke(this, EventArgs.Empty);
                return true;
            }
            return false;
        }

        public List<Room> GetAvailableRooms()
        {
            return rooms.FindAll(r => r.IsAvailable);
        }

        public List<Booking> GetCurrentBookings()
        {
            return bookings.FindAll(b => b.Status == "Checked In" || b.Status == "Pending");
        }

        public decimal GetTodayRevenue()
        {
            decimal revenue = 0;
            foreach (var booking in bookings)
            {
                if (booking.CheckInDate.Date <= DateTime.Today && booking.CheckOutDate.Date >= DateTime.Today)
                {
                    revenue += booking.TotalAmount / (booking.CheckOutDate - booking.CheckInDate).Days;
                }
            }
            return revenue;
        }

        public int GetOccupancyRate()
        {
            int occupiedRooms = bookings.Count(b => b.Status == "Checked In");
            int totalRooms = rooms.Count;
            return totalRooms > 0 ? (occupiedRooms * 100) / totalRooms : 0;
        }

        public void UpdateRoomAvailability(string roomNumber, bool isAvailable)
        {
            var room = rooms.Find(r => r.RoomNumber == roomNumber);
            if (room != null)
            {
                room.IsAvailable = isAvailable;
                DataOperationPerformed?.Invoke(this, $"Room {roomNumber} availability updated to {(isAvailable ? "Available" : "Occupied")}");
                DataChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public void AddStaffMember(string name, string role, string department)
        {
            var staff = new Staff
            {
                Id = Guid.NewGuid(),
                Name = name,
                Role = role,
                Department = department,
                IsActive = true
            };
            staffMembers.Add(staff);
            DataOperationPerformed?.Invoke(this, $"Staff member {name} added");
            DataChanged?.Invoke(this, EventArgs.Empty);
        }

        public List<Staff> GetActiveStaff()
        {
            return staffMembers.FindAll(s => s.IsActive);
        }

        public List<SystemUser> GetAllUsers()
        {
            return new List<SystemUser>(users);
        }

        public void AddUser(SystemUser user)
        {
            users.Add(user);
            DataOperationPerformed?.Invoke(this, $"User {user.Username} added");
            DataChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool RemoveUser(Guid userId)
        {
            var user = users.Find(u => u.Id == userId);
            if (user != null)
            {
                users.Remove(user);
                DataOperationPerformed?.Invoke(this, $"User {user.Username} removed");
                DataChanged?.Invoke(this, EventArgs.Empty);
                return true;
            }
            return false;
        }

        public void UpdateUser(SystemUser updatedUser)
        {
            var existingUser = users.Find(u => u.Id == updatedUser.Id);
            if (existingUser != null)
            {
                // Update properties
                existingUser.Username = updatedUser.Username;
                existingUser.Email = updatedUser.Email;
                // In real app, handle password change securely
                if (!string.IsNullOrEmpty(updatedUser.Password))
                {
                    existingUser.Password = updatedUser.Password;
                }
                
                DataOperationPerformed?.Invoke(this, $"User {updatedUser.Username} updated");
                DataChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public List<SystemUser> FilterUsers(string type, string searchQuery, string sortOrder)
        {
            var filtered = users.AsEnumerable();

            // Filter by type
            if (type == "Admin")
            {
                filtered = filtered.OfType<AdminUser>();
            }
            else if (type == "User")
            {
                filtered = filtered.OfType<GeneralUser>();
            }

            // Search
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                string q = searchQuery.ToLower();
                filtered = filtered.Where(u => u.Username.ToLower().Contains(q) || u.Email.ToLower().Contains(q));
            }

            // Sort
            if (sortOrder == "A-Z")
            {
                filtered = filtered.OrderBy(u => u.Username);
            }
            else if (sortOrder == "Z-A")
            {
                filtered = filtered.OrderByDescending(u => u.Username);
            }
            // Add more sort options if needed

            return filtered.ToList();
        }
        #endregion

        #region Properties
        [Category("Data")]
        [Description("Gets the total number of bookings")]
        public int TotalBookings => bookings.Count;

        [Category("Data")]
        [Description("Gets the total number of rooms")]
        public int TotalRooms => rooms.Count;

        [Category("Data")]
        [Description("Gets the total number of guests")]
        public int TotalGuests => guests.Count;

        [Category("Data")]
        [Description("Gets the number of available rooms")]
        public int AvailableRoomsCount => GetAvailableRooms().Count;

        [Category("Data")]
        [Description("Gets today's revenue")]
        public decimal TodayRevenue => GetTodayRevenue();

        [Category("Data")]
        [Description("Gets current occupancy rate")]
        public int OccupancyRate => GetOccupancyRate();

        [Category("Behavior")]
        [Description("Gets or sets whether to auto-load sample data on initialization")]
        public bool AutoLoadSampleData { get; set; } = true;

        [Browsable(false)]
        public List<Booking> Bookings => new List<Booking>(bookings);

        [Browsable(false)]
        public List<Room> Rooms => new List<Room>(rooms);

        [Browsable(false)]
        public List<Guest> Guests => new List<Guest>(guests);

        [Browsable(false)]
        public List<Staff> StaffList => new List<Staff>(staffMembers); // Renamed from Staff to StaffList
        #endregion

        #region Data Classes
        public class Booking
        {
            public string BookingId { get; set; }
            public string GuestName { get; set; }
            public string RoomNumber { get; set; }
            public DateTime CheckInDate { get; set; }
            public DateTime CheckOutDate { get; set; }
            public string Status { get; set; }
            public decimal TotalAmount { get; set; }
        }

        public class Room
        {
            public string RoomNumber { get; set; }
            public string Type { get; set; }
            public decimal Price { get; set; }
            public bool IsAvailable { get; set; }
            public int Floor { get; set; }
        }

        public class Guest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
        }

        public class Staff
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Role { get; set; }
            public string Department { get; set; }
            public bool IsActive { get; set; }
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                bookings?.Clear();
                rooms?.Clear();
                guests?.Clear();
                staffMembers?.Clear();
            }
            base.Dispose(disposing);
        }
    }
}
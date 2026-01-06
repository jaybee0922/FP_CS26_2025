using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.SqlClient;
using FP_CS26_2025.Data;

namespace FP_CS26_2025.Rooms
{
    public class RoomService : IRoomService
    {
        private readonly List<IRoom> _rooms;
        private readonly string _baseImagePath;

        public RoomService()
        {
            _baseImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Assets", "IMAGES");
            _rooms = new List<IRoom>();
            InitializeRooms();
        }

        private void InitializeRooms()
        {
            // 1. Get unique room types from Database
            var activeRoomTypes = GetActiveTypesFromDb();

            // 2. Metadata for rooms - Default fallbacks if DB description is empty
            var metadata = new Dictionary<string, (string Cat, string Desc)>
            {
                { "Celebrity Suite", ("Luxe", "Experience the ultimate luxury in our most prestigious suite, featuring panoramic views and bespoke service.") },
                { "Club Room", ("Premium", "Modern elegance meets comfort, with exclusive access to the Club Lounge and premium amenities.") },
                { "Deluxe King", ("standard", "A spacious retreat with a plush King-sized bed, perfect for business or leisure travelers.") },
                { "Deluxe Twin", ("standard", "Comfortably designed with twin beds and contemporary decor, ideal for friends or family.") },
                { "Executive Suite Double", ("Executive", "Combining sophistication with functionality, featuring two double beds and a separate living area.") },
                { "Executive Suite King", ("Executive", "Our premier suite for executives, offering a King bed, work-friendly space, and luxury bathroom.") },
                { "Garden Suite", ("Nature", "Tranquil and serene, this suite offers direct views of our lush tropical gardens.") },
                { "Grand Deluxe Family", ("Family", "Perfectly sized for families, featuring multiple bedding options and child-friendly features.") },
                { "Grand Deluxe King", ("standard+", "An elevated stay with extra space and stunning views of the city skyline.") },
                { "Grand Deluxe Twin", ("standard+", "Spacious and modern, designed for those who value both style and shared comfort.") },
                { "Junior Suite", ("Luxe", "A seamless blend of living and sleeping areas with a touch of modern artistic design.") },
                { "Manila Bay Suite", ("Luxe", "Breathtaking floor-to-ceiling views of the iconic Manila Bay sunset.") },
                { "Premium Suite Double", ("Premium", "Two double beds and premium furnishings make this an excellent choice for groups.") },
                { "Premium Suite King", ("Premium", "Unmatched comfort with a King bed, premium linens, and a dedicated workspace.") },
                { "Presidential Suite", ("Imperial", "The pinnacle of opulence, offering sprawling space, a private dining room, and 24/7 butler service.") }
            };

            // 3. Populate services with ONLY rooms that exist in the DB
            foreach (var dbType in activeRoomTypes)
            {
                string name = dbType.Name;
                string cat = "standard";
                string desc = "A high-quality room offering modern comfort and essential amenities.";
                decimal price = dbType.Price;
                string imgName = dbType.ImageFilename ?? name;

                // Priority: DB Description -> Hardcoded Metadata -> Default
                if (!string.IsNullOrWhiteSpace(dbType.Description))
                {
                    desc = dbType.Description;
                }
                else
                {
                    var match = metadata.Keys.FirstOrDefault(k => k.Equals(name, StringComparison.OrdinalIgnoreCase));
                    if (match != null)
                    {
                        desc = metadata[match].Desc;
                    }
                }
                
                // Category fallback to hardcoded or inference
                var catMatch = metadata.Keys.FirstOrDefault(k => k.Equals(name, StringComparison.OrdinalIgnoreCase));
                if (catMatch != null)
                {
                    cat = metadata[catMatch].Cat;
                }

                // Use Image Filename from DB
                string imagePath = GetImagePath(imgName);
                _rooms.Add(new Room(name, imagePath, category: cat, price: price, description: desc, capacity: dbType.Capacity));
            }
        }

        private List<(string Name, decimal Price, int Capacity, string Description, string ImageFilename)> GetActiveTypesFromDb()
        {
            var types = new List<(string Name, decimal Price, int Capacity, string Description, string ImageFilename)>();
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    // Get only types that are assigned to at least one room
                    // Note: If Description or ImageFilename column doesn't exist yet, this might fail unless EnsureSchema run first.
                    // We rely on EnsureSchema
                    const string query = @"
                        SELECT DISTINCT rt.TypeName, rt.BasePrice, rt.Capacity, rt.Description, rt.ImageFilename
                        FROM Rooms r
                        JOIN RoomTypes rt ON r.RoomTypeID = rt.RoomTypeID
                        ORDER BY rt.TypeName";
                    
                    using (var cmd = new SqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string desc = reader.IsDBNull(3) ? null : reader.GetString(3);
                            string imgFile = reader.IsDBNull(4) ? reader.GetString(0) : reader.GetString(4); // Fallback to Name if null
                            types.Add((reader.GetString(0), reader.GetDecimal(1), reader.GetInt32(2), desc, imgFile));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("RoomService DB Error: " + ex.Message);
            }
            return types;
        }

        private string GetImagePath(string roomName)
        {
            if (string.IsNullOrEmpty(_baseImagePath)) return "";
            string[] extensions = { ".png", ".jpg", ".jpeg" };
            foreach (var ext in extensions)
            {
                string path = Path.Combine(_baseImagePath, roomName + ext);
                if (File.Exists(path))
                    return path;
            }
            return "";
        }

        public IEnumerable<IRoom> GetAllRooms() => _rooms;

        public IEnumerable<IRoom> GetRoomsPage(int page, int pageSize)
        {
            if (page < 1) page = 1;
            return _rooms.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public int GetTotalPages(int pageSize)
        {
            if (pageSize <= 0) return 1;
            return (int)Math.Ceiling((double)_rooms.Count / pageSize);
        }
    }
}

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
            // 2. Metadata for rooms - Default fallbacks if DB description is empty
            // Format: (Category, Description, Amenities)
            var metadata = new Dictionary<string, (string Cat, string Desc, string Amenities)>
            {
                { "Celebrity Suite", ("Luxe", 
                    "A luxurious suite designed for VIP guests, featuring a spacious living area, elegant bedroom, private dining space, premium furnishings, and panoramic views.",
                    "King bed, separate living room, minibar, luxury bathroom, smart TV, high-speed Wi-Fi") },
                { "Club Room", ("Premium", 
                    "A stylish room offering comfort and exclusivity, with access to club-level services and a relaxing atmosphere ideal for business or leisure stays.",
                    "King or twin bed, work desk, flat-screen TV, coffee/tea maker, Wi-Fi") },
                { "Deluxe King", ("standard", 
                    "A well-appointed room with a king-sized bed, perfect for couples seeking comfort and modern convenience.",
                    "King bed, air conditioning, flat-screen TV, minibar, private bathroom, Wi-Fi") },
                { "Deluxe Twin", ("standard", 
                    "A comfortable room with two single beds, ideal for friends or colleagues traveling together.",
                    "Twin beds, air conditioning, flat-screen TV, minibar, private bathroom, Wi-Fi") },
                { "Executive Suite Double", ("Executive", 
                    "A spacious suite featuring a double bed and a separate sitting area, ideal for extended stays or business travelers.",
                    "Double bed, living area, work desk, minibar, flat-screen TV, Wi-Fi") },
                { "Executive Suite King", ("Executive", 
                    "An executive-level suite offering a king bed and elegant living space for enhanced comfort and privacy.",
                    "King bed, separate living room, work desk, luxury bathroom, minibar, Wi-Fi") },
                { "Garden Suite", ("Nature", 
                    "A relaxing suite with garden views, providing a peaceful and nature-inspired ambiance.",
                    "King bed, private terrace or garden view, living area, minibar, flat-screen TV, Wi-Fi") },
                { "Grand Deluxe Family", ("Family", 
                    "A spacious room designed for families, offering extra beds and ample space for comfort.",
                    "King bed and additional bed(s), family seating area, flat-screen TV, minibar, Wi-Fi") },
                { "Grand Deluxe King", ("standard+", 
                    "An upgraded deluxe room with a king bed and enhanced space for a more luxurious stay.",
                    "King bed, seating area, flat-screen TV, minibar, private bathroom, Wi-Fi") },
                { "Grand Deluxe Twin", ("standard+", 
                    "A larger deluxe room with twin beds, offering added space and comfort.",
                    "Twin beds, seating area, flat-screen TV, minibar, private bathroom, Wi-Fi") },
                { "Junior Suite", ("Luxe", 
                    "A cozy suite offering a semi-separate living space, ideal for guests wanting extra room without a full suite.",
                    "King bed, sitting area, flat-screen TV, minibar, Wi-Fi") },
                { "Manila Bay Suite", ("Luxe", 
                    "An elegant suite featuring breathtaking views of Manila Bay, ideal for romantic or relaxing stays.",
                    "King bed, living area, panoramic bay view, minibar, luxury bathroom, Wi-Fi") },
                { "Premium Suite Double", ("Premium", 
                    "A refined suite with a double bed, offering upgraded amenities and stylish interiors.",
                    "Double bed, separate living area, minibar, flat-screen TV, Wi-Fi") },
                { "Premium Suite King", ("Premium", 
                    "A premium suite with a king bed and spacious living area for ultimate comfort.",
                    "King bed, living room, luxury bathroom, minibar, flat-screen TV, Wi-Fi") },
                { "Presidential Suite", ("Imperial", 
                    "The most exclusive and luxurious suite, designed for high-profile guests, with expansive space and top-tier amenities.",
                    "Master bedroom, private living and dining areas, luxury bathroom, minibar, premium entertainment system, Wi-Fi") }
            };

            // 3. Populate services with ONLY rooms that exist in the DB
            foreach (var dbType in activeRoomTypes)
            {
                string name = dbType.Name;
                string cat = "standard";
                string desc = "A high-quality room offering modern comfort and essential amenities.";
                string amenities = "Standard amenities available.";
                decimal price = dbType.Price;
                string imgName = dbType.ImageFilename ?? name;

                // Priority: DB Description -> Hardcoded Metadata -> Default
                if (!string.IsNullOrWhiteSpace(dbType.Description))
                {
                    desc = dbType.Description;
                }
                
                // Always try to fetch metadata for accurate category and amenities
                var matchKey = metadata.Keys.FirstOrDefault(k => k.Equals(name, StringComparison.OrdinalIgnoreCase));
                if (matchKey != null)
                {
                    var meta = metadata[matchKey];
                    cat = meta.Cat;
                    amenities = meta.Amenities;
                    
                    // If DB desc is empty, use metadata desc
                    if (string.IsNullOrWhiteSpace(dbType.Description))
                    {
                        desc = meta.Desc;
                    }
                }
                
                // Use Image Filename from DB
                string imagePath = GetImagePath(imgName);
                _rooms.Add(new Room(name, imagePath, category: cat, price: price, description: desc, amenities: amenities, capacity: dbType.Capacity));
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

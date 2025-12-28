using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FP_CS26_2025.Rooms
{
    /// <summary>
    /// Service to manage room data and pagination.
    /// Following Single Responsibility Principle (SRP).
    /// </summary>
    public class RoomService : IRoomService
    {
        private readonly List<IRoom> _rooms;
        private readonly string _baseImagePath;

        public RoomService()
        {
            // Path setup - using the provided Assets/IMAGES path
            _baseImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Assets", "IMAGES");
            
            _rooms = new List<IRoom>();
            InitializeRooms();
        }

        private void InitializeRooms()
        {
            var roomDetails = new[]
            {
                new { Name = "Celebrity Suite", Cat = "Luxe", Price = 1200m, Desc = "Experience the ultimate luxury in our most prestigious suite, featuring panoramic views and bespoke service." },
                new { Name = "Club Room", Cat = "Premium", Price = 450m, Desc = "Modern elegance meets comfort, with exclusive access to the Club Lounge and premium amenities." },
                new { Name = "Deluxe King", Cat = "standard", Price = 250m, Desc = "A spacious retreat with a plush King-sized bed, perfect for business or leisure travelers." },
                new { Name = "Deluxe Twin", Cat = "standard", Price = 250m, Desc = "Comfortably designed with twin beds and contemporary decor, ideal for friends or family." },
                new { Name = "Executive Suite Double", Cat = "Executive", Price = 650m, Desc = "Combining sophistication with functionality, featuring two double beds and a separate living area." },
                new { Name = "Executive Suite King", Cat = "Executive", Price = 700m, Desc = "Our premier suite for executives, offering a King bed, work-friendly space, and luxury bathroom." },
                new { Name = "Garden Suite", Cat = "Nature", Price = 400m, Desc = "Tranquil and serene, this suite offers direct views of our lush tropical gardens." },
                new { Name = "Grand Deluxe Family", Cat = "Family", Price = 550m, Desc = "Perfectly sized for families, featuring multiple bedding options and child-friendly features." },
                new { Name = "Grand Deluxe King", Cat = "standard+", Price = 350m, Desc = "An elevated stay with extra space and stunning views of the city skyline." },
                new { Name = "Grand Deluxe Twin", Cat = "standard+", Price = 350m, Desc = "Spacious and modern, designed for those who value both style and shared comfort." },
                new { Name = "Junior Suite", Cat = "Luxe", Price = 500m, Desc = "A seamless blend of living and sleeping areas with a touch of modern artistic design." },
                new { Name = "Manila Bay Suite", Cat = "Luxe", Price = 850m, Desc = "Breathtaking floor-to-ceiling views of the iconic Manila Bay sunset." },
                new { Name = "Premium Suite Double", Cat = "Premium", Price = 480m, Desc = "Two double beds and premium furnishings make this an excellent choice for groups." },
                new { Name = "Premium Suite King", Cat = "Premium", Price = 520m, Desc = "Unmatched comfort with a King bed, premium linens, and a dedicated workspace." },
                new { Name = "Presidential Suite", Cat = "Imperial", Price = 2500m, Desc = "The pinnacle of opulence, offering sprawling space, a private dining room, and 24/7 butler service." }
            };

            foreach (var room in roomDetails)
            {
                string imagePath = GetImagePath(room.Name);
                _rooms.Add(new Room(room.Name, imagePath, category: room.Cat, price: room.Price, description: room.Desc));
            }
        }

        private string GetImagePath(string roomName)
        {
            string[] extensions = { ".png", ".jpg", ".jpeg" };
            foreach (var ext in extensions)
            {
                string path = Path.Combine(_baseImagePath, roomName + ext);
                if (File.Exists(path))
                    return path;
            }
            return ""; // Fallback or handle missing image
        }

        public IEnumerable<IRoom> GetAllRooms()
        {
            return _rooms;
        }

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

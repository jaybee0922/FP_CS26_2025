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
            var roomDetails = new Dictionary<string, string>
            {
                { "Celebrity Suite", "Experience the ultimate luxury in our most prestigious suite, featuring panoramic views and bespoke service." },
                { "Club Room", "Modern elegance meets comfort, with exclusive access to the Club Lounge and premium amenities." },
                { "Deluxe King", "A spacious retreat with a plush King-sized bed, perfect for business or leisure travelers." },
                { "Deluxe Twin", "Comfortably designed with twin beds and contemporary decor, ideal for friends or family." },
                { "Executive Suite Double", "Combining sophistication with functionality, featuring two double beds and a separate living area." },
                { "Executive Suite King", "Our premier suite for executives, offering a King bed, work-friendly space, and luxury bathroom." },
                { "Garden Suite", "Tranquil and serene, this suite offers direct views of our lush tropical gardens." },
                { "Grand Deluxe Family", "Perfectly sized for families, featuring multiple bedding options and child-friendly features." },
                { "Grand Deluxe King", "An elevated stay with extra space and stunning views of the city skyline." },
                { "Grand Deluxe Twin", "Spacious and modern, designed for those who value both style and shared comfort." },
                { "Junior Suite", "A seamless blend of living and sleeping areas with a touch of modern artistic design." },
                { "Manila Bay Suite", "Breathtaking floor-to-ceiling views of the iconic Manila Bay sunset." },
                { "Premium Suite Double", "Two double beds and premium furnishings make this an excellent choice for groups." },
                { "Premium Suite King", "Unmatched comfort with a King bed, premium linens, and a dedicated workspace." },
                { "Presidential Suite", "The pinnacle of opulence, offering sprawling space, a private dining room, and 24/7 butler service." }
            };

            foreach (var detail in roomDetails)
            {
                string imagePath = GetImagePath(detail.Key);
                _rooms.Add(new Room(detail.Key, imagePath, description: detail.Value));
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

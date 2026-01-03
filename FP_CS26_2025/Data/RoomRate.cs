using System;

namespace FP_CS26_2025.Data
{
    public class RoomRate
    {
        public string RoomType { get; set; }
        public decimal PricePerNight { get; set; } // Changed to decimal for money
        public string Status { get; set; }
        public string CapacityInfo { get; set; }
    }
}

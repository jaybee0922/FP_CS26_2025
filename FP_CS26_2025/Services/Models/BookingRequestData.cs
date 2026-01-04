using System;

namespace FP_CS26_2025.Services.Models
{
    public class BookingRequestData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string RoomType { get; set; }
        public int NumRooms { get; set; }
        public int NumAdults { get; set; }
        public int NumChildren { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

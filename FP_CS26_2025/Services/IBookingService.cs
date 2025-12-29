using System;
using System.Collections.Generic;

namespace FP_CS26_2025.Services
{
    public interface IBookingService
    {
        bool CheckAvailability(DateTime arrival, DateTime departure, string promoCode);
        
        /// <summary>
        /// Processes a new booking request.
        /// </summary>
        /// <param name="bookingData">Dictionary or DTO containing booking details</param>
        /// <returns>A unique reservation ID or reference number</returns>
        string ProcessBookingRequest(BookingRequestData request);

        decimal CalculateTotal(string roomType, int numRooms, int nights);
    }

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

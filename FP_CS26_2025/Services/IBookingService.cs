using System;
using FP_CS26_2025.Services.Models;

namespace FP_CS26_2025.Services
{
    public interface IBookingService
    {
        /// <summary>
        /// Precision Availability Check: Verifies if a specific room type is free.
        /// Accounts for overlapping reservations to prevent double-booking.
        /// </summary>
        bool CheckAvailability(DateTime arrival, DateTime departure, string roomType, int numRooms = 1);
        
        /// <summary>
        /// Processes a new booking request following SRP and Abstraction.
        /// Implements security via input parameterization in implementation.
        /// </summary>
        string ProcessBookingRequest(BookingRequestData request);

        /// <summary>
        /// Calculates the total price based on room type and duration.
        /// Ensures financial accuracy with decimal types.
        /// </summary>
        decimal CalculateTotal(string roomType, int numRooms, int nights);
    }
}

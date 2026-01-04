using System;

namespace FP_CS26_2025.ModernDesign
{
    // Abstraction: Interface defining the contract for booking operations
    public interface IBookingService
    {
        bool CheckAvailability(DateTime arrival, DateTime departure, string promoCode);
        void BookNow();
    }
}

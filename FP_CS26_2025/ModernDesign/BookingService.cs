using System;
using System.Windows.Forms;

namespace FP_CS26_2025.ModernDesign
{
    // Encapsulation: Logic is contained within the service
    // SOLID: Single Responsibility Principle (handling booking logic)
    public class BookingService : IBookingService
    {
        public bool CheckAvailability(DateTime arrival, DateTime departure, string promoCode)
        {
            // Simulation of a check
            if (arrival >= departure)
            {
                MessageBox.Show("Departure date must be after arrival date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // In a real app, this would query a database
            return true;
        }

        public void BookNow()
        {
             MessageBox.Show("Redirecting to booking engine...", "Booking", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

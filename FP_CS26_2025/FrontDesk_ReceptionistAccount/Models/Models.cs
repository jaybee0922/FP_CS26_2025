using System;
using System.Collections.Generic;
using System.Linq;

namespace FP_CS26_2025.FrontDesk_MVC
{
    public class Guest
    {
        public string GuestId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Guest()
        {
            GuestId = Guid.NewGuid().ToString().Substring(0, 8);
        }
    }

    public class Reservation
    {
        public string ReservationId { get; set; }
        public Guest Guest { get; set; }
        public IRoom AssignedRoom { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public bool IsCheckedIn { get; set; }
        public string Status { get; set; } = "Pending";
        public decimal TotalPrice { get; set; }
        public string RoomType { get; set; }
        public int NumAdults { get; set; }
        public int NumChildren { get; set; }
        public int NumRooms { get; set; }

        public int Duration => (CheckOutDate - CheckInDate).Days > 0 ? (CheckOutDate - CheckInDate).Days : 1;

        public Reservation()
        {
            ReservationId = "RES-" + Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
        }
    }

    public class Bill
    {
        public string BillId { get; set; }
        public Reservation Reservation { get; set; }
        public List<BillLineItem> LineItems { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime BillingDate { get; set; }
        
        // Payment details
        public string PaymentMethod { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal Change { get; set; }

        public Bill(Reservation reservation)
        {
            BillId = "INV-" + DateTime.Now.ToString("yyyyMMddHHmmss");
            Reservation = reservation;
            BillingDate = DateTime.Now;
            IsPaid = false;
            LineItems = new List<BillLineItem>();
            
            // Calculate billing
            CalculateBill();
        }

        private void CalculateBill()
        {
            // Calculate room charges
            int nights = Reservation.Duration;
            decimal roomRate = Reservation.TotalPrice / Math.Max(nights, 1); // Get per-night rate
            
            LineItems.Add(new BillLineItem
            {
                Description = $"Room Charge - {Reservation.RoomType}",
                Quantity = nights,
                UnitPrice = roomRate,
                Total = Reservation.TotalPrice
            });

            // Calculate totals
            Subtotal = LineItems.Sum(item => item.Total);
            Tax = 0; // No tax for now, can be configured later
            TotalAmount = Subtotal + Tax;
        }

        public void ProcessPayment(string paymentMethod, decimal amountPaid)
        {
            PaymentMethod = paymentMethod;
            AmountPaid = amountPaid;
            Change = amountPaid - TotalAmount;
            IsPaid = true;
        }
    }

    public class BillLineItem
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }

    public class PaymentRecord
    {
        public int PaymentId { get; set; }
        public string ReservationId { get; set; }
        public string GuestName { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}

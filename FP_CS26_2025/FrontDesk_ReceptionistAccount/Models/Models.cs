using System;
using System.Collections.Generic;

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
        public decimal TotalAmount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime BillingDate { get; set; }

        public Bill(Reservation reservation)
        {
            BillId = "INV-" + Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
            Reservation = reservation;
            TotalAmount = reservation.AssignedRoom.CalculateTotalPrice(reservation.Duration);
            BillingDate = DateTime.Now;
            IsPaid = false;
        }
    }
}

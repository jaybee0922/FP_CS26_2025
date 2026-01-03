using System;
using System.Collections.Generic;
using System.Linq;

namespace FP_CS26_2025.FrontDesk_MVC
{
    public class FrontDeskController
    {
        private readonly IHotelDataService _dataService;

        public FrontDeskController(IHotelDataService dataService)
        {
            _dataService = dataService;
        }

        public IEnumerable<IRoom> GetAvailableRooms() 
            => _dataService.GetAllRooms().Where(r => r.Status == RoomStatus.Available);

        public void CreateReservation(Guest guest, int roomNumber, DateTime checkIn, DateTime checkOut)
        {
            var room = _dataService.GetRoomByNumber(roomNumber);
            if (room == null || room.Status != RoomStatus.Available)
                throw new Exception("Room is not available.");

            var reservation = new Reservation
            {
                Guest = guest,
                AssignedRoom = room,
                CheckInDate = checkIn,
                CheckOutDate = checkOut,
                IsCheckedIn = false
            };

            _dataService.AddReservation(reservation);
        }

        public void CheckIn(string reservationId)
        {
            var res = _dataService.GetAllReservations().FirstOrDefault(r => r.ReservationId == reservationId);
            if (res == null) throw new Exception("Reservation not found.");
            
            // Persist to database
            ((SqlHotelDataService)_dataService).UpdateReservationStatus(reservationId, "CheckedIn");
            
            if (res.AssignedRoom != null)
            {
                _dataService.UpdateRoomStatus(res.AssignedRoom.RoomNumber, RoomStatus.Occupied);
            }
        }

        public Bill CheckOut(string reservationId)
        {
            var res = _dataService.GetAllReservations().FirstOrDefault(r => r.ReservationId == reservationId);
            if (res == null) throw new Exception("Reservation not found.");

            // Generate detailed bill
            var bill = new Bill(res);
            
            return bill;
        }

        public void CompleteCheckOut(Bill bill, string paymentMethod, decimal amountPaid)
        {
            if (bill == null) throw new ArgumentNullException(nameof(bill));
            
            // Process payment
            bill.ProcessPayment(paymentMethod, amountPaid);
            
            // Save payment to database
            ((SqlHotelDataService)_dataService).SavePayment(
                bill.Reservation.ReservationId, 
                bill.AmountPaid, 
                bill.PaymentMethod);
            
            // Update reservation status to CheckedOut
            ((SqlHotelDataService)_dataService).UpdateReservationStatus(
                bill.Reservation.ReservationId, 
                "CheckedOut");
            
            // Update room status to Available
            if (bill.Reservation.AssignedRoom != null)
            {
                _dataService.UpdateRoomStatus(
                    bill.Reservation.AssignedRoom.RoomNumber, 
                    RoomStatus.Available);
            }
        }

        public IEnumerable<Reservation> GetActiveReservations() => _dataService.GetAllReservations();
        public IEnumerable<IRoom> GetAllRooms() => _dataService.GetAllRooms();

        public void ApproveReservation(string reservationId)
        {
            if (string.IsNullOrWhiteSpace(reservationId))
                throw new ArgumentException("Reservation ID cannot be empty.", nameof(reservationId));

            ((SqlHotelDataService)_dataService).ApproveReservation(reservationId);
        }
    }
}

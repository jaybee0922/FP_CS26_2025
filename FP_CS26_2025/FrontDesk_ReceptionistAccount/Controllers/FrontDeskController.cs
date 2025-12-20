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
            
            res.IsCheckedIn = true;
            res.AssignedRoom.Status = RoomStatus.Occupied;
        }

        public Bill CheckOut(string reservationId)
        {
            var res = _dataService.GetAllReservations().FirstOrDefault(r => r.ReservationId == reservationId);
            if (res == null) throw new Exception("Reservation not found.");

            var bill = new Bill(res);
            _dataService.AddBill(bill);
            
            res.AssignedRoom.Status = RoomStatus.Available;
            return bill;
        }

        public IEnumerable<Reservation> GetActiveReservations() => _dataService.GetAllReservations();
        public IEnumerable<IRoom> GetAllRooms() => _dataService.GetAllRooms();
    }
}

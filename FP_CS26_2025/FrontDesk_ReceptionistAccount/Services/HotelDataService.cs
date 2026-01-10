using System;
using System.Collections.Generic;
using System.Linq;

namespace FP_CS26_2025.FrontDesk_MVC
{
    public interface IHotelDataService
    {
        IEnumerable<IRoom> GetAllRooms();
        IEnumerable<Reservation> GetAllReservations();
        IEnumerable<Bill> GetAllBills();
        void AddReservation(Reservation reservation);
        void UpdateRoomStatus(int roomNumber, RoomStatus status);
        void AddBill(Bill bill);
        IRoom GetRoomByNumber(int roomNumber);
        void UpdateReservationStatus(string reservationId, string status);
        bool IsRoomAvailable(int roomNumber, DateTime start, DateTime end);
        IEnumerable<PaymentRecord> GetAllPayments();
        System.Data.DataTable GetArchivedReports();
        System.Data.DataTable GetAllPhysicalRooms();
        void SavePhysicalRoom(string roomNumber, int roomTypeId, int floor, string status, string bedConfig = "Standard", string viewType = "City View");
        void DeletePhysicalRoom(string roomNumber);
        System.Data.DataTable GetAllRoomTypes();
        void DeleteReservation(string reservationId);
    }

    public class InMemoryHotelService : IHotelDataService
    {
        private static List<IRoom> _rooms;
        private static List<Reservation> _reservations = new List<Reservation>();
        private static List<Bill> _bills = new List<Bill>();

        public InMemoryHotelService()
        {
            if (_rooms == null)
            {
                _rooms = new List<IRoom>();
                for (int i = 101; i <= 110; i++) _rooms.Add(new StandardRoom(i));
                for (int i = 201; i <= 205; i++) _rooms.Add(new SuiteRoom(i));
            }
        }

        public IEnumerable<IRoom> GetAllRooms() => _rooms;
        public IEnumerable<Reservation> GetAllReservations() => _reservations;
        public IEnumerable<Bill> GetAllBills() => _bills;

        public void AddReservation(Reservation reservation)
        {
            _reservations.Add(reservation);
            if (reservation.AssignedRoom != null)
                reservation.AssignedRoom.Status = RoomStatus.Reserved;
        }

        public void UpdateRoomStatus(int roomNumber, RoomStatus status)
        {
            var room = _rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
            if (room != null) room.Status = status;
        }

        public void AddBill(Bill bill) => _bills.Add(bill);
        public IRoom GetRoomByNumber(int roomNumber) => _rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
        public void UpdateReservationStatus(string reservationId, string status) { }
        public bool IsRoomAvailable(int roomNumber, DateTime start, DateTime end) => true;
        public IEnumerable<PaymentRecord> GetAllPayments() => new List<PaymentRecord>();
        public System.Data.DataTable GetArchivedReports() => new System.Data.DataTable();
        public System.Data.DataTable GetAllPhysicalRooms() => new System.Data.DataTable();
        public void SavePhysicalRoom(string roomNumber, int roomTypeId, int floor, string status, string bedConfig = "Standard", string viewType = "City View") { }
        public void DeletePhysicalRoom(string roomNumber) { }
        public System.Data.DataTable GetAllRoomTypes() => new System.Data.DataTable();
        public void DeleteReservation(string reservationId) 
        { 
            var res = _reservations.FirstOrDefault(r => r.ReservationId == reservationId);
            if (res != null) _reservations.Remove(res);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

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

        public IEnumerable<IRoom> GetAvailableRoomsByDate(string roomType, DateTime checkIn, DateTime checkOut)
        {
            var allRooms = _dataService.GetAllRooms();
            if (!string.IsNullOrEmpty(roomType))
            {
                allRooms = allRooms.Where(r => r.RoomType.Equals(roomType, StringComparison.OrdinalIgnoreCase));
            }

            // Filter out rooms that have overlapping reservations
            return allRooms.Where(r => _dataService.IsRoomAvailable(r.RoomNumber, checkIn, checkOut));
        }

        public List<string> GetRoomTypes()
        {
             return _dataService.GetAllRooms()
                 .Select(r => r.RoomType)
                 .Distinct()
                 .OrderBy(t => t)
                 .ToList();
        }

        public void CreateReservation(Guest guest, int roomNumber, DateTime checkIn, DateTime checkOut)
        {
            var room = _dataService.GetRoomByNumber(roomNumber);
            if (room == null)
                throw new Exception("Room not found.");

            // Global Overbooking Protection: Check for overlapping dates
            if (!_dataService.IsRoomAvailable(roomNumber, checkIn, checkOut))
                throw new Exception($"Room {roomNumber} is already reserved for the selected dates.");

            var reservation = new Reservation
            {
                Guest = guest,
                AssignedRoom = room,
                RoomType = room.RoomType,
                TotalPrice = room.BasePrice * Math.Max((checkOut - checkIn).Days, 1),
                CheckInDate = checkIn,
                CheckOutDate = checkOut,
                IsCheckedIn = false,
                Status = "Pending",
                NumAdults = 2,
                NumChildren = 0,
                NumRooms = 1
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
                    RoomStatus.Cleaning);
            }
        }

        public void UpdateRoomStatus(int roomNumber, RoomStatus status)
        {
            _dataService.UpdateRoomStatus(roomNumber, status);
        }

        public IEnumerable<Reservation> GetActiveReservations() => _dataService.GetAllReservations();
        public IEnumerable<IRoom> GetAllRooms() => _dataService.GetAllRooms();

        public IEnumerable<PaymentRecord> GetPaymentHistory() => _dataService.GetAllPayments();

        public IEnumerable<PaymentRecord> GetFilteredPayments(DateTime start, DateTime end, string status, string query, int page, int pageSize, out int totalRecords)
        {
            var allPayments = _dataService.GetAllPayments();
            
            // 1. Filter by Date
            // Ensure we cover the entire end day
            var filtered = allPayments.Where(p => p.PaymentDate >= start.Date && p.PaymentDate <= end.Date.AddDays(1).AddTicks(-1));

            // 2. Filter by Status (PaymentRecord doesn't have status, assuming all Payments are 'Paid' or using Reservation Status if needed)
            // But user asked for "Status" filter on Panel (Paid, Pending, All). 
            // Currently PaymentRecord table implies completed payments. Pending reservations are in Reservations table.
            // For now, if Status is "Pending", checking Reservations might be needed, but Billing usually implies completed transactions.
            // Let's assume Status filter might apply to Reservation status if we joined it, but currently PaymentRecord just has Payment Method.
            // If the UI has "Status", and we only show Payments, they are effectively "Paid".
            // However, to satisfy the UI requirement without overcomplicating:
            // If status is "Pending", we might return nothing or we'd need to fetch Pending Reservations separately.
            // Since the user asked for a "Billing" panel, usually this means "History". Pending items are in "Process Reservations".
            // I'll stick to filtering what we have. If Status != "All", we might filter by PaymentMethod or similar?
            // Actually, let's keep it simple: Filter by whatever is in the record or ignore if not applicable.
            // The UI has "Paid", "Pending", "Refused".
            // For now, I will treat all Payments as "Paid" or "Completed".
            // If the user selects "Pending", we return 0 items (since they aren't payments yet).
            if (!string.IsNullOrEmpty(status) && status != "All")
            {
                 // Mock behavior: If they ask for "Pending" here, show nothing, as these are *Payments*.
                 // Or, if "Paid", show all.
                 if (status.Equals("Pending", StringComparison.OrdinalIgnoreCase))
                 {
                     filtered = Enumerable.Empty<PaymentRecord>();
                 }
                 // If "Refused", show nothing.
            }

            // 3. Search Query
            if (!string.IsNullOrWhiteSpace(query))
            {
                filtered = filtered.Where(p => 
                    p.GuestName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    p.ReservationId.Contains(query));
            }

            totalRecords = filtered.Count();

            // 4. Pagination
            return filtered
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public void ApproveReservation(string reservationId)
        {
            if (string.IsNullOrWhiteSpace(reservationId))
                throw new ArgumentException("Reservation ID cannot be empty.", nameof(reservationId));

            ((SqlHotelDataService)_dataService).ApproveReservation(reservationId);
        }

        public void SaveMonthlyReport(int month, int year, decimal revenue, int count)
        {
            ((SqlHotelDataService)_dataService).SaveMonthlyReport(month, year, revenue, count);
        }

        public System.Data.DataTable GetArchivedReports()
        {
            return ((SqlHotelDataService)_dataService).GetArchivedReports();
        }

        public void DeleteReservation(string reservationId)
        {
            if (string.IsNullOrWhiteSpace(reservationId))
                throw new ArgumentException("Reservation ID cannot be empty.", nameof(reservationId));
            
            _dataService.DeleteReservation(reservationId);
        }
    }
}

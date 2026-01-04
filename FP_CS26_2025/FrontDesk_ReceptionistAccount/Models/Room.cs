using System;

namespace FP_CS26_2025.FrontDesk_MVC
{
    public abstract class Room : IRoom
    {
        public int RoomNumber { get; protected set; }
        public string RoomType { get; protected set; }
        public decimal BasePrice { get; protected set; }
        public int Capacity { get; protected set; }
        public RoomStatus Status { get; set; }

        protected Room(int roomNumber, string roomType, decimal basePrice, int capacity)
        {
            RoomNumber = roomNumber;
            RoomType = roomType;
            BasePrice = basePrice;
            Capacity = capacity;
            Status = RoomStatus.Available;
        }

        public virtual decimal CalculateTotalPrice(int nights)
        {
            return BasePrice * nights;
        }

        public virtual string GetDetails()
        {
            return $"Room {RoomNumber} ({RoomType}) - {Status} - ${BasePrice}/night";
        }
    }
}

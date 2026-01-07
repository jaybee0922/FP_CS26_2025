using System;

namespace FP_CS26_2025.FrontDesk_MVC
{
    public enum RoomStatus
    {
        Available,
        Occupied,
        Reserved,
        UnderMaintenance,
        Cleaning,
        OutOfService,
        ReadyForCheckIn
    }

    public interface IRoom
    {
        int RoomNumber { get; }
        string RoomType { get; }
        decimal BasePrice { get; }
        int Capacity { get; }
        int Floor { get; set; }
        RoomStatus Status { get; set; }
        
        decimal CalculateTotalPrice(int nights);
        string GetDetails();
    }
}

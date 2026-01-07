using System;

namespace FP_CS26_2025.FrontDesk_MVC
{
    public class StandardRoom : Room
    {
        public StandardRoom(int roomNumber, int floor = 1) 
            : base(roomNumber, "Standard", 100.00m, 2, floor) { }
    }

    public class SuiteRoom : Room
    {
        public SuiteRoom(int roomNumber, int floor = 1) 
            : base(roomNumber, "Suite", 250.00m, 4, floor) { }

        public override decimal CalculateTotalPrice(int nights)
        {
            return (BasePrice * nights) + 50.00m;
        }

        public override string GetDetails()
        {
            return base.GetDetails() + " [Includes Mini-Bar & City View]";
        }
    }
}

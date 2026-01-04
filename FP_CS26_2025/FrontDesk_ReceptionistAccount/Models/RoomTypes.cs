using System;

namespace FP_CS26_2025.FrontDesk_MVC
{
    public class StandardRoom : Room
    {
        public StandardRoom(int roomNumber) 
            : base(roomNumber, "Standard", 100.00m, 2) { }
    }

    public class SuiteRoom : Room
    {
        public SuiteRoom(int roomNumber) 
            : base(roomNumber, "Suite", 250.00m, 4) { }

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

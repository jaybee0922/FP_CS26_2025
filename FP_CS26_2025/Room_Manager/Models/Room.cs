using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP_CS26_2025.Room_Rates___Policies.Models
{
    public class Room
    {
        private int roomNumber { get; set; }
        public string roomType { get; set; }
        public int Pricing { get; set; }
        public bool availability { get; set; }
        public int capacity { get; set; }


        public string getRoomDetails()
        {
            return $"Room details: {roomNumber}, {roomType}, {Pricing}, {availability}, {capacity}";
        }

        public void addRoom(int roomNumber, string roomType, int Pricing, bool availability, int capacity)
        {

            this.roomNumber = roomNumber;
            this.roomType = roomType;
            this.Pricing = Pricing;
            this.availability = availability;
            this.capacity = capacity;

        }


    }
}
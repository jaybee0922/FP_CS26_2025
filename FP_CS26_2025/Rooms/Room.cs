namespace FP_CS26_2025.Rooms
{
    /// <summary>
    /// Concrete implementation of a Room.
    /// </summary>
    public class Room : IRoom
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Amenities { get; set; }
        public int Capacity { get; set; }

        public Room(string name, string imagePath, string category = "Hotel", decimal price = 0, string description = "", string amenities = "", int capacity = 0)
        {
            Name = name;
            ImagePath = imagePath;
            Category = category;
            Price = price;
            Description = description;
            Amenities = amenities;
            Capacity = capacity;
        }
    }
}

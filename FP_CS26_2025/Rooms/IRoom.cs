using System.Drawing;

namespace FP_CS26_2025.Rooms
{
    /// <summary>
    /// Abstraction for Room data to support SOLID principles.
    /// </summary>
    public interface IRoom
    {
        string Name { get; }
        string ImagePath { get; }
        string Category { get; }
        decimal Price { get; }
        string Description { get; }
    }
}

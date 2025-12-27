using System.Collections.Generic;

namespace FP_CS26_2025.Rooms
{
    /// <summary>
    /// Interface for room data service to support Abstraction and Dependency Injection.
    /// </summary>
    public interface IRoomService
    {
        IEnumerable<IRoom> GetAllRooms();
        IEnumerable<IRoom> GetRoomsPage(int page, int pageSize);
        int GetTotalPages(int pageSize);
    }
}

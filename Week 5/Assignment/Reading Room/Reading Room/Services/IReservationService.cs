using Reading_Room.Models;
using Reading_Room.DTO;

namespace Reading_Room.Services
{
    public interface IReservationService
    {
        Task<List<Reservation>> GetAsync(int? roomId, DateTime? from, DateTime? to);
        Task<(bool success, string? error, Reservation? reservation)> CreateAsync(Reservation res);
        Task<ReservationDto?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);

        // Analytics
        Task<List<(int RoomId, string RoomName, TimeSpan BusyTime)>> TopNBusiestRoomsAsync(DateTime from, DateTime to, int topN);
        Task<Dictionary<int, double>> UtilizationPercentPerRoomAsync(DateTime from, DateTime to);
    }
}

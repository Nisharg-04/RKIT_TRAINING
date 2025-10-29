using Reading_Room.DTO;
using Reading_Room.Models;

namespace Reading_Room.Services
{
    public interface IRoomService
    {
        Task<List<RoomDto>> GetAllAsync();
        Task<RoomDto?> GetByIdAsync(int id);
        Task<RoomDto> CreateAsync(RoomDto room);
        Task<bool> UpdateAsync(int id, RoomDto room);
        Task<bool> DeleteAsync(int id);
    }
}

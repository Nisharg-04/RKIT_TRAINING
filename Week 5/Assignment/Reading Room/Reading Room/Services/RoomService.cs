using Reading_Room.Data;
using Reading_Room.DTO;
using Reading_Room.Models;
using Microsoft.EntityFrameworkCore;

namespace Reading_Room.Services
{
    public class RoomService : IRoomService
    {
        private readonly AppDbContext _db;
        public RoomService(AppDbContext db) => _db = db;

        public async Task<RoomDto> CreateAsync(RoomDto room)
        {
            Room newRoom = new Room();
            newRoom.Id = room.Id;
            newRoom.Name = room.Name;
            newRoom.Capacity = room.Capacity;
            
            _db.Rooms.Add(newRoom);
            await _db.SaveChangesAsync();
            return new RoomDto(room.Id, room.Name, room.Capacity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var r = await _db.Rooms.FindAsync(id);
            if (r == null) return false;
            _db.Rooms.Remove(r);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<RoomDto>> GetAllAsync()
        {
            return await _db.Rooms
                .Select(r => new RoomDto(r.Id, r.Name, r.Capacity))
                .ToListAsync();
        }

        public async Task<RoomDto?> GetByIdAsync(int id)
        {
            var r = await _db.Rooms.FindAsync(id);
            if (r == null) return null;
            return new RoomDto(r.Id, r.Name, r.Capacity);
        }

        public async Task<bool> UpdateAsync(int id, RoomDto room)
        {
            var existing = await _db.Rooms.FindAsync(id);
            if (existing == null) return false;
            existing.Name = room.Name;
            existing.Capacity = room.Capacity;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}

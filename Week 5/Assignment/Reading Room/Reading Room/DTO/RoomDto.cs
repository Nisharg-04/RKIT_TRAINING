namespace Reading_Room.DTO
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public RoomDto(int id, string name, int capacity)
        {
            Id = id;
            Name = name;
            Capacity = capacity;
        }

    }
}

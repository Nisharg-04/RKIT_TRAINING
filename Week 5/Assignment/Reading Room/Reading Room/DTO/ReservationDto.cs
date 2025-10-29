using Reading_Room.Models;
namespace Reading_Room.DTO

{
    public class ReservationDto
    {
        public int Id;
       public string PatronName;
        public DateTime Start;
       public DateTime End;
        public ReservationStatus Status;
       public string RoomName;

        public ReservationDto(int id, string patronName, DateTime start, DateTime end, ReservationStatus status, string roomName)
        {
            Id = id;
            PatronName = patronName;
            Start = start;
            End = end;
            Status = status;
            RoomName = roomName;
        }
    }
}

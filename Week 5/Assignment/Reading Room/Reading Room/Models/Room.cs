namespace Reading_Room.Models
{
    /// <summary>
    /// This class stores details about a room in the reading room.
    /// </summary>
    public class Room
    {
        /// <summary>
        /// Unique ID for each room.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the room.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Total number of people the room can hold.
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// List of all bookings made for this room.
        /// </summary>
        public List<Reservation>? Reservations { get; set; }
    }
}

namespace Reading_Room.Models
{
    /// <summary>
    /// A class representing a room in the reading room application.
    /// </summary>
    public class Room
    {
        /// <summary>
        /// A unique identifier for the room.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the room.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Capacity of the room.
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Navigation property representing the collection of reservations associated with this room.
        /// </summary>
        public List<Reservation>? Reservations { get; set; }
    }
}
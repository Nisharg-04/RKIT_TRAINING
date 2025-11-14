using System.ComponentModel.DataAnnotations.Schema;

namespace Reading_Room.Models
{
    /// <summary>
    /// This class stores details about a room reservation in the reading room.
    /// </summary>
    public class Reservation
    {
        /// <summary>
        /// Unique ID for each reservation.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID of the room that is booked.
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// The room linked to this reservation.
        /// </summary>
        [ForeignKey("RoomId")]
        public Room? Room { get; set; }

        /// <summary>
        /// Name of the person who booked the room.
        /// </summary>
        public string PatronName { get; set; }

        /// <summary>
        /// Date and time when the booking starts.
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// Date and time when the booking ends.
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        /// Current status of the booking (for example, Pending, Approved, or Cancelled).
        /// </summary>
        public ReservationStatus Status { get; set; }
    }
}

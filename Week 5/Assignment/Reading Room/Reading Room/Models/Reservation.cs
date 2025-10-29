using System.ComponentModel.DataAnnotations.Schema;

namespace Reading_Room.Models
{
    /// <summary>
    /// A class representing a reservation for a room in the reading room application.
    /// </summary>
    public class Reservation
    {
        /// <summary>
        /// A unique identifier for the reservation.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// A foreign key referencing the associated Room entity.
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// A navigation property to the associated Room entity.
        /// </summary>

        [ForeignKey("RoomId")]
        public Room? Room { get; set; }

        /// <summary>
        /// Name of the patron who made the reservation.
        /// </summary>
        public string PatronName { get; set; }

        /// <summary>
        /// From when the reservation starts.
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// To when the reservation ends.
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        /// Status of the reservation.
        /// </summary>
        public ReservationStatus Status { get; set; }
    }
}

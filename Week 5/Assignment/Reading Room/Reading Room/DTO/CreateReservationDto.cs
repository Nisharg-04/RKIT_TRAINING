using System.ComponentModel.DataAnnotations;

namespace Reading_Room.DTO
{
    /// <summary>
    /// This class is used when a new room booking is created.
    /// </summary>
    public class CreateReservationDto
    {
        /// <summary>
        /// ID of the room that is being booked.
        /// </summary>
        [Required]
        public int RoomId { get; set; }

        /// <summary>
        /// Name of the person who is booking the room.
        /// </summary>
        [Required, MaxLength(200)]
        public string PatronName { get; set; } = null!;

        /// <summary>
        /// Date and time when the booking starts.
        /// </summary>
        [Required]
        public DateTime Start { get; set; }

        /// <summary>
        /// Date and time when the booking ends.
        /// </summary>
        [Required]
        public DateTime End { get; set; }

        /// <summary>
        /// Current status of the booking (optional, will be linked to an enum).
        /// </summary>
        public string? Status { get; set; }
    }
}

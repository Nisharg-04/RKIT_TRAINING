using System.ComponentModel.DataAnnotations;

namespace Reading_Room.DTO
{
    public class CreateReservationDto
    {
        [Required]
        public int RoomId { get; set; }

        [Required, MaxLength(200)]
        public string PatronName { get; set; } = null!;

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        public string? Status { get; set; } // optional will map to enum
    }
}

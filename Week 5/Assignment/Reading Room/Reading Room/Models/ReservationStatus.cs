namespace Reading_Room.Models
{
    /// <summary>
    /// Shows the current status of a room reservation.
    /// </summary>
    public enum ReservationStatus
    {
        /// <summary>
        /// The booking request is waiting for approval.
        /// </summary>
        Pending,

        /// <summary>
        /// The booking has been approved.
        /// </summary>
        Confirmed,

        /// <summary>
        /// The booking has been cancelled.
        /// </summary>
        Cancelled
    }
}

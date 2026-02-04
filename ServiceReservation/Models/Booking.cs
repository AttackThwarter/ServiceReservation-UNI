using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceReservation.Models
{
    public enum BookingStatus
    {
        Pending,
        Approved,
        Rejected,
        Cancelled
    }

    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public int ServiceId { get; set; }

        [Required]
        public int TimeSlotId { get; set; }

        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

        [ForeignKey("ServiceId")]
        public Service? Service { get; set; }

        [ForeignKey("TimeSlotId")]
        public TimeSlot? TimeSlot { get; set; }
    }
}

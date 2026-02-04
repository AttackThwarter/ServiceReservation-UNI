using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceReservation.Models
{
    public class TimeSlot
    {
        public int Id { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Range(1, 100)]
        public int Capacity { get; set; }

        // Navigation Properties
        [ForeignKey("ServiceId")]
        public Service? Service { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}

using System.ComponentModel.DataAnnotations;

namespace ServiceReservation.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        public int Duration { get; set; } // به دقیقه

        public bool IsActive { get; set; } = true;

        // Navigation Properties
        public ICollection<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}

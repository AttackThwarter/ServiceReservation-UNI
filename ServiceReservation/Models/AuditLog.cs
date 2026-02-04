using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceReservation.Models
{
    public class AuditLog
    {
        public int Id { get; set; }

        public string? UserId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Action { get; set; } = string.Empty;

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [MaxLength(50)]
        public string? IPAddress { get; set; }

        // Navigation Property
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }
    }
}

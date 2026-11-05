
using System;
using System.ComponentModel.DataAnnotations;

namespace CommunityEventScheduler.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [StringLength(50)]
        public string Organizer { get; set; } = string.Empty;

        public int LocationId { get; set; }

        public Location Location { get; set; } = null!;

        public int EventCategoryId { get; set; }

        public EventCategory EventCategory { get; set; } = null!;

        // Identity user who created this event.
        public string? CreatedByUserId { get; set; }

        // Shows the current booking status.
        public string BookingStatus { get; set; } = "Pending";

        // Shows the current payment status.
        public string PaymentStatus { get; set; } = "Unpaid";

        // Saves when the booking was created.
        public DateTime BookingCreatedAt { get; set; } = DateTime.UtcNow;
    }
}

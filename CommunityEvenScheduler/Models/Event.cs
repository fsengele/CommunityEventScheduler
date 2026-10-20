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
        public string Description { get; set; } = string.Empty;

        public string Organizer { get; set; } = string.Empty;

        public int LocationId { get; set; }

        public Location Location { get; set; } = null!;

        public int EventCategoryId { get; set; }

        public EventCategory EventCategory { get; set; } = null!;

        // Identity user who created this event
        public string? CreatedByUserId { get; set; }
    }
}
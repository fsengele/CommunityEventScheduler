using CommunityEvenScheduler.Models;
using System.ComponentModel.DataAnnotations;

namespace CommunityEventScheduler.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime EventDate { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }
        [Required]
        public string Description { get; set; }
        
        public string Organizer { get; set; }

        public int LocationId { get; set; }

        public Location Location { get; set; }

        public int EventCategoryId { get; set; }

        public EventCategory EventCategory { get; set; }
    }
}
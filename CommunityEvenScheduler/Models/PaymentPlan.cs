
using System.ComponentModel.DataAnnotations;

namespace CommunityEventScheduler.Models
{
    // Stores a payment plan in the database.
    public class PaymentPlan
    {
        // Unique ID for each payment plan.
        public int Id { get; set; }

        // Name of the plan, like Basic or Premium.
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = "";

        // Description of the payment plan.
        [StringLength(500)]
        public string Description { get; set; } = "";

        // Price in US dollars.
        [Range(0.01, 100000)]
        public decimal Price { get; set; }

        // Admin can enable or disable this plan.
        public bool IsActive { get; set; } = true;

        // Controls the order of plans on the page.
        public int DisplayOrder { get; set; }
    }
}

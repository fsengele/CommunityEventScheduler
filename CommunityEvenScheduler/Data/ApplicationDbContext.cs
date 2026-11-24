
using CommunityEventScheduler.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CommunityEventScheduler.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }

        // NEW: Store payment plans in the database.
        public DbSet<PaymentPlan> PaymentPlans { get; set; }

        // NEW: Configure the database tables.
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Keep the Identity database configuration.
            base.OnModelCreating(builder);

            // NEW: Store prices with two decimal places.
            builder.Entity<PaymentPlan>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
        }
    }
}

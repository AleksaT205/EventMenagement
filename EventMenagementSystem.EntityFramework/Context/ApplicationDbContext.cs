using EventManagementSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace EventMenagementSystem.EntityFramework.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<UserEvent> UserEvents { get; set; }
        public DbSet<Review> Reviews { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEvent>()
                .HasKey(ue => new { ue.UserID, ue.EventID });

            modelBuilder.Entity<User>()
                .HasMany(u => u.OrganizedEvents)
                .WithOne(e => e.Organizer)
                .HasForeignKey(e => e.OrganizerID);
        }
    }
}











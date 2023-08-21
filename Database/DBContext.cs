using Microsoft.EntityFrameworkCore;

namespace MyWebApi.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<User> Users { get; set; }
        
        // Configure many-to-many relationship
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserMeeting>()
                .HasKey(um => new { um.UserId, um.MeetingId });

            modelBuilder.Entity<UserMeeting>()
                .HasOne(um => um.User)
                .WithMany(u => u.UserMeetings)
                .HasForeignKey(um => um.UserId);

            modelBuilder.Entity<UserMeeting>()
                .HasOne(um => um.Meeting)
                .WithMany(m => m.UserMeetings)
                .HasForeignKey(um => um.MeetingId);
        }
    }
}

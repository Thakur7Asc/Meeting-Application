//using MeetingApplicationAPI.Models;
//using Microsoft.EntityFrameworkCore;

//namespace MeetingApplicationAPI.Data;



//public class ApplicationDbContext : DbContext
//{
//    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

//    public DbSet<User> Users { get; set; }
//    public DbSet<Meeting> Meetings { get; set; }



//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        base.OnModelCreating(modelBuilder);

//        // Configure the UserMeeting join table
//        modelBuilder.Entity<UserMeeting>()
//            .HasKey(um => new { um.UserId, um.MeetingId }); // Composite primary key

//        modelBuilder.Entity<UserMeeting>()
//            .HasOne(um => um.User)
//            .WithMany(u => u.UserMeetings)
//            .HasForeignKey(um => um.UserId);

//        modelBuilder.Entity<UserMeeting>()
//            .HasOne(um => um.Meeting)
//            .WithMany(m => m.UserMeetings)
//            .HasForeignKey(um => um.MeetingId);

//        // Seed Users
//        modelBuilder.Entity<User>().HasData(
//            new User { Id = 1, Username = "admin", Password = "admin123", Email = "admin@example.com" },
//            new User { Id = 2, Username = "user1", Password = "user123", Email = "user1@example.com" },
//            new User { Id = 3, Username = "user2", Password = "user123", Email = "user2@example.com" }
//        );

//        // Seed Meetings
//        modelBuilder.Entity<Meeting>().HasData(
//            new Meeting { Id = 1, Title = "Project Kickoff", Description = "Initial meeting", Date = DateTime.UtcNow },
//            new Meeting { Id = 2, Title = "Sprint Planning", Description = "Plan the next sprint", Date = DateTime.UtcNow.AddDays(7) }
//        );

//        // Seed UserMeeting relationships
//        modelBuilder.Entity<UserMeeting>().HasData(
//            new UserMeeting { UserId = 1, MeetingId = 1 },
//            new UserMeeting { UserId = 1, MeetingId = 2 },
//            new UserMeeting { UserId = 2, MeetingId = 1 },
//            new UserMeeting { UserId = 3, MeetingId = 2 }
//        );
//    }





//}
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MeetingApplicationAPI.Models;

namespace MeetingApplicationAPI.Data;
public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<UserMeeting> UserMeetings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure many-to-many relationship
        builder.Entity<UserMeeting>()
            .HasKey(um => new { um.UserId, um.MeetingId });

        builder.Entity<UserMeeting>()
            .HasOne(um => um.User)
            .WithMany(u => u.UserMeetings)
            .HasForeignKey(um => um.UserId);

        builder.Entity<UserMeeting>()
            .HasOne(um => um.Meeting)
            .WithMany(m => m.UserMeetings)
            .HasForeignKey(um => um.MeetingId);
    }
}


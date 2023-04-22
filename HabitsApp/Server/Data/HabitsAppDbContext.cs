using HabitsApp.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace HabitsApp.Server.Data
{
    public class HabitsAppDbContext : DbContext
    {
        public HabitsAppDbContext(DbContextOptions<HabitsAppDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Goals
            modelBuilder.Entity<Goal>().HasData(new Goal
            {
                Id = 1,
                ActivityId = 3, // Meditation
                Date = new DateTime(2023, 04, 22),
                DurationMinutes = 15
            });
            modelBuilder.Entity<Goal>().HasData(new Goal
            {
                Id = 2,
                ActivityId = 1, // Guitar
                Date = new DateTime(2023, 04, 22),
                DurationMinutes = 45,
                IsCompleted = false
            });
            modelBuilder.Entity<Goal>().HasData(new Goal
            {
                Id = 3,
                ActivityId = 5, // Italian
                Date = new DateTime(2023, 04, 23),
                DurationMinutes = 30,
                IsCompleted = false
            });
            modelBuilder.Entity<Goal>().HasData(new Goal
            {
                Id = 4,
                ActivityId = 7, // Running
                Date = new DateTime(2023, 04, 21),
                DurationMinutes = 30,
                IsCompleted = false
            });
            modelBuilder.Entity<Goal>().HasData(new Goal
            {
                Id = 5,
                ActivityId = 6, // Freeletics
                Date = new DateTime(2023, 04, 22),
                DurationMinutes = 40,
                IsCompleted = false
            });
            modelBuilder.Entity<Goal>().HasData(new Goal
            {
                Id = 6,
                ActivityId = 3, // Meditation
                Date = new DateTime(2023, 04, 24),
                DurationMinutes = 15,
                IsCompleted = false
            });
            modelBuilder.Entity<Goal>().HasData(new Goal
            {
                Id = 7,
                ActivityId = 1, // Guitar
                Date = new DateTime(2023, 04, 24),
                DurationMinutes = 30,
                IsCompleted = false
            });
            modelBuilder.Entity<Goal>().HasData(new Goal
            {
                Id = 8,
                ActivityId = 2, // Blazor
                Date = new DateTime(2023, 04, 21),
                DurationMinutes = 60*11, // 11 Hours
                IsCompleted = true
            });

            // Categories
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = 1,
                Name = "Sport"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = 2,
                Name = "Hobby"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = 3,
                Name = "Coding"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = 4,
                Name = "Ausbildung"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = 5,
                Name = "Language"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = 6,
                Name = "Youtube"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = 7,
                Name = "Health"
            });
            
            // Activities
            modelBuilder.Entity<Activity>().HasData(new Activity
            {
                Id = 1,
                CategoryId = 2, // Hobby
                Name = "Guitar"        
            });
            modelBuilder.Entity<Activity>().HasData(new Activity
            {
                Id = 2,
                CategoryId = 3, // Coding
                Name = "Blazor"
            });
            modelBuilder.Entity<Activity>().HasData(new Activity
            {
                Id = 3,
                CategoryId = 7, // Health
                Name = "Meditation"
            });
            modelBuilder.Entity<Activity>().HasData(new Activity
            {
                Id = 4,
                CategoryId = 4, // Ausbildung
                Name = "LF8: Project - Data Serialization"
            });
            modelBuilder.Entity<Activity>().HasData(new Activity
            {
                Id = 5,
                CategoryId = 5, // Language
                Name = "Italian"
            });
            modelBuilder.Entity<Activity>().HasData(new Activity
            {
                Id = 6,
                CategoryId = 1, // Sport
                Name = "Freeletics"
            });
            modelBuilder.Entity<Activity>().HasData(new Activity
            {
                Id = 7,
                CategoryId = 1, // Sport
                Name = "Running"
            });
            modelBuilder.Entity<Activity>().HasData(new Activity
            {
                Id = 8,
                CategoryId = 6, // Youtube
                Name = "Video 3"
            });
            modelBuilder.Entity<Activity>().HasData(new Activity
            {
                Id = 9,
                CategoryId = 2, // Hobby
                Name = "Analog Photography"
            });
            modelBuilder.Entity<Activity>().HasData(new Activity
            {
                Id = 10,
                CategoryId = 2, // Hobby
                Name = "Digital Photography"
            });
            modelBuilder.Entity<Activity>().HasData(new Activity
            {
                Id = 11,
                CategoryId = 7, // Health
                Name = "Cook new healthy recipy"
            });
            modelBuilder.Entity<Activity>().HasData(new Activity
            {
                Id = 12,
                CategoryId = 3, // Coding
                Name = "React"
            });
            modelBuilder.Entity<Activity>().HasData(new Activity
            {
                Id = 13,
                CategoryId = 5, // Language
                Name = "German"
            });

            // Calendar Entries
            modelBuilder.Entity<CalendarEntry>().HasData(new CalendarEntry
            {
                Id = 1,
                ActivityId = 3, // Meditation
                Date = new DateTime(2023, 04, 12),
                Start = new DateTime(2023, 04, 12, 06, 00, 00),
                End = new DateTime(2023, 04, 12, 06, 20, 00)             
            });
            modelBuilder.Entity<CalendarEntry>().HasData(new CalendarEntry
            {
                Id = 2,
                ActivityId = 7, // Running
                Date = new DateTime(2023, 04, 12),
                Start = new DateTime(2023, 04, 12, 06, 30, 00),
                End = new DateTime(2023, 04, 12, 06, 55, 00),
                Comment = "First time running after a long time"
            });
            modelBuilder.Entity<CalendarEntry>().HasData(new CalendarEntry
            {
                Id = 3,
                ActivityId = 1, // Guitar
                Date = new DateTime(2023, 04, 12),
                Start = new DateTime(2023, 04, 12, 17, 00, 00),
                End = new DateTime(2023, 04, 12, 17, 30, 00)
            });
            modelBuilder.Entity<CalendarEntry>().HasData(new CalendarEntry
            {
                Id = 4,
                ActivityId = 1, // Guitar
                Date = new DateTime(2023, 04, 12),
                Start = new DateTime(2023, 04, 12, 17, 30, 00),
                End = new DateTime(2023, 04, 12, 17, 50, 00)
            });
            modelBuilder.Entity<CalendarEntry>().HasData(new CalendarEntry
            {
                Id = 5,
                ActivityId = 5, // Italian
                Date = new DateTime(2023, 04, 13),
                Start = new DateTime(2023, 04, 13, 17, 30, 00),
                End = new DateTime(2023, 04, 13, 17, 50, 00)
            });
            modelBuilder.Entity<CalendarEntry>().HasData(new CalendarEntry
            {
                Id = 6,
                ActivityId = 2, // Blazor
                Date = new DateTime(2023, 04, 13),
                Start = new DateTime(2023, 04, 13, 20, 00, 00),
                End = new DateTime(2023, 04, 13, 21, 15, 00),
                Comment = "Started planning app (DB Design)"
            });
        }

        public DbSet<Goal> Goals { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<CalendarEntry> CalendarEntries { get; set; }
    }
}

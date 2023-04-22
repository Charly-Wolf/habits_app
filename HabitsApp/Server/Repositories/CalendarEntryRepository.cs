using HabitsApp.Models.Dtos;
using HabitsApp.Server.Data;
using HabitsApp.Server.Repositories.Contracts;
using HabitsApp.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace HabitsApp.Server.Repositories
{
    public class CalendarEntryRepository : ICalendarEntryRepository
    {
        private readonly HabitsAppDbContext habitsAppDbContext;

        public CalendarEntryRepository(HabitsAppDbContext habitsAppDbContext)
        {
            this.habitsAppDbContext = habitsAppDbContext;
        }
        public async Task<Activity> GetCalendarEntryActivity(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CalendarEntry>> GetCalendarEntries()
        {
            var calendarEntries = await habitsAppDbContext.CalendarEntries.ToListAsync();
            return calendarEntries;
        }

        public async Task<CalendarEntry> GetCalendarEntry(int id)
        {
            var calendarEntry =  await (from entry in habitsAppDbContext.CalendarEntries
                          join activity in habitsAppDbContext.Activities
                          on entry.ActivityId equals activity.Id
                          where entry.Id == id
                          select new CalendarEntry
                          {
                              Id = entry.Id,
                              ActivityId = entry.ActivityId,
                              Date = entry.Date,
                              Start = entry.Start,
                              End = entry.End,
                              Comment = entry.Comment
                          }).SingleOrDefaultAsync();

            return calendarEntry;
        }

        public async Task<IEnumerable<Activity>> GetCalendarEntryActivities()
        {
            var calendarEntryActivities = await habitsAppDbContext.Activities.ToListAsync();
            return calendarEntryActivities;
        }

        public async Task<IEnumerable<Category>> GetCalendarEntryActivityCategories()
        {
            var calendarEntryActivityCategories = await habitsAppDbContext.Categories.ToListAsync();
            return calendarEntryActivityCategories;
        }

        public Task<Category> GetCalendarEntryActivityCategory(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CalendarEntry> AddCalendarEntry(CalendarEntryDto newEntry)
        {
            // Check that the new entry contains an existing Activity
            var entry = await (from activity in habitsAppDbContext.Activities
                               where activity.Id == newEntry.ActivityId
                               select new CalendarEntry { 
                                    Id = newEntry.Id,
                                    ActivityId = activity.Id,
                                    Date = newEntry.Date,
                                    Start = newEntry.Start,
                                    End = newEntry.End,
                                    Comment = newEntry.Comment
                                }).SingleOrDefaultAsync();

            if(entry != null)
            {
                var result = await this.habitsAppDbContext.CalendarEntries.AddAsync(entry);
                await habitsAppDbContext.SaveChangesAsync();
                return result.Entity;
            }
            // else
            return null;
        }

        public Task<CalendarEntry> UpdateCalendarEntry(int id, CalendarEntryDto entry)
        {
            throw new NotImplementedException();
        }

        public Task<CalendarEntry> DeleteCalendarEntry(int id)
        {
            throw new NotImplementedException();
        }
    }
}

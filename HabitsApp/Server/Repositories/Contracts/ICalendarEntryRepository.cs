using HabitsApp.Models.Dtos;
using HabitsApp.Shared.Entities;

namespace HabitsApp.Server.Repositories.Contracts
{
    public interface ICalendarEntryRepository
    {
        // GET Methods
        Task<List<CalendarEntry>> GetCalendarEntries();
        Task<List<Activity>> GetCalendarEntryActivities();
        Task<List<Category>> GetCalendarEntryActivityCategories();
        Task<CalendarEntry> GetCalendarEntry(int id);
        Task<Activity> GetCalendarEntryActivity(int id);
        Task<Category> GetCalendarEntryActivityCategory(int id);

        // POST Methods
        Task<CalendarEntry> AddCalendarEntry(CalendarEntryDto newEntry);

        // PUT Methods
        Task<CalendarEntry> UpdateCalendarEntry(int id, CalendarEntryDto entry);

        // DELETE Methods
        Task<CalendarEntry> DeleteCalendarEntry(int id);
    }
}

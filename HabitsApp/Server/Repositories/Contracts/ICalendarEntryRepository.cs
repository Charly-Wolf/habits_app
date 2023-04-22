using HabitsApp.Models.Dtos;
using HabitsApp.Shared.Entities;

namespace HabitsApp.Server.Repositories.Contracts
{
    public interface ICalendarEntryRepository
    {
        // GET Methods
        Task<IEnumerable<CalendarEntry>> GetCalendarEntries();
        Task<IEnumerable<Activity>> GetCalendarEntryActivities();
        Task<IEnumerable<Category>> GetCalendarEntryActivityCategories();
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

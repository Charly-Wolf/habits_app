using HabitsApp.Models.Dtos;

namespace HabitsApp.Client.Services.Contracts
{
    public interface ICalendarEntryService
    {
        Task<IEnumerable<CalendarEntryDto>> GetCalendarEntries();
        Task<CalendarEntryDto> GetCalendarEntry(int id);
        Task<CalendarEntryDto> AddCalendarEntry(CalendarEntryDto entry);
    }
}

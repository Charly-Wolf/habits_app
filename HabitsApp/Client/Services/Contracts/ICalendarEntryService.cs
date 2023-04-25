using HabitsApp.Models.Dtos;

namespace HabitsApp.Client.Services.Contracts
{
    public interface ICalendarEntryService
    {
        Task<List<CalendarEntryDto>> GetCalendarEntries();
        Task<CalendarEntryDto> GetCalendarEntry(int id);
        Task<CalendarEntryDto> AddCalendarEntry(CalendarEntryDto entry);
        Task<CalendarEntryDto> DeleteCalendarEntry(int id);
    }
}

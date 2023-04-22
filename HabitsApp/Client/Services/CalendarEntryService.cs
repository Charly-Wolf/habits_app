using HabitsApp.Client.Services.Contracts;
using HabitsApp.Models.Dtos;
using System.Net.Http.Json;

namespace HabitsApp.Client.Services
{
    public class CalendarEntryService : ICalendarEntryService
    {
        private readonly HttpClient httpClient;

        public CalendarEntryService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CalendarEntryDto> AddCalendarEntry(CalendarEntryDto entryToAddDto)
        {
            var response = await httpClient.PostAsJsonAsync<CalendarEntryDto>("api/CalendarEntry", entryToAddDto);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent) 
                {
                    return default(CalendarEntryDto);
                }

                return await response.Content.ReadFromJsonAsync<CalendarEntryDto>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status: {response.StatusCode} Message -{message}");
            }
        }

        public async Task<IEnumerable<CalendarEntryDto>> GetCalendarEntries()
        {
			try
			{
                var calendarEntries = await httpClient.GetFromJsonAsync<IEnumerable<CalendarEntryDto>>("api/CalendarEntry");

                return calendarEntries;
            }
			catch (Exception)
			{

				throw;
			}
        }

        public Task<CalendarEntryDto> GetCalendarEntry(int id)
        {
            throw new NotImplementedException();
        }
    }
}

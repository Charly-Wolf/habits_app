using HabitsApp.Client.Services.Contracts;
using HabitsApp.Models.Dtos;
using Microsoft.AspNetCore.Components;

namespace HabitsApp.Client.Pages
{
    public partial class ActivityDetails
    {
        [Parameter] public int Id { get; set; }

        [Inject] public ICalendarEntryService? CalendarEntryService { get; set; }
        [Inject] public IActivityService? ActivityService { get; set; }
        public ActivityDto? Activity { get; set; }
        public IEnumerable<CalendarEntryDto>? CalendarEntries { get; set; }
        public List<CalendarEntryDto>? ActivityCalendarEntries = new List<CalendarEntryDto>();
        public string? ErrorMessage { get; set; }
        private string pathToActivityPractice = "/";
        private const string pathToCalendar = "/calendarEntries";

        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (ActivityService !=  null && CalendarEntryService != null) 
                {
                    Activity = await ActivityService.GetActivity(Id);
                    CalendarEntries = await CalendarEntryService.GetCalendarEntries();
                    CalendarEntries = CalendarEntries.OrderBy(entry => entry.Start); // TODO: Check if Sorting works

                    foreach (var entry in CalendarEntries) 
                    {
                        Console.WriteLine($"Entry Name in total calendar: {entry.ActivityName}");
                        if (entry.ActivityName == Activity.Name) 
                        {
                            Console.WriteLine("-------->Bingo Added!!");
                            ActivityCalendarEntries?.Add(entry);
                            Console.WriteLine($"So far {ActivityCalendarEntries?.Count().ToString()} Activities Added!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}

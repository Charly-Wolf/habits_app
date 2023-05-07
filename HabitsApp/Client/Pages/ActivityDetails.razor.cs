using HabitsApp.Client.Services;
using HabitsApp.Client.Services.Contracts;
using HabitsApp.Models.Dtos;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace HabitsApp.Client.Pages
{
    public partial class ActivityDetails
    {
        [Parameter] public int Id { get; set; }

        [Inject] public ICalendarEntryService? CalendarEntryService { get; set; }
        [Inject] public IActivityService? ActivityService { get; set; }
        [Inject] public DialogService? DialogService { get; set; }
        [Inject] private NavigationManager? NavigationManager { get; set; }
        public ActivityDto? Activity { get; set; }
        public List<ActivityDto>? Activities { get; set; }
        public IEnumerable<CalendarEntryDto>? CalendarEntries { get; set; }
        public List<CalendarEntryDto>? ActivityCalendarEntries = new List<CalendarEntryDto>();
        public string? ErrorMessage { get; set; }
        private string pathToActivityPractice = "/";
        private const string pathToCalendar = "/calendarEntries";

        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (ActivityService != null && CalendarEntryService != null)
                {
                    Activity = await ActivityService.GetActivity(Id);
                    Activities = await ActivityService.GetActivities();
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

        public async void DeleteActivity()
        {
            if (DialogService != null && Activity != null && ActivityCalendarEntries != null)
            {
                var confirmed = (bool)await DialogService.Confirm(
                    $"Are you sure you want to Delete the activity " +
                    $"{Activity.Name}" +
                    $"{(ActivityCalendarEntries.Count > 0? $" and its {(ActivityCalendarEntries.Count == 1? "entry" : $"{ActivityCalendarEntries.Count} entries")}" : "" )}?");

                if (confirmed && NavigationManager != null && Activities != null && ActivityService != null)
                {
                    await ActivityService.DeleteActivity(Id); // DELETE Method
                    NavigationManager.NavigateTo("/activities/");
                }
            }
        }
    }
}

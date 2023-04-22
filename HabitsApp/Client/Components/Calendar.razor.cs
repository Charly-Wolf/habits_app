using HabitsApp.Client.Services.Contracts;
using HabitsApp.Models.Dtos;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace HabitsApp.Client.Components
{
    public partial class Calendar
    {
        RadzenDataGrid<CalendarEntryDto>? entriesGrid;
        [Inject] public ICalendarEntryService? CalendarEntryService { get; set; }
        [Inject] public IActivityService? ActivityService { get; set; }

        public IEnumerable<CalendarEntryDto>? CalendarEntries { get; set; }
        public IEnumerable<ActivityDto>? Activities { get; set; }

        CalendarEntryDto? entryToInsert;
        CalendarEntryDto? entryToUpdate;

        protected override async Task OnInitializedAsync()
        {
            if (CalendarEntryService != null && ActivityService != null)
            {
                Activities = await ActivityService.GetActivities();
                Activities = Activities.OrderBy(activity  => activity.Name);
                CalendarEntries = await CalendarEntryService.GetCalendarEntries();
                CalendarEntries = CalendarEntries.OrderBy(entry => entry.Start); // TODO: Check if Sorting works
            }
        }

        async Task EditRow(CalendarEntryDto entry) // TODO: Editing should also impact in the goals, if I delete an entry or change the duration for a shorter one as the goal, then goal should be marked as not completed
        {
            entryToUpdate = entry;
            if (entriesGrid != null) 
            {
                await entriesGrid.EditRow(entry);
            }    
        }

        void CancelEdit(CalendarEntryDto entry)
        {
            if (entry == entryToInsert)
            {
                entryToInsert = null;
            }

            entryToUpdate = null;

            entriesGrid?.CancelEditRow(entry);

            // TODO: update UI if cancelled
        }
    }
}

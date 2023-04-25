using HabitsApp.Client.Pages;
using HabitsApp.Client.Services;
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

        public List<CalendarEntryDto>? CalendarEntries { get; set; }
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
                //CalendarEntries = CalendarEntries.OrderBy(entry => entry.Start); // TODO: Check if Sorting works
            }
        }

        async Task EditRow(CalendarEntryDto entry) // TODO: Editing should also impact in the entries, if I delete an entry or change the duration for a shorter one as the entry, then entry should be marked as not completed
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

        async Task DeleteRow(CalendarEntryDto entryToDelete) // When Pressing the Delete Button
        {
            if (entryToDelete == entryToInsert)
            {
                entryToInsert = null;
            }

            if (entryToDelete == entryToUpdate)
            {
                entryToUpdate = null;
            }

            if (CalendarEntries != null && CalendarEntries.Contains(entryToDelete) && entriesGrid != null && CalendarEntryService != null)
            {
                await CalendarEntryService.DeleteCalendarEntry(entryToDelete.Id); // DELETE Method
                removeCalendarEntryFromGrid(entryToDelete.Id); // Update UI
                await entriesGrid.Reload();
            }
            else
            {
                entriesGrid?.CancelEditRow(entryToDelete);
                await entriesGrid.Reload();
            }
        }

        // Helper Methods for the UI
        private CalendarEntryDto GetCalendarEntry(int id)
        {
            return CalendarEntries.FirstOrDefault(g => g.Id == id); // The arrow function works as a ForEach loop
        }
        private void removeCalendarEntryFromGrid(int id)
        {
            var goalToRemove = GetCalendarEntry(id);

            CalendarEntries?.Remove(goalToRemove);
        }
    }
}

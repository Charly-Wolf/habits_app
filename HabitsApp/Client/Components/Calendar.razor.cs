﻿using HabitsApp.Client.Pages;
using HabitsApp.Client.Services;
using HabitsApp.Client.Services.Contracts;
using HabitsApp.Models.Dtos;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace HabitsApp.Client.Components
{
    public partial class Calendar
    {
        RadzenDataGrid<CalendarEntryDto>? entriesGrid;
        [Inject] public ICalendarEntryService? CalendarEntryService { get; set; }
        [Inject] public IActivityService? ActivityService { get; set; }
        [Inject] public DialogService? DialogService { get; set; }

        public List<CalendarEntryDto>? CalendarEntries { get; set; }
        public IEnumerable<ActivityDto>? Activities { get; set; }

        CalendarEntryDto? entryToInsert;
        CalendarEntryDto? entryToUpdate;
        public string? EntryDurationString;
        public bool DeleteBtnVisible = true;
        public bool SaveBtnDisabled = true;

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

        public string CalculateDuration(DateTime start, DateTime end) 
        {
            TimeSpan duration = end.Subtract(start);
            EntryDurationString = duration.Hours >= 0 ?
                string.Format("{0:D2}h : {1:D2}m", duration.Hours, duration.Minutes, duration.Seconds) :
                string.Format("{0:D2}m", duration.Minutes);
            return EntryDurationString;
        }

        async Task InsertRow() // Creates a new CalendarEntryDto that will be added (POST Request) to the DB if saved
        {
            if (entriesGrid != null)
            {
                entryToInsert = new CalendarEntryDto();
                entryToInsert.Date = DateTime.Now; // Default Date for a new Calendar Entry = today
                await entriesGrid.InsertRow(entryToInsert);
            }
            DeleteBtnVisible = false;
        }

        async Task SaveRow(CalendarEntryDto entryToAdd) // When clicking the SAVE BTN
        {
            if (entriesGrid != null)
            {
                await entriesGrid.UpdateRow(entryToAdd);
            }
            DeleteBtnVisible = true;
            ToggleSaveBtn();
            await OnInitializedAsync();
        }

        async void OnCreateRow(CalendarEntryDto newEntry) // After saving a NEW Calendar Entry
        {
            if (CalendarEntries != null && Activities != null)
            {
                foreach (var entry in CalendarEntries)
                {
                    foreach (var activity in Activities)
                    {
                        if (newEntry.ActivityId == activity.Id)
                        {
                            newEntry.ActivityName = activity.Name;
                            break;
                        }
                    }
                }
            }

            if (CalendarEntryService != null)
            {
                await CalendarEntryService.AddCalendarEntry(newEntry); // POST Request
            }
            entryToInsert = null;
        }

        async Task EditRow(CalendarEntryDto entry) // TODO: Editing should also impact in the entries, if I delete an entry or change the duration for a shorter one as the entry, then entry should be marked as not completed
        {
            entryToUpdate = entry;
            if (entriesGrid != null) 
            {
                await entriesGrid.EditRow(entry);
            }
            DeleteBtnVisible = false;
        }

        void CancelEdit(CalendarEntryDto entry)
        {
            if (entry == entryToInsert)
            {
                entryToInsert = null;
            }

            entryToUpdate = null;

            entriesGrid?.CancelEditRow(entry);
            DeleteBtnVisible = true;
            ToggleSaveBtn();
            // TODO: update UI if cancelled
        }

        async Task DeleteRow(CalendarEntryDto entryToDelete) // When Pressing the Delete Button
        {
            if (DialogService != null)
            {
                var confirmed = (bool)await DialogService.Confirm(
                    $"Are you sure you want to Delete the entry for " +
                    $"{entryToDelete.ActivityName}?");

                if (confirmed && entriesGrid != null)
                {
                    if (entryToDelete == entryToInsert)
                    {
                        entryToInsert = null;
                    }

                    if (entryToDelete == entryToUpdate)
                    {
                        entryToUpdate = null;
                    }

                    if (CalendarEntries != null && CalendarEntries.Contains(entryToDelete) && CalendarEntryService != null)
                    {
                        await CalendarEntryService.DeleteCalendarEntry(entryToDelete.Id); // DELETE Method
                        removeCalendarEntryFromGrid(entryToDelete.Id); // Update UI
                        await entriesGrid.Reload();
                    }
                    else
                    {
                        entriesGrid.CancelEditRow(entryToDelete);
                        await entriesGrid.Reload();
                    }
                }
            }
        }

        // Helper Methods for the UI
        private CalendarEntryDto GetCalendarEntry(int id)
        {
            return CalendarEntries.FirstOrDefault(g => g.Id == id); // The arrow function works as a ForEach loop
        }
        private void removeCalendarEntryFromGrid(int id)
        {
            var entryToRemove = GetCalendarEntry(id);

            CalendarEntries?.Remove(entryToRemove);
        }

        public void ToggleSaveBtn()
        {
            SaveBtnDisabled = !SaveBtnDisabled;
        }

        public void OnChangeActDropDown()
        {
            if (SaveBtnDisabled) ToggleSaveBtn();
        }
    }
}

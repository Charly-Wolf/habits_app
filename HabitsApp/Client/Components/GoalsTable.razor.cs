using HabitsApp.Client.Services;
using HabitsApp.Client.Services.Contracts;
using HabitsApp.Models.Dtos;
using HabitsApp.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace HabitsApp.Client.Components
{
    public partial class GoalsTable
    {
        RadzenDataGrid<GoalDto>? goalsGrid;
        public IEnumerable<ActivityDto>? Activities { get; set; }
        public List<GoalDto>? Goals { get; set; }
        [Inject] public IGoalService? GoalsService { get; set; }
        [Inject] public IActivityService? ActivityService { get; set; }

        private DateTime defaultDate = new DateTime(2023, 04, 22); // DEFAULT DATE FILTER - TODO: it should be TODAY, for now as test is set to 22/04/23

        GoalDto? goalToInsert; // new Goal to be added to the DB (POST)
        GoalDto? goalToUpdate; // Goal to be edited in the DB (PUT)

        protected override async Task OnInitializedAsync()
        {
            if (ActivityService != null && GoalsService != null)
            {
                Activities = await ActivityService.GetActivities();
                Activities = Activities.OrderBy(activity => activity.Name);
                Goals = await GoalsService.GetGoals();
            }
        }
        async Task InsertRow() // Creates a new GoalDto that will be added (POST Request) to the DB if saved
        {
            if (goalsGrid != null) 
            {
                goalToInsert = new GoalDto();
                goalToInsert.Date = DateTime.Now; // Default Date for a new Goal = today
                await goalsGrid.InsertRow(goalToInsert);
            }
        }
        async Task EditRow(GoalDto goalToEdit) 
        {
            goalToUpdate = goalToEdit;
            if (goalsGrid != null)
            {
                await goalsGrid.EditRow(goalToEdit);
            }
        }

        void CancelEdit(GoalDto goal)
        {
            if (goal == goalToInsert)
            {
                goalToInsert = null;
            }

            goalToUpdate = null;

            goalsGrid?.CancelEditRow(goal);

            // TODO: update UI if cancelled
        }

        async Task SaveRow(GoalDto goalToAdd) // When clicking the SAVE BTN
        {
            if (goalsGrid != null)
            {
                await goalsGrid.UpdateRow(goalToAdd);
                Console.WriteLine($"--------NEW GOAL: Activity Id: {goalToAdd.ActivityId} Name: {goalToAdd.ActivityName} Date: {goalToAdd.Date} Duration: {goalToAdd.DurationMinutes}");
            }
            await OnInitializedAsync();
        }

        async void OnCreateRow(GoalDto newGoal) // After saving a NEW Goal
        {
            if (Goals != null && Activities != null) 
            {
                foreach (var goal in Goals)
                {
                    foreach (var activity in Activities)
                    {
                        if (newGoal.ActivityId == activity.Id)
                        {
                            newGoal.ActivityName = activity.Name;
                            break;
                        }
                    }
                }
            }

            if (GoalsService != null)
            {
                await GoalsService.AddGoal(newGoal); // POST Request
            }
            goalToInsert = null;
        }

        async Task DeleteRow(GoalDto goalToDelete) // When Pressing the Delete Button
        {
            if (goalToDelete == goalToInsert)
            {
                goalToInsert = null;
            }

            if (goalToDelete == goalToUpdate)
            {
                goalToUpdate = null;
            }

            if (Goals != null && Goals.Contains(goalToDelete) && goalsGrid != null && GoalsService != null)
            {
                await GoalsService.DeleteGoal(goalToDelete.Id); // DELETE Method
                removeGoalFromGrid(goalToDelete.Id); // Update UI
                await goalsGrid.Reload();
            }
            else
            {
                goalsGrid?.CancelEditRow(goalToDelete);
                await goalsGrid.Reload();
            }
        }

        // Helper Methods for the UI
        private GoalDto GetGoal(int id)
        {
            return Goals.FirstOrDefault(g => g.Id == id); // The arrow function works as a ForEach loop
        }
        private void removeGoalFromGrid(int id)
        {
            var goalToRemove = GetGoal(id);

            Goals?.Remove(goalToRemove);
        }
    }
}

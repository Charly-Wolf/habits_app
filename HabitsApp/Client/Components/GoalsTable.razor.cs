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
        [Parameter] public IEnumerable<ActivityDto>? Activities { get; set; }
        [Parameter] public IEnumerable<GoalDto>? Goals { get; set; }
        [Parameter] public IGoalService? GoalsService { get; set; }

        private DateTime defaultDate = new DateTime(2023, 04, 22); // DEFAULT DATE FILTER - TODO: it should be TODAY, for now as test is set to 22/04/23

        GoalDto? goalToInsert; // new Goal to be added to the DB (POST)
        GoalDto? goalToUpdate; // Goal to be edited in the DB (PUT)


        async Task InsertRow() // Creates a new GoalDto that will be added (POST Request) to the DB if saved
        {
            if (goalsGrid != null) 
            {
                goalToInsert = new GoalDto();
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
            }
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

            Console.WriteLine($"GOAL TO BE SAVED: Date: {newGoal.Date} - Activity ID {newGoal.ActivityId} - Activity Name: {newGoal.ActivityName} - Duration: {newGoal.DurationMinutes}");

            if (GoalsService != null)
            {
                Console.WriteLine("GoalService not null !!!");
                await GoalsService.AddGoal(newGoal); // POST Request
            }
            goalToInsert = null;
        }
    }
}

using HabitsApp.Client.Services;
using HabitsApp.Client.Services.Contracts;
using HabitsApp.Models.Dtos;
using HabitsApp.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Radzen;
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
        [Inject] DialogService? DialogService { get; set; }

        private DateTime defaultDate = new DateTime(2023, 04, 22); // DEFAULT DATE FILTER - TODO: it should be TODAY, for now as test is set to 22/04/23

        GoalDto? goalToInsert; // new Goal to be added to the DB (POST)
        GoalDto? goalToUpdate; // Goal to be edited in the DB (PUT)
        public string CompletedGoalsString = "";
        public bool DeleteBtnVisible = true;
        public bool SaveBtnDisabled = true;

        protected override async Task OnInitializedAsync()
        {
            if (ActivityService != null && GoalsService != null)
            {
                Activities = await ActivityService.GetActivities();
                Activities = Activities.OrderBy(activity => activity.Name);
                Goals = await GoalsService.GetGoals();
                await calculateCompletedGoals();
            }
        }
        async Task InsertRow() // Creates a new GoalDto that will be added (POST Request) to the DB if saved
        {
            if (goalsGrid != null) 
            {
                goalToInsert = new GoalDto();
                goalToInsert.Date = DateTime.Now; // Default Date for a new Goal = today
                DeleteBtnVisible = false;
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
            DeleteBtnVisible = false;
        }

        void CancelEdit(GoalDto goal)
        {
            if (goal == goalToInsert)
            {
                goalToInsert = null;
            }

            goalToUpdate = null;

            goalsGrid?.CancelEditRow(goal);
            DeleteBtnVisible = true;
            ToggleSaveBtn();

            // TODO: update UI if cancelled
        }

        async Task SaveRow(GoalDto goalToAdd) // When clicking the SAVE BTN
        {
            if (goalsGrid != null)
            {
                await goalsGrid.UpdateRow(goalToAdd);
                DeleteBtnVisible = true;
                await OnInitializedAsync();
                ToggleSaveBtn();
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

            if (GoalsService != null)
            {
                await GoalsService.AddGoal(newGoal); // POST Request
            }
            goalToInsert = null;
        }

        async Task DeleteRow(GoalDto goalToDelete) // When Pressing the Delete Button
        {
            if (DialogService != null)
            {
                var confirmed = (bool)await DialogService.Confirm(
                    $"Are you sure you want to Delete the goal " +
                    $"{goalToDelete.ActivityName}?");

                if (confirmed && goalsGrid != null)
                {
                    if (confirmed && GoalsService != null)
                        if (goalToDelete == goalToInsert)
                        {
                            goalToInsert = null;
                        }

                    if (goalToDelete == goalToUpdate)
                    {
                        goalToUpdate = null;
                    }

                    if (Goals != null && Goals.Contains(goalToDelete) && GoalsService != null)
                    {
                        await GoalsService.DeleteGoal(goalToDelete.Id); // DELETE Method
                        removeGoalFromGrid(goalToDelete.Id); // Update UI
                        await goalsGrid.Reload();
                    }
                    else
                    {
                        goalsGrid.CancelEditRow(goalToDelete);
                        await goalsGrid.Reload();
                    }
                }
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

        private async Task calculateCompletedGoals()
        {
            var CompletedGoals = 0;
            if (GoalsService != null)
            {
                Goals = await GoalsService.GetGoals();

                foreach (var goal in Goals) { if (goal.IsCompleted) CompletedGoals++; }
                CompletedGoalsString = $"Goals completed: {CompletedGoals} /  {Goals.Count()}";
            }
        }

        public async void CheckGoal(object goal)
        {
            GoalDto goalToEdit = (GoalDto)goal;
            if (DialogService != null)
            {
                var confirmed = (bool)await DialogService.Confirm(
                    $"Are you sure you want to mark the goal as " +
                    $"{(goalToEdit.IsCompleted ? "not " : "")}completed?");

                if (confirmed && GoalsService != null)
                {
                    goalToEdit.IsCompleted = !goalToEdit.IsCompleted;
                    await GoalsService.UpdateGoal(goalToEdit);
                    await calculateCompletedGoals();
                }
                StateHasChanged();
            }
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

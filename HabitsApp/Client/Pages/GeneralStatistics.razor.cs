using HabitsApp.Client.Services.Contracts;
using HabitsApp.Models.Dtos;
using Microsoft.AspNetCore.Components;

namespace HabitsApp.Client.Pages
{
    public partial class GeneralStatistics
    {

        //TODO: THIS IS JUST TEMPORARY, FOR TESTING
        
        public IEnumerable<GoalDto>? Goals { get; set; }
        public int CompletedGoals = 0;
        public string text = "";
        [Inject] public IGoalService? goalService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            if (goalService != null)
            {
                Goals = await goalService.GetGoals();

                foreach (var goal in Goals) { if (goal.IsCompleted) CompletedGoals++;  }
                text = $"Goals completed: {CompletedGoals} /  {Goals.Count()}";
            }
        }
    }
}

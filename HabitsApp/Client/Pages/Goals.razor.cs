using HabitsApp.Client.Services.Contracts;
using HabitsApp.Models.Dtos;
using Microsoft.AspNetCore.Components;

namespace HabitsApp.Client.Pages
{
    public partial class Goals
    {
        public IEnumerable<ActivityDto>? ActivityDtos { get; set; }
        public IEnumerable<GoalDto>? GoalDtos { get; set; }
        [Inject] public IActivityService? ActivityService { get; set; }
        [Inject] public IGoalService? GoalService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            if (ActivityService != null && GoalService != null)
            {
                ActivityDtos = await ActivityService.GetActivities();
                ActivityDtos = ActivityDtos.OrderBy(activity => activity.Name);
                GoalDtos = await GoalService.GetGoals();
                GoalDtos = GoalDtos.OrderBy(entry => entry.Date);
            }
        } 
    }
}

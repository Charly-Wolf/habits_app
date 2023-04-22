using HabitsApp.Client.Services.Contracts;
using HabitsApp.Models.Dtos;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace HabitsApp.Client.Components
{
    public partial class ActivityCards
    {
        [Inject] public IActivityService? ActivityService { get; set; }

        public IEnumerable<ActivityDto>? Activities { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (ActivityService != null)
            {
                Activities = await ActivityService.GetActivities();
            }
        }

        Orientation orientation = Orientation.Horizontal;
        FlexWrap flexWrap = FlexWrap.Wrap;
    }
}

using HabitsApp.Client.Services.Contracts;
using HabitsApp.Models.Dtos;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace HabitsApp.Client.Components
{
    public partial class ActivityCards
    {
        [Inject] public IActivityService? ActivityService { get; set; }

        public IEnumerable<ActivityDto>? Activities { get; set; }
        public bool CreateActivityVisible = false;
        public bool AddActivityDisabled = false;

        protected override async Task OnInitializedAsync()
        {
            if (ActivityService != null)
            {
                Activities = await ActivityService.GetActivities();
            }
        }

        Orientation orientation = Orientation.Horizontal;
        FlexWrap flexWrap = FlexWrap.Wrap;

        private async void ToggleNewActivityDialog()
        {
            CreateActivityVisible = !CreateActivityVisible;
            AddActivityDisabled = !AddActivityDisabled;

            await OnInitializedAsync();
            StateHasChanged();
        }
    }
}

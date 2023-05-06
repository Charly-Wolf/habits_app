using HabitsApp.Client.Services.Contracts;
using HabitsApp.Models.Dtos;
using HabitsApp.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace HabitsApp.Client.Components
{
    public partial class CreateActivityDialog
    {
        [Parameter] public bool AddActivityDialogVisible {get; set; }
        [Parameter] public EventCallback CloseAddActivityDialog { get; set; }
   
        [Inject] public ICategoryService? CategoryService { get; set; }
        [Inject] public IActivityService? ActivityService { get; set; }

        public IEnumerable<Category>? Categories { get; set; }
        public int NewActivityCategoryId { get; set; }
        public string? NewActivityName;

        protected override async Task OnInitializedAsync()
        {
            if (CategoryService != null)
            {
                Categories = await CategoryService.GetCategories();
                Categories = Categories.OrderBy(category => category.Name);
            }
        }

        public async void SaveActivity()
        {
            //var newActivityCategory = await CategoryService.GetCategory(NewActivityCategoryId);

            //var newActivity = new ActivityDto
            //{
                //Name = NewActivityName,
                //CategoryId = NewActivityCategoryId,
                ////CategoryName = newActivityCategory.Name
            //};

            //await ActivityService.AddActivity(newActivity);

            await CloseAddActivityDialog.InvokeAsync();
        }

        public async void CancelAddActivity()
        {
            await CloseAddActivityDialog.InvokeAsync();
        }
    }
}

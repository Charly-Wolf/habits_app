using HabitsApp.Client.Services.Contracts;
using HabitsApp.Models.Dtos;
using HabitsApp.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace HabitsApp.Client.Components
{
    public partial class CreateActivityDialog
    {
        [Parameter] public bool AddActivityDialogVisible {get; set; }
        [Parameter] public EventCallback CloseAddActivityDialog { get; set; }
   
        [Inject] public ICategoryService? CategoryService { get; set; }
        [Inject] public IActivityService? ActivityService { get; set; }

        public RadzenRequiredValidator? ActivityNameValidator { get; set; }
        public IEnumerable<Category>? Categories { get; set; }
        public string? NewActivityCategoryName { get; set; }
        public Category? SelectedCategory { get; set; }
        public bool SaveBtnDisabled = true;
        public ActivityDto? NewActivityDto = new();

        protected override async Task OnInitializedAsync()
        {
            if (CategoryService != null)
            {
                Categories = await CategoryService.GetCategories();
                Categories = Categories.OrderBy(category => category.Name);
                SelectedCategory = Categories.First();
            }
        }

        public async void SaveActivity()
        {
            if (SelectedCategory != null && ActivityService != null && ActivityNameValidator != null && ActivityNameValidator.IsValid)
            {
                NewActivityDto.CategoryId = SelectedCategory.Id;
                NewActivityDto.CategoryName = SelectedCategory.Name;
                await ActivityService.AddActivity(NewActivityDto);
                await CloseAddActivityDialog.InvokeAsync();
                ToggleSaveBtn();
                ClearActivityTextBox();
            }
            Console.WriteLine($"{(ActivityNameValidator.IsValid? "VALID" : "INVALID" )}");
        }

        public async void CancelAddActivity()
        {
            await CloseAddActivityDialog.InvokeAsync();
            ToggleSaveBtn();
            ClearActivityTextBox();
        }

        public void OnChangeCategory(object dropdownCatName)
        {
            if (Categories != null)
            {
                foreach (var cat in Categories)
                {
                    if (cat.Name == dropdownCatName.ToString())
                    {
                        SelectedCategory = cat;
                    }
                }
            }
            if (SaveBtnDisabled) ToggleSaveBtn();
        }

        public void ToggleSaveBtn()
        {
            SaveBtnDisabled = !SaveBtnDisabled;
        }

        public void ClearActivityTextBox()
        {
            NewActivityDto = new();
        }
    }
}

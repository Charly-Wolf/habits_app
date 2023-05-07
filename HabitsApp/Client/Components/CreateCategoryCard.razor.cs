using HabitsApp.Client.Services.Contracts;
using HabitsApp.Models.Dtos;
using HabitsApp.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace HabitsApp.Client.Components
{
    public partial class CreateCategoryCard
    {
        [Parameter] public bool AddCategoryCardVisible { get; set; }
        [Parameter] public EventCallback CloseAddCategoryCard { get; set; }

        [Inject] public ICategoryService? CategoryService { get; set; }

        public RadzenRequiredValidator? CategoryNameValidator { get; set; }
        public IEnumerable<Category>? Categories { get; set; }
        public Category? NewCategory = new();

        public async void SaveCategory()
        {
            if (CategoryService != null && CategoryNameValidator != null && CategoryNameValidator.IsValid)
            {
                await CategoryService.AddCategory(NewCategory);
                await CloseAddCategoryCard.InvokeAsync();
                ClearCategoryTextBox();
            }
        }

        public async void CancelAddCategory()
        {
            await CloseAddCategoryCard.InvokeAsync();
            ClearCategoryTextBox();
        }

        public void ClearCategoryTextBox()
        {
            NewCategory = new();
        }
    }
}

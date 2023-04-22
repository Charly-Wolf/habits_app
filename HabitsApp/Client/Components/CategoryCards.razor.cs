﻿using HabitsApp.Client.Services.Contracts;
using HabitsApp.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace HabitsApp.Client.Components
{
    public partial class CategoryCards
    {
        [Inject] public ICategoryService? CategoryService { get; set; }

        public IEnumerable<Category>? Categories { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (CategoryService != null)
            {
                Categories = await CategoryService.GetCategories();
            }
        }

        Orientation orientation = Orientation.Horizontal;
        FlexWrap flexWrap = FlexWrap.Wrap;
    }
}

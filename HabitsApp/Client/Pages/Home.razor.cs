﻿using HabitsApp.Client.Services.Contracts;
using HabitsApp.Models.Dtos;
using HabitsApp.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace HabitsApp.Client.Pages
{
    public partial class Home
    {
        public List<ActivityDto>? Activities { get; set; }
        public IEnumerable<Category>? AllCategories { get; set; }
        public IEnumerable<Category>? FilteredCategories { get; set; }
        public IEnumerable<GoalDto>? Goals { get; set; }
        
        [Inject] public IActivityService? ActivityService { get; set; }
        [Inject] public ICategoryService? CategoryService { get; set; }
        [Inject] public IGoalService? GoalService { get; set; }
        //public ActivityDto? ActiveActivity = null;
        public ActivityDto? ActiveActivity = null;
        public Category? ActiveCategory = null;

        protected override async Task OnInitializedAsync()
        {
            if (ActivityService != null && CategoryService != null && GoalService != null)
            {
                AllCategories = await CategoryService.GetCategories();
                AllCategories = AllCategories.OrderBy(category => category.Name);
                Activities = await ActivityService.GetActivities();

                var categoryIdsWithActivities = Activities.Select(a => a.CategoryId).Distinct();
                FilteredCategories = AllCategories.Where(c => categoryIdsWithActivities.Contains(c.Id)).ToList();// Does not contain categories with no activities

                //Activities = Activities.OrderBy(activity => activity.Name);
                Goals = await GoalService.GetGoals();
            }
        }

        public void UpdateActiveActivity(ActivityDto activity)
        {
            ActiveActivity = activity;
        }

        public void UpdateActiveCategory(Category category) 
        { 
            ActiveCategory = category;
        }
    }
}

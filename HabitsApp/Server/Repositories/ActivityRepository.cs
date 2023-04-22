using HabitsApp.Server.Data;
using HabitsApp.Server.Repositories.Contracts;
using HabitsApp.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace HabitsApp.Server.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly HabitsAppDbContext habitsAppDbContext;

        public ActivityRepository(HabitsAppDbContext habitsAppDbContext)
        {
            this.habitsAppDbContext = habitsAppDbContext;
        }
        public async Task<IEnumerable<Activity>> GetActivities()
        {
            var activities = await habitsAppDbContext.Activities.ToListAsync();
            return activities;
        }

        public async Task<Activity> GetActivity(int id)
        {
            var activity = await habitsAppDbContext.Activities.FindAsync(id);
            return activity;
        }

        public async Task<Category> GetActivityCategory(int id) // The id Parameter passed in the Activity Controller when calling this method will be the selected Activity's Category Id (not the Activity Id)
        {
            //var activity = await habitsAppDbContext.Activities.FindAsync(id);
            //var activityCategory = activity?.Category;
            var activityCategory = await habitsAppDbContext.Categories.SingleOrDefaultAsync(c => c.Id == id);
            return activityCategory;
        }

        public async Task<IEnumerable<Category>> GetActivityCategories()
        {
            var categories = await habitsAppDbContext.Categories.ToListAsync();
            return categories;
        }
    }
}

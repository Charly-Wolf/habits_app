using HabitsApp.Models.Dtos;
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
        public async Task<List<Activity>> GetActivities()
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

        public async Task<List<Category>> GetActivityCategories()
        {
            var categories = await habitsAppDbContext.Categories.ToListAsync();
            return categories;
        }

        private async Task<bool> activityAlreadExists(int actId, int catId, string actName)
        {
            var exactActExists = await habitsAppDbContext.Activities.AnyAsync(a => a.Id == actId &&
                                                                              a.CategoryId == catId);
            var sameNameActExists = await habitsAppDbContext.Activities.AnyAsync(a => a.Name == actName &&
                                                                                 a.CategoryId == catId);
            return (exactActExists || sameNameActExists);
        }
        public async Task<Activity> PostActivity(ActivityDto activityToAddDto)
        {
            if (activityToAddDto.Name != null)
            {
                if (await activityAlreadExists( // Check if this activity already exists TODO: Handle it in the FRONT END
                    activityToAddDto.Id, 
                    activityToAddDto.CategoryId, 
                    activityToAddDto.Name) == false)
                {
                    var activity = await (from category in habitsAppDbContext.Categories
                                          where category.Id == activityToAddDto.CategoryId
                                          select new Activity
                                          {
                                              Id = activityToAddDto.Id,
                                              CategoryId = activityToAddDto.CategoryId,
                                              Category = category,
                                              Name = activityToAddDto.Name
                                          }).SingleOrDefaultAsync();

                    if (activity != null)
                    {
                        var result = await habitsAppDbContext.Activities.AddAsync(activity);
                        await habitsAppDbContext.SaveChangesAsync();
                        return result.Entity;
                    }
                }
            } 
            
            // If the activity was not successfully added to the DB
            return null;
        }

        public async Task<Activity> DeleteActivity(int id)
        {
            var activityToDelete = await habitsAppDbContext.Activities.FindAsync(id);
            if (activityToDelete != null)
            {
                habitsAppDbContext.Activities.Remove(activityToDelete);
                await habitsAppDbContext.SaveChangesAsync();
            }
            return activityToDelete;
        }
    }
}

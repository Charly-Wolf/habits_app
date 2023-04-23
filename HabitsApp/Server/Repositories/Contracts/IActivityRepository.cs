using HabitsApp.Shared.Entities;

namespace HabitsApp.Server.Repositories.Contracts
{
    public interface IActivityRepository
    {
        Task<List<Activity>> GetActivities();
        Task<List<Category>> GetActivityCategories();
        Task<Activity> GetActivity(int id);
        Task<Category> GetActivityCategory(int id);
    }
}

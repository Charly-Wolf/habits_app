using HabitsApp.Shared.Entities;

namespace HabitsApp.Server.Repositories.Contracts
{
    public interface IActivityRepository
    {
        Task<IEnumerable<Activity>> GetActivities();
        Task<IEnumerable<Category>> GetActivityCategories();
        Task<Activity> GetActivity(int id);
        Task<Category> GetActivityCategory(int id);
    }
}

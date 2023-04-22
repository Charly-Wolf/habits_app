using HabitsApp.Shared.Entities;

namespace HabitsApp.Server.Repositories.Contracts
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(int id);
    }
}

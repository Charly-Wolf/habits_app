using HabitsApp.Shared;
using HabitsApp.Shared.Entities;

namespace HabitsApp.Client.Services.Contracts
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();
    }
}

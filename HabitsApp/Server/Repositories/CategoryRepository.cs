using HabitsApp.Server.Data;
using HabitsApp.Server.Repositories.Contracts;
using HabitsApp.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace HabitsApp.Server.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly HabitsAppDbContext habitsAppDbContext;

        public CategoryRepository(HabitsAppDbContext habitsAppDbContext)
        {
            this.habitsAppDbContext = habitsAppDbContext;
        }
        public async Task<List<Category>> GetCategories()
        {
            var categories = await habitsAppDbContext.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category> GetCategory(int id)
        {
            var category = await habitsAppDbContext.Categories.FindAsync(id);
            return category;
        }
    }
}

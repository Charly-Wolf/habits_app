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

        private async Task<bool> categoryAlreadExists(int catId, string catName)
        {
            var exactCatExists = await habitsAppDbContext.Categories.AnyAsync(c => c.Id == catId);
            var sameNameCatExists = await habitsAppDbContext.Categories.AnyAsync(c => c.Name == catName);
            return (exactCatExists || sameNameCatExists);
        }

        public async Task<Category> PostCategory(Category categoryToAdd)
        {
            if (categoryToAdd != null)
            {
                if (await categoryAlreadExists( // Check if this category already exists TODO: Handle it in the FRONT END
                    categoryToAdd.Id,
                    categoryToAdd.Name) == false)
                {
                    var category = new Category { Name = categoryToAdd.Name };

                    if (category != null)
                    {
                        var result = await habitsAppDbContext.Categories.AddAsync(category);
                        await habitsAppDbContext.SaveChangesAsync();
                        return result.Entity;
                    }
                }
            }

            // If the activity was not successfully added to the DB
            return null;
        }
    }
}

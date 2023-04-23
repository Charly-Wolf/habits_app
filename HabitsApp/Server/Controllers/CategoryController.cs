using HabitsApp.Server.Repositories.Contracts;
using HabitsApp.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HabitsApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            try
            {
                var categories = await categoryRepository.GetCategories();

                if (categories == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(categories);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

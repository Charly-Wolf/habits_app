using HabitsApp.Models.Dtos;
using HabitsApp.Server.Repositories;
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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            try
            {
                var category = await categoryRepository.GetCategory(id);

                if (category == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(category);
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory([FromBody] Category categoryToAdd)
        {
            try
            {
                var newCategory = await categoryRepository.PostCategory(categoryToAdd);
                if (newCategory == null)
                {
                    return NoContent();
                }

                return CreatedAtAction(nameof(GetCategory), new { id = newCategory.Id }, newCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

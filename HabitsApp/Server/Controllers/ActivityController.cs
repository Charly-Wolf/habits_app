using HabitsApp.Models.Dtos;
using HabitsApp.Server.Extensions;
using HabitsApp.Server.Repositories;
using HabitsApp.Server.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HabitsApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityRepository activityRepository;
        private readonly ICategoryRepository categoryRepository;

        public ActivityController(IActivityRepository activityRepository, ICategoryRepository categoryRepository)
        {
            this.activityRepository = activityRepository;
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityDto>>> GetActivities()
        {
            try
            {
                var activities = await activityRepository.GetActivities();
                var categories = await activityRepository.GetActivityCategories();

                if (activities == null || categories == null)
                {
                    return NotFound();
                }
                else
                {
                    var activityDtos = activities.ConvertToDto(categories);

                    return Ok(activityDtos);
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ActivityDto>> GetActivity(int id)
        {
            try
            {
                var activity = await activityRepository.GetActivity(id);

                if (activity == null)
                {
                    return BadRequest();
                }
                else
                {
                    var activityCategory = await activityRepository.GetActivityCategory(activity.CategoryId); // Gets the selected Activity's Category

                    var activityDto = activity.ConvertToDto(activityCategory);

                    return Ok(activityDto);
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ActivityDto>> PostActivity([FromBody] ActivityDto activityToAddDto)
        {
            try
            {
                var newActivity = await activityRepository.PostActivity(activityToAddDto);
                if (newActivity == null)
                {
                    return NoContent();
                }

                var newActivityCategory = await categoryRepository.GetCategory(activityToAddDto.CategoryId);
                if (newActivityCategory == null)
                {
                    throw new Exception($"Something went wrong when attempting to retrieve Category (categoryId:({activityToAddDto.CategoryId})");
                }

                var newActivityDto = newActivity.ConvertToDto(newActivityCategory);

                return CreatedAtAction(nameof(GetActivity), new {id=newActivityDto.Id }, newActivityDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ActivityDto>> DeleteActivity(int id)
        {
            try
            {
                var activityToDelete = await activityRepository.DeleteActivity(id);
                if (activityToDelete == null) { return NotFound(); }

                var activityCategory = await categoryRepository.GetCategory(activityToDelete.CategoryId);

                if (activityCategory == null) { return NotFound(); }

                var activityToDeleteDto = activityToDelete.ConvertToDto(activityCategory);

                return Ok(activityToDeleteDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

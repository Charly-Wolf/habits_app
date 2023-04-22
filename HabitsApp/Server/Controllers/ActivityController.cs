using HabitsApp.Models.Dtos;
using HabitsApp.Server.Extensions;
using HabitsApp.Server.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HabitsApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityRepository activityRepository;

        public ActivityController(IActivityRepository activityRepository)
        {
            this.activityRepository = activityRepository;
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
    }
}

using HabitsApp.Models.Dtos;
using HabitsApp.Server.Extensions;
using HabitsApp.Server.Repositories.Contracts;
using HabitsApp.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HabitsApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalController : ControllerBase
    {
        private readonly IActivityRepository activityRepository;
        private readonly IGoalRepository goalRepository;

        public GoalController(IActivityRepository activityRepository, IGoalRepository goalRepository)
        {
            this.activityRepository = activityRepository;
            this.goalRepository = goalRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<GoalDto>>> GetGoals()
        {
            try
            {
                var goalActivities = await goalRepository.GetGoalActivities();
                var goals = await goalRepository.GetGoals();

                if (goalActivities == null || goals == null)
                {
                    return NotFound();
                }
                else
                {
                    var goalDtos = goals.ConvertToDto(goalActivities);

                    return Ok(goalDtos);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GoalDto>> GetGoal(int id)
        {
            try
            {
                var goal = await goalRepository.GetGoal(id);
                if (goal == null)
                {
                    return NotFound();
                }
                var goalActivity = await activityRepository.GetActivity(goal.ActivityId);
                if (goalActivity == null)
                {
                    return NotFound();
                }

                var goalDto = goal.ConvertToDto(goalActivity);

                return Ok(goalDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<GoalDto>> PostGoal([FromBody] GoalDto goalToAddDto)
        {
            try
            {
                var newGoal = await goalRepository.AddGoal(goalToAddDto);

                if (newGoal == null)
                {
                    return NoContent();
                }

                var newGoalActivity = await activityRepository.GetActivity(goalToAddDto.ActivityId);

                if (newGoalActivity == null)
                {
                    return NoContent();
                }

                var newGoalDto = newGoal.ConvertToDto(newGoalActivity);

                return CreatedAtAction(nameof(GetGoal), new { id = newGoal.Id }, newGoalDto); // If successfull -> return location of the newly added Goal (Best Practices...)
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<GoalDto>> UpdateGoalIsCompleted(int id, GoalDto goalToUpdateDto)
        {
            try
            {
                var goal = await goalRepository.UpdateGoalIsCompleted(id, goalToUpdateDto);
                if (goal == null) { return NotFound(); }

                var activity = await activityRepository.GetActivity(goal.ActivityId);

                var goalDto = goal.ConvertToDto(activity);

                return Ok(goalDto);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<GoalDto>> DeleteGoal(int id)
        {
            try
            {
                var goalToDelete = await goalRepository.DeleteGoal(id);
                if ( goalToDelete == null) { return NotFound(); }

                var goalActivity = await activityRepository.GetActivity(goalToDelete.ActivityId);
                
                if (goalActivity == null) { return NotFound(); }

                var goalToDeleteDto = goalToDelete.ConvertToDto(goalActivity);

                return Ok(goalToDeleteDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

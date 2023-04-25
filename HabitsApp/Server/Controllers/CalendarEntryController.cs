using HabitsApp.Models.Dtos;
using HabitsApp.Server.Data;
using HabitsApp.Server.Extensions;
using HabitsApp.Server.Repositories;
using HabitsApp.Server.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HabitsApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarEntryController : ControllerBase
    {
        private readonly ICalendarEntryRepository calendarEntryRepository;
        private readonly IActivityRepository activityRepository;
        private readonly ICategoryRepository categoryRepository;

        public CalendarEntryController(ICalendarEntryRepository calendarEntryRepository, 
                    IActivityRepository activityRepository,
                    ICategoryRepository categoryRepository)
        {
            this.calendarEntryRepository = calendarEntryRepository;
            this.activityRepository = activityRepository;
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<CalendarEntryDto>>> GetCalendarEntries()
        {
            try
            {
                var calendarEntries = await calendarEntryRepository.GetCalendarEntries();
                var activities = await calendarEntryRepository.GetCalendarEntryActivities();
                var categories = await calendarEntryRepository.GetCalendarEntryActivityCategories();

                if (calendarEntries == null || activities == null || categories == null)
                {
                    return NotFound();
                }
                else
                {
                    var calendarEntryDtos = calendarEntries.ConvertToDto(activities.ConvertToDto(categories));

                    return Ok(calendarEntryDtos);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CalendarEntryDto>> GetCalendarEntry(int id)
        {
            try
            {
                var calendarEntry = await calendarEntryRepository.GetCalendarEntry(id);
                if (calendarEntry == null)
                {
                    return NotFound();
                }
                var entryActivity = await activityRepository.GetActivity(calendarEntry.ActivityId);
                if (entryActivity == null)
                {
                    return NotFound();
                }
                var entryCategory = await categoryRepository.GetCategory(entryActivity.CategoryId);

                var calendarEntryDto = calendarEntry.ConvertToDto(entryActivity.ConvertToDto(entryCategory));
                
                return Ok(calendarEntryDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<CalendarEntryDto>> PostCalendarEntry([FromBody] CalendarEntryDto calendarEntryToAddDto)
        {
            try
            {
                var newEntry = await calendarEntryRepository.AddCalendarEntry(calendarEntryToAddDto);

                if (newEntry == null)
                {
                    return NoContent();
                }

                var newEntryActivity = await activityRepository.GetActivity(calendarEntryToAddDto.ActivityId);

                if (newEntryActivity == null)
                {
                    return NoContent();
                }

                var newEntryCategory = await categoryRepository.GetCategory(newEntryActivity.CategoryId);

                if (newEntryCategory == null)
                {
                    return NoContent();
                }

                var newEntryDto = newEntry.ConvertToDto(newEntryActivity.ConvertToDto(newEntryCategory));

                return CreatedAtAction(nameof(GetCalendarEntry), new { id = newEntry.Id }, newEntryDto); // If successfull -> return location of the newly added Calendar Entry (Best Practices...)
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CalendarEntryDto>> DeleteGoal(int id)
        {
            try
            {
                var entryToDelete = await calendarEntryRepository.DeleteCalendarEntry(id);
                if (entryToDelete == null) { return NotFound(); }

                var entryActivityDto = await activityRepository.GetActivity(entryToDelete.ActivityId);
                if (entryActivityDto == null) { return NotFound(); }

                var entryActivityCategory = await categoryRepository.GetCategory(entryActivityDto.CategoryId);

                var entryToDeleteDto = entryToDelete.ConvertToDto(entryActivityDto.ConvertToDto(entryActivityCategory));

                return Ok(entryToDeleteDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

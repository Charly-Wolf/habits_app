using HabitsApp.Client.Pages;
using HabitsApp.Models.Dtos;
using HabitsApp.Shared.Entities;

namespace HabitsApp.Server.Extensions
{
    public static class DtoConversions
    {
        public static List<ActivityDto> ConvertToDto(this List<Activity> activities,
                                                            List<Category> categories)
        {
            return (from activity in activities
                    join category in categories
                    on activity.CategoryId equals category.Id
                    select new ActivityDto
                    {
                        Id = activity.Id,
                        CategoryId = category.Id,
                        CategoryName = category.Name,
                        Name = activity.Name
                    }
                    ).ToList();
        }

        // Overload (adapt previous method to a SINGLE activity, instead of a collection of activities):
        public static ActivityDto ConvertToDto(this Activity activity, Category category)
        {
            return new ActivityDto
            {
                Id = activity.Id,
                CategoryId = activity.CategoryId,
                CategoryName = category.Name,
                Name = activity.Name
            };
        }

        // Overload (adapt previous method to a collection of Calendar Entries, instead of a collection of activities):
        public static List<CalendarEntryDto> ConvertToDto(this List<CalendarEntry> calendarEntries, List<ActivityDto> activities)
        {
            return (from calendarEntry in calendarEntries
                    join activity in activities
                    on calendarEntry.ActivityId equals activity.Id
                    select new CalendarEntryDto
                    {
                        Id = calendarEntry.Id,
                        ActivityId = activity.Id,
                        ActivityName = activity.Name,
                        ActivityCategoryName = activity.CategoryName,
                        Date = calendarEntry.Date,
                        Start = calendarEntry.Start,
                        End = calendarEntry.End,
                        Comment = calendarEntry.Comment
                    }
                    ).ToList();
        }

        // Overload (adapt previous method to a SINGLE Calendar Entry, instead of a collection of calendar entries):
        public static CalendarEntryDto ConvertToDto(this CalendarEntry calendarEntry, ActivityDto activityDto)
        {
            return new CalendarEntryDto
            {
                Id = calendarEntry.Id,
                ActivityId = calendarEntry.ActivityId,
                ActivityName = activityDto.Name,
                ActivityCategoryName = activityDto.CategoryName,
                Date = calendarEntry.Date,
                Start = calendarEntry.Start,
                End = calendarEntry.End,
                Comment = calendarEntry.Comment
            };
        }

        public static List<GoalDto> ConvertToDto(this List<Goal> goals, List<Activity> activities) 
        {
            return (from goal in goals
                    join activity in activities
                    on goal.ActivityId equals activity.Id
                    select new GoalDto
                    {
                        Id = goal.Id,
                        ActivityId = activity.Id,
                        ActivityName = activity.Name,
                        Date = (DateTime)goal.Date,
                        DurationMinutes = goal.DurationMinutes,
                        IsCompleted = goal.IsCompleted
                    }
                    ).ToList();
        }

        public static GoalDto ConvertToDto(this Goal goal, Activity activity) 
        {
            return new GoalDto
            {
                Id = goal.Id,
                ActivityId = activity.Id,
                ActivityName = activity?.Name,
                Date = (DateTime)goal.Date,
                DurationMinutes = goal.DurationMinutes,
                IsCompleted = goal.IsCompleted
            };
        }
    }
}

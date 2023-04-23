using HabitsApp.Models.Dtos;
using HabitsApp.Server.Data;
using HabitsApp.Server.Repositories.Contracts;
using HabitsApp.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace HabitsApp.Server.Repositories
{
    public class GoalRepository : IGoalRepository
    {
        private readonly HabitsAppDbContext habitsAppDbContext;

        public GoalRepository(HabitsAppDbContext habitsAppDbContext)
        {
            this.habitsAppDbContext = habitsAppDbContext;
        }
        public async Task<Goal> AddGoal(GoalDto newGoal)
        {
            //var goal = await (from g in habitsAppDbContext.Goals
            //                  join activity in habitsAppDbContext.Activities
            //                  on g.ActivityId equals activity.Id
            //                  where g.Id == newGoal.Id
            //                  select new Goal
            //                  {
            //                      Id = newGoal.Id,
            //                      ActivityId = newGoal.ActivityId,
            //                      Date = newGoal.Date,
            //                      DurationMinutes = newGoal.DurationMinutes,
            //                      IsCompleted = newGoal.IsCompleted
            //                  }).SingleOrDefaultAsync();

            //if (goal != null)
            //{

            var goal = new Goal(); // Reverse from GoalDto to Goal
            goal.ActivityId = newGoal.ActivityId;
            goal.Date = newGoal.Date;
            goal.DurationMinutes = newGoal.DurationMinutes;
            goal.IsCompleted = false;

                var result = await habitsAppDbContext.Goals.AddAsync(goal);
                await habitsAppDbContext.SaveChangesAsync();
                return result.Entity;
            //}
            // else
            //return null;
        }

        public async Task<Goal> DeleteGoal(int id)
        {
            var goalToDelete = await habitsAppDbContext.Goals.FindAsync(id);

            if(goalToDelete != null) 
            {
                habitsAppDbContext.Goals.Remove(goalToDelete);
                await habitsAppDbContext.SaveChangesAsync();
            }
            return goalToDelete;
        }

        public async Task<List<Goal>> GetGoals()
        {
            var goals = await habitsAppDbContext.Goals.ToListAsync();
            return goals;
        }

        public async Task<List<Activity>> GetGoalActivities()
        {
            var  goalActivities = await habitsAppDbContext.Activities.ToListAsync();
            return goalActivities;
        }

        public async Task<Goal> GetGoal(int id)
        {
            var goal = await habitsAppDbContext.Goals.FindAsync(id);
            return goal;
        }

        public async Task<Goal> UpdateGoalIsCompleted(int id, GoalDto goalToUpdateDto)
        {
            var goal = await habitsAppDbContext.Goals.FindAsync(id);

            if (goal != null)
            {
                goal.IsCompleted = goalToUpdateDto.IsCompleted;

                await habitsAppDbContext.SaveChangesAsync();
                return goal;
            }

            return null;
        }
    }
}

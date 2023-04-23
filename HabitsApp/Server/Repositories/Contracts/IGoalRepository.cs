using HabitsApp.Models.Dtos;
using HabitsApp.Shared.Entities;

namespace HabitsApp.Server.Repositories.Contracts
{
    public interface IGoalRepository
    {
        // GET Methods
        Task<List<Goal>> GetGoals();
        Task<List<Activity>> GetGoalActivities();
        Task<Goal> GetGoal(int id);
        //Task<Activity> GetGoalActivity(int id);

        // POST Methods
        Task<Goal> AddGoal(GoalDto newGoal);

        // PUT Methods
        Task<Goal> UpdateGoalIsCompleted(int id, GoalDto goal);

        // DELETE Methods
        Task<Goal> DeleteGoal(int id);
    }
}

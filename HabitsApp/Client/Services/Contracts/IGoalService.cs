using HabitsApp.Models.Dtos;

namespace HabitsApp.Client.Services.Contracts
{
    public interface IGoalService
    {
        Task<List<GoalDto>> GetGoals();
        Task<GoalDto> GetGoal(int id);
        Task<GoalDto> AddGoal(GoalDto newGoalDto);
        Task<GoalDto> UpdateGoal(GoalDto goalToUpdateDto);
        Task<GoalDto> DeleteGoal(int id);
    }
}

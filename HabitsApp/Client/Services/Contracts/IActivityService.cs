using HabitsApp.Models.Dtos;

namespace HabitsApp.Client.Services.Contracts
{
    public interface IActivityService
    {
        Task<IEnumerable<ActivityDto>> GetActivities();
        Task<ActivityDto> GetActivity(int id);
    }
}

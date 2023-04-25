using HabitsApp.Models.Dtos;

namespace HabitsApp.Client.Services.Contracts
{
    public interface IActivityService
    {
        Task<List<ActivityDto>> GetActivities();
        Task<ActivityDto> GetActivity(int id);
        Task<ActivityDto> AddActivity(ActivityDto activityToAddDto);
    }
}

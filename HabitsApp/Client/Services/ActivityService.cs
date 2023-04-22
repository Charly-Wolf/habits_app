using HabitsApp.Client.Services.Contracts;
using HabitsApp.Models.Dtos;
using System.Net.Http.Json;

namespace HabitsApp.Client.Services
{
    public class ActivityService : IActivityService
    {
        private readonly HttpClient httpClient;

        public ActivityService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<ActivityDto>> GetActivities()
        {
            try
            {
                var response = await httpClient.GetAsync("api/Activity");

                if (response.IsSuccessStatusCode) 
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.NoContent) 
                    {
                        return Enumerable.Empty<ActivityDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<ActivityDto>>();
                }
                else 
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ActivityDto> GetActivity(int id)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Activity/{id}");

                if (response.IsSuccessStatusCode) 
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ActivityDto);
                    }

                    return await response.Content.ReadFromJsonAsync<ActivityDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

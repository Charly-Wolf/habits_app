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

        public async Task<List<ActivityDto>> GetActivities()
        {
            try
            {
                var response = await httpClient.GetAsync("api/Activity");

                if (response.IsSuccessStatusCode) 
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.NoContent) 
                    {
                        return (List<ActivityDto>)Enumerable.Empty<ActivityDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<List<ActivityDto>>();
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

        public async Task<ActivityDto> AddActivity(ActivityDto activityToAddDto)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync<ActivityDto>("api/Activity", activityToAddDto);

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
                    throw new Exception($"Http status: {response.StatusCode} - {message}");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ActivityDto> DeleteActivity(int id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/Activity/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ActivityDto>();
                }

                return default(ActivityDto);
            }
            catch (Exception)
            {
                // Log exception
                throw;
            }
        }
    }
}

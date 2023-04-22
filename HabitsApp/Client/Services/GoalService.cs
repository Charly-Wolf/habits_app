using HabitsApp.Client.Services.Contracts;
using HabitsApp.Models.Dtos;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace HabitsApp.Client.Services
{
    public class GoalService : IGoalService
    {
        private readonly HttpClient httpClient;

        public GoalService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<GoalDto> AddGoal(GoalDto goalToAddDto)
        {
            var response = await httpClient.PostAsJsonAsync<GoalDto>("api/Goal", goalToAddDto);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(GoalDto);
                }

                return await response.Content.ReadFromJsonAsync<GoalDto>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status: {response.StatusCode} Message -{message}");
            };
        }

        public Task<GoalDto> GetGoal(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GoalDto>> GetGoals()
        {
            try
            {
                var goals = await httpClient.GetFromJsonAsync<IEnumerable<GoalDto>>("api/Goal");

                return goals;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<GoalDto> UpdateGoal(GoalDto goalToUpdateDto)
        {
            try
            {
                var jsonRequest =  JsonConvert.SerializeObject(goalToUpdateDto); // Serialize ObjectDto that we wish to pass to the server into Json format
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json"); // Create an Object of type String Content to pass the relevant Data in the appropiate Format to the Server

                var response = await httpClient.PatchAsync($"api/Goal/{goalToUpdateDto.Id}", content); // Use the URI and the relevant Data with the PatchAsync Method
            
                if(response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<GoalDto>();
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

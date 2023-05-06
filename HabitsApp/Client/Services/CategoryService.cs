using HabitsApp.Client.Services.Contracts;
using HabitsApp.Models.Dtos;
using HabitsApp.Shared.Entities;
using System.Net.Http.Json;

namespace HabitsApp.Client.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient httpClient;

        public CategoryService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<Category>> GetCategories()
        {
            try
            {
                var categories = await httpClient.GetFromJsonAsync<List<Category>>("api/Category");

                return categories;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Category> GetCategory(int id)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Category/{id}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(Category);
                    }

                    return await response.Content.ReadFromJsonAsync<Category>();
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

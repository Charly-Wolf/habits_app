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
    }
}

using MatchArena.Application.Interfaces.Services;
using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels.Category;
using MatchArena.MVC.ViewModels.Products;

namespace MatchArena.MVC.Services.Implementations
{
    public class CategoryClientService:ICategoryClientService
    {
        private readonly HttpClient _httpClient;
        public CategoryClientService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("MatchArenaClient");
        }

        public async Task<List<GetCategoryItemVM>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<GetCategoryItemVM>>("Categories");
        }

        public async Task<GetCategoryVM?> GetByIdAsync(long id)
        {
            return await _httpClient.GetFromJsonAsync<GetCategoryVM>($"Categories/{id}");
        }

        public async Task<bool> CreateAsync(PostCategoryVM categoryVM)
        {
            var response = await _httpClient.PostAsJsonAsync("Categories", categoryVM);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(long id, PutCategoryVM categoryVM)
        {
            var response = await _httpClient.PutAsJsonAsync($"Categories/{id}", categoryVM);
            return response.IsSuccessStatusCode;
        }
    }
}

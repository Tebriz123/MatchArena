using MatchArena.Application.Interfaces.Services;
using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels.Category;
using MatchArena.MVC.ViewModels.Products;
using System.Net.Http.Headers;

namespace MatchArena.MVC.Services.Implementations
{
    public class CategoryClientService : ICategoryClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryClientService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = clientFactory.CreateClient("MatchArenaClient");
            _httpContextAccessor = httpContextAccessor;
        }

        private void AddJwtToken()
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["jwtToken"];
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<List<GetCategoryItemVM>?> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("Categories");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<List<GetCategoryItemVM>>();
        }

        public async Task<GetCategoryVM?> GetByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync($"Categories/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<GetCategoryVM>();
        }

        public async Task<bool> CreateAsync(string name)
        {
            AddJwtToken();
            var response = await _httpClient.PostAsync($"Categories?name={name}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(long id, string name)
        {
            AddJwtToken();
            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(name), "Name");
            var response = await _httpClient.PutAsync($"Categories/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            AddJwtToken();
            var response = await _httpClient.DeleteAsync($"Categories/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}

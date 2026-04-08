using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels.Category;
using MatchArena.MVC.ViewModels.Colors;
using MatchArena.MVC.ViewModels.Sizes;
using System.Net.Http.Headers;

namespace MatchArena.MVC.Services.Implementations
{
    public class SizeClientService : ISizeClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SizeClientService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
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

        public async Task<List<GetSizeItemVM>?> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("Sizes");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<List<GetSizeItemVM>>();
        }

        public async Task<GetSizeVM?> GetByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync($"Sizes/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<GetSizeVM>();
        }

        public async Task<bool> CreateAsync(string name)
        {
            AddJwtToken();
            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(name), "Name");
            var response = await _httpClient.PostAsync("Sizes", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(long id, string name)
        {
            AddJwtToken();
            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(name), "Name");
            var response = await _httpClient.PutAsync($"Sizes/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            AddJwtToken();
            var response = await _httpClient.DeleteAsync($"Sizes/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}

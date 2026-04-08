using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels.Category;
using MatchArena.MVC.ViewModels.Colors;
using System.Net.Http.Headers;

namespace MatchArena.MVC.Services.Implementations
{
    public class ColorClientService : IColorClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ColorClientService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
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

        public async Task<List<GetColorItemVM>?> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("Colors");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<List<GetColorItemVM>>();
        }

        public async Task<GetColorVM?> GetByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync($"Colors/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<GetColorVM>();
        }

        public async Task<bool> CreateAsync(string name)
        {
            AddJwtToken();
            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(name), "Name");
            var response = await _httpClient.PostAsync("Colors", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(long id, string name)
        {
            AddJwtToken();
            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(name), "Name");
            var response = await _httpClient.PutAsync($"Colors/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            AddJwtToken();
            var response = await _httpClient.DeleteAsync($"Colors/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}

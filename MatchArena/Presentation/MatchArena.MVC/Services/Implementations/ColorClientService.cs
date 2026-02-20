using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels.Category;
using MatchArena.MVC.ViewModels.Colors;

namespace MatchArena.MVC.Services.Implementations
{
    public class ColorClientService:IColorClientService
    {
        private readonly HttpClient _httpClient;
        public ColorClientService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("MatchArenaClient");
        }

        public async Task<List<GetColorItemVM>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<GetColorItemVM>>("Colors");
        }

        public async Task<GetColorVM?> GetByIdAsync(long id)
        {
            return await _httpClient.GetFromJsonAsync<GetColorVM>($"Colors/{id}");
        }

        public async Task<bool> CreateAsync(PostColorVM colorVM)
        {
            var response = await _httpClient.PostAsJsonAsync("Colors", colorVM);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(long id, PutColorVM colorVM)
        {
            var response = await _httpClient.PutAsJsonAsync($"Colors/{id}", colorVM);
            return response.IsSuccessStatusCode;
        }
    }

}

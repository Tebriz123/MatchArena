using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels.Category;
using MatchArena.MVC.ViewModels.Colors;
using MatchArena.MVC.ViewModels.Sizes;

namespace MatchArena.MVC.Services.Implementations
{
    public class SizeClientService:ISizeClientService
    {
        private readonly HttpClient _httpClient;
        public SizeClientService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("MatchArenaClient");
        }

        public async Task<List<GetSizeItemVM>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<GetSizeItemVM>>("Sizes");
        }

        public async Task<GetSizeVM?> GetByIdAsync(long id)
        {
            return await _httpClient.GetFromJsonAsync<GetSizeVM>($"Sizes/{id}");
        }
        public async Task<bool> CreateAsync(PostSizeVM sizeVM)
        {
            var response = await _httpClient.PostAsJsonAsync("Sizes", sizeVM);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(long id, PutSizeVM sizeVM)
        {
            var response = await _httpClient.PutAsJsonAsync($"Sizes/{id}", sizeVM);
            return response.IsSuccessStatusCode;
        }

    }
}

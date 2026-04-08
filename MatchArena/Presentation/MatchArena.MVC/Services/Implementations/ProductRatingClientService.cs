using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using System.Net.Http.Headers;

namespace MatchArena.MVC.Services.Implementations
{
    public class ProductRatingClientService : IProductRatingClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductRatingClientService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
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

        public async Task<GetProductRatingResponseVM?> GetProductRatingsAsync(long productId)
        {
            var response = await _httpClient.GetAsync($"ProductRatings/{productId}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<GetProductRatingResponseVM>();
        }

        public async Task<bool> PostRatingAsync(long productId, PostRatingVM vm)
        {
            AddJwtToken();

            var dto = new { vm.Rating, vm.Comment };
            var response = await _httpClient.PostAsJsonAsync($"ProductRatings/{productId}", dto);
            return response.IsSuccessStatusCode;
        }
    }
}

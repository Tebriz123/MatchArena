using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using System.Net.Http.Headers;

namespace MatchArena.MVC.Services.Implementations
{
    public class FieldRatingClientService : IFieldRatingClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FieldRatingClientService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
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

        public async Task<GetFieldRatingResponseVM?> GetFieldRatingsAsync(long fieldId)
        {
            AddJwtToken();
            var response = await _httpClient.GetAsync($"FieldRatings/{fieldId}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<GetFieldRatingResponseVM>();
        }

        public async Task<bool> PostRatingAsync(long playerId, long fieldId, PostRatingVM vm)
        {
            AddJwtToken();
            var dto = new { vm.Rating, vm.Comment };
            var response = await _httpClient.PostAsJsonAsync(
                $"FieldRatings/{fieldId}?playerId={playerId}", dto);
            return response.IsSuccessStatusCode;
        }
    }
}

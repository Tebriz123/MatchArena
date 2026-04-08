using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using System.Net.Http.Headers;

namespace MatchArena.MVC.Services.Implementations
{
    public class PlayerRatingClientService : IPlayerRatingClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PlayerRatingClientService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
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

        public async Task<GetPlayerRatingResponseVM?> GetPlayerRatingsAsync(long playerId)
        {
            var response = await _httpClient.GetAsync($"PlayerRatings/{playerId}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<GetPlayerRatingResponseVM>();
        }

        public async Task<bool> PostRatingAsync(long raterPlayerId, long ratedPlayerId, PostRatingVM vm)
        {
            AddJwtToken();

            var dto = new { vm.Rating, vm.Comment };
            var response = await _httpClient.PostAsJsonAsync(
                $"PlayerRatings/{ratedPlayerId}?raterPlayerId={raterPlayerId}", dto);
            return response.IsSuccessStatusCode;
        }
    }
}

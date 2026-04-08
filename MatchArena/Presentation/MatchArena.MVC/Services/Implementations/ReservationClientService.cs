using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels.Reservation;
using System.Net.Http.Headers;

namespace MatchArena.MVC.Services.Implementations
{
    public class ReservationClientService : IReservationClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReservationClientService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
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

        public async Task<(long reservationId, string sessionUrl)?> CreateReservationAsync(PostReservationVM vm)
        {
            AddJwtToken();

            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(vm.FieldId.ToString()), "FieldId");
            content.Add(new StringContent(vm.ReservedTime.ToString()), "ReservedTime");
            content.Add(new StringContent(vm.ReservedDate.ToString("yyyy-MM-dd")), "ReservedDate");

            var response = await _httpClient.PostAsync("Reservations", content);
            if (!response.IsSuccessStatusCode) return null;

            var result = await response.Content.ReadFromJsonAsync<ReservationResponseVM>();
            if (result is null) return null;

            return (result.ReservationId, result.SessionUrl);
        }

        public async Task<List<GetReservationVM>?> GetMyReservationsAsync()
        {
            AddJwtToken();
            var response = await _httpClient.GetAsync("Reservations/my");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<List<GetReservationVM>>();
        }

        public async Task<bool> CancelAsync(long id)
        {
            AddJwtToken();
            var response = await _httpClient.DeleteAsync($"Reservations/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}

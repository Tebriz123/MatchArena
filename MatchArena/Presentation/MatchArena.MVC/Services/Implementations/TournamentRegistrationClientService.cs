using MatchArena.Application.Interfaces.Services;
using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using MatchArena.MVC.ViewModels.TournamentRegistrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace MatchArena.MVC.Services
{
    public class TournamentRegistrationClientService : ITournamentRegistrationClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TournamentRegistrationClientService(
            IHttpClientFactory clientFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = clientFactory.CreateClient("MatchArenaClient");
            _httpContextAccessor = httpContextAccessor;
        }

        private void AddJwtToken()
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["jwtToken"];
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<(long registrationId, string sessionUrl)?> RegisterAsync(PostTournamentRegistrationVM vm)
        {
            AddJwtToken();

            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(vm.TournamentId.ToString()), "TournamentId");
            content.Add(new StringContent(vm.TeamId.ToString()), "TeamId");

            var response = await _httpClient.PostAsync("TournamentRegistrations", content);

            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadFromJsonAsync<RegisterResponseVM>();
            if (result is null) return null;

            return (result.RegistrationId, result.SessionUrl);
        }

        public async Task<List<GetTournamentRegistrationVM>?> GetTournamentTeamsAsync(long tournamentId)
        {
            return await _httpClient.GetFromJsonAsync<List<GetTournamentRegistrationVM>>(
                $"TournamentRegistrations/{tournamentId}/teams");
        }

        public async Task<bool> CancelAsync(long id)
        {
            AddJwtToken();
            var response = await _httpClient.DeleteAsync($"TournamentRegistrations/{id}");
            return response.IsSuccessStatusCode;
        }
        public async Task<GetTeamVM?> GetMyTeamAsync()
        {
            AddJwtToken();

            var response = await _httpClient.GetAsync("Teams/my-team");

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<GetTeamVM>();
        }
    }
}
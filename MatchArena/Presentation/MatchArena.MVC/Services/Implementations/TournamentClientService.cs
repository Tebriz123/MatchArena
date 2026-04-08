using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using System.Net.Http.Headers;

namespace MatchArena.MVC.Services.Implementations
{
    public class TournamentClientService : ITournamentClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TournamentClientService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
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

        public async Task<List<GetTournamentItemVM>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<GetTournamentItemVM>>("Tournaments");
        }

        public async Task<GetTournamentVM?> GetByIdAsync(long id)
        {
            return await _httpClient.GetFromJsonAsync<GetTournamentVM>($"Tournaments/{id}");
        }

        public async Task<bool> CreateAsync(PostTournamentVM tournamentVM)
        {
            AddJwtToken();

            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(tournamentVM.Name ?? ""), "Name");
            content.Add(new StringContent(tournamentVM.Description ?? ""), "Description");
            content.Add(new StringContent(tournamentVM.Address ?? ""), "Address");
            content.Add(new StringContent(tournamentVM.City ?? ""), "City");
            content.Add(new StringContent(tournamentVM.StartDate.ToString()), "StartDate");
            content.Add(new StringContent(tournamentVM.EndDate.ToString()), "EndDate");
            content.Add(new StringContent(tournamentVM.RegistrationDeadline.ToString()), "RegistrationDeadline");
            content.Add(new StringContent(tournamentVM.MaxTeams.ToString()), "MaxTeams");
            content.Add(new StringContent(tournamentVM.EntryFee.ToString()), "EntryFee");
            content.Add(new StringContent(tournamentVM.PrizeFund.ToString()), "PrizeFund");
            content.Add(new StringContent(tournamentVM.Status.ToString()), "Status");

            if (tournamentVM.Photo is not null)
            {
                var stream = tournamentVM.Photo.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(tournamentVM.Photo.ContentType);
                content.Add(fileContent, "Photo", tournamentVM.Photo.FileName);
            }

            var response = await _httpClient.PostAsync("Tournaments", content);

            var statusCode = response.StatusCode;
            var errorBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"==> Tournament Create Status: {statusCode}");
            Console.WriteLine($"==> Tournament Create Body: {errorBody}");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(long id, PutTournamentVM tournamentVM)
        {
            AddJwtToken();

            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(tournamentVM.Name ?? ""), "Name");
            content.Add(new StringContent(tournamentVM.Description ?? ""), "Description");
            content.Add(new StringContent(tournamentVM.Address ?? ""), "Address");
            content.Add(new StringContent(tournamentVM.City ?? ""), "City");
            content.Add(new StringContent(tournamentVM.StartDate.ToString()), "StartDate");
            content.Add(new StringContent(tournamentVM.EndDate.ToString()), "EndDate");
            content.Add(new StringContent(tournamentVM.RegistrationDeadline.ToString()), "RegistrationDeadline");
            content.Add(new StringContent(tournamentVM.MaxTeams.ToString()), "MaxTeams");
            content.Add(new StringContent(tournamentVM.EntryFee.ToString()), "EntryFee");
            content.Add(new StringContent(tournamentVM.PrizeFund.ToString()), "PrizeFund");
            content.Add(new StringContent(tournamentVM.Status.ToString()), "Status");

            if (tournamentVM.Photo is not null)
            {
                var stream = tournamentVM.Photo.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(tournamentVM.Photo.ContentType);
                content.Add(fileContent, "Photo", tournamentVM.Photo.FileName);
            }

            var response = await _httpClient.PutAsync($"Tournaments/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            AddJwtToken();
            var response = await _httpClient.DeleteAsync($"Tournaments/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateStatusAsync(long id, int status)
        {
            AddJwtToken();
            var response = await _httpClient.PatchAsync($"Tournaments/{id}/status?status={status}", null);
            return response.IsSuccessStatusCode;
        }
    }
}
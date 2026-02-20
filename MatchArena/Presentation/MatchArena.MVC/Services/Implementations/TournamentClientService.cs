using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using MatchArena.MVC.ViewModels.Category;
using System.Net.Http.Headers;

namespace MatchArena.MVC.Services.Implementations
{
    public class TournamentClientService:ITournamentClientService
    {
        private readonly HttpClient _httpClient;
        public TournamentClientService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("MatchArenaClient");
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
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(tournamentVM.Name), "Name");
            content.Add(new StringContent(tournamentVM.Description), "Description");
            content.Add(new StringContent(tournamentVM.Address), "Address");
            content.Add(new StringContent(tournamentVM.City), "City");
            content.Add(new StringContent(tournamentVM.Icon), "Icon");
            content.Add(new StringContent(tournamentVM.StartTime.ToString()), "StartTime");
            content.Add(new StringContent(tournamentVM.EndTime.ToString()), "EndTime");
            content.Add(new StringContent(tournamentVM.RegistrationDeadline.ToString()), "RegistrationDeadline");
            content.Add(new StringContent(tournamentVM.MaxTeams.ToString()), "MaxTeams");
            content.Add(new StringContent(tournamentVM.CurrentTeams.ToString()), "CurrentTeams");
            content.Add(new StringContent(tournamentVM.EntryFee.ToString()), "EntryFee");
            content.Add(new StringContent(tournamentVM.PrizeFund.ToString()), "PrizeFund");
            content.Add(new StringContent(tournamentVM.Format), "Format");
            content.Add(new StringContent(tournamentVM.GameFormat), "GameFormat");
            content.Add(new StringContent(tournamentVM.Status.ToString()), "Status");

            var stream = tournamentVM.Photo.OpenReadStream();
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(tournamentVM.Photo.ContentType);
            content.Add(fileContent, "Photo", tournamentVM.Photo.FileName);

            var response = await _httpClient.PostAsync("Tournaments", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(long id, PutTournamentVM tournamentVM)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(tournamentVM.Name), "Name");
            content.Add(new StringContent(tournamentVM.Description), "Description");
            content.Add(new StringContent(tournamentVM.Address), "Address");
            content.Add(new StringContent(tournamentVM.City), "City");
            content.Add(new StringContent(tournamentVM.Icon), "Icon");
            content.Add(new StringContent(tournamentVM.StartTime.ToString()), "StartTime");
            content.Add(new StringContent(tournamentVM.EndTime.ToString()), "EndTime");
            content.Add(new StringContent(tournamentVM.RegistrationDeadline.ToString()), "RegistrationDeadline");
            content.Add(new StringContent(tournamentVM.MaxTeams.ToString()), "MaxTeams");
            content.Add(new StringContent(tournamentVM.CurrentTeams.ToString()), "CurrentTeams");
            content.Add(new StringContent(tournamentVM.EntryFee.ToString()), "EntryFee");
            content.Add(new StringContent(tournamentVM.PrizeFund.ToString()), "PrizeFund");
            content.Add(new StringContent(tournamentVM.Format), "Format");
            content.Add(new StringContent(tournamentVM.GameFormat), "GameFormat");
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
    }
}

using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using MatchArena.MVC.ViewModels.Category;
using System.Net.Http.Headers;

namespace MatchArena.MVC.Services.Implementations
{
    public class TeamClientService:ITeamClientService
    {
        private readonly HttpClient _httpClient;
        public TeamClientService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("MatchArenaClient");
        }

        public async Task<List<GetTeamItemVM>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<GetTeamItemVM>>("Teams");
        }

        public async Task<GetTeamVM?> GetByIdAsync(long id)
        {
            return await _httpClient.GetFromJsonAsync<GetTeamVM>($"Teams/{id}");
        }

        public async Task<bool> CreateAsync(PostTeamVM teamVM)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(teamVM.Name), "Name");
            content.Add(new StringContent(teamVM.City), "City");
            content.Add(new StringContent(teamVM.MaxPlayer.ToString()), "MaxPlayer");
            content.Add(new StringContent(teamVM.Information), "Information");

            var stream = teamVM.Photo.OpenReadStream();
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(teamVM.Photo.ContentType);
            content.Add(fileContent, "Photo", teamVM.Photo.FileName);

            var response = await _httpClient.PostAsync("Teams", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(long id, PutTeamVM teamVM)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(teamVM.Name), "Name");
            content.Add(new StringContent(teamVM.CaptainName), "CaptainName");
            content.Add(new StringContent(teamVM.City), "City");
            content.Add(new StringContent(teamVM.Information), "Information");

            if (teamVM.Photo is not null)
            {
                var stream = teamVM.Photo.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(teamVM.Photo.ContentType);
                content.Add(fileContent, "Photo", teamVM.Photo.FileName);
            }

            var response = await _httpClient.PutAsync($"Teams/{id}", content);
            return response.IsSuccessStatusCode;
        }
    }
}

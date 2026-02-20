using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using MatchArena.MVC.ViewModels.Category;
using System.Net.Http.Headers;

namespace MatchArena.MVC.Services.Implementations
{
    public class PlayerClientService:IPlayerClientService
    {
        private readonly HttpClient _httpClient;
        public PlayerClientService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("MatchArenaClient");
        }

        public async Task<List<GetPlayerItemVM>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<GetPlayerItemVM>>("Players");
        }

        public async Task<GetPlayerVM?> GetByIdAsync(long id)
        {
            return await _httpClient.GetFromJsonAsync<GetPlayerVM>($"Players/{id}");
        }

        public async Task<bool> CreateAsync(PostPlayerVM playerVM)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(playerVM.Name), "Name");
            content.Add(new StringContent(playerVM.Surname), "Surname");
            content.Add(new StringContent(playerVM.Age.ToString()), "Age");
            content.Add(new StringContent(playerVM.Height.ToString()), "Height");
            content.Add(new StringContent(playerVM.Image), "Image");
            content.Add(new StringContent(playerVM.Information), "Information");
            content.Add(new StringContent(playerVM.City), "City");
            content.Add(new StringContent(playerVM.Position.ToString()), "Position");
            content.Add(new StringContent(playerVM.Level.ToString()), "Level");

            var stream = playerVM.Photo.OpenReadStream();
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(playerVM.Photo.ContentType);
            content.Add(fileContent, "Photo", playerVM.Photo.FileName);

            var response = await _httpClient.PostAsync("Players", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(long id, PutPlayerVM playerVM)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(playerVM.Name), "Name");
            content.Add(new StringContent(playerVM.Surname), "Surname");
            content.Add(new StringContent(playerVM.Age.ToString()), "Age");
            content.Add(new StringContent(playerVM.Information), "Information");
            content.Add(new StringContent(playerVM.City), "City");
            content.Add(new StringContent(playerVM.Height.ToString()), "Height");
            content.Add(new StringContent(playerVM.Image), "Image");
            content.Add(new StringContent(playerVM.Position.ToString()), "Position");
            content.Add(new StringContent(playerVM.Level.ToString()), "Level");

            if (playerVM.Photo is not null)
            {
                var stream = playerVM.Photo.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(playerVM.Photo.ContentType);
                content.Add(fileContent, "Photo", playerVM.Photo.FileName);
            }

            var response = await _httpClient.PutAsync($"Players/{id}", content);
            return response.IsSuccessStatusCode;
        }


    }
}

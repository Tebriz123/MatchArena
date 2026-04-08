    using MatchArena.MVC.Services.Interfaces;
    using MatchArena.MVC.ViewModels;
    using MatchArena.MVC.ViewModels.Teams;
    using System.Net.Http.Headers;

    namespace MatchArena.MVC.Services.Implementations
    {
        public class PlayerClientService : IPlayerClientService
        {
            private readonly HttpClient _httpClient;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public PlayerClientService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
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
                AddJwtToken();

                using var content = new MultipartFormDataContent();
                content.Add(new StringContent(playerVM.Name ?? ""), "Name");
                content.Add(new StringContent(playerVM.Surname ?? ""), "Surname");
                content.Add(new StringContent(playerVM.Age.ToString()), "Age");
                content.Add(new StringContent(playerVM.Height.ToString()), "Height");
                content.Add(new StringContent(playerVM.Information ?? ""), "Information");
                content.Add(new StringContent(playerVM.City ?? ""), "City");
                content.Add(new StringContent(playerVM.Position.ToString()), "Position");
                content.Add(new StringContent(playerVM.Level.ToString()), "Level");

                if (playerVM.Photo != null)
                {
                    var stream = playerVM.Photo.OpenReadStream();
                    var fileContent = new StreamContent(stream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(playerVM.Photo.ContentType);
                    content.Add(fileContent, "Photo", playerVM.Photo.FileName);
                }

                var response = await _httpClient.PostAsync("Players", content);

                if (response.StatusCode == System.Net.HttpStatusCode.Conflict) return false;
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) return false;

                return response.IsSuccessStatusCode;
            }

            public async Task<bool> UpdateAsync(long id, PutPlayerVM playerVM)
            {
                AddJwtToken();

                using var content = new MultipartFormDataContent();
                content.Add(new StringContent(playerVM.Name), "Name");
                content.Add(new StringContent(playerVM.Surname), "Surname");
                content.Add(new StringContent(playerVM.Age.ToString()), "Age");
                content.Add(new StringContent(playerVM.Information), "Information");
                content.Add(new StringContent(playerVM.City), "City");
                content.Add(new StringContent(playerVM.Height.ToString()), "Height");
                content.Add(new StringContent(playerVM.Image ?? ""), "Image");
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

            public async Task<bool> DeleteAsync(long id)
            {
                AddJwtToken();
                var response = await _httpClient.DeleteAsync($"Players/{id}");
                return response.IsSuccessStatusCode;
            }

            public async Task<bool> LeaveTeamAsync(long teamId)
            {
                AddJwtToken();
                var response = await _httpClient.DeleteAsync($"Players/leave/{teamId}");
                return response.IsSuccessStatusCode;
            }

            public async Task<bool> AcceptInviteAsync(long inviteId)
            {
                AddJwtToken();
                var response = await _httpClient.PostAsync($"Players/invites/{inviteId}/accept", null);
                return response.IsSuccessStatusCode;
            }
        public async Task<GetPlayerVM?> GetMyPlayerAsync()
        {
            AddJwtToken();
            var response = await _httpClient.GetAsync("Players/my-player");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<GetPlayerVM>();
        }
        public async Task<bool> RejectInviteAsync(long inviteId)
            {
                AddJwtToken();
                var response = await _httpClient.PostAsync($"Players/invites/{inviteId}/reject", null);
                return response.IsSuccessStatusCode;
            }

            public async Task<List<GetInviteVM>?> GetMyInvitesAsync()
            {
                AddJwtToken();
                return await _httpClient.GetFromJsonAsync<List<GetInviteVM>>("Players/my-invites");
            }
        }
    }
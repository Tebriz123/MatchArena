using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;

namespace MatchArena.MVC.Services.Implementations
{
    public class AccountClientService:IAccountClientService
    {
        private readonly HttpClient _httpClient;

        public AccountClientService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("MatchArenaClient");
        }

        public async Task<string?> LoginAsync(LoginVM loginVM)
        {
            var response = await _httpClient.PostAsJsonAsync("Accounts/login", loginVM);
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return body;
        }

        public async Task<bool> RegisterAsync(RegisterVM registerVM)
        {
            var response = await _httpClient.PostAsJsonAsync("Accounts/register", registerVM);
            return response.IsSuccessStatusCode;
        }


    }
}

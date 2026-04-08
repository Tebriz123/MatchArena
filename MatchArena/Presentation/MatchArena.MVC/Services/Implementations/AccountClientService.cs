using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using MatchArena.MVC.ViewModels.AppUsers;
using System.Net.Http.Json;

namespace MatchArena.MVC.Services.Implementations
{
    public class AccountClientService : IAccountClientService
    {
        private readonly HttpClient _httpClient;

        public AccountClientService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("MatchArenaClient");
        }

        public async Task<TokenResponseVM?> LoginAsync(LoginVM loginVM)
        {
            var response = await _httpClient.PostAsJsonAsync("Accounts/login", loginVM);
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<TokenResponseVM>();
        }

        public async Task<bool> RegisterAsync(RegisterVM registerVM)
        {
            var response = await _httpClient.PostAsJsonAsync("Accounts/register", registerVM);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ForgotPasswordAsync(ForgotPasswordVM forgotPasswordVM)
        {
            var response = await _httpClient.PostAsJsonAsync("Accounts/forgot-password", forgotPasswordVM);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordVM resetPasswordVM)
        {
            var response = await _httpClient.PostAsJsonAsync("Accounts/reset-password", resetPasswordVM);
            return response.IsSuccessStatusCode;
        }
    }
}
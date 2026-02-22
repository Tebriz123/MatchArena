using MatchArena.MVC.ViewModels;

namespace MatchArena.MVC.Services.Interfaces
{
    public interface IAccountClientService
    {
        Task<string?> LoginAsync(LoginVM loginVM);
        Task<bool> RegisterAsync(RegisterVM registerVM);
    }
}

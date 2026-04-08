using MatchArena.MVC.ViewModels;
using MatchArena.MVC.ViewModels.AppUsers;

namespace MatchArena.MVC.Services.Interfaces
{
    public interface IAccountClientService
    {
        Task<TokenResponseVM?> LoginAsync(LoginVM loginVM);
        Task<bool> RegisterAsync(RegisterVM registerVM);
        Task<bool> ForgotPasswordAsync(ForgotPasswordVM forgotPasswordVM);
        Task<bool> ResetPasswordAsync(ResetPasswordVM resetPasswordVM);
    }
}

using System.ComponentModel.DataAnnotations;

namespace MatchArena.MVC.ViewModels.AppUsers
{
    public class ForgotPasswordVM
    {
        [Required(ErrorMessage = "Bu sahə mütləqdir.")]
        [EmailAddress(ErrorMessage = "Düzgün email daxil edin.")]
        public string Email { get; set; }
    }
}

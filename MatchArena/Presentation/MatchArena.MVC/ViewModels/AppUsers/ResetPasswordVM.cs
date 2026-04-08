using System.ComponentModel.DataAnnotations;

namespace MatchArena.MVC.ViewModels.AppUsers
{
    public class ResetPasswordVM
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }

        [Required(ErrorMessage = "Bu sahə mütləqdir.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Şifrə minimum 6 simvol olmalıdır.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Bu sahə mütləqdir.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Şifrələr uyğun gəlmir.")]
        public string ConfirmPassword { get; set; }
    }
}

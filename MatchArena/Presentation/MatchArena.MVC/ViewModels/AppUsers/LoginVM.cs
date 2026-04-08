using System.ComponentModel.DataAnnotations;

namespace MatchArena.MVC.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Bu sahə mütləqdir.")]
        public string UsernameOrEmail { get; set; }

        [Required(ErrorMessage = "Bu sahə mütləqdir.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }


}

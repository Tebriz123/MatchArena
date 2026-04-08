using MatchArena.Domain;
using MatchArena.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MatchArena.MVC.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Bu sahə mütləqdir.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu sahə mütləqdir.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Bu sahə mütləqdir.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Bu sahə mütləqdir.")]
        [EmailAddress(ErrorMessage = "Düzgün email daxil edin.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bu sahə mütləqdir.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Şifrə minimum 6 simvol olmalıdır.")]
        public string Password { get; set; }
    }

}

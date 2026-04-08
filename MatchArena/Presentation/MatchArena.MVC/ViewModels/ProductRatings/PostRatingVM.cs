using System.ComponentModel.DataAnnotations;

namespace MatchArena.MVC.ViewModels
{
    public class PostRatingVM
    {
        [Required(ErrorMessage = "Reytinq boş ola bilməz")]
        [Range(1, 5, ErrorMessage = "Reytinq 1 ilə 5 arasında olmalıdır")]
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}

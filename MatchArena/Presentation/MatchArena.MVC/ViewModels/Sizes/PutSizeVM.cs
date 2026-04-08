using System.ComponentModel.DataAnnotations;


namespace MatchArena.MVC.ViewModels.Sizes
{
    public class PutSizeVM
    {
        [Required(ErrorMessage = "Ad boş ola bilməz")]
        public string Name { get; set; } = null!;
    }
}

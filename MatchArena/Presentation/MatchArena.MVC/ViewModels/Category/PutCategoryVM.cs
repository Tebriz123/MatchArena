using System.ComponentModel.DataAnnotations;

namespace MatchArena.MVC.ViewModels.Category
{
    public class PutCategoryVM
    {
        [Required(ErrorMessage = "Ad boş ola bilməz")]
        public string Name { get; set; } = null!;
    }
}

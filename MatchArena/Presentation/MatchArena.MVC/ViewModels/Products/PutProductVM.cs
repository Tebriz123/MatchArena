using System.ComponentModel.DataAnnotations;
namespace MatchArena.MVC.ViewModels.Products
{
    public class PutProductVM
    {
        [Required(ErrorMessage = "Ad boş ola bilməz")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Qiymət boş ola bilməz")]
        public decimal Price { get; set; }

        public IFormFile? PrimaryPhoto { get; set; }
        public ICollection<IFormFile>? AdditionalPhotos { get; set; }

        [Required(ErrorMessage = "Açıqlama boş ola bilməz")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Kateqoriya seçin")]
        public long CategoryId { get; set; }

        public ICollection<long> SizeIds { get; set; } = new List<long>();
        public ICollection<long> ColorIds { get; set; } = new List<long>();
    }
}

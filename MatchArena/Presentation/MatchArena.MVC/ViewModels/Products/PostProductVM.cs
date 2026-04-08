namespace MatchArena.MVC.ViewModels.Products
{
    public class PostProductVM
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IFormFile PrimaryPhoto { get; set; }
        public ICollection<IFormFile>? AdditionalPhotos { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
        public ICollection<long>? SizeIds { get; set; }
        public ICollection<long>? ColorIds { get; set; }
    }
}

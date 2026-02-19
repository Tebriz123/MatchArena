namespace MatchArena.MVC.ViewModels.Products
{
    public record PostProductVM(
       string Name,
       decimal Price,
       IFormFile PrimaryPhoto,
       ICollection<IFormFile> AdditionalPhotos,
       string Description,
       long CategoryId,
       ICollection<long> SizeIds,
       ICollection<long> ColorIds
       );
}

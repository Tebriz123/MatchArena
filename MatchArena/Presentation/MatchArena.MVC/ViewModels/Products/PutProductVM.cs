namespace MatchArena.MVC.ViewModels.Products
{
    public record PutProductDto(
         string Name,
         decimal Price,
         string Description,
         long CategoryId,
         IFormFile PrimaryPhoto,
         ICollection<IFormFile> AdditionalPhotos,
         ICollection<long> SizeIds,
         ICollection<long> ColorIds
         );
}

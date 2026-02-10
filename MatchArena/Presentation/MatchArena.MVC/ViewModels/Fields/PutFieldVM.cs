namespace MatchArena.MVC.ViewModels
{
    public record PutFieldVM(
        string Name,
        string City,
        string Address,
        string Image,
        IFormFile PrimaryPhoto,
        ICollection<IFormFile> AdditionalPhotos,
        decimal PricePerHour,
        DateTime StartTime,
        DateTime EndTime,
        string FieldInformation
        );

}
 
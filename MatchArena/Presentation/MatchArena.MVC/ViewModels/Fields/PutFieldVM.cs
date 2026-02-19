namespace MatchArena.MVC.ViewModels
{
    public record PutFieldVM(
            string Name,
            string City,
            string Address,
            IFormFile PrimaryPhoto,
            ICollection<IFormFile> AdditionalPhotos,
            decimal PricePerHour,
            TimeOnly StartTime,
            TimeOnly EndTime,
            string FieldInformation
            );

}
 
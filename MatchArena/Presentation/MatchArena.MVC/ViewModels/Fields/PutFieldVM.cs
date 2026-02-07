namespace MatchArena.MVC.ViewModels
{
    public record PutFieldVM(
          string Name,
        string City,
        string Address,
        string Image,
        IFormFile Photo,
        decimal PricePerHour
        );

}

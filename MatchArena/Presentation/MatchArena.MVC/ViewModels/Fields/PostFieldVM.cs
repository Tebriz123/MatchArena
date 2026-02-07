namespace MatchArena.MVC.ViewModels
{
    public record PostFieldVM(
        string Name,
        string City,
        string Address,
        string Image,
        IFormFile Photo,
        decimal PricePerHour
        );

}

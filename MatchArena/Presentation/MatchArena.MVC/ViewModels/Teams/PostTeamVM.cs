namespace MatchArena.MVC.ViewModels
{
    public record PostTeamVM(
        string Name,
        string Logo,
        IFormFile Photo
        );

}

namespace MatchArena.MVC.ViewModels
{
    public record PostTeamVM(
         string Name,
         IFormFile Photo,
         string City,
         int MaxPlayer,
         string Information
         );
}

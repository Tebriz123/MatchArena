namespace MatchArena.MVC.ViewModels
{
    public record PutTeamVM(
        string Name,
        string Logo,
        IFormFile Photo
        ); 
}

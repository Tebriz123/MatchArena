namespace MatchArena.MVC.ViewModels
{
    public record PutTeamVM(
        string Name,
        string Logo,
        string CaptainName,
        IFormFile Photo,
        string City,
        int PlayerCount,
        int MaxPlayer,
        string Information
        ); 
}

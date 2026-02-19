namespace MatchArena.MVC.ViewModels
{
    public record PutTeamVM(
        string Name,
         string CaptainName,
         IFormFile Photo,
         string City,
         string Information
         );

}

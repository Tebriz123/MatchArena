namespace MatchArena.MVC.ViewModels.Players
{
    public record GetPlayerInTeamVM(
         long Id,
         string Name,
         string Surname,
         string Image,
         bool IsCaptain
         );
}

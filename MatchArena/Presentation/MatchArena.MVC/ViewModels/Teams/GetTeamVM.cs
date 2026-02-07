using MatchArena.Application.DTOs.Player;

namespace MatchArena.MVC.ViewModels
{
    public record GetTeamVM(
        long Id,
        string Name,
        int PlayerCount,
        string City,
        string Logo,
        ICollection<GetPlayerInTeamDto> Players,
        int GameCount
        );

}

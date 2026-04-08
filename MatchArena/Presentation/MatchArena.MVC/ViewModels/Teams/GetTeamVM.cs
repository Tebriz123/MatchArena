using MatchArena.Application.DTOs.Player;

namespace MatchArena.MVC.ViewModels
{
    public record GetTeamVM(
          long Id,
          string Name,
          int PlayerCount,
          string CaptainName,
          string City,
          string Logo,
          ICollection<GetPlayerInTeamDto> PlayerDtos,
          int GameCount,
          bool IsCaptain,
          int MaxPlayer,
          double Rating,
          string Information
          );

}

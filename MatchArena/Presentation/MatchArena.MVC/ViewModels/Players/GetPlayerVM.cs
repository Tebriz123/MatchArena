using MatchArena.Application.DTOs.Teams;
using MatchArena.Domain;

namespace MatchArena.MVC.ViewModels
{
    public record GetPlayerVM(
        long Id,
        string Image,
        string Name,
        string Surname,
        string Information,
        PlayerPosition Position,
        int Height,
        double Rating,
        int Age,
        string City,
        ICollection<GetTeamInPlayerDto> TeamDtos,
        int GameCount,
        int Goal
        );
     
}

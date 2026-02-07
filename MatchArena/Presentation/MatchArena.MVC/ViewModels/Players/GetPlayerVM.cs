using MatchArena.Application.DTOs.Teams;
using MatchArena.Domain.Entities;

namespace MatchArena.MVC.ViewModels
{
    public record GetPlayerVM(
        long Id,
        string Image,
        string Name,
        string Surname,
        string Description,
        PlayerPosition Position,
        double Rating,
        int Age,
        string City,
        ICollection<GetTeamInPlayerDto> TeamDtos,
        int GameCount,
        int Goal
        );
     
}

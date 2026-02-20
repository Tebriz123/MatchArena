using MatchArena.Application.DTOs.Teams;
using MatchArena.Domain;
using MatchArena.MVC.ViewModels.Teams;

namespace MatchArena.MVC.ViewModels
{
    public record GetPlayerVM(
         long Id,
         string Image,
         string Name,
         string Surname,
         int Height,
         string Information,
         PlayerPosition Position,
         PlayerLevel Level,
         int Age,
         string City,
         ICollection<GetTeamInPlayerVM> TeamVM,
         int GameCount,
         int Goal
         );

}

using MatchArena.Domain.Entities;

namespace MatchArena.MVC.ViewModels
{
    public record GetTeamItemVM(
      long Id,
      string Name,
      int PlayerCount,
      int MaxPlayer,
      string City,
      int GameCount,
      string CaptainName,
      string Logo,
      double Rating,
      ICollection<Team> Teams,
      string CaptainUserId
  );

}

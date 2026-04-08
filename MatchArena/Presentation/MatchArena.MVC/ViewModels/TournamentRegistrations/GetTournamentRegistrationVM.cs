using MatchArena.Domain.Entities.Enums;

namespace MatchArena.MVC.ViewModels.TournamentRegistrations
{
    public class GetTournamentRegistrationVM
    {
        public long Id { get; set; }
        public long TournamentId { get; set; }
        public string TournamentName { get; set; } = null!;
        public long TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public RegistrationStatus Status { get; set; }
    }
}

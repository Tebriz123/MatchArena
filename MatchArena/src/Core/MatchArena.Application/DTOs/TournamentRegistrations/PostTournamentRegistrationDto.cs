using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.TournamentRegistrations
{
    public record PostTournamentRegistrationDto(
    long TournamentId,
    long TeamId
);
}

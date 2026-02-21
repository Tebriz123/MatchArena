using MatchArena.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.TournamentRegistrations
{
    public record GetTournamentRegistrationDto(
    long Id,
    long TournamentId,
    string TournamentName,
    long TeamId,
    string TeamName,
    RegistrationStatus Status
);

}

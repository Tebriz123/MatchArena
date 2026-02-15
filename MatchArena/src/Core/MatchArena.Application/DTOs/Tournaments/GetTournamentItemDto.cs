using MatchArena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Tournaments
{
    public record GetTournamentItemDto(
        long Id,
        string Name,
        string City,
        string Logo,
        DateTime StartDate,
        DateTime EndDate,
        int CurrentTeams,
        int MaxTeams,
        TournamentStatus Status,
        decimal PrizeFund
        );

}

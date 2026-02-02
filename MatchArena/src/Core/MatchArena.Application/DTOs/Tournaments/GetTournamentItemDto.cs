using MatchArena.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Tournaments
{
    public record GetTournamentItemDto(
        long id,
        string Name,
        string City,
        string Icon,
        DateTime StartTime,
        DateTime EndTime,
        int CurrrentTeams,
        int MaxTeams,
        TournamentStatus Status,
        decimal PrizeFund,
        double Rating
        );

}

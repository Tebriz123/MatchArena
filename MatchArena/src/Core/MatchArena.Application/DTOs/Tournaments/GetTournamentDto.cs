using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Tournaments
{
    public record GetTournamentDto(
        long Id,
        string Name,
        string City,
        string Address,
        string Logo,
        decimal PrizeFund,
        int CurrentTeams,
        int MaxTeams,
        string Format,
        string GameFormat,
        DateTime StartDate,
        DateTime EndDate,
        DateTime RegistrationDeadline  
        );
    
}

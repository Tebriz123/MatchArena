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
        string Icon,
        decimal PrizeFund,
        int CurrentTeams,
        int MaxTeams,
        string Format,
        string GameFormat,
        double Rting,
        DateTime StartTime,
        DateTime EndTime,
        DateTime RegistrationDeadline  
        );
    
}

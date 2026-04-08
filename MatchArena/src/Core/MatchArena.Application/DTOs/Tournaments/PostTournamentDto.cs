using MatchArena.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Tournaments
{
    public record PostTournamentDto(
        string Name,
        string Description,
        string Address,
        string City,
        IFormFile Photo,
        DateTime StartDate,
        DateTime EndDate,
        DateTime RegistrationDeadline,
        int MaxTeams,
        decimal EntryFee,
        decimal PrizeFund,
        TournamentStatus Status
        );
    
}

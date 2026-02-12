using MatchArena.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Tournaments
{
    public record PutTournamentDto(
         string Name,
        string Description,
        String Address,
        string City,
        string Icon,
        IFormFile Photo,
        DateTime StartTime,
        DateTime EndTime,
        DateTime RegistrationDeadline,
        int MaxTeams,
        int CurrentTeams,
        decimal EntryFee,
        decimal PrizeFund,
        string Format,
        string GameFormat,
        TournamentStatus Status
        );
   
}

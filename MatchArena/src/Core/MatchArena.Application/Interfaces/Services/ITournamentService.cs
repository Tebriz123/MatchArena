using MatchArena.Application.DTOs.Tournaments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Interfaces.Services
{
    public interface ITournamentService
    {
        Task<IReadOnlyList<GetTournamentItemDto>> GetAllAsync(int page, int take);
        Task<GetTournamentDto> GetByIdAsync(long id);
        Task CreateTournamentAsync(PostTournamentDto tournamentDto);
        Task UpdateTournamentAsync(long id, PutTournamentDto tournamentDto);
        Task RemoveAsync(long id);

    }
}

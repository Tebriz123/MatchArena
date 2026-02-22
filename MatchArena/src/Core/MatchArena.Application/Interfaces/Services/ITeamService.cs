using MatchArena.Application.DTOs.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Interfaces.Services
{
    public interface ITeamService
    {
        Task<IReadOnlyList<GetTeamItemDto>> GetAllAsync(int page, int take);
        Task<GetTeamDto> GetByIdAsync(long id);
        Task CreateTeamAsync(PostTeamDto teamDto, string userId);
        Task UpdateTeamAsync(long id, PutTeamDto teamDto);
        Task RemoveAsync(long id);
        Task AddPlayerToTeamAsync(long teamId, string userId);
        Task RemovePlayerFromTeamAsync(long playerId, string userId);
        Task SendInviteAsync(long teamId, long playerId, string userId);
    }
}

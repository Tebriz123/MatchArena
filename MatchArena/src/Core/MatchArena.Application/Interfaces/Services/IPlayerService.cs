using MatchArena.Application.DTOs.Player;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Interfaces.Services
{
    public interface IPlayerService
    {
        Task<IReadOnlyList<GetPlayerItemDto>> GetAllAsync(int page, int take);
        Task<GetPlayerDto> GetByIdAsync(long id);
        Task CreatePlayerAsync(PostPlayerDto playerDto);
        Task UpdateProfileAsync(long id, PutPlayerDto playerDto);

    }
}

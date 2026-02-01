using AutoMapper;
using MatchArena.Application.DTOs.Player;
using MatchArena.Application.Interfaces.Repositories;
using MatchArena.Application.Interfaces.Services;
using MatchArena.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Persistence.Implementations.Services
{
    internal class PlayerService:IPlayerService
    {
        private readonly IPlayerRepository _repository;
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;

        public PlayerService(
            IPlayerRepository repository,
            IMapper mapper,
            ITeamRepository teamRepository

            
            )
        {
            _repository = repository;
            _mapper = mapper;
            _teamRepository = teamRepository;
        }


        public async Task<IReadOnlyList<GetPlayerItemDto>> GetAllAsync(int page,int take)
        {
            IReadOnlyList<Player> players = await _repository.GetAll(
               page: page,
               take: take
                
               ).ToListAsync();
            return _mapper.Map<IReadOnlyList<GetPlayerItemDto>>(players);
        }


        public async Task<GetPlayerDto> GetByIdAsync(long id)
        {
            Player player = await _repository.GetByIdAsync(id, "PlayerTeams.Team");
            if (player is null) throw new Exception("Player not found");

            return _mapper.Map<GetPlayerDto>(player);
        }


        public async Task CreatePlayerAsync(PostPlayerDto playerDto)
        {
            Player player = _mapper.Map<Player>(playerDto);
            _repository.Add(player);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdatePlayerAsync(long id, PutPlayerDto playerDto)
        {
            Player player = await _repository.GetByIdAsync(id);
           if (player is null) throw new Exception("Player not found");

            _repository.Update(player);
            await _repository.SaveChangesAsync();
        }

        public async Task RemoveAsync(long id)
        {
            Player player = await _repository.GetByIdAsync(id);
            if (player is null) throw new Exception("player not found");
            _repository.Remove(player);
            await _repository.SaveChangesAsync();   
        }
    }
}

using AutoMapper;
using MatchArena.Application.DTOs.Player;
using MatchArena.Application.DTOs.Teams;
using MatchArena.Application.Interfaces.Repositories;
using MatchArena.Application.Interfaces.Services;
using MatchArena.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Persistence.Implementations.Services
{
    internal class TeamService:ITeamService
    {
        private readonly ITeamRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPlayerRepository _playerRepository;

        public TeamService(
            ITeamRepository repository,
            IMapper mapper,
            IPlayerRepository playerRepository
            )
        {
            _repository = repository;
            _mapper = mapper;
            _playerRepository = playerRepository;
        }

        public async Task<IReadOnlyList<GetTeamItemDto>> GetAllAsync(int page, int take)
        {
            IReadOnlyList<Team> teams = await _repository.GetAll(
                 page: page,
               take: take,
               includes: nameof(Team.Player)
                ).ToListAsync();
            return _mapper.Map<IReadOnlyList<GetTeamItemDto>>(teams);
        }

        public async Task<GetTeamDto> GetByIdAsync(long id)
        {
            Team team = await _repository.GetByIdAsync(id, "TeamPlayers.Player", "Player");


            if (team is null)
                throw new Exception($"Team not found");

            return _mapper.Map<GetTeamDto>(team);
        }
         
        public async Task CreateTeamAsync(PostTeamDto teamDto)
        {
            Team team = _mapper.Map<Team>(teamDto);
            _repository.Add(team);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateTeamAsync(long id, PutTeamDto teamDto)
        {
            Team team = await _repository.GetByIdAsync(id);

            if (team is null) throw new Exception("Team not found");

            _mapper.Map(teamDto, team);

            _repository.Update(team);
            await _repository.SaveChangesAsync();
        }

        public async Task RemoveAsync(long id)
        {
            Team team = await _repository.GetByIdAsync(id);

            if (team is null) throw new Exception("Team not found");

            _repository.Remove(team);

            await _repository.SaveChangesAsync();
        }

    }
}

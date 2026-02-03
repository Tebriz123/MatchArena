using AutoMapper;
using MatchArena.Application.DTOs.Tournaments;
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
    internal class TournamentService:ITournamentService
    {
        private readonly ITournamentRepository _repository;
        private readonly IMapper _mapper;

        public TournamentService(
            ITournamentRepository repository,
            IMapper mapper
            
            )
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<GetTournamentItemDto>> GetAllAsync(int page,int take)
        {
            IReadOnlyList<Tournament> tournaments = await _repository.GetAll(
                page: page,
                take: take
                ).ToListAsync();
            return _mapper.Map<IReadOnlyList<GetTournamentItemDto>>(tournaments);
        }
        public async Task<GetTournamentDto> GetByIdAsync(long id)
        {
            Tournament tournament = await _repository.GetByIdAsync(id);

            if (tournament is null) throw new Exception("Tournament not found");

            return _mapper.Map<GetTournamentDto>(tournament);

        }

        public async Task CreateTournamentAsync(PostTournamentDto tournamentDto)
        {
            Tournament tournament = _mapper.Map<Tournament>(tournamentDto);

            _repository.Add(tournament);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateTournamentAsync(long id, PutTournamentDto tournamentDto)
        {
            Tournament tournament = await _repository.GetByIdAsync(id);

            if (tournament is null) throw new Exception("Tournament not found");

            _repository.Update(tournament);
            await _repository.SaveChangesAsync();
        }

        public async Task RemoveAsync(long id)
        {
            Tournament tournament = await _repository.GetByIdAsync(id);

            if (tournament is null) throw new Exception("Tournament not found");

            _repository.Remove(tournament);

            await _repository.SaveChangesAsync();
        }

    }
}

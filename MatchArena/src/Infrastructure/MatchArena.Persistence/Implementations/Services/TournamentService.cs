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
    internal class TournamentService : ITournamentService
    {
        private readonly ITournamentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public TournamentService(
            ITournamentRepository repository,
            IMapper mapper,
            IFileService fileService)
        {
            _repository = repository;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<IReadOnlyList<GetTournamentItemDto>> GetAllAsync(int page, int take)
        {
            IReadOnlyList<Tournament> tournaments = await _repository.GetAll(
                page: page,
                take: take
            ).ToListAsync();
            return _mapper.Map<IReadOnlyList<GetTournamentItemDto>>(tournaments);
        }

        public async Task<GetTournamentDto> GetByIdAsync(long id)
        {
            Tournament tournament = await _repository.GetByIdAsync(id, "Registrations.Team");
            if (tournament is null) throw new Exception("Tournament is not faund");
            return _mapper.Map<GetTournamentDto>(tournament);
        }

        public async Task CreateTournamentAsync(PostTournamentDto tournamentDto)
        {
            if (tournamentDto is null) throw new Exception(nameof(tournamentDto));

            Tournament tournament = _mapper.Map<Tournament>(tournamentDto);

            if (tournamentDto.Photo is not null)
                tournament.Logo = await _fileService.FileCreateAsync(tournamentDto.Photo);

            tournament.Status = TournamentStatus.Upcoming;
            tournament.CurrentTeams = 0;
            tournament.CreatedAt = DateTime.UtcNow;
            tournament.UpdatedAt = DateTime.UtcNow;

            _repository.Add(tournament);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateTournamentAsync(long id, PutTournamentDto tournamentDto)
        {
            Tournament tournament = await _repository.GetByIdAsync(id);
            if (tournament is null) throw new Exception("Turnir tapılmadı");

            string? oldLogo = tournament.Logo;
            _mapper.Map(tournamentDto, tournament);

            if (tournamentDto.Photo is not null)
            {
                tournament.Logo = await _fileService.FileCreateAsync(tournamentDto.Photo);
                if (!string.IsNullOrEmpty(oldLogo))
                    await _fileService.FileDeleteAsync(oldLogo);
            }
            else
            {
                tournament.Logo = oldLogo;
            }

            tournament.UpdatedAt = DateTime.UtcNow;

            _repository.Update(tournament);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateStatusAsync(long id, TournamentStatus status)
        {
            Tournament tournament = await _repository.GetByIdAsync(id);
            if (tournament is null) throw new Exception("Turnir tapılmadı");

            tournament.Status = status;
            tournament.UpdatedAt = DateTime.UtcNow;

            _repository.Update(tournament);
            await _repository.SaveChangesAsync();
        }

        public async Task RemoveAsync(long id)
        {
            Tournament tournament = await _repository.GetByIdAsync(id);
            if (tournament is null) throw new Exception("Turnir tapılmadı");

            if (!string.IsNullOrEmpty(tournament.Logo))
                await _fileService.FileDeleteAsync(tournament.Logo);

            _repository.Remove(tournament);
            await _repository.SaveChangesAsync();
        }
    }
}

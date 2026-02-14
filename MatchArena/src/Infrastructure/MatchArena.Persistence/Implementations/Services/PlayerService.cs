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
    internal class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _repository;
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;
        private readonly IFileService _fileService;

        public PlayerService(
            IPlayerRepository repository,
            IMapper mapper,
            ITeamRepository teamRepository,
            IFileService fileService
        )
        {
            _repository = repository;
            _mapper = mapper;
            _teamRepository = teamRepository;
            _fileService = fileService;
        }

        public async Task<IReadOnlyList<GetPlayerItemDto>> GetAllAsync(int page, int take)
        {
            IReadOnlyList<Player> players = await _repository.GetAll(
                page: page,
                take: take,
                includes: "User" 
            ).ToListAsync();

            return _mapper.Map<IReadOnlyList<GetPlayerItemDto>>(players);
        }

        public async Task<GetPlayerDto> GetByIdAsync(long id)
        {
            Player player = await _repository.GetByIdAsync(id, "PlayerTeams.Team", "User");
            if (player is null)
                throw new Exception("Player not found");

            return _mapper.Map<GetPlayerDto>(player);
        }

        public async Task CreatePlayerAsync(PostPlayerDto playerDto, string userId) 
        {
            string imageUrl = string.Empty;
            if (playerDto.Photo is not null)
            {
                imageUrl = await _fileService.FileCreateAsync(playerDto.Photo);
            }

            Player player = _mapper.Map<Player>(playerDto);
            player.Image = imageUrl;
            player.UserId = userId;
            player.Rating = 0;
            player.PlayedMatches = 0;
            player.GameCount = 0;
            player.Goal = 0;

            _repository.Add(player);
            await _repository.SaveChangesAsync();
        }
        public async Task UpdatePlayerAsync(long id, PutPlayerDto playerDto)
        {
            Player player = await _repository.GetByIdAsync(id);
            if (player is null)
                throw new Exception("Player not found");

            string oldImage = player.Image;

            _mapper.Map(playerDto, player);

            if (playerDto.Photo is not null)
            {
                string newImageUrl = await _fileService.FileCreateAsync(playerDto.Photo);

                if (!string.IsNullOrEmpty(oldImage))
                {
                    await _fileService.FileDeleteAsync(oldImage);
                }

                player.Image = newImageUrl;
            }

            _repository.Update(player);
            await _repository.SaveChangesAsync();
        }

        public async Task RemoveAsync(long id)
        {
            Player player = await _repository.GetByIdAsync(id);
            if (player is null)
                throw new Exception("Player not found");

            if (!string.IsNullOrEmpty(player.Image))
            {
                await _fileService.FileDeleteAsync(player.Image);
            }

            _repository.Remove(player);
            await _repository.SaveChangesAsync();
        }
    }
}

using AutoMapper;
using MatchArena.Application.DTOs.Player;
using MatchArena.Application.Interfaces.Repositories;
using MatchArena.Application.Interfaces.Services;
using MatchArena.Domain.Entities;
using MatchArena.Domain.Entities.Enums;
using MatchArena.Persistence.Implementations.Repositories;
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
        private readonly IInviteRepository _inviteRepository;

        public PlayerService(
            IPlayerRepository repository,
            IMapper mapper,
            ITeamRepository teamRepository,
            IFileService fileService,
            IInviteRepository inviteRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _teamRepository = teamRepository;
            _fileService = fileService;
            _inviteRepository = inviteRepository;
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
            player.Name = playerDto.Name;      
            player.Surname = playerDto.Surname;
            player.Rating = 0;
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
        public async Task<bool> PlayerExistsAsync(string userId)
        {
            return await _repository.AnyAsync(p => p.UserId == userId);
        }
        public async Task LeaveTeamAsync(long teamId, string userId)
        {
            var player = await _repository.GetAll(
                func: p => p.UserId == userId,
                includes: "PlayerTeams"
            ).FirstOrDefaultAsync();

            if (player is null)
                throw new Exception("Player not found");

            var teamPlayer = player.PlayerTeams.FirstOrDefault(pt => pt.TeamId == teamId);
            if (teamPlayer is null)
                throw new Exception("Player is not a member of this team");

            if (teamPlayer.IsCaptain)
            {
                var team = await _teamRepository.GetByIdAsync(teamId);
                if (team is null)
                    throw new Exception("Team not found");

                _teamRepository.Remove(team);
                await _teamRepository.SaveChangesAsync();
                return;
            }

            player.PlayerTeams.Remove(teamPlayer);
            await _repository.SaveChangesAsync();
        }

        public async Task AcceptInviteAsync(long inviteId, string userId)
        {
            Player? player = _repository.GetAll(p => p.UserId == userId).FirstOrDefault();
            if (player is null) throw new Exception("You are not a player");

            TeamInvite invite = await _inviteRepository.GetByIdAsync(inviteId, "Team.TeamPlayers");
            if (invite is null) throw new Exception("Invite not found");

            if (invite.PlayerId != player.Id) throw new Exception("This invite is not for you");
            if (invite.Status != InviteStatus.Pending) throw new Exception("Invite is no longer valid");
            if (invite.Team.PlayerCount >= invite.Team.MaxPlayer) throw new Exception("Team is full");

            invite.Status = InviteStatus.Accepted;
            invite.Team.TeamPlayers.Add(new TeamPlayer
            {
                PlayerId = player.Id,
                IsCaptain = false
            });
            invite.Team.PlayerCount++;

            await _inviteRepository.SaveChangesAsync();
        }

        public async Task RejectInviteAsync(long inviteId, string userId)
        {
            Player player = _repository.GetAll(p => p.UserId == userId).FirstOrDefault();
            if (player is null) throw new Exception("You are not a player");

            TeamInvite invite = await _inviteRepository.GetByIdAsync(inviteId);
            if (invite is null) throw new Exception("Invite not found");

            if (invite.PlayerId != player.Id) throw new Exception("This invite is not for you");
            if (invite.Status != InviteStatus.Pending) throw new Exception("Invite is no longer valid");

            invite.Status = InviteStatus.Rejected;
            await _inviteRepository.SaveChangesAsync();
        }

        public async Task<List<GetInviteDto>> GetMyInvitesAsync(string userId)
        {
            Player player = _repository.GetAll(p => p.UserId == userId).FirstOrDefault();
            if (player is null) return new List<GetInviteDto>();

            return await _inviteRepository.GetAll(
                i => i.PlayerId == player.Id && i.Status == InviteStatus.Pending,
                includes: "Team"
            ).Select(i => new GetInviteDto
            {
                Id = i.Id,
                TeamName = i.Team.Name
            }).ToListAsync();
        }
    }
}

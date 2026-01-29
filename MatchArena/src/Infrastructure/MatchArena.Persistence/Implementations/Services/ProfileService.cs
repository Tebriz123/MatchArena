using AutoMapper;
using MatchArena.Application.DTOs.Player;
using MatchArena.Application.Interfaces.Repositories;
using MatchArena.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Persistence.Implementations.Services
{
    internal class ProfileService
    {
        private readonly IPlayerRepository _repository;
        private readonly IMapper _mapper;

        public ProfileService(
            IPlayerRepository repository,
            IMapper mapper

            
            )
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<IReadOnlyList<GetPlayerItemDto>> GetAllAsync(int page,int take)
        {
            IReadOnlyList<PlayerProfile> players = await _repository.GetAll(
               page: page,
               take: take
                
               ).ToListAsync();
            return _mapper.Map<IReadOnlyList<GetPlayerItemDto>>(players);
        }


        public async Task<GetPlayerDto> GetByIdAsync(long id)
        {
            PlayerProfile player = await _repository.GetByIdAsync(id)
        }
    }
}

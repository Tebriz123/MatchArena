using AutoMapper;
using MatchArena.Application.DTOs.Sizes;
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
    internal class SizeService : ISizeService
    {
        private readonly ISizeRepository _repository;
        private readonly IMapper _mapper;

        public SizeService(ISizeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task CreateAsync(PostSizeDto sizeDto)
        {
            bool result = await _repository.AnyAsync(s => s.Name == sizeDto.Name);
            if (result)
            {
                throw new Exception("Tag Name Existed");
            }


            Size size = _mapper.Map<Size>(sizeDto);
            size.CreatedAt = DateTime.Now;

            _repository.Add(size);
            await _repository.SaveChangesAsync();

        }

        public async Task<IReadOnlyList<GetSizeItemDto>> GetAllAsync(int page, int take)
        {
            IReadOnlyList<Size> sizes = await _repository.GetAll(
                page: page,
                take: take
                ).ToListAsync();
            return _mapper.Map<IReadOnlyList<GetSizeItemDto>>(sizes);
        }

        public async Task<GetSizeDto> GetByIdAsync(int id)
        {
            Size? size = await _repository.GetByIdAsync(id);

            if (size is null) throw new Exception("Size not found");

            return _mapper.Map<GetSizeDto>(size);
        }

        public async Task RemoveAsync(int id)
        {
            Size? size = await _repository.GetByIdAsync(id);
            if (size is null) throw new Exception("Size not found");

            _repository.Remove(size);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(PutSizeDto sizeDto, int id)
        {
            Size? size = await _repository.GetByIdAsync(id);


            if (size is null) throw new Exception("Size not found");

            size = _mapper.Map(sizeDto, size);

            size.UpdatedAt = DateTime.Now;

            _repository.Update(size);
            await _repository.SaveChangesAsync();
        }
    }
}

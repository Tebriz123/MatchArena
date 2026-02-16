using AutoMapper;
using MatchArena.Application.DTOs.Colors;
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
    internal class ColorService : IColorService
    {
        private readonly IColorRepository _repository;
        private readonly IMapper _mapper;

        public ColorService(IColorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task CreateAsync(PostColorDto colorDto)
        {
            bool result = await _repository.AnyAsync(c => c.Name == colorDto.Name);
            if (result)
            {
                throw new Exception("Color Name Existed");
            }


            Color color = _mapper.Map<Color>(colorDto);
            color.CreatedAt = DateTime.Now;

            _repository.Add(color);
            await _repository.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<GetColorItemDto>> GetAllAsync(int page, int take)
        {
            IReadOnlyList<Color> colors = await _repository.GetAll(
                page: page,
                take: take
                ).ToListAsync();
            return _mapper.Map<IReadOnlyList<GetColorItemDto>>(colors);
        }

        public async Task<GetColorDto> GetByIdAsync(int id)
        {
            Color? color = await _repository.GetByIdAsync(id);

            if (color is null) throw new Exception("Color not found");

            return _mapper.Map<GetColorDto>(color);
        }

        public async Task RemoveAsync(int id)
        {
            Color? color = await _repository.GetByIdAsync(id);
            if (color is null) throw new Exception("Color not found");

            _repository.Remove(color);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(PutColorDto colorDto, int id)
        {
            Color? color = await _repository.GetByIdAsync(id);


            if (color is null) throw new Exception("Color not found");

            color = _mapper.Map(colorDto, color);

            color.UpdatedAt = DateTime.Now;

            _repository.Update(color);
            await _repository.SaveChangesAsync();
        }
        public async Task RemoveAsync(long id)
        {
            Color color = await _repository.GetByIdAsync(id);
            if (color is null) throw new Exception("Color not found");

            _repository.Remove(color);
            await _repository.SaveChangesAsync();
        }
    }

}

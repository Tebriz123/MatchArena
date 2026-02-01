using AutoMapper;
using MatchArena.Application.DTOs.Fields;
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
    internal class FieldService:IFieldService
    {
        private readonly IFieldRepository _repository;
        private readonly IMapper _mapper;

        public FieldService(
            IFieldRepository repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<GetFieldItemDto>> GetAllAsync(int page,int take)
        {
            IReadOnlyList<Field> fields= await _repository.GetAll(
                page:page,
                take:take
                ).ToListAsync();
            return _mapper.Map<IReadOnlyList<GetFieldItemDto>>(fields);
        }

        public async Task<GetFieldDto> GetByIdAsync(long id)
        {
            Field filed = await _repository.GetByIdAsync(id,nameof(FieldImage.Field));
            if (filed is null) throw new Exception("Field not found");

            return _mapper.Map<GetFieldDto>(filed);
        }

        public async Task CreateFieldAsync(PostFieldDto fieldDto)
        {
            Field field = _mapper.Map<Field>(fieldDto);

            _repository.Add(field);

            await _repository.SaveChangesAsync();

        }

        public async Task UpdateFieldAsync(long id, PutFieldDto fieldDto)
        {
            Field field = await _repository.GetByIdAsync(id);

            if (field is null) throw new Exception("Field not found");
            _repository.Update(field);
            await _repository.SaveChangesAsync();
        }

        public async Task Remove(long id)
        {
            Field field = await _repository.GetByIdAsync(id);
            if (field is null) throw new Exception("Field not found");

            _repository.Remove(field);
            await _repository.SaveChangesAsync();

        }
    }
}

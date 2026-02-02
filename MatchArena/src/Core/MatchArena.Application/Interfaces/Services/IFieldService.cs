using MatchArena.Application.DTOs.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Interfaces.Services
{
    public interface IFieldService
    {
        Task<IReadOnlyList<GetFieldItemDto>> GetAllAsync(int page, int take);
        Task<GetFieldDto> GetByIdAsync(long id);
        Task CreateFieldAsync(PostFieldDto fieldDto);
        Task UpdateFieldAsync(long id, PutFieldDto fieldDto);
        Task Remove(long id);
    }
}

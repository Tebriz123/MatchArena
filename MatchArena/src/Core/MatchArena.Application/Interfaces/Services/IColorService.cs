using MatchArena.Application.DTOs.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Interfaces.Services
{
    public interface IColorService
    {
        Task CreateAsync(PostColorDto colorDto);
        Task<IReadOnlyList<GetColorItemDto>> GetAllAsync(int page, int take);
        Task<GetColorDto> GetByIdAsync(int id);
        Task UpdateAsync(PutColorDto colorDto, int id);
        Task RemoveAsync(long id);
    }
}

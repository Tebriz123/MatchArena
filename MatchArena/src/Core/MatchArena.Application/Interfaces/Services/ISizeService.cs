using MatchArena.Application.DTOs.Sizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Interfaces.Services
{
    public interface ISizeService
    {
        Task<IReadOnlyList<GetSizeItemDto>> GetAllAsync(int page, int take);
        Task<GetSizeDto> GetByIdAsync(int id);
        Task CreateAsync(PostSizeDto sizeDto);
        Task UpdateAsync(PutSizeDto sizeDto, int id);
        Task RemoveAsync(int id);


    }

}

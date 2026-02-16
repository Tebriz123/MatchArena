using MatchArena.Application.DTOs.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task CreateAsync(PostCategoryDto categoryDto);
        Task<IReadOnlyList<GetCategoryItemDto>> GetAllAsync(int page, int take);
        Task<GetCategoryDto> GetByIdAsync(int id);
        Task UpdateAsync(PutCategoryDto categoryDto, int id);
        Task RemoveAsync(int id);
    }
}

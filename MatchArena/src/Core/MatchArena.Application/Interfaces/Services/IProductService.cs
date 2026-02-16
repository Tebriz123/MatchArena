using MatchArena.Application.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<IReadOnlyList<GetProductItemDto>> GetAllAsync(int page, int take);
        Task<GetProductDto> GetByIdAsync(long id);
        Task CreateProductAsync(PostProductDto productDto);
        Task UpdateProductAsync(long id, PutProductDto productDto);
        Task RemoveAsync(long id);



    }
}

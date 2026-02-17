using MatchArena.Application.DTOs.Categories;
using MatchArena.Application.DTOs.Colors;
using MatchArena.Application.DTOs.Sizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Products
{
    public record GetProductDto(
        long Id,
        string Name,
        decimal Price,
        string SKU,
        string Image,
        string Description,
        GetCategoryInProductDto CategoryDto,
        ICollection<GetColorInProductDto> ColorDtos,
        ICollection<GetSizeInProductDto> SizeDtos

        );
    
}

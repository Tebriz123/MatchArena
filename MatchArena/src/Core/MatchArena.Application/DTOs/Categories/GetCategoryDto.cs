using MatchArena.Application.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Categories
{
    public record GetCategoryDto(
       long Id,
       string Name,
       IEnumerable<GetProductInCategoryDto> ProductDtos
       );
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Categories
{
    public record GetCategoryItemDto(
       long Id,
       string Name,
       int ProductCount
       );
}

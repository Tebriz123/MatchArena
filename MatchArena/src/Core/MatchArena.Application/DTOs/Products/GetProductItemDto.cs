using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Products
{
    public record GetProductItemDto(
        long Id,
        string Name,
        decimal Price,
        string CategoryName
        );
   
}

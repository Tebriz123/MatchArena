using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Products
{
    public record PutProductDto(
        string Name,
        decimal Price,
        string Description,
        string SKU,
        long CategoryId,
        ICollection<long> TagIds,
        ICollection<long> SizeIds,
        ICollection<long> ColorIds
        );
   
}

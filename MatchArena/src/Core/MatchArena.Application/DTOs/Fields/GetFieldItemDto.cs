using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Fields
{
    public record GetFieldItemDto(
        long Id,
        string Name,
        string Address,
        int TotalRating,
        double AverageRating,
        string City,
        decimal PricePerHour
        );
   
}

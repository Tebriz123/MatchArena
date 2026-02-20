using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Fields
{
    public record GetFieldDto(
        long Id,
        string Name,
        string City,
        int TotalRating,
        string Information,
        double AverageRating,
        string Address,
        decimal PricePerHour,
        TimeOnly StartDate,
        TimeOnly EndDate,
        ICollection<TimeOnly> EmptySpace
        );
   
}

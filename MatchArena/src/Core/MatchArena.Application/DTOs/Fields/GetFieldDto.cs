using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Fields
{
    public record GetFieldDto(
        long Id,
        string City,
        int TotalRating,
        double AverageRating,
        string Address,
        decimal PricePerHour,
        DateTime StartDate,
        DateTime EndDate,
        ICollection<DateTime> EmptySpace
        );
   
}

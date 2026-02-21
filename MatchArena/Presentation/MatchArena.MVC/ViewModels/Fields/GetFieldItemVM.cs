using MatchArena.Domain.Entities;

namespace MatchArena.MVC.ViewModels
{
  public record GetFieldItemVM(
        long Id,
        string Name,
        string Address,
        int TotalRating,
        string Image,
        double AverageRating,
        string City,
        decimal PricePerHour, 
        TimeOnly StartTime,
        TimeOnly EndTime,
        ICollection<TimeOnly> EmptySpace
		);

}

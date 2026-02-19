using MatchArena.Domain.Entities;

namespace MatchArena.MVC.ViewModels
{
  public record GetFieldItemVM(
        long Id,
        string Name,
        string Address,
        int TotalRating,
        double AverageRating,
        string City,
        decimal PricePerHour,
        ICollection<Field> Fields
        );

}

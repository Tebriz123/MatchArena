namespace MatchArena.MVC.ViewModels
{
    public record GetFieldVM(
        long Id,
        string Name,
        string City,
        string Information,
        int TotalRating,
        double AverageRating,
        string Address,
        decimal PricePerHour,
        TimeOnly StartDate,
        TimeOnly EndDate,
        ICollection<TimeOnly> EmptySpace
        );

}

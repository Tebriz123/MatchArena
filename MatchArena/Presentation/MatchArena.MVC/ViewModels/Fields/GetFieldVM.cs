namespace MatchArena.MVC.ViewModels
{
    public record GetFieldVM(
        long Id,
        string Name,
        string City,
        int TotalRating,
        double AverageRating,
        string Address,
        decimal PricePerHour,
        TimeOnly StartDate,
        TimeOnly EndDate,
        ICollection<TimeOnly> EmptySpace
        );

}

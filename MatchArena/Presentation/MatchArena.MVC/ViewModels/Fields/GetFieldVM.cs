namespace MatchArena.MVC.ViewModels
{
    public record GetFieldVM(
        long Id,
        string Name,    
        string City,
        double Rating,
        string Address,
        decimal PricePerHour,
        string Image,
        DateTime StartDate,
        DateTime EndDate,
        ICollection<DateTime> EmptyTime
        );

}

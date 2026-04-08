namespace MatchArena.MVC.ViewModels
{
    public class GetProductRatingResponseVM
    {
        public int TotalRatings { get; set; }
        public double AverageRating { get; set; }
        public List<GetRatingItemVM> Ratings { get; set; } = new();
    }
}

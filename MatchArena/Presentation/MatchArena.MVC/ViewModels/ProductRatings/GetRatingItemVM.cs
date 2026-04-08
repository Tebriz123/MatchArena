namespace MatchArena.MVC.ViewModels
{
    public class GetRatingItemVM
    {
        public string UserName { get; set; } = null!;
        public string? Comment { get; set; }
        public int Rating { get; set; }
        public DateTime RatedAt { get; set; }
    }
}

namespace MatchArena.MVC.ViewModels
{
    public class GetFieldVM
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Information { get; set; } = null!;
        public int TotalRating { get; set; }
        public double AverageRating { get; set; }
        public GetFieldRatingResponseVM Ratings { get; set; } = new();
        public string PrimaryPhoto { get; set; } = null!;
        public ICollection<string> AdditionalPhoto { get; set; } = new List<string>();
        public string Address { get; set; } = null!;
        public decimal PricePerHour { get; set; }
        public TimeOnly StartDate { get; set; }
        public TimeOnly EndDate { get; set; }
        public ICollection<TimeOnly> EmptySpace { get; set; } = new List<TimeOnly>();
    }

}



using MatchArena.Domain.Entities;

namespace MatchArena.MVC.ViewModels.Sizes
{
    public class GetSizeItemVM
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public int ProductCount { get; set; }
    }

}

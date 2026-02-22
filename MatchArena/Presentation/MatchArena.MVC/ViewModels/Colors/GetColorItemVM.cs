using MatchArena.Domain.Entities;

namespace MatchArena.MVC.ViewModels.Colors
{
    public class GetColorItemVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }

    }
}

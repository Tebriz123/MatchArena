

using MatchArena.Domain.Entities;

namespace MatchArena.MVC.ViewModels.Sizes
{
    public record GetSizeItemVM(
        long Id,
        string Name,
        List<Product> Products
        );
}

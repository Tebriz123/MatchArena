using MatchArena.Domain.Entities;

namespace MatchArena.MVC.ViewModels.Products
{
    public record GetProductItemVM(
       long Id,
       string Name,
       string Image,
       decimal Price,
       string CategoryName,
       ICollection<Player> Players
       );

}

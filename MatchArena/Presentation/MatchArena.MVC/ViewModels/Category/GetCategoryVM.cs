using MatchArena.Application.DTOs.Products;

namespace MatchArena.MVC.ViewModels.Category
{
    public record GetCategoryVM(
       long Id,
       string Name,
       IEnumerable<GetProductInCategoryDto> ProductDtos
       );
}

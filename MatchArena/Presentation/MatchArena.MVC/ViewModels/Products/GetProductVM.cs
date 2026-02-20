using MatchArena.Application.DTOs.Categories;
using MatchArena.Application.DTOs.Colors;
using MatchArena.Application.DTOs.Sizes;

namespace MatchArena.MVC.ViewModels.Products
{
    public record GetProductVM(
        long Id,
        string Name,
        decimal Price,
        string Image,
        string Description,
        GetCategoryInProductDto CategoryDto,
        ICollection<GetColorInProductDto> ColorDtos,
        ICollection<GetSizeInProductDto> SizeDtos
        );
}

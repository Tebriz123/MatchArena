using MatchArena.Application.DTOs.Categories;
using MatchArena.Application.DTOs.Colors;
using MatchArena.Application.DTOs.Sizes;
using MatchArena.Domain.Entities;

namespace MatchArena.MVC.ViewModels.Products
{
    public class GetProductVM
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public string Image { get; set; } = null!;
        public long CategoryId { get; set; }
        public int ProductCount { get; set; } 
        public string CategoryName { get; set; } = null!;
        public List<string> Colors { get; set; } = new();
        public List<string> Sizes { get; set; } = new();
        public List<string> Images { get; set; } = new();
    }
}

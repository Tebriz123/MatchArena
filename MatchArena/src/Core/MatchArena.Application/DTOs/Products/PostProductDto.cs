using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Products
{
    public record PostProductDto(
        string Name,
        decimal Price,
        IFormFile PrimaryPhoto,
        ICollection<IFormFile>? AdditionalPhotos,
        string Description,
        long CategoryId,
        ICollection<long> SizeIds,
        ICollection<long> ColorIds
        );
   
}

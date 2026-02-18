using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Products
{
    public record PutProductDto(
        string Name,
        decimal Price,
        string Description,
        long CategoryId,
        IFormFile PrimaryPhoto,
        ICollection<IFormFile> AdditionalPhotos,
        ICollection<long> SizeIds,
        ICollection<long> ColorIds
        );
   
}

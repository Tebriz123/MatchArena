using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Fields
{
    public record PostFieldDto(
        string Name,
        string City,
        string Address,
        string Image,
        IFormFile PrimaryPhoto,
        ICollection<IFormFile> AdditionalPhotos,
        decimal PricePerHour,
        DateTime StartTime,
        DateTime EndTime,
        string FieldInformation
        );
   
}

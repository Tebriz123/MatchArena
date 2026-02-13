using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Teams
{
    public record PutTeamDto(
       string Name,
        string CaptainName,
        IFormFile Photo,
        string City,
        string Information
        );
    
}

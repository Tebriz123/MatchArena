using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Teams
{
    public record PostTeamDto(
        string Name,
        string Logo,
        IFormFile Photo    
        );
   
}

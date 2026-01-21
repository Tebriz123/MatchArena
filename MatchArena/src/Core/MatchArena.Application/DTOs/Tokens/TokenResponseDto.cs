using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Tokens
{
    public record TokenResponseDto(
        string Token,
        string UserName,
        DateTime Expires
        );
    
}

using MatchArena.Application.DTOs.Tokens;
using MatchArena.Application.Interfaces.Services;
using MatchArena.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Infrastructure.Implementations.Services
{
    internal class TokenService: ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenResponseDto CreateAccessToken(AppUser user,int minutes)
        {
            IEnumerable<Claim> userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Surname,user.Surname),
                new Claim(ClaimTypes.GivenName,user.Name)
            };


            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:issuer"],
                audience: _configuration["JWT:audience"],
                expires: DateTime.UtcNow.AddMinutes(minutes),
                notBefore: DateTime.UtcNow,
                claims: userClaims,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                         Encoding.ASCII.GetBytes(_configuration["JWT:secretKey"])),
                    SecurityAlgorithms.HmacSha256));

            return new TokenResponseDto(new JwtSecurityTokenHandler()
                .WriteToken(securityToken),
                user.UserName, 
                securityToken.ValidFrom);
        }
    }
}

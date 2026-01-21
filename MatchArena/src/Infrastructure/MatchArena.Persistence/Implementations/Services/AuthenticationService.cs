using AutoMapper;
using MatchArena.Application.DTOs.AppUsers;
using MatchArena.Application.DTOs.Tokens;
using MatchArena.Application.Interfaces.Services;
using MatchArena.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Persistence.Implementations.Services
{
    internal class AuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public AuthenticationService(
            UserManager<AppUser> userManager,
            IMapper mapper,
            IConfiguration configuration,
            ITokenService tokenService
            )
        {
            _userManager = userManager;
            _mapper = mapper;
           _configuration = configuration;
            _tokenService = tokenService;
        }

        public async Task RegisterAsync(RegisterDto registerDto)
        {
            IdentityResult result = await _userManager.CreateAsync(_mapper.Map<AppUser>(registerDto), registerDto.Password);
            if(!result.Succeeded)
            {
                StringBuilder sb = new();
                foreach (IdentityError error in result.Errors)
                {
                    sb.Append(error.Description);

                }
                throw new Exception(sb.ToString());
            }
        }

        public async Task<TokenResponseDto> LoginAsync(LoginDto loginDto)
        {
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == loginDto.UsernameOrEmail || u.UserName == loginDto.UsernameOrEmail);

            if(user is null)
            {
                throw new Exception("User information is wrong");
            }

            bool result = await _userManager.CheckPasswordAsync(user,loginDto.Password);
            if (!result)
            {
                await _userManager.AccessFailedAsync(user);
                throw new Exception("User information is wrong");
            }

            return _tokenService.CreateAccessToken(user, 15);
        }
    }
}

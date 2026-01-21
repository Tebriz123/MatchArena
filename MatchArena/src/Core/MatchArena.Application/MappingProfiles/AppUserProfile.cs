using AutoMapper;
using MatchArena.Application.DTOs.AppUsers;
using MatchArena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.MappingProfiles
{
    internal class AppUserProfile:Profile
    {
        public AppUserProfile()
        {
            CreateMap<RegisterDto, AppUser>();
        }
    }
}

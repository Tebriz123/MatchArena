using AutoMapper;
using MatchArena.Application.DTOs.Colors;
using MatchArena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.MappingProfiles
{
    internal class ColorProfile : Profile
    {

        public ColorProfile()
        {
            CreateMap<Color, GetColorItemDto>();
            CreateMap<Color, GetColorInProductDto>();
            CreateMap<PostColorDto, Color>();

            CreateMap<PutColorDto, Color>();
            CreateMap<Color, GetColorDto>();
        }
    }
}

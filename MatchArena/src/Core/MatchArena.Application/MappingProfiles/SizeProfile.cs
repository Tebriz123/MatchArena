using AutoMapper;
using MatchArena.Application.DTOs.Sizes;
using MatchArena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.MappingProfiles
{
    internal class SizeProfile : Profile
    {

        public SizeProfile()
        {
            CreateMap<Size, GetSizeItemDto>();
            CreateMap<Size, GetSizeInProductDto>();
            CreateMap<PostSizeDto, Size>();

            CreateMap<PutSizeDto, Size>();
            CreateMap<Size, GetSizeDto>();
        }


    }
}

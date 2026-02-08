using AutoMapper;
using MatchArena.Application.DTOs.Fields;
using MatchArena.Application.DTOs.Teams;
using MatchArena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.MappingProfiles
{
    internal class FieldProfile:Profile
    {
        public FieldProfile()
        {
            CreateMap<Field, GetFieldDto>();
            CreateMap<Field, GetFieldItemDto>();
            CreateMap<PostFieldDto, Field>();
            CreateMap<PutFieldDto, Field>();
        }
    }
}

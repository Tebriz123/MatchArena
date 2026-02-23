using AutoMapper;
using MatchArena.Application.DTOs.Ratings;
using MatchArena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.MappingProfiles
{
    internal class RatingProfile:Profile
    {
        public RatingProfile()
        {
            CreateMap<FieldRating, GetRatingItemDto>()
                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Player.Name + " " + src.Player.Surname));

            CreateMap<PlayerRating, GetRatingItemDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.RaterPlayer.Name + " " + src.RaterPlayer.Surname));

            CreateMap<ProductRating, GetRatingItemDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name+ " "+src.User.Surname));
            CreateMap<PostRatingDto, FieldRating>();

            CreateMap<PostRatingDto, PlayerRating>();

            CreateMap<PostRatingDto, ProductRating>();
        }
    }
}

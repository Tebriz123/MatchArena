using AutoMapper;
using MatchArena.Application.DTOs.Categories;
using MatchArena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.MappingProfiles
{
    internal class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, GetCategoryItemDto>()
                .ForCtorParam(nameof(GetCategoryItemDto.ProductCount),
                opt => opt.MapFrom(c => c.Products.Count));


            CreateMap<Category, GetCategoryDto>()
                .ForCtorParam(nameof(GetCategoryDto.ProductDtos)
                , opt => opt.MapFrom(c => c.Products));

            CreateMap<Category, GetCategoryInProductDto>().ReverseMap();
            CreateMap<PostCategoryDto, Category>().ReverseMap();

            CreateMap<PutCategoryDto, Category>();
        }

    }
}

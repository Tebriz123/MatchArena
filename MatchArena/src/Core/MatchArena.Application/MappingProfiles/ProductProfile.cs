using AutoMapper;
using MatchArena.Application.DTOs.Colors;
using MatchArena.Application.DTOs.Products;
using MatchArena.Application.DTOs.Sizes;
using MatchArena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.MappingProfiles
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, GetProductInCategoryDto>();
            CreateMap<Product, GetProductItemDto>()
                .ForCtorParam(nameof(GetProductItemDto.CategoryName),
                opt => opt.MapFrom(p => p.Category.Name));

            CreateMap<Product, GetProductDto>()
                .ForCtorParam(nameof(GetProductDto.CategoryDto),
                opt => opt.MapFrom(p => p.Category))

                .ForCtorParam(nameof(GetProductDto.ColorDtos),
                 opt => opt.MapFrom(p => p.ProductColors
                     .Select(pc => new GetColorInProductDto(pc.Color.Id, pc.Color.Name))
                      .ToList()))

                .ForCtorParam(nameof(GetProductDto.SizeDtos),
                opt => opt.MapFrom(p => p.ProductSizes
                .Select(ps => new GetSizeInProductDto(ps.Size.Id, ps.Size.Name))
                .ToList()));

            CreateMap<PostProductDto, Product>()
               
                 .ForMember(
                p => p.ProductColors,
                opt => opt.MapFrom(pDto => pDto.ColorIds
                                                      .Select(cId => new ProductColor { ColorId = cId })))
                  .ForMember(
                p => p.ProductSizes,
                opt => opt.MapFrom(pDto => pDto.SizeIds
                                                      .Select(sId => new ProductSize { SizeId = sId })));


            CreateMap<PutProductDto, Product>()
                .ForMember(
               p => p.ProductColors,
               opt => opt.MapFrom(pDto => pDto.ColorIds
                                                     .Select(cId => new ProductColor { ColorId = cId })))
                 .ForMember(
               p => p.ProductSizes,
               opt => opt.MapFrom(pDto => pDto.SizeIds
                                                     .Select(sId => new ProductSize { SizeId = sId })));

        }
    }
}

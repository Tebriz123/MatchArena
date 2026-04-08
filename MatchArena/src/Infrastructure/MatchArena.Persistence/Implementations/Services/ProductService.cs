using AutoMapper;
using MatchArena.Application.DTOs.Products;
using MatchArena.Application.Interfaces.Repositories;
using MatchArena.Application.Interfaces.Services;
using MatchArena.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


    namespace MatchArena.Persistence.Implementations.Services
    {
        internal class ProductService : IProductService
        {
            private readonly IProductRepository _repository;
            private readonly IMapper _mapper;
            private readonly ICategoryRepository _categoryRepository;
            private readonly IColorRepository _colorRepository;
            private readonly ISizeRepository _sizeRepository;
            private readonly IFileService _fileService;

            public ProductService(
                IProductRepository repository,
                IMapper mapper,
                ICategoryRepository categoryRepository,
                IColorRepository colorRepository,
                ISizeRepository sizeRepository,
                IFileService fileService
            )
            {
                _repository = repository;
                _mapper = mapper;
                _categoryRepository = categoryRepository;
                _colorRepository = colorRepository;
                _sizeRepository = sizeRepository;
                _fileService = fileService;
            }

            public async Task<IReadOnlyList<GetProductItemDto>> GetAllAsync(int page, int take)
            {
                IReadOnlyList<Product> products = await _repository.GetAll(
                    page: page,
                    take: take,
                    includes: nameof(Product.Category)
                ).ToListAsync();

                return _mapper.Map<IReadOnlyList<GetProductItemDto>>(products);
            }

            public async Task<GetProductDto> GetByIdAsync(long id)
            {
                Product product = await _repository.GetByIdAsync(
                    id,
                    nameof(Product.Category),
                    "ProductColors.Color",
                    "ProductSizes.Size",
                    "ProductImages"
                );

                if (product is null)
                    throw new Exception("Entity not found");

                return _mapper.Map<GetProductDto>(product);
            }

        public async Task CreateProductAsync(PostProductDto productDto)
        {
            bool exists = await _repository.AnyAsync(p => p.Name == productDto.Name);
            if (exists)
                throw new Exception("Entity already exists");

            bool categoryExists = await _categoryRepository.AnyAsync(c => c.Id == productDto.CategoryId);
            if (!categoryExists)
                throw new Exception("Category does not exist");

            if (productDto.ColorIds != null && productDto.ColorIds.Any())
            {
                var colors = await _colorRepository
                    .GetAll(c => productDto.ColorIds.Distinct().Contains(c.Id))
                    .ToListAsync();
                if (colors.Count != productDto.ColorIds.Distinct().Count())
                    throw new Exception("One or more colors do not exist");
            }

            if (productDto.SizeIds != null && productDto.SizeIds.Any())
            {
                var sizes = await _sizeRepository
                    .GetAll(s => productDto.SizeIds.Distinct().Contains(s.Id))
                    .ToListAsync();
                if (sizes.Count != productDto.SizeIds.Distinct().Count())
                    throw new Exception("One or more sizes do not exist");
            }

            string primaryImageUrl = string.Empty;
            if (productDto.PrimaryPhoto is not null)
                primaryImageUrl = await _fileService.FileCreateAsync(productDto.PrimaryPhoto);

            Product product = _mapper.Map<Product>(productDto);
            product.Image = primaryImageUrl;
            product.ProductImages = new List<ProductImage>();

            if (!string.IsNullOrEmpty(primaryImageUrl))
            {
                product.ProductImages.Add(new ProductImage
                {
                    Image = primaryImageUrl,
                    IsPrimary = true,
                    Product = product
                });
            }

            if (productDto.AdditionalPhotos is not null && productDto.AdditionalPhotos.Any())
            {
                foreach (IFormFile photo in productDto.AdditionalPhotos)
                {
                    string imageUrl = await _fileService.FileCreateAsync(photo);
                    product.ProductImages.Add(new ProductImage
                    {
                        Image = imageUrl,
                        IsPrimary = false,
                        Product = product
                    });
                }
            }

            _repository.Add(product);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(long id, PutProductDto productDto)
            {

                bool exists = await _repository.AnyAsync(p => p.Name == productDto.Name && p.Id != id);
                if (exists)
                    throw new Exception("Entity already exists");


                bool categoryExists = await _categoryRepository.AnyAsync(c => c.Id == productDto.CategoryId);
                if (!categoryExists)
                    throw new Exception("Category does not exist");


                var colors = await _colorRepository
                    .GetAll(c => productDto.ColorIds.Distinct().Contains(c.Id))
                    .ToListAsync();
                if (colors.Count != productDto.ColorIds.Distinct().Count())
                    throw new Exception("One or more colors do not exist");


                var sizes = await _sizeRepository
                    .GetAll(s => productDto.SizeIds.Distinct().Contains(s.Id))
                    .ToListAsync();
                if (sizes.Count != productDto.SizeIds.Distinct().Count())
                    throw new Exception("One or more sizes do not exist");


                Product product = await _repository.GetByIdAsync(
                    id,
                    "ProductColors",
                    "ProductSizes",
                    "ProductImages"
                );

                if (product is null)
                    throw new Exception("Product not found");


                if (productDto.PrimaryPhoto is not null)
                {
                    var oldPrimary = product.ProductImages?.FirstOrDefault(pi => pi.IsPrimary);
                    if (oldPrimary is not null)
                    {
                        await _fileService.FileDeleteAsync(oldPrimary.Image);
                        product.ProductImages!.Remove(oldPrimary);
                    }

                    string newPrimaryUrl = await _fileService.FileCreateAsync(productDto.PrimaryPhoto);
                    product.Image = newPrimaryUrl;
                    product.ProductImages!.Add(new ProductImage
                    {
                        Image = newPrimaryUrl,
                        IsPrimary = true
                    });
                }


                if (productDto.AdditionalPhotos is not null && productDto.AdditionalPhotos.Any())
                {
                    var oldAdditionals = product.ProductImages?
                        .Where(pi => !pi.IsPrimary)
                        .ToList();

                    if (oldAdditionals is not null)
                    {
                        foreach (var oldImg in oldAdditionals)
                        {
                            await _fileService.FileDeleteAsync(oldImg.Image);
                            product.ProductImages!.Remove(oldImg);
                        }
                    }

                    foreach (IFormFile photo in productDto.AdditionalPhotos)
                    {
                        string imageUrl = await _fileService.FileCreateAsync(photo);
                        product.ProductImages!.Add(new ProductImage
                        {
                            Image = imageUrl,
                            IsPrimary = false
                        });
                    }
                }

                _mapper.Map(productDto, product);
                _repository.Update(product);
                await _repository.SaveChangesAsync();
            }

            public async Task RemoveAsync(long id)
            {
                Product product = await _repository.GetByIdAsync(id, "ProductImages");

                if (product is null)
                    throw new Exception("Product not found");


                if (product.ProductImages is not null)
                {
                    foreach (var image in product.ProductImages)
                        await _fileService.FileDeleteAsync(image.Image);
                }

                _repository.Remove(product);
                await _repository.SaveChangesAsync();
            }
        }
    }

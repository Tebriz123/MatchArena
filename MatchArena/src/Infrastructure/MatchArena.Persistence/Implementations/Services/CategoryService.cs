using AutoMapper;
using MatchArena.Application.DTOs.Categories;
using MatchArena.Application.Interfaces.Repositories;
using MatchArena.Application.Interfaces.Services;
using MatchArena.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Persistence.Implementations.Services
{
    internal class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(PostCategoryDto categoryDto)
        {

            bool result = await _repository.AnyAsync(c => c.Name == categoryDto.Name);
            if (result)
            {
                throw new Exception("Category Name Existed");
            }


            Category category = _mapper.Map<Category>(categoryDto);

            _repository.Add(category);
            await _repository.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<GetCategoryItemDto>> GetAllAsync(int page, int take)
        {


            var categories = await _repository
                .GetAll(
               sort: c => c.Name,
               page: page,
               take: take,
               includes: nameof(Category.Products)
               ).ToListAsync();

            return _mapper.Map<IReadOnlyList<GetCategoryItemDto>>(categories);


        }

        public async Task<GetCategoryDto> GetByIdAsync(int id)
        {
            Category? category = await _repository.GetByIdAsync(id, nameof(Category.Products));

            if (category is null) throw new Exception("Category not found");

            return _mapper.Map<GetCategoryDto>(category);
        }

        public async Task UpdateAsync(PutCategoryDto categoryDto, int id)
        {
            Category? category = await _repository.GetByIdAsync(id);



            if (category is null) throw new Exception("Category not found");

            category = _mapper.Map(categoryDto, category);

            //category.Name = categoryDto.Name;


            _repository.Update(category);
            await _repository.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            Category? category = await _repository.GetByIdAsync(id);
            if (category is null) throw new Exception("Category not found");

            _repository.Remove(category);
            await _repository.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            Category? category = await _repository.GetByIdAsync(id);

            if (category is null) throw new Exception("Category not found");

            category.IsDeleted = true;
            _repository.Update(category);
            await _repository.SaveChangesAsync();
        }

    }
}

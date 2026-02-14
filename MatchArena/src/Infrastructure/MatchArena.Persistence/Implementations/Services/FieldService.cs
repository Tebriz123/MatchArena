using AutoMapper;
using MatchArena.Application.DTOs.Fields;
using MatchArena.Application.DTOs.Player;
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
    internal class FieldService : IFieldService
    {
        private readonly IFieldRepository _repository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService; 

        public FieldService(
            IFieldRepository repository,
            IMapper mapper,
            IFileService fileService) 
        {
            _repository = repository;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<IReadOnlyList<GetFieldItemDto>> GetAllAsync(int page, int take)
        {
            IReadOnlyList<Field> fields = await _repository.GetAll(
                page: page,
                take: take,
                includes: "FieldRatings" 
            ).ToListAsync();

            return _mapper.Map<IReadOnlyList<GetFieldItemDto>>(fields);
        }

        public async Task<GetFieldDto> GetByIdAsync(long id)
        {
            Field field = await _repository.GetByIdAsync(
                id,
                "FieldRatings",
                "Images");

            if (field is null)
                throw new Exception("Field not found");

            return _mapper.Map<GetFieldDto>(field);
        }

        public async Task CreateFieldAsync(PostFieldDto fieldDto)
        {
            if (fieldDto is null)
                throw new ArgumentNullException(nameof(fieldDto));

            string primaryImageUrl = string.Empty;
            if (fieldDto.PrimaryPhoto is not null)
            {
                primaryImageUrl = await _fileService.FileCreateAsync(fieldDto.PrimaryPhoto);
            }

            Field field = _mapper.Map<Field>(fieldDto);
            field.Image = primaryImageUrl;
            field.Images = new List<FieldImage>();

            field.EmptySpace = GenerateEmptyTimeSlots(fieldDto.StartTime, fieldDto.EndTime);

            if (!string.IsNullOrEmpty(primaryImageUrl))
            {
                field.Images.Add(new FieldImage
                {
                    Image = primaryImageUrl,
                    IsPrimary = true
                });
            }

            if (fieldDto.AdditionalPhotos is not null && fieldDto.AdditionalPhotos.Any())
            {
                foreach (var photo in fieldDto.AdditionalPhotos)
                {
                    string imageUrl = await _fileService.FileCreateAsync(photo);
                    field.Images.Add(new FieldImage
                    {
                        Image = imageUrl,
                        IsPrimary = false
                    });
                }
            }

            _repository.Add(field);
            await _repository.SaveChangesAsync();
        }

        private List<TimeOnly> GenerateEmptyTimeSlots(TimeOnly startTime, TimeOnly endTime)
        {
            List<TimeOnly> emptySlots = new List<TimeOnly>();

            TimeOnly currentTime = startTime;

            while (currentTime < endTime)
            {
                emptySlots.Add(currentTime);
                currentTime = currentTime.AddHours(1);
            }

            return emptySlots;
        }

        public async Task UpdateFieldAsync(long id, PutFieldDto fieldDto)
        {

            Field field = await _repository.GetByIdAsync(id, "Images");
            if (field is null)
                throw new Exception("Field not found");

            string oldPrimaryImage = field.Image;

            _mapper.Map(fieldDto, field);

            if (fieldDto.StartTime != field.StartTime || fieldDto.EndTime != field.EndTime)
            {
                field.EmptySpace = GenerateEmptyTimeSlots(fieldDto.StartTime, fieldDto.EndTime);
            }

            if (fieldDto.PrimaryPhoto is not null)
            {
                string newPrimaryImage = await _fileService.FileCreateAsync(fieldDto.PrimaryPhoto);

                if (!string.IsNullOrEmpty(oldPrimaryImage))
                {
                    await _fileService.FileDeleteAsync(oldPrimaryImage);

                    var oldPrimaryInCollection = field.Images
                        .FirstOrDefault(img => img.Image == oldPrimaryImage && img.IsPrimary);
                    if (oldPrimaryInCollection is not null)
                    {
                        field.Images.Remove(oldPrimaryInCollection);
                    }
                }

                field.Image = newPrimaryImage;

                field.Images.Add(new FieldImage
                {
                    Image = newPrimaryImage,
                    IsPrimary = true
                });
            }

            if (fieldDto.AdditionalPhotos is not null && fieldDto.AdditionalPhotos.Any())
            {
                var oldAdditionalImages = field.Images
                    .Where(img => !img.IsPrimary)
                    .ToList();

                foreach (var oldImg in oldAdditionalImages)
                {
                    await _fileService.FileDeleteAsync(oldImg.Image);
                    field.Images.Remove(oldImg);
                }

                foreach (var photo in fieldDto.AdditionalPhotos)
                {
                    string imageUrl = await _fileService.FileCreateAsync(photo);
                    field.Images.Add(new FieldImage
                    {
                        Image = imageUrl,
                        IsPrimary = false
                    });
                }
            }

            _repository.Update(field);
            await _repository.SaveChangesAsync();
        }

        public async Task Remove(long id)
        {
            Field field = await _repository.GetByIdAsync(id, "Images");
            if (field is null)
                throw new Exception("Field not found");

            if (field.Images is not null && field.Images.Any())
            {
                foreach (var image in field.Images)
                {
                    if (!string.IsNullOrEmpty(image.Image))
                    {
                        await _fileService.FileDeleteAsync(image.Image);
                    }
                }
            }

            if (!string.IsNullOrEmpty(field.Image))
            {
                await _fileService.FileDeleteAsync(field.Image);
            }

            _repository.Remove(field);
            await _repository.SaveChangesAsync();
        }
    }
}

﻿using DataAccessLayer.Entities;
using DTOs.CategoryDtos;
using DTOs.ColorDtos;
using DTOs.ImageDtos;

namespace DTOs.FurnitureDtos;
public class FurnitureDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public CategoryDto Category { get; set; } = new();
    public List<ImageDto> Images { get; set; } = [];
    public List<int> ColorIds { get; set; } = [];
    public List<ColorDto> Colors { get; set; } = [];

    public static implicit operator FurnitureDto(Furniture furniture)
        => new()
        {
            Id = furniture.Id,
            Name = furniture.Name,
            Description = furniture.Description,
            Price = furniture.Price,
            CategoryId = furniture.CategoryId,
            Category = (CategoryDto)furniture.Category,
            Images = furniture.Images
                              .Select(i => (ImageDto)i)
                              .ToList(),
            Colors = furniture.Colors
                              .Select(c => (ColorDto)c)
                              .ToList()
        };

    public static implicit operator Furniture(FurnitureDto furnitureDto)
        => new()
        {
            Id = furnitureDto.Id,
            Name = furnitureDto.Name,
            Description = furnitureDto.Description,
            Price = furnitureDto.Price,
            CategoryId = furnitureDto.CategoryId,
            Category = furnitureDto.Category,
            Images = furnitureDto.Images
                              .Select(i => (Image)i)
                              .ToList(),
            Colors = furnitureDto.Colors.Select(c => (Color)c)
                                        .ToList()
        };
}
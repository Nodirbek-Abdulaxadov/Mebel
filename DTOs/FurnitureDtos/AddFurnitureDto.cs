using DataAccessLayer.Entities;
using DTOs.ColorDtos;
using DTOs.ImageDtos;

namespace DTOs.FurnitureDtos;
public class AddFurnitureDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } = new();
    public List<ImageDto> Images { get; set; } = [];
    public List<int> ColorIds { get; set; } = [];
    public List<ColorDto> Colors { get; set; } = [];

    public static implicit operator Furniture(AddFurnitureDto addFurnitureDto)
        => new()
        {
            Name = addFurnitureDto.Name,
            Description = addFurnitureDto.Description,
            Price = addFurnitureDto.Price,
            CategoryId = addFurnitureDto.CategoryId,
            Category = addFurnitureDto.Category,
            Images = addFurnitureDto.Images
                              .Select(i => (Image)i)
                              .ToList(),
            FurnitureColors = addFurnitureDto.ColorIds
                                            .Select(c => new FurnitureColor
                                            {
                                                ColorId = c
                                            })
                                            .ToList()
        };
}
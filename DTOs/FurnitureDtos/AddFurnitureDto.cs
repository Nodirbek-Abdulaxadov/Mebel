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
    public List<string> ImageUrls { get; set; } = [];
    public List<int> ColorIds { get; set; } = [];

    public static implicit operator Furniture(AddFurnitureDto addFurnitureDto)
        => new()
        {
            Name = addFurnitureDto.Name,
            Description = addFurnitureDto.Description,
            Price = addFurnitureDto.Price,
            CategoryId = addFurnitureDto.CategoryId,
            Category = null
        };
}
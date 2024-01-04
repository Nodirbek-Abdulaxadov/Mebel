using DTOs.CategoryDtos;
using DTOs.ColorDtos;

namespace DTOs.FurnitureDtos;
public class FurnitureDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int PreparationDays { get; set; }
    public int InQueue { get; set; }
    public decimal Price { get; set; }
    public CategoryDto? Category { get; set; } = new();
    public List<string>? Images { get; set; }
    public List<ColorDto>? Colors { get; set; }
    public int LikesCount { get; set; }
}
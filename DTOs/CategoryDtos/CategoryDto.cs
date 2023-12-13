using DataAccessLayer.Entities;

namespace DTOs.CategoryDtos;
public class CategoryDto : BaseDto
{
    public string Name { get; set; } = string.Empty;

    public static implicit operator CategoryDto(Category category)
        => new()
        {
            Id = category.Id,
            Name = category.Name
        };

    public static implicit operator Category(CategoryDto categoryDto)
        => new()
        {
            Id = categoryDto.Id,
            Name = categoryDto.Name
        };
}
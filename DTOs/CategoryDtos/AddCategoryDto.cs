using DataAccessLayer.Entities;

namespace DTOs.CategoryDtos;
public class AddCategoryDto
{
    public string Name { get; set; } = string.Empty;

    public static implicit operator Category(AddCategoryDto addCategoryDto)
        => new()
        {
            Name = addCategoryDto.Name,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
}
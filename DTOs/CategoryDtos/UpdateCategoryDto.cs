using DataAccessLayer.Entities;

namespace DTOs.CategoryDtos;
public class UpdateCategoryDto : AddCategoryDto
{
    public int Id { get; set; }

    public static implicit operator Category(UpdateCategoryDto categoryDto)
        => new()
        {
            Id = categoryDto.Id,
            Name = categoryDto.Name,
            UpdatedAt = DateTime.Now
        };

    public static explicit operator CategoryDto(UpdateCategoryDto categoryDto)
        => new()
        {
            Id = categoryDto.Id,
            Name = categoryDto.Name
        };
}
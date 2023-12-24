using DataAccessLayer.Entities;
using DTOs.Extended;

namespace DTOs.CategoryDtos;
public class UpdateCategoryDto : AddCategoryDto
{
    public int Id { get; set; }

    public static implicit operator Category(UpdateCategoryDto categoryDto)
        => new()
        {
            Id = categoryDto.Id,
            NameUz = categoryDto.NameUz,
            NameRu = categoryDto.NameRu,
            UpdatedAt = LocalTime.GetUtc5Time()
        };
}
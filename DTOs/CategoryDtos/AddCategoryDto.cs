using DataAccessLayer.Entities;
using DTOs.Extended;

namespace DTOs.CategoryDtos;
public class AddCategoryDto
{
    public string NameUz { get; set; } = string.Empty;
    public string NameRu { get; set; } = string.Empty;

    public static implicit operator Category(AddCategoryDto addCategoryDto)
        => new()
        {
            NameUz = addCategoryDto.NameUz,
            NameRu = addCategoryDto.NameRu,
            CreatedAt = LocalTime.GetUtc5Time(),
            UpdatedAt = LocalTime.GetUtc5Time()
        };
}
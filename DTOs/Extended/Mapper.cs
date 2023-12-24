using DataAccessLayer.Entities;
using DTOs.CategoryDtos;
using DTOs.ColorDtos;

namespace DTOs.Extended;
public static class Mapper
{
    public static CategoryDto ToDto(this Category category, Language language)
        => new()
        {
            Id = category.Id,
            Name = language switch
            {
                Language.Uz => category.NameUz,
                Language.Ru => category.NameRu,
                _ => category.NameUz
            }
        };

    public static ColorDto ToDto(this Color color, Language language)
        => new()
        {
            Id = color.Id,
            Name = language switch
            {
                Language.Uz => color.NameUz,
                Language.Ru => color.NameRu,
                _ => color.NameUz
            },
            HexCode = color.HexCode
        };
}
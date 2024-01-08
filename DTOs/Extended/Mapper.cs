using DataAccessLayer.Entities;
using DTOs.CategoryDtos;
using DTOs.ColorDtos;
using DTOs.FurnitureDtos;

namespace DTOs.Extended;
public static class Mapper
{
    public static CategoryDto ToDto(this Category category, Language language)
        => new()
        {
            Id = category.Id,
            ImageUrl = category.ImageUrl,
            Name = language switch
            {
                Language.Uz => category.NameUz,
                Language.Ru => category.NameRu,
                _ => category.NameUz
            },
            IsActive = category.IsActive,
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
            HexCode = color.HexCode,
            IsActive = color.IsActive,
        };

    public static Language ToLanguage(this string lang)
        => lang.ToLower() switch
        {
            "uz" => Language.Uz,
            "ru" => Language.Ru,
            _ => Language.Uz
        };

    public static FurnitureDto ToDto(this Furniture furniture, Language language)
        => new()
        {
            Id = furniture.Id,
            Name = language switch
            {
                Language.Uz => furniture.NameUz,
                Language.Ru => furniture.NameRu,
                _ => furniture.NameUz
            },
            Description = language switch
            {
                Language.Uz => furniture.DescriptionUz,
                Language.Ru => furniture.DescriptionRu,
                _ => furniture.DescriptionUz
            },
            Quantity = furniture.Quantity,
            PreparationDays = furniture.PreparationDays,
            InQueue = furniture.InQueue,
            Price = furniture.Price,
            Category = furniture.Category!.ToDto(language),
            Images = furniture.Images.Select(i => i.Url).ToList(),
            Colors = furniture.Colors?.Select(c => c.Color!.ToDto(language)).ToList(),
            LikesCount = furniture.LikedUsers.Count,
            IsActive = furniture.IsActive
        };
}
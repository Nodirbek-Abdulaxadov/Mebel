using DataAccessLayer.Entities;

namespace DTOs.ColorDtos;
public class UpdateColorDto : BaseDto
{
    public string NameUz { get; set; } = string.Empty;
    public string NameRu { get; set; } = string.Empty;
    public string HexCode { get; set; } = string.Empty;

    public static explicit operator Color(UpdateColorDto color)
        => new()
        {
            Id = color.Id,
            NameUz = color.NameUz,
            NameRu = color.NameRu,
            HexCode = color.HexCode
        };
}
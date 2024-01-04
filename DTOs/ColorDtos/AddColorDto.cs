using DataAccessLayer.Entities;

namespace DTOs.ColorDtos;
public class AddColorDto
{
    public string NameUz { get; set; } = string.Empty;
    public string NameRu { get; set; } = string.Empty;
    public string HexCode { get; set; } = string.Empty;

    public static implicit operator Color(AddColorDto addColorDto)
        => new()
        {
            NameUz = addColorDto.NameUz,
            NameRu = addColorDto.NameRu,
            HexCode = addColorDto.HexCode
        };
}
using DataAccessLayer.Entities;

namespace DTOs.ColorDtos;
public class AddColorDto
{
    public string Name { get; set; } = string.Empty;
    public string HexCode { get; set; } = string.Empty;

    public static implicit operator Color(AddColorDto addColorDto)
        => new()
        {
            NameUz = addColorDto.Name,
            HexCode = addColorDto.HexCode
        };
}
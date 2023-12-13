using DataAccessLayer.Entities;
namespace DTOs.ColorDtos;
public class ColorDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string HexCode { get; set; } = string.Empty;

    public static implicit operator ColorDto(Color color)
        => new()
        {
            Id = color.Id,
            Name = color.Name,
            HexCode = color.HexCode
        };

    public static implicit operator Color(ColorDto colorDto)
        => new()
        {
            Id = colorDto.Id,
            Name = colorDto.Name,
            HexCode = colorDto.HexCode
        };
}

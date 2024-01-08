using DataAccessLayer.Entities;
namespace DTOs.ColorDtos;
public class ColorDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string HexCode { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

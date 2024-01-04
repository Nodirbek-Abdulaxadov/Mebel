using DataAccessLayer.Entities;
using DTOs.Extended;

namespace DTOs.ImageDtos;
public class AddImageDto
{
    public string Url { get; set; } = string.Empty;
    public int FurnitureId { get; set; }

    public static implicit operator Image(AddImageDto addImageDto)
        => new()
        {
            Url = addImageDto.Url,
            FurnitureId = addImageDto.FurnitureId,
            CreatedAt = LocalTime.GetUtc5Time(),
            UpdatedAt = LocalTime.GetUtc5Time()
        };
}
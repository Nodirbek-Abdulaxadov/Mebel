using DataAccessLayer.Entities;

namespace DTOs.ImageDtos;
public class AddImageDto
{
    public string Url { get; set; } = string.Empty;
    public int FurnitureId { get; set; }

    public static implicit operator Image(AddImageDto addImageDto)
        => new()
        {
            Url = addImageDto.Url,
            FurnitureId = addImageDto.FurnitureId
        };
}
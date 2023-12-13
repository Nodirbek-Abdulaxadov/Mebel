using DataAccessLayer.Entities;

namespace DTOs.ImageDtos;
public class ImageDto : BaseDto
{
    public string Url { get; set; } = string.Empty;
    public int FurnitureId { get; set; }

    public static implicit operator ImageDto(Image image)
        => new()
        {
            Id = image.Id,
            Url = image.Url,
            FurnitureId = image.FurnitureId
        };

    public static implicit operator Image(ImageDto imageDto)
        => new()
        {
            Id = imageDto.Id,
            Url = imageDto.Url,
            FurnitureId = imageDto.FurnitureId
        };
}

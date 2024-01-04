using DataAccessLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace DTOs.FurnitureDtos;
public class AddFurnitureDto
{
    [Required, StringLength(100)]
    public string NameUz { get; set; } = string.Empty;
    [Required, StringLength(500)]
    public string NameRu { get; set; } = string.Empty;
    [StringLength(1000)]
    public string DescriptionUz { get; set; } = string.Empty;
    [StringLength(1000)]
    public string DescriptionRu { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int PreparationDays { get; set; }
    public int InQueue { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int CategoryId { get; set; }

    public List<string> ImageUrls { get; set; } = new();
    public List<int> ColorIds { get; set; } = new();

    public static explicit operator Furniture(AddFurnitureDto dto)
    {
        return new()
        {
            NameUz = dto.NameUz,
            NameRu = dto.NameRu,
            DescriptionUz = dto.DescriptionUz,
            DescriptionRu = dto.DescriptionRu,
            Quantity = dto.Quantity,
            PreparationDays = dto.PreparationDays,
            InQueue = dto.InQueue,
            Price = dto.Price,
            CategoryId = dto.CategoryId,
        };
    }
}
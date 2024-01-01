using DataAccessLayer.Entities;
using DTOs.Extended;

namespace DTOs.CategoryDtos;
public class CategoryDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}
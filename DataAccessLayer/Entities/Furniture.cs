using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;
public class Furniture : BaseEntity
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
    public Category? Category { get; set; } = new();
    public ICollection<Image> Images { get; set; } 
        = new List<Image>();
    public ICollection<Color>? Colors { get; set; }
        = new List<Color>();
    public ICollection<User> LikedUsers { get; set; } 
        = new List<User>();
}
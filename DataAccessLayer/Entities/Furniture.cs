using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;
public class Furniture : BaseEntity
{
    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int CategoryId { get; set; }
    public Category Category { get; set; } = new();
    public IEnumerable<Image> Images { get; set; } 
        = new List<Image>();
    public IEnumerable<FurnitureColor> FurnitureColors { get; set; } 
        = new List<FurnitureColor>();
}
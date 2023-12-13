using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;
public class FurnitureColor : BaseEntity 
{
    [Required]
    public int FurnitureId { get; set; }
    public Furniture Furniture { get; set; } = new();
    [Required]
    public int ColorId { get; set; }
    public Color Color { get; set; } = new();
}
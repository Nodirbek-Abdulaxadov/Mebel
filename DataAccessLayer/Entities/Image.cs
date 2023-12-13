using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;
public class Image : BaseEntity
{
    [Required, StringLength(500)]
    public string Url { get; set; } = string.Empty;
    [Required]
    public int FurnitureId { get; set; }
    public Furniture Furniture { get; set; } = new();
}
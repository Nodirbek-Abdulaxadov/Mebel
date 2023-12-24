using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;
public class Color : BaseEntity
{
    [Required, StringLength(100)]
    public string NameUz { get; set; } = string.Empty;
    [Required, StringLength(100)]
    public string NameRu { get; set; } = string.Empty;

    [Required, StringLength(7)]
    public string HexCode { get; set; } = string.Empty;

    public ICollection<Furniture>? Furnitures { get; set; }
}
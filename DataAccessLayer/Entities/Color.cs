using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;
public class Color : BaseEntity
{
    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required, StringLength(7)]
    public string HexCode { get; set; } = string.Empty;

    public IEnumerable<FurnitureColor> FurnitureColors { get; set; } 
        = new List<FurnitureColor>();
}
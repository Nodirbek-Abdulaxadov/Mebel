using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;
public class Category : BaseEntity
{
    [Required, StringLength(100)]
    public string NameUz { get; set; } =  string.Empty;

    [Required, StringLength(100)]
    public string NameRu { get; set; } =  string.Empty;

    [Required]
    public string ImageUrl { get; set; } = string.Empty;

    public ICollection<Furniture> Furnitures { get; set; } 
        = new List<Furniture>();
}
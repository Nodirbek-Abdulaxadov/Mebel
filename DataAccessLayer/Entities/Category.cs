using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;
public class Category : BaseEntity
{
    [Required, StringLength(100)]
    public string Name { get; set; } =  string.Empty;

    public IEnumerable<Furniture> Furnitures { get; set; } 
        = new List<Furniture>();
}
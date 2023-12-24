using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;
public class Feedback : BaseEntity
{
    [Required]
    public string UserId { get; set; } = string.Empty;
    public User User { get; set; } = new();
    [Required, StringLength(500)]
    public string Text { get; set; } = string.Empty;
    public int Rating { get; set; }
    public int FurnitureId { get; set; }
    public Furniture Furniture { get; set; } = new();
    public ICollection<Image> Images { get; set; }
        = new List<Image>();
}
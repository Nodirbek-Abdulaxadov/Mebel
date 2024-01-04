using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;
public class BaseEntity
{
    [Key, Required]
    public int Id { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    [Required]
    public DateTime UpdatedAt { get; set; }
    [Required]
    public bool IsActive { get; set; } = true;
}
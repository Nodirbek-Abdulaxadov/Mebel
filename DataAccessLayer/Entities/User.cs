using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;
public class User : IdentityUser
{
    [Required, StringLength(200)]
    public string Address { get; set; } = string.Empty;
}

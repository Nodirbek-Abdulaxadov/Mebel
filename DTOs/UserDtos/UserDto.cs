using DataAccessLayer.Entities;

namespace DTOs.UserDtos;
public class UserDto 
{
    public string Id { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string? ProfilePictureUrl { get; set; }
    public string Gender { get; set; } = null!;
    public string BirthDate { get; set; } = null!;
    public bool PhoneNumberConfirmed { get; set; }

    public static implicit operator UserDto(User user)
        => new()
        {
            Id = user.Id,
            FullName = user.FullName,
            PhoneNumber = user.PhoneNumber!,
            Address = user.Address,
            ProfilePictureUrl = user.AvatarUrl,
            Gender = nameof(user.Gender),
            BirthDate = user.BirthDate.ToString("dd/MM/yyyy"),
            PhoneNumberConfirmed = user.PhoneNumberConfirmed
        };
}
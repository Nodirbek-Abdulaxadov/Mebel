using DataAccessLayer.Entities;
using DataAccessLayer.Enums;

namespace DTOs.UserDtos;
public class RegisterUserDto : LoginUserDto
{
    public string FullName { get; set; } = string.Empty;
    public Gender Gender { get; set;}
    public string BirthDate { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public static explicit operator User(RegisterUserDto v)
        => new()
        {
            FullName = v.FullName,
            Gender = v.Gender,
            BirthDate = v.BirthDate.ToDateTime(),
            Address = v.Address,
            PhoneNumber = v.PhoneNumber
        };
}
using DataAccessLayer.Enums;

namespace DTOs.UserDtos;
public class RegisterUserDto : LoginUserDto
{
    public string FullName { get; set; } = string.Empty;
    public Gender Gender { get; set;}
    public DateTime BirtDate { get; set; }
    public string Address { get; set; } = string.Empty;
}
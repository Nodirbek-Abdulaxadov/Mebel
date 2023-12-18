namespace DTOs.UserDtos;
public class RegisterUserDto : LoginUserDto
{
    public string FullName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}
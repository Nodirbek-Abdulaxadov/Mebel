namespace DTOs.UserDtos;
public class LoginResult
{
    public string FullName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
    public string? Token { get; set; }
}
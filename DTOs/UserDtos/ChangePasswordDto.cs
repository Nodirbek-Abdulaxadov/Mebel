namespace DTOs.UserDtos;
public class ChangePasswordDto
{
    public string PhoneNumber { get; set; } = string.Empty;
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}
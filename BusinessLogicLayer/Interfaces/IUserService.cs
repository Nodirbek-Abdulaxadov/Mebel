using DTOs.UserDtos;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.Interfaces;
public interface IUserService
{
    Task CreateAsync(RegisterUserDto dto);
    Task<LoginResult> LoginAsync(LoginUserDto dto);
    Task DeleteAccountAsync(LoginUserDto dto);
    Task LogoutAsync(LoginUserDto dto);
    Task ChangePasswordAsync(ChangePasswordDto dto);
    Task ConfirmPhoneNumberAsync(ConfirmPhoneNumberDto dto);
    Task SendOtpAsync(SendOtpDto dto);
}
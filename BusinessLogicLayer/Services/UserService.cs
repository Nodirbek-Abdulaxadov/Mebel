#region Usings
using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DTOs.UserDtos;
using Messager.EskizUz;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
#endregion

namespace BusinessLogicLayer.Services;
#region Class Definition and Dependencies
public class UserService(UserManager<User> userManager,
                          SignInManager<User> signInManager,
                          IConfiguration configuration,
                          IUnitOfWork unitOfWork)
    : IUserService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly IConfiguration _configuration = configuration;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    #endregion

    /// <summary>
    /// Change user password using user manager
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="MarketException"></exception>
    public async Task ChangePasswordAsync(ChangePasswordDto dto)
    {
        if (dto is null)
        {
            throw new ArgumentNullException(nameof(dto));
        }

        var user = await _userManager.FindByNameAsync(dto.PhoneNumber.Replace("+", ""));
        if (user is null)
        {
            throw new ArgumentNullException("User not found");
        }

        var resul = await _userManager.ChangePasswordAsync(user, 
                                                      dto.OldPassword, 
                                                      dto.NewPassword);
        if (!resul.Succeeded)
        {
            throw new MarketException("Failed to change password");
        }
    }

    /// <summary>
    /// Confirm user phone number using user manager
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="MarketException"></exception>
    public async Task ConfirmPhoneNumberAsync(ConfirmPhoneNumberDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.PhoneNumber.Replace("+", ""));
        if (user is null)
        {
            throw new ArgumentNullException("User not found");
        }

        var otp = (await _unitOfWork.OtpModels
                                    .GetAllAsync())
                                    .FirstOrDefault(x => x.PhoneNumber == dto.PhoneNumber);
        if (otp is null)
        {
            throw new ArgumentNullException("OTP not found");
        }

        var date = DateTime.UtcNow;
        if (date > otp.ExpirationDate)
        {
            _unitOfWork.OtpModels.Delete(otp.Id);
            await _unitOfWork.SaveAsync();
            throw new MarketException("OTP expired");
        }

        if (otp.Code != dto.Code)
        {
            throw new MarketException("Invalid OTP");
        }

        user.PhoneNumberConfirmed = true;
        await _userManager.UpdateAsync(user);

        _unitOfWork.OtpModels.Delete(otp.Id);
        await _unitOfWork.SaveAsync();
    }

    /// <summary>
    /// Creates a new user account using user manager and adds the user to "User" role
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="MarketException"></exception>
    public async Task CreateAsync(RegisterUserDto dto)
    {
        if (dto is null)
        {
            throw new ArgumentNullException(nameof(dto));
        }

        if (!dto.IsValid())
        {
            throw new MarketException("Invalid data");
        }

        var user = new User()
        {
            FullName = dto.FullName,
            PhoneNumber = dto.PhoneNumber
        };

        await _userManager.SetUserNameAsync(user, dto.PhoneNumber.Replace("+", ""));
        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            throw new MarketException("Failed to create user");
        }

        result = await _userManager.AddToRoleAsync(user, "User");
        if (!result.Succeeded)
        {
            throw new MarketException("Failed to add user to role");
        }
    }

    /// <summary>
    /// Deletes a user account using user manager
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task DeleteAccountAsync(LoginUserDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.PhoneNumber.Replace("+", ""));
        if (user is null)
        {
            throw new ArgumentException("User not found");
        }

        await _userManager.RemoveAuthenticationTokenAsync(user, _configuration["Jwt:Issuer"]??"", "Token");

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            throw new MarketException("Failed to delete user");
        }
    }

    /// <summary>
    /// Login a user using user manager and generate a JWT token
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="MarketException"></exception>
    public async Task<LoginResult> LoginAsync(LoginUserDto dto)
    {
        if (dto is null)
        {
            throw new ArgumentNullException(nameof(dto));
        }

        if (!dto.IsValid())
        {
            throw new MarketException("Invalid data");
        }

        var user = await _userManager.FindByNameAsync(dto.PhoneNumber.Replace("+", ""));
        if (user is null)
        {
            throw new ArgumentNullException("User not found");
        }

        var phoneNumberConfirmed = await _userManager.IsPhoneNumberConfirmedAsync(user);
        if (!phoneNumberConfirmed)
        {
            throw new MarketException("Phone number not confirmed");
        }

        var result = await _signInManager.PasswordSignInAsync(user, dto.Password, false, false);
        if (!result.Succeeded)
        {
            throw new MarketException("Invalid password");
        }

        var roles = await _userManager.GetRolesAsync(user);

        var token = GenerateJwtToken(user.FullName, user.UserName, roles.ToList());
        var provider = _configuration["Jwt:Issuer"]??"";
        await _userManager.RemoveAuthenticationTokenAsync(user, provider, "Token");
        await _userManager.SetAuthenticationTokenAsync(user, provider, "Token", token);

        return new LoginResult()
        {
            FullName = user.FullName,
            PhoneNumber = user.PhoneNumber,
            Token = token
        };
    }

    /// <summary>
    /// Logout a user by removing the JWT token from user manager
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task LogoutAsync(LoginUserDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.PhoneNumber.Replace("+", ""));
        if (user is null)
        {
            throw new ArgumentNullException("User not found");
        }

        await _userManager.RemoveAuthenticationTokenAsync(user, 
                                                          _configuration["Jwt:Issuer"]??"", 
                                                          "Token");
    }

    /// <summary>
    /// Generates a JWT token using JWT security token handler
    /// </summary>
    /// <param name="fullName"></param>
    /// <param name="username"></param>
    /// <param name="roles"></param>
    /// <returns></returns>
    public string GenerateJwtToken(string fullName, string? username, List<string> roles)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]??"key"); // Same key as used in authentication configuration

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, username??""),
                new Claim(ClaimTypes.GivenName, fullName),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddMonths(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        foreach (var role in roles)
        {
            tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role));
        }

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    /// <summary>
    /// Sends an OTP code to the user using Messager
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    /// <exception cref="MarketException"></exception>
    public async Task SendOtpAsync(SendOtpDto dto)
    {
        var email = _configuration["EskizUz:Email"]??"";
        var key = _configuration["EskizUz:Key"]??"";
        using var messager = new MessagerAgent(email, key);
        var result = await messager.SendOtpAsync(dto.PhoneNumber);

        if (!result.Success)
        {
            throw new MarketException("Failed to send OTP");
        }

        var otpModel = new OtpModel()
        {
            Code = result.Code,
            PhoneNumber = dto.PhoneNumber
        };

        _unitOfWork.OtpModels.Add(otpModel);
        await _unitOfWork.SaveAsync();
    }
}
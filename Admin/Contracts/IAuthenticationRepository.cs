using Admin.Models;
using System.Threading.Tasks;

namespace Admin.Contracts;

public interface IAuthenticationRepository
{
    public Task<bool> Register(RegistrationModel user);
    public Task<bool> Login(LoginModel user);
    public Task Logout();
}
using SmartShop.Models;
using SmartShop.Models.Request;
using System.Threading.Tasks;

namespace SmartShop.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> Register(string name, string email, string password, string confirmedPassword);
        Task<AuthResponse> LogIn(LoginRequest request);
        Task<bool> LogOut();
    }
}

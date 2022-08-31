using Newtonsoft.Json;
using SmartShop.Models;
using SmartShop.Models.Request;
using SmartShop.Services;
using SmartShop.Services.RequestProvider;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(AuthService))]
namespace SmartShop.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRequestProvider _requestProvider;
        public AuthService()
        {
            _requestProvider = DependencyService.Get<IRequestProvider>();
        }
        public async Task<AuthResponse> LogIn(LoginRequest request)
        {
            var token = await _requestProvider.PostAsync<LoginRequest, AuthResponse>($"{Config.APIUrl}login", request).ConfigureAwait(false);

            return token;
        }

        public Task<bool> LogOut()
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponse> Register(string name, string email, string password, string confirmedPassword)
        {
            throw new NotImplementedException();
        }
    }
}

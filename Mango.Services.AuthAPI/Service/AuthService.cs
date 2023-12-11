using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        public readonly AppDBContext _db;
        public readonly UserManager<ApplicationUser> _userManger;
        public readonly RoleManager<IdentityRole> _roleManger;

        public AuthService(AppDBContext appDBContext, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _db = appDBContext;
            _userManger = userManager;
            _roleManger = roleManager;
        }

        public Task<LoginRequestDto> Login(LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> Register(RegistrationRequestDto registrationRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
